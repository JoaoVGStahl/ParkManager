using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.IO;

namespace Teste
{
    public partial class FrmTelaOperacao : Form
    {
        public DirectX.Capture.Filter Camera;
        public DirectX.Capture.Capture CaptureInfo;
        public DirectX.Capture.Filters CamContainer;
        public int Inicializacao;
        Image capturaImagem;

        Banco banco = new Banco();
        public FrmTelaOperacao()
        {
            InitializeComponent();
            CarregarCores();
        }
        private void AbrirForm(int nivel, Form F)
        {
            //Função Genérica para abrir formulários

            //Verifica se o usuário logado tem nivel suficiente para acessar o form
            if (Globais.Nivel >= nivel)
            {
                //Abre o form
                F.ShowDialog();
            }
            else
            {
                MessageBox.Show("Acesso não permitido! contate o administrador do sistema para mais informações!", "Acesso negado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmTelaOperacao_Load(object sender, EventArgs e)
        {
            Globais.CaminhoFoto = @"c:\ParkManager\fotos";
            if (!(Globais.Login == Properties.Settings.Default.UserRoot) || (Properties.Settings.Default["StringBanco"].ToString() == ""))
            {
                txtPlaca.Select();
                CarregarBarraStatus();
                PopularComboTipo();
                ContadorTicket();
                CarregarParametros();
                if(!IniciaCamera())
                {
                    picCam.Visible = false;
                    picImagem.Image = picImagem.InitialImage;
                    picImagem.Visible = true;
                    MessageBox.Show("Câmera não encontrada!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }else
                {
                    picImagem.Visible = false;
                    picCam.Visible = true;
                } 
            }
        }
        private void CarregarCores()
        {

        }
        private void CarregarBarraStatus()
        {
            string DataCompleta = DateTime.Now.ToLongDateString();
            //Seleciona a primeira letra da data completa e adiciona um UpperCase e obtem o restante da string
            string Semana = DataCompleta.Substring(0, 1).ToUpper() + DataCompleta.Substring(1, DataCompleta.Length - 1);
            //Inserir na Barra de status o dia, mes e ano de hoje.
            lblData.Text = Semana;
            //Ativa um timer para mostra o horário em tempo real ao usuario.
            HoraData.Enabled = true;
        }
        private void PopularComboTipo()
        {
            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ProcedureSemParametros(0);
                cmbTipo.DataSource = null;
                cmbTipo.DataSource = dt;
                cmbTipo.ValueMember = "id_automovel";
                cmbTipo.DisplayMember = "automovel";
                cmbTipo.SelectedItem = null;
                cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao carregar Tipo de veiculos!");
            }

        }
        public void ContadorTicket()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ProcedureSemParametros(2);
                lblQtdTicket.Text = Convert.ToString(dt.Rows[0].ItemArray[0]);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao Carregar Contador de Tickets!");
            }
            finally
            {
                dt.Dispose();
            }

        }
        private void CarregarParametros()
        {
            try
            {
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 14}
                };
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                if (dt.Rows.Count > 0)
                {
                    DateTime aux = Convert.ToDateTime("00:00:00");
                    Globais.ValorHora = Convert.ToDecimal(dt.Rows[0].ItemArray[1]);
                    Globais.ValorMinimo = Convert.ToDecimal(dt.Rows[0].ItemArray[2]);
                    Globais.ValorUnico = Convert.ToDecimal(dt.Rows[0].ItemArray[3]);
                    DateTime tempo = Convert.ToDateTime(dt.Rows[0].ItemArray[4].ToString());
                    TimeSpan ts = tempo - aux;
                    Globais.Tolerencia = ts;
                    Properties.Settings.Default["ArquivoAuditoria"] = dt.Rows[0].ItemArray[5].ToString();
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Parametros não Encontrados!", "Falha ao Carregar Parametros!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao carregar as informações!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Novo
        private bool IniciaCamera()
        {
            bool Cam = false;
            try
            {
                CamContainer = new DirectX.Capture.Filters();
                int quantCam = CamContainer.VideoInputDevices.Count;
                if (quantCam > 0)
                {
                    for ( int i = 0; i < quantCam; i++)
                    {

                        // obtém o dispositivo de entrada do vídeo
                        Camera = CamContainer.VideoInputDevices[i];
                        
                        // inicializa a Captura usando o dispositivo
                        CaptureInfo = new DirectX.Capture.Capture(Camera, null)
                        {
                            // Define a janela de visualização do vídeo
                            PreviewWindow = this.picCam
                        };
                        // Capturando o tratamento de evento
                        if (CaptureInfo != null)
                        {
                            CaptureInfo.CaptureFrame();
                            Inicializacao = 1;
                            CaptureInfo.FrameCaptureComplete += AtualizaImagem;

                            // Captura o frame do dispositivo
                            Cam = true;

                        }
                        
                        // Se o dispositivo foi encontrado e inicializado então sai sem checar o resto
                        break;
                    }

                }

            }
            catch (Exception)
            {
                Cam = false;
            }
            return Cam;
        }

        //Novo
        public void AtualizaImagem(PictureBox frame)
        {
            try
            {
                capturaImagem = frame.Image;
                this.picImagem.Image = capturaImagem;
                SalvarImagem();                            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }

        //Novo
        private void CapturarFoto(string placa)
        {
            try
            {
                CaptureInfo.CaptureFrame();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }

        //Novo
        private void SalvarImagem()
        {
            if(Inicializacao == 0)
            {
                string caminhoImagemSalva = @"c:\ParkManager\fotos\";
                caminhoImagemSalva += "veiculo_" + txtPlaca.Text + DateTime.Now.ToShortDateString().Replace("/", "_") + DateTime.Now.ToLongTimeString().Replace(":", "_") + ".jpg";
                Globais.CaminhoFoto = caminhoImagemSalva;
                try
                {
                    if (picImagem.Image != null)
                    {
                        picImagem.Image.Save(Globais.CaminhoFoto, ImageFormat.Jpeg);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro " + ex.Message);
                }
                finally
                {
                    Inicializacao = 1;
                }
            }         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FecharForm();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            FrmTelaPesquisaTicket Frm = new FrmTelaPesquisaTicket(this);
            AbrirForm(0, Frm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmTelaEncerrarTicket Frm = new FrmTelaEncerrarTicket(this);
            AbrirForm(0, Frm);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool caixa;
            //Caso for digitado 7 caracteres na caixa de texto da placa, Ativa algumas funções

            if (txtPlaca.TextLength == 7)
            {
                txtNome.Enabled = true;
                mskTelefone.Enabled = true;
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName= "@Flag", SqlDbType = SqlDbType.Int, Value = 13 },
                    new SqlParameter(){ParameterName= "@Placa", SqlDbType = SqlDbType.VarChar, Value = txtPlaca.Text}
                };
                try
                {
                    DataTable dt = new DataTable();
                    dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                    if (dt.Rows.Count > 0)
                    {
                        cmbTipo.Text = dt.Rows[0].ItemArray[1].ToString();
                        cmbMarca.Text = dt.Rows[0].ItemArray[2].ToString();
                        cmbTipo.Enabled = false;
                        cmbMarca.Enabled = false;
                        btnPesquisaTicket.Enabled = true;
                        btnIniciar.Enabled = true;
                    }
                    else
                    {
                        caixa = true;
                        AtivarFuncoes(caixa);
                    }
                    dt.Dispose();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Falha ao Consultar a placa do veiculo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                cmbTipo.SelectedIndex = -1;
                cmbTipo.Enabled = false;
                cmbMarca.SelectedIndex = -1;
                cmbMarca.Enabled = false;
                txtNome.Enabled = false;
                mskTelefone.Enabled = false;

            }

        }
        private void AtivarFuncoes(bool caixa)
        {
            if (caixa)
            {
                cmbTipo.Enabled = true;
                txtNome.Enabled = true;
                mskTelefone.Enabled = true;
                btnPesquisaTicket.Enabled = true;
                btnIniciar.Enabled = true;
            }
            else
            {
                cmbTipo.Enabled = false;
                txtNome.Enabled = false;
                mskTelefone.Enabled = false;
                btnPesquisaTicket.Enabled = false;
                btnIniciar.Enabled = false;
            }
        }


        private void HoraData_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VerificarCaixas();
        }
        private void VerificarCaixas()
        {
            string placa = txtPlaca.Text;
            string nome = txtNome.Text, telefone = mskTelefone.Text;
            //Verifica se as caixas possuem texto
            if (placa != "" && cmbTipo.SelectedIndex >= 0 && cmbMarca.SelectedIndex >= 0)
            {
                //Regex para validar a placa do veiculo
                if (Regex.IsMatch(placa, "^[A-Z]{3}[0-9]{1}[A-Z0-9]{1}[0-9]{2}"))
                {
                    if (nome == "" && telefone == "(  )     -")
                    {
                        nome = "Convidado";
                        telefone = "(00)00000-0000";
                        VerificarTicket(placa, nome, telefone);
                    }
                    else
                    {
                        if (nome == "")
                        {
                            MessageBox.Show("O Nome neste caso, precisa estar preenchido!", "Falha ao iniciar Ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtNome.Focus();
                            return;
                        }
                        else
                        {
                            //Regex para validar Telefone.
                            if (Regex.IsMatch(nome, @"^[A-Za-záàâãéèêíïóôõöúçÁÀÂÃÉÈÍÏÓÔÕÖÚÇ ]+$"))
                            {
                                if (Regex.IsMatch(telefone, "^[(]{1}[11-99]{2}[)]{1}[0|9]{1}[0-9]{4}-[0-9]{4}"))
                                {
                                    VerificarTicket(placa, nome, telefone);
                                }
                                else
                                {
                                    MessageBox.Show("O Telefone inválido!", "Falha ao iniciar Ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    mskTelefone.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Nome inválido!", "Falha ao iniciar Ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtNome.Focus();
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Placa Inválida!", "Falha ao iniciar Ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPlaca.Focus();
                }
            }
            else
            {
                MessageBox.Show("Há campos que precisam ser preenchidos!", "Ticket não iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void VerificarTicket(string placa, string nome, string telefone)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ProcedurePesquisaTicketVeiculo(7, placa);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Já existe um ticket em andamento para este veiculo!", "Ticket não iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimparCaixas();

                }
                else
                {
                    //Novo
                    if (Camera != null && CamContainer.VideoInputDevices.Count > 0)
                    {
                        Inicializacao = 0;
                        CapturarFoto(txtPlaca.Text);
                    }
                    MessageBox.Show("Ok");
                    Globais.CaminhoFoto = @"c:\ParkManager\fotos";
                    //InserirTicket(placa, nome, telefone);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao iniciar ticket!");
            }

        }
        private void InserirTicket(string placa, string nome, string telefone)
        {
            string marca = cmbMarca.Text, tipo = cmbTipo.Text;
            try
            {
                int idTicket;
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@idUsuario", SqlDbType = SqlDbType.Int, Value = Globais.IdUsuario},
                    new SqlParameter(){ParameterName = "@Nome_Cliente", SqlDbType = SqlDbType.NVarChar, Value = nome },
                    new SqlParameter(){ParameterName = "@Telefone", SqlDbType = SqlDbType.NVarChar, Value = telefone },
                    new SqlParameter(){ParameterName = "@Placa",SqlDbType = SqlDbType.NVarChar, Value = placa },
                    new SqlParameter(){ParameterName = "@Marca", SqlDbType = SqlDbType.NVarChar, Value = marca },
                    new SqlParameter(){ParameterName = "@Tipo", SqlDbType = SqlDbType.NVarChar, Value = tipo },
                    new SqlParameter(){ParameterName = "@Hr_Entrada", SqlDbType = SqlDbType.Time, Value = DateTime.Now.ToLongTimeString() },
                    new SqlParameter(){ParameterName = "@Data_Entrada", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.ToShortDateString() },
                    new SqlParameter() { ParameterName = "@Caminho_Foto", SqlDbType = SqlDbType.NVarChar, Value = Globais.CaminhoFoto }

                };
               
                dt = banco.InsertData("dbo.InsertTicket", sp);
                //Verifica se houve algum retorno da procedure
                if (dt.Rows.Count > 0)
                {
                    idTicket = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                    if (idTicket > 0)
                    {
                        
                        ContadorTicket();
                        LimparCaixas();
                        Globais.RegistrarLog(Globais.Login + " Inicou o Ticket #" + idTicket);
                        dt.Dispose();
                        sp.Clear();
                        MessageBox.Show("Ticket Iniciado com sucesso! \n #Ticket:" + idTicket, "Ticket Iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        MessageBox.Show("Falha ao iniciar Ticket!", "Ticket não iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Falha ao iniciar Ticket!", "Ticket não iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao iniciar ticket!");
            }
            finally
            {
                Globais.CaminhoFoto = @"c:\ParkManager\fotos";
            }
        }
        private void LimparCaixas()
        {
            txtPlaca.Clear();
            txtNome.Clear();
            txtNome.Enabled = false;
            mskTelefone.Clear();
            mskTelefone.Enabled = false;
            cmbMarca.SelectedIndex = -1;
            cmbMarca.Enabled = false;
            cmbTipo.SelectedIndex = -1;
            cmbTipo.Enabled = false;
            btnIniciar.Enabled = false;
            btnEncerrar.Enabled = false;
            btnPesquisaTicket.Enabled = false;
        }
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopularComboMarca();
        }
        private void PopularComboMarca()
        {
            string tipo = cmbTipo.Text;
            //Verifica se tem algum Tipo que foi carregando do banco.
            if (cmbTipo.Items.Count > 0)
            {
                cmbMarca.Enabled = true;
                DataTable dt = new DataTable();
                //Limpa o DataTable
                dt.Clear();
                //Chama a função que executa a query no banco de dados
                try
                {
                    dt = banco.ProcedureMarca(1, tipo, "");
                    //Limpar o DataSource do combo
                    cmbMarca.DataSource = null;
                    //Seleciona o DataTable como o DataSoucer do combo
                    cmbMarca.DataSource = dt;
                    //Preenche o ComboBox com o DataTable
                    cmbMarca.ValueMember = "Tipo";
                    cmbMarca.DisplayMember = "Marca";
                    //Desmacar qualquer seleção pré-selecionada
                    cmbMarca.SelectedItem = null;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Falha ao carregar Marcas!");
                }
            }

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FrmTelaConfig Frm = new FrmTelaConfig();
            AbrirForm(2, Frm);
        }
        private void PreencherLabels(DataTable dt)
        {
            string CaminhoFoto = dt.Rows[0].ItemArray[8].ToString();

            //Preenchendo as labels com as informações do banco
            Globais.IdTicket = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
            lblTipo.Text = Convert.ToString(dt.Rows[0].ItemArray[1]); //Tipo
            lblMarca.Text = Convert.ToString(dt.Rows[0].ItemArray[2]);//Marca
            lblPlaca.Text = Convert.ToString(dt.Rows[0].ItemArray[3]);//Placa
            txtNomeP.Text = Convert.ToString(dt.Rows[0].ItemArray[4]);//Nome
            txtTelefoneP.Text = Convert.ToString(dt.Rows[0].ItemArray[5]);// Telefone
            lblHrEntrada.Text = Convert.ToString(dt.Rows[0].ItemArray[6]) + " " + Convert.ToString(dt.Rows[0].ItemArray[7]);// Hora + Data
            picCam.Visible = false;
            picImagem.Visible = true;
            if (CaminhoFoto != @"c:\ParkManager\fotos" && File.Exists(CaminhoFoto))
            {
                picImagem.Image = Image.FromFile(CaminhoFoto);
            }
            else
            {
                picImagem.Image = picImagem.InitialImage;
            }
            //Novo
            //Adicionar na Procedure (String da foto) 
            //picImagem.Image = Image.FromFile(Convert.ToString(dt.Rows[0].ItemArray[]));
        }
        private void AlinharLabels()
        {
            lblTipo.Location = new Point(-1, -1);
            lblHoraEntradaVisual.Visible = true;
            btnLimpaP.Enabled = true;
        }

        private void btnLimpaP_Click(object sender, EventArgs e)
        {
            LimparLabels();
        }
        private void LimparLabels()
        {
            lblTipo.Text = "Tipo";
            lblMarca.Text = "Marca";
            lblPlaca.Text = "PLACA";
            lblHrEntrada.Text = "Horário da entrada";
            lblHoraEntradaVisual.Visible = false;
            txtNomeP.Text = "Nome";
            txtTelefoneP.Text = "Telefone";
            btnEncerrar.Enabled = false;
            picCam.Visible = true;
            picImagem.Visible = false;
        }

        private void FrmTelaOperacao_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globais.RegistrarLog(Globais.Login + " Efetuou logout.");
            this.Dispose();
            Application.Exit();
        }
        private void FecharForm()
        {
            string mensagem = "Tem certeza que deseja sair?";
            string titulo = "Efetuar Logout?";
            bool escolha = (MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
            if (escolha)
            {
                //Destroi o Formulario principal e abre o formulario de login
                FrmTelaLogin Frm = new FrmTelaLogin();
                this.Dispose();
                Frm.ShowDialog();
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Caracteres permitidos
            string caracterespermitidos = "ABCDEFGHIJKLMNOPQRTUVWXYZ0123456789";
            //Apenas Letras E BackSpace nos 3 primeiros digitos
            if (txtPlaca.TextLength < 3)
            {
                if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            //Apenas Números E BackSpace no 4º Digito
            if (txtPlaca.TextLength == 3)
            {
                if (!char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            // Apenas letras de A-Z e 0-9 e BackSpace no 5º Digito
            if (txtPlaca.TextLength == 4)
            {
                if (!(caracterespermitidos.Contains(e.KeyChar.ToString().ToUpper())) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            //Apenas Números e BackSpace nos 6º e 7º Digitos
            if (txtPlaca.TextLength > 4)
            {
                if (!char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Apenas letras, espaço e BackSpace
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        private void cmbMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Apenas letras, espaço e BackSpace
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        private void btnCadastros_Click(object sender, EventArgs e)
        {
            FrmTelaCadastros Frm = new FrmTelaCadastros();
            AbrirForm(2, Frm);
        }

        private void btnPesquisaTicket_Click_2(object sender, EventArgs e)
        {
            PesquisaTicket(txtPlaca.Text);
        }
        public void PesquisaTicket(string placa)
        {
            DataTable dt = new DataTable();
            if (placa != "")
            {

                try
                {
                    dt = banco.ProcedurePesquisaTicketVeiculo(7, placa);
                    //Verifica se houve algum retorno no DataTable
                    if (dt.Rows.Count > 0)
                    {
                        PreencherLabels(dt);
                        AlinharLabels();
                        LimparCaixas();
                        btnEncerrar.Enabled = true;
                        btnIniciar.Enabled = false;

                    }
                    else
                    {
                        MessageBox.Show("Nenhum ticket em aberto encontrado para este veiculo!", "Ticket não existe!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Falha ao relaizar pesquisa!");
                }
                finally
                {
                    dt.Dispose();
                }

            }
            else
            {
                MessageBox.Show("Preencha o campo 'Placa'!", "Ticket não existe!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblQtdTicket_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //Novo
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                CaptureInfo.CaptureFrame();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }
    }
}


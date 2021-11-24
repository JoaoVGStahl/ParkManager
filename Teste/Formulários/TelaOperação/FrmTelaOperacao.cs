using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO.Ports;


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
        GeraPDF geradorPdf = new GeraPDF();

        public FrmTelaOperacao()
        {
            InitializeComponent();
        }
        public string NomeCli
        {
            set { txtNome.Text = value; }
        }
        public string TelCli
        {
            set { mskTelefone.Text = value; }
        }
        public string Placa
        {
            set { txtPlaca.Text = value; }
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

            if (!(Globais.Login == Properties.Settings.Default.UserRoot) || (Properties.Settings.Default["StringBanco"].ToString() == ""))
            {
                txtPlaca.Select();
                CarregarBarraStatus();
                PopularComboTipo();
                ContadorTicket();
                CarregarParametros();
                if (Properties.Settings.Default.Foto)
                {
                    if (!IniciaCamera())
                    {
                        picCam.Visible = false;
                        picImagem.Image = picImagem.InitialImage;
                        picImagem.Visible = true;
                        MessageBox.Show("Câmera não encontrada!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        picImagem.Visible = false;
                        picCam.Visible = true;
                    }
                }
                if (Properties.Settings.Default.Cancelas)
                {
                    try
                    {
                        Arduino.Port = serialPort1;
                        Arduino.PortaCom = Properties.Settings.Default["PortaArduino"].ToString();
                        Arduino.Inicializar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }

            }
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
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.ComboBox_Tipo");
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
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Tickets_Abertos");
                lblQtdTicket.Text = dt.Rows[0]["Ticket's Abertos"].ToString();
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
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Parametros_Sistema");
                if (dt.Rows.Count > 0)
                {
                    Estacionamento.valor_hr = Convert.ToDecimal(dt.Rows[0]["valor_hr"]);
                    Estacionamento.tolerancia = Convert.ToDateTime(dt.Rows[0]["Tolerancia"].ToString()) - Convert.ToDateTime("00:00:00");
                    Estacionamento.cnpj = dt.Rows[0]["cnpj"].ToString();
                    Estacionamento.razao_social = dt.Rows[0]["razao_social"].ToString();
                    Estacionamento.endereco = dt.Rows[0]["endereco"].ToString();
                    Estacionamento.bairro = dt.Rows[0]["bairro"].ToString();
                    Estacionamento.numero = Convert.ToInt32(dt.Rows[0]["numero"]);
                    Estacionamento.cidade = dt.Rows[0]["cidade"].ToString();
                    Estacionamento.estado = dt.Rows[0]["estado"].ToString();
                    Estacionamento.cep = dt.Rows[0]["cep"].ToString();
                    Estacionamento.telefone = dt.Rows[0]["telefone"].ToString();
                    Estacionamento.valor_min = Convert.ToDecimal(dt.Rows[0]["valor_min"]);
                    Estacionamento.valor_unico = Convert.ToDecimal(dt.Rows[0]["valor_unico"]);
                    Estacionamento.caminho_foto_padrao = dt.Rows[0]["caminho_foto_padrao"].ToString();
                    Globais.CaminhoFoto = Estacionamento.caminho_foto_padrao;
                    Properties.Settings.Default["ArquivoAuditoria"] = dt.Rows[0]["caminho_log"].ToString();
                    Properties.Settings.Default["PortaArduino"] = dt.Rows[0]["porta_arduino"].ToString();
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
                    for (int i = 0; i < quantCam; i++)
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
        private void AtualizaImagem(PictureBox frame)
        {         
            
                try
                {
                    capturaImagem = frame.Image;
                    this.picImagem.Image = capturaImagem;
                    SalvaArquivo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro " + ex.Message);
                }
        }

        //Novo
        private void CapturarFoto()
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
            if (Inicializacao == 0)
            {
                string caminhoImagemSalva = Estacionamento.caminho_foto_padrao;
                caminhoImagemSalva += @"\veiculo_" + txtPlaca.Text + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".jpg";
                Globais.CaminhoFoto = caminhoImagemSalva;
                CapturarFoto();
                
            }
        }
        private void SalvaArquivo()
        {
            if (Inicializacao == 0)
            {
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

            FrmTelaPesquisaTicket Frm = new FrmTelaPesquisaTicket();
            AbrirForm(0, Frm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmTelaEncerrarTicket Frm = new FrmTelaEncerrarTicket();
            AbrirForm(0, Frm);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            //Caso for digitado 7 caracteres na caixa de texto da placa, Ativa algumas funções

            if (txtPlaca.TextLength == 7)
            {
                txtNome.Enabled = true;
                mskTelefone.Enabled = true;
                CarregarVeiculo();
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
        private void CarregarVeiculo()
        {
            bool caixa;
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName= "@Placa", SqlDbType = SqlDbType.VarChar, Value = txtPlaca.Text}
                    };
                DataTable dt = new DataTable();
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Pesquisa_Info_Placa", sp);
                if (dt.Rows.Count > 0)
                {
                    cmbTipo.Text = dt.Rows[0]["Tipo"].ToString();
                    cmbMarca.Text = dt.Rows[0]["Marca"].ToString();
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
            if (Properties.Settings.Default.Cancelas)
            {
                if (Arduino.entrada)
                {
                    VerificarCaixas();
                }
                else
                {
                    MessageBox.Show("Não é possivel iniciar um Ticket, pois não há veiculos na entrada!", "Falha ao iniciar Ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                VerificarCaixas();
            }
            
            
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
                            //Regex para validar Nome.   
                            if (Regex.IsMatch(nome, @"^[A-Za-záàâãéèêíïóôõöúçÁÀÂÃÉÈÍÏÓÔÕÖÚÇ ]+$"))
                            {
                                //Regex para validar Telefone.
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
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Placa", SqlDbType = SqlDbType.VarChar, Value = placa}
                };
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Pesquisa_TicketAberto_Placa", sp: sp);
                if (Convert.ToInt32(dt.Rows[0]["QTD"]) > 0)
                {
                    MessageBox.Show("Já existe um ticket em andamento para este veiculo!", "Ticket não iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimparCaixas();
                }
                else
                {
                    if (Properties.Settings.Default.Foto)
                    {
                        Inicializacao = 0;
                        SalvarImagem();                        
                    }
                    InserirTicket(placa, nome, telefone);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao iniciar ticket!");
            }
            finally
            {
                dt.Dispose();
            }

        }
        private void InserirTicket(string placa, string nome, string telefone)
        {
            string marca = cmbMarca.Text, tipo = cmbTipo.Text;
            string hr = DateTime.Now.ToLongTimeString(), data = DateTime.Now.ToShortDateString();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@idUsuario", SqlDbType = SqlDbType.Int, Value = Globais.IdUsuario},
                    new SqlParameter(){ParameterName = "@Nome_Cliente", SqlDbType = SqlDbType.NVarChar, Value = nome },
                    new SqlParameter(){ParameterName = "@Telefone", SqlDbType = SqlDbType.NVarChar, Value = telefone },
                    new SqlParameter(){ParameterName = "@Placa",SqlDbType = SqlDbType.NVarChar, Value = placa },
                    new SqlParameter(){ParameterName = "@Marca", SqlDbType = SqlDbType.NVarChar, Value = marca },
                    new SqlParameter(){ParameterName = "@Tipo", SqlDbType = SqlDbType.NVarChar, Value = tipo },
                    new SqlParameter(){ParameterName = "@Hr_Entrada", SqlDbType = SqlDbType.Time, Value = hr },
                    new SqlParameter(){ParameterName = "@Data_Entrada", SqlDbType = SqlDbType.DateTime, Value = data },
                    new SqlParameter() { ParameterName = "@Caminho_Foto", SqlDbType = SqlDbType.NVarChar, Value = Globais.CaminhoFoto }
                };
                int idTicket = banco.ExecuteProcedureWithReturnValue(NameProcedure: "dbo.InsertTicket", sp: sp);
                //Verifica se houve algum retorno da procedure
                if (idTicket > 0)
                {
                    if (Properties.Settings.Default.Cancelas)
                    {
                        AbrirCancelaEntrada();
                    }
                    ContadorTicket();
                    LimparCaixas();
                    Globais.RegistrarLog(Globais.Login + " Inicou o Ticket #" + idTicket);
                    MessageBox.Show("Ticket Iniciado com sucesso! \n #Ticket:" + idTicket, "Ticket Iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Ticket.idTicket = idTicket;
                    Ticket.cliente = nome;
                    Ticket.telefone = telefone;
                    Ticket.placa = placa;
                    Ticket.marca = marca;
                    Ticket.tipo = tipo;
                    Ticket.hora_entrada = hr + " " + data;
                    Ticket.Usuario_entrada = Globais.Login;
                    if (Properties.Settings.Default.GerarPDF)
                    {
                        Imprimir();
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
                Globais.CaminhoFoto = Estacionamento.caminho_foto_padrao;
            }
        }
        private void Imprimir()
        {
            try
            {
                geradorPdf.filename = "Ticket_Entrada_" + Ticket.idTicket.ToString() + "_" + Ticket.placa + ".pdf";
                string caminho = geradorPdf.TicketEntrada();
                if (caminho != null)
                {
                    System.Diagnostics.Process.Start(caminho);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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
        public void PopularComboMarca()
        {

            //Verifica se tem algum Tipo que foi carregando do banco.
            if (cmbTipo.Items.Count > 0)
            {
                if (cmbTipo.SelectedIndex != -1)
                {
                    string tipo = cmbTipo.Text;
                    cmbMarca.Enabled = true;
                    DataTable dt = new DataTable();
                    //Limpa o DataTable
                    dt.Clear();
                    //Chama a função que executa a query no banco de dados
                    try
                    {
                        List<SqlParameter> sp = new List<SqlParameter>()
                        {
                        new SqlParameter(){ParameterName="@Tipo", SqlDbType = SqlDbType.VarChar,Value = tipo}
                        };
                        dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.ComboBox_Marca", sp: sp);
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
        }
        private void btnConfig_Click(object sender, EventArgs e)
        {
            FrmTelaConfig Frm = new FrmTelaConfig(this);
            AbrirForm(2, Frm);
        }
        private void PreencherLabels(DataTable dt)
        {
            string CaminhoFoto = dt.Rows[0]["Caminho Foto"].ToString();

            //Preenchendo as labels com as informações do banco
            Globais.IdTicket = Convert.ToInt32(dt.Rows[0]["#Ticket"]);
            lblTipo.Text = dt.Rows[0]["Tipo"].ToString(); //Tipo
            lblMarca.Text = dt.Rows[0]["Marca"].ToString();//Marca
            lblPlaca.Text = dt.Rows[0]["Placa"].ToString();//Placa
            txtNomeP.Text = dt.Rows[0]["Nome Cliente"].ToString();//Nome
            txtTelefoneP.Text = dt.Rows[0]["Telefone"].ToString();// Telefone
            lblHrEntrada.Text = dt.Rows[0]["Hora Entrada"].ToString() + " " + dt.Rows[0]["Data Entrada"].ToString();// Hora + Data
            picCam.Visible = false;
            picImagem.Visible = true;
            btnLimpaP.Enabled = true;
            if (CaminhoFoto != Estacionamento.caminho_foto_padrao && File.Exists(CaminhoFoto))
            {
                picImagem.Image = Image.FromFile(CaminhoFoto);
            }
            else
            {
                picImagem.Image = picImagem.ErrorImage;
            }
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
            if (Properties.Settings.Default.Foto)
            {
                picCam.Visible = true;
                picImagem.Visible = false;
            }
            else
            {
                picImagem.Visible = true;
                picImagem.Image = picImagem.InitialImage;
            }
            
        }

        private void FrmTelaOperacao_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globais.RegistrarLog(Globais.Login + " Efetuou logout.");
            
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
                if (Camera != null)
                {
                    CaptureInfo.DisposeCapture();
                }
                Arduino.Parar();
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
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@Placa", SqlDbType = SqlDbType.VarChar, Value = placa}
                    };
                    dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Pesquisa_Ticket_TelaOperacao", sp: sp);
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
        public void PesquisaTicketID()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@idTicket", SqlDbType = SqlDbType.Int, Value = Ticket.idTicket}
                };
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Select_Ticket_Encerrado", sp: sp);
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        lblHoraEntradaVisual.Visible = true;
                        PreencherLabels(dt);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Erro ao carregar ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                }
                else
                {
                    MessageBox.Show("Ticket inexistente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve uma falha ao carregar o ticket! \nRealize a pesquisa e tente novamente!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (Properties.Settings.Default.Cancelas)
            {
                if (serialPort1.IsOpen)
                {
                    AbrirCancelaEntrada("N");
                }
                else
                {
                    MessageBox.Show("Falha na comunicação serial", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ative o gerenciamento de Cancelas na configurações!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        private void AbrirCancelaEntrada(string method = "E")
        {
            try
            {
                Arduino.WriteCom(method);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Novo

        private void button8_Click(object sender, EventArgs e)
        {
            FrmTelaRelatorios Frm = new FrmTelaRelatorios();
            AbrirForm(2, Frm);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Cancelas)
            {
                if (serialPort1.IsOpen)
                {
                    AbrirCancelaSaida("M");
                }else
                {
                    MessageBox.Show("Falha na comunicação serial", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Ative o gerenciamento de Cancelas na configurações!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        private void AbrirCancelaSaida(string method = "S")
        {
            try
            {
                Arduino.WriteCom(method);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int resp = Convert.ToInt32(serialPort1.ReadExisting());

            switch (resp)
            {
                case 1:
                    PintaBotaoAbrir(btnEntrada);
                    break;

                case 2:
                    PintaBotaoFechar(btnEntrada);
                    break;
                case 3:
                    PintaBotaoAbrir(btnSaida);
                    break;
                case 4:
                    PintaBotaoFechar(btnSaida);
                    break;
                case 5:
                    Arduino.entrada = true;
                    break;
                case 6:
                    Arduino.entrada = false;
                    break;
                case 7:
                    Arduino.saida = true;
                    break;
                case 8:
                    Arduino.saida = false;
                    break;
                default:
                    break;
            }
        }
        private void PintaBotaoAbrir(Button btn)
        {
            btn.BackColor = Color.DarkBlue;
            btn.ForeColor = Color.White;
        }
        private void PintaBotaoFechar(Button btn)
        {
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.Black;
        }
        private void picImagem_Click(object sender, EventArgs e)
        {

        }
    }
}


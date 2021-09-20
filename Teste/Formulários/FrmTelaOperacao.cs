using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaOperacao : Form
    {
        Banco banco = new Banco();
        public FrmTelaOperacao()
        {
            InitializeComponent();
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
            if (!(Globais.Login == Properties.Settings.Default.UserRoot))
            {
                txtPlaca.Select();
                CarregarBarraStatus();
                PopularComboTipo();
                ContadorTicket();
                CarregarParametros();
            }
            //Foca a Caixa de texto da Placa

            /*
            Globais.IdUsuario = 1;
            Globais.Login = "admin";
            Globais.Nivel = 3;
            Globais.UserStatus = 1;
            lblUsername.Text = Globais.Login;
            */
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
        private void ContadorTicket()
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
            bool caixa;
            //Caso for digitado 7 caracteres na caixa de texto da placa, Ativa algumas funções

            if (txtPlaca.TextLength == 7)
            {
                caixa = true;
            }
            else
            {
                caixa = false;

            }
            AtivarFuncoes(caixa);
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
            if (txtPlaca.Text != "" && cmbTipo.SelectedIndex >= 0 && cmbTipo.SelectedIndex >= 0)
            {

                VerificarTicket(txtPlaca.Text);
            }
            else
            {
                MessageBox.Show("Há campos que precisam ser preenchidos!", "Ticket não iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void VerificarTicket(string placa)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ProcedurePesquisaTicketVeiculo(7,placa);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Já existe um ticket em andamento para este veiculo!", "Ticket não iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimparCaixas();
                    
                }
                else
                {
                    InserirTicket();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao iniciar ticket!");
            }
            
        }
        private void InserirTicket()
        {
            string placa = txtPlaca.Text, tipo = cmbTipo.Text, marca = cmbMarca.Text;
            string nome, telefone;
            int idTicket;
            
            if (txtNome.Text == "")
            {
                nome = "Convidado";
                telefone = "(00)00000-0000";
            }
            else
            {
                nome = txtNome.Text;
                telefone = mskTelefone.Text;
            }
            try
            {
                //Chama a função que executa uma Stored Procedure no banco de dados.
                idTicket = banco.ProcedureInserirTicket(placa, tipo, marca, nome, telefone);
                //Verifica se houve algum retorno da procedure
                if (idTicket > 0)
                {
                    MessageBox.Show("Ticket Iniciado com sucesso! \n #Ticket:" + idTicket, "Ticket Iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    ContadorTicket();
                    LimparCaixas();
                    Globais.RegistrarLog(Globais.Login + " Inicou o Ticket #" + idTicket);
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
        }
        private void LimparCaixas()
        {
            bool caixa = false;
            txtPlaca.Clear();
            cmbMarca.SelectedIndex = -1;
            PopularComboTipo();
            txtNome.Clear();
            mskTelefone.Clear();
            AtivarFuncoes(caixa);

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
                    dt = banco.ProcedureMarca(1,tipo,"");
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
            AbrirForm(0, Frm);
        }

        private void btnPesquisaTicket_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string placa = txtPlaca.Text;
            if (placa != "")
            {

                try
                {
                    dt = banco.ProcedurePesquisaTicketVeiculo(7,placa);
                    //Verifica se houve algum retorno no DataTable
                    if (dt.Rows.Count > 0)
                    {
                        PreencherLabels(dt);
                        AlinharLabels();
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
        private void PreencherLabels(DataTable dt)
        {
            //Preenchendo as labels com as informações do banco
            Globais.IdTicket = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
            lblTipo.Text = Convert.ToString(dt.Rows[0].ItemArray[1]); //Tipo
            lblMarca.Text = Convert.ToString(dt.Rows[0].ItemArray[2]);//Marca
            lblPlaca.Text = Convert.ToString(dt.Rows[0].ItemArray[3]);//Placa
            txtNomeP.Text = Convert.ToString(dt.Rows[0].ItemArray[4]);//Nome
            txtTelefoneP.Text = Convert.ToString(dt.Rows[0].ItemArray[5]);// Telefone
            lblHrEntrada.Text = Convert.ToString(dt.Rows[0].ItemArray[6]) + " " + Convert.ToString(dt.Rows[0].ItemArray[7]);// Hora + Data
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
        }

        private void FrmTelaOperacao_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globais.RegistrarLog(Globais.Login + " Efetuou Logout.");
            Globais.RegistrarLog("Sistema foi encerrado.");
            this.Dispose();
            Application.Exit();
        }
        private void FecharForm()
        {
            string mensagem = "Tem certeza que deseja sair?";
            string titulo = "Efetuar Logout?";
            bool escolha = (MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) == DialogResult.Yes);
            if (escolha)
            {
                //Registra que o Usuário efetuou logout
                Globais.RegistrarLog(Globais.Login + " Efetuou Logout.");
                //Destroi o Formulario principal e abre o formulario de login
                FrmTelaLogin Frm = new FrmTelaLogin();
                this.Dispose();
                Frm.ShowDialog();
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Caracteres permitidos
            string caracterespermitidos = "ABCDEFGHIJ0123456789";
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
            // Apenas letras de A-J e 0-9 e BackSpace no 5º Digito
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
    }
}

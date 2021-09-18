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
            int UserNivel = Globais.Nivel;
            //Verifica se o usuário logado tem nivel suficiente para acessar o form
            if (UserNivel >= nivel)
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
            //Foca a Caixa de texto da Placa
            txtPlaca.Select();
            CarregarBarraStatus();
            PopularComboTipo();
            ContadorTicket();
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
            string sql = @"
                SELECT 
                    id_automovel, automovel 
                FROM
                    tb_automovel 
                ORDER BY  
                    automovel desc";

            dt.Clear();
            dt = banco.QueryBancoSql(sql);
            cmbTipo.DataSource = null;
            cmbTipo.DataSource = dt;
            cmbTipo.ValueMember = "id_automovel";
            cmbTipo.DisplayMember = "automovel";
            cmbTipo.SelectedItem = null;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
        }
        private void ContadorTicket()
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT COUNT
                    (id_ticket) 
                FROM 
                    tb_ticket 
                WHERE status=1";
            dt = banco.QueryBancoSql(sql);
            lblQtdTicket.Text = Convert.ToString(dt.Rows[0].ItemArray[0]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FecharForm();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmTelaPesquisaTicket Frm = new FrmTelaPesquisaTicket();
            Frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmTelaEncerrarTicket Frm = new FrmTelaEncerrarTicket();
            Frm.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool caixa;
            //Caso for digitado 7 caracteres na caixa de texto da placa, Ativa algumas funções
            if (txtPlaca.TextLength == 7)
            {
                caixa = true;
                AtivarFuncoes(caixa);
            }
            else
            {
                caixa = false;
                AtivarFuncoes(caixa);
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
            //Obtem as informações das caixa de texto
            int idTicket = 0;
            string placa = txtPlaca.Text,
                   tipo = cmbTipo.Text,
                   marca = cmbMarca.Text,
                   nome,
                   telefone;
            bool caixafill = VerificarCaixas();
            if (caixafill)
            {
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
                //Chama a função que executa uma Stored Procedure no banco de dados.
                idTicket = banco.ProcedureInserirTicket(placa, tipo, marca, nome, telefone);
                //Verifica se houve algum retorno da procedure
                if (idTicket > 0)
                {
                    MessageBox.Show("Ticket Iniciado com sucesso! \n #Ticket:" + idTicket, "Ticket Iniciado!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    ContadorTicket();
                    LimparCaixas();
                    Globais.RegistrarLog(Globais.Login + " Inicou o Ticket #:" + idTicket);
                }
                else
                {
                    MessageBox.Show("Falha ao iniciar Ticket!", "Ticket não iniciado!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Há campos que precisam ser preenchidos!", "Ticket não iniciado!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        public bool VerificarCaixas()
        {
            bool caixafill;
            if (txtPlaca.Text != "" && cmbTipo.SelectedIndex >= 0 && cmbTipo.SelectedIndex >= 0)
            {
                return caixafill = true;
            }
            else
            {
                return caixafill = false;
            }
        }
        private void LimparCaixas()
        {
            bool caixa = false;
            txtPlaca.Clear();
            cmbTipo.SelectedItem = null;
            cmbMarca.SelectedItem = null;
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
                //Montagem da Query de acordo com o Tipo que foi selecionado na ComboBox
                string sql = @"
                   SELECT 
                        A.automovel[Tipo], M.marca [Marca] FROM tb_automovel as A 
                   INNER JOIN 
                        tb_marca as M ON 
                    A.id_automovel = M.id_automovel AND A.automovel='" + tipo + "'";
                //Limpa o DataTable
                dt.Clear();
                //Chama a função que executa a query no banco de dados
                dt = banco.QueryBancoSql(sql);
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

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FrmTelaConfig frm = new FrmTelaConfig();
            frm.ShowDialog();
        }

        private void btnPesquisaTicket_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string placa = txtPlaca.Text;
            if (placa != "")
            {
                //Query a ser executada no banco
                string query = @"
                   SELECT 
                        Ticket.id_ticket[#Ticket],Car.tipo[Tipo], Car.marca[Marca], Car.placa[Placa], Cli.nome[Nome], Cli.telefone[Telefone],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada]
                   FROM 
                        tb_ticket AS Ticket 
                   INNER JOIN
                        tb_carro AS Car ON Ticket.carro_id = Car.id_Carro 
                   INNER JOIN 
                        tb_cliente AS Cli ON Car.cliente_id = Cli.id_cliente 
                   INNER JOIN 
                        tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket 
                   AND Placa='" + placa + "' AND Ticket.status=1";
                //Chamando a função que executa a query no banco e retorna um Data Table
                dt = banco.QueryBancoSql(query);
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
            lblHrEntrada.Text = Convert.ToString(dt.Rows[0].ItemArray[6]) + " " + Convert.ToString(dt.Rows[0].ItemArray[7]);// Data e Hora
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

        private void button1_Click(object sender, EventArgs e)
        {
            FrmTelaCadastros frm = new FrmTelaCadastros();
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}

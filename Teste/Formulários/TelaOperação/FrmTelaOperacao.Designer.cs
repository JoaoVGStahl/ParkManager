
namespace Teste
{
    partial class FrmTelaOperacao
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTelaOperacao));
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.cmbMarca = new System.Windows.Forms.ComboBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblQtdTicket = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnEncerrar = new System.Windows.Forms.Button();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.txtNomeP = new System.Windows.Forms.TextBox();
            this.txtTelefoneP = new System.Windows.Forms.TextBox();
            this.lblHrEntrada = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblHoraEntradaVisual = new System.Windows.Forms.Label();
            this.btnLimpaP = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblPlacal1 = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnCadastros = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblData = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHora = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.HoraData = new System.Windows.Forms.Timer(this.components);
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnPesquisaTicket = new System.Windows.Forms.Button();
            this.mskTelefone = new System.Windows.Forms.MaskedTextBox();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbTipo
            // 
            this.cmbTipo.BackColor = System.Drawing.Color.White;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Enabled = false;
            this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.Location = new System.Drawing.Point(282, 41);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(189, 37);
            this.cmbTipo.TabIndex = 1;
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.Color.White;
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.Location = new System.Drawing.Point(88, 41);
            this.txtPlaca.MaxLength = 7;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(183, 35);
            this.txtPlaca.TabIndex = 0;
            this.txtPlaca.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // cmbMarca
            // 
            this.cmbMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbMarca.BackColor = System.Drawing.Color.White;
            this.cmbMarca.Enabled = false;
            this.cmbMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMarca.FormattingEnabled = true;
            this.cmbMarca.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbMarca.IntegralHeight = false;
            this.cmbMarca.Location = new System.Drawing.Point(484, 41);
            this.cmbMarca.Name = "cmbMarca";
            this.cmbMarca.Size = new System.Drawing.Size(240, 37);
            this.cmbMarca.TabIndex = 2;
            this.cmbMarca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMarca_KeyPress);
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.White;
            this.txtNome.Enabled = false;
            this.txtNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNome.Location = new System.Drawing.Point(732, 41);
            this.txtNome.MaxLength = 50;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(397, 35);
            this.txtNome.TabIndex = 3;
            this.txtNome.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNome_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(274, 7);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(78, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "*Tipo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "*Placa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(728, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 31);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nome";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(476, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 31);
            this.label4.TabIndex = 10;
            this.label4.Text = "*Marca";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1129, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 31);
            this.label5.TabIndex = 11;
            this.label5.Text = "Telefone";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblQtdTicket);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 471);
            this.panel1.TabIndex = 12;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.pictureBox1);
            this.panel9.Location = new System.Drawing.Point(3, 111);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(656, 346);
            this.panel9.TabIndex = 36;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(656, 346);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(216, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(212, 31);
            this.label8.TabIndex = 3;
            this.label8.Text = "Ticket Abertos:";
            // 
            // lblQtdTicket
            // 
            this.lblQtdTicket.BackColor = System.Drawing.Color.Transparent;
            this.lblQtdTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtdTicket.Location = new System.Drawing.Point(233, 35);
            this.lblQtdTicket.Name = "lblQtdTicket";
            this.lblQtdTicket.Size = new System.Drawing.Size(147, 73);
            this.lblQtdTicket.TabIndex = 10;
            this.lblQtdTicket.Text = "0";
            this.lblQtdTicket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.Transparent;
            this.btnIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnIniciar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciar.Enabled = false;
            this.btnIniciar.FlatAppearance.BorderSize = 0;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciar.Image = ((System.Drawing.Image)(resources.GetObject("btnIniciar.Image")));
            this.btnIniciar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIniciar.Location = new System.Drawing.Point(142, 2);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(220, 85);
            this.btnIniciar.TabIndex = 13;
            this.btnIniciar.Text = "Iniciar (F5)";
            this.btnIniciar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnEncerrar
            // 
            this.btnEncerrar.BackColor = System.Drawing.Color.Transparent;
            this.btnEncerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEncerrar.Enabled = false;
            this.btnEncerrar.FlatAppearance.BorderSize = 0;
            this.btnEncerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEncerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnEncerrar.Image")));
            this.btnEncerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEncerrar.Location = new System.Drawing.Point(372, 2);
            this.btnEncerrar.Name = "btnEncerrar";
            this.btnEncerrar.Size = new System.Drawing.Size(204, 85);
            this.btnEncerrar.TabIndex = 14;
            this.btnEncerrar.Text = "Encerrar Ticket (F4)";
            this.btnEncerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEncerrar.UseVisualStyleBackColor = false;
            this.btnEncerrar.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblPlaca
            // 
            this.lblPlaca.BackColor = System.Drawing.Color.Transparent;
            this.lblPlaca.Font = new System.Drawing.Font("Arial Narrow", 65.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaca.Location = new System.Drawing.Point(2, 32);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPlaca.Size = new System.Drawing.Size(468, 82);
            this.lblPlaca.TabIndex = 15;
            this.lblPlaca.Text = "PLACA";
            this.lblPlaca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.BackColor = System.Drawing.Color.Transparent;
            this.lblTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.ForeColor = System.Drawing.Color.White;
            this.lblTipo.Location = new System.Drawing.Point(-1, -1);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(67, 29);
            this.lblTipo.TabIndex = 17;
            this.lblTipo.Text = "Tipo";
            // 
            // txtNomeP
            // 
            this.txtNomeP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNomeP.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeP.Location = new System.Drawing.Point(13, 182);
            this.txtNomeP.MaxLength = 32;
            this.txtNomeP.Name = "txtNomeP";
            this.txtNomeP.ReadOnly = true;
            this.txtNomeP.Size = new System.Drawing.Size(637, 35);
            this.txtNomeP.TabIndex = 21;
            this.txtNomeP.Text = "Nome";
            this.txtNomeP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTelefoneP
            // 
            this.txtTelefoneP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTelefoneP.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefoneP.Location = new System.Drawing.Point(176, 228);
            this.txtTelefoneP.Name = "txtTelefoneP";
            this.txtTelefoneP.ReadOnly = true;
            this.txtTelefoneP.Size = new System.Drawing.Size(311, 35);
            this.txtTelefoneP.TabIndex = 21;
            this.txtTelefoneP.Text = "Telefone";
            this.txtTelefoneP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHrEntrada
            // 
            this.lblHrEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHrEntrada.Location = new System.Drawing.Point(110, 315);
            this.lblHrEntrada.Name = "lblHrEntrada";
            this.lblHrEntrada.Size = new System.Drawing.Size(447, 82);
            this.lblHrEntrada.TabIndex = 24;
            this.lblHrEntrada.Text = "Horário de entrada";
            this.lblHrEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(218, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(275, 31);
            this.label10.TabIndex = 25;
            this.label10.Text = "Resultado da Busca";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblHoraEntradaVisual);
            this.panel2.Controls.Add(this.btnLimpaP);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.lblHrEntrada);
            this.panel2.Controls.Add(this.txtTelefoneP);
            this.panel2.Controls.Add(this.txtNomeP);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(684, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(662, 471);
            this.panel2.TabIndex = 18;
            // 
            // lblHoraEntradaVisual
            // 
            this.lblHoraEntradaVisual.AutoSize = true;
            this.lblHoraEntradaVisual.BackColor = System.Drawing.Color.Transparent;
            this.lblHoraEntradaVisual.Font = new System.Drawing.Font("Microsoft Sans Serif", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraEntradaVisual.Location = new System.Drawing.Point(176, 299);
            this.lblHoraEntradaVisual.Name = "lblHoraEntradaVisual";
            this.lblHoraEntradaVisual.Size = new System.Drawing.Size(327, 40);
            this.lblHoraEntradaVisual.TabIndex = 29;
            this.lblHoraEntradaVisual.Text = "Horário da entrada:";
            this.lblHoraEntradaVisual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHoraEntradaVisual.Visible = false;
            // 
            // btnLimpaP
            // 
            this.btnLimpaP.BackColor = System.Drawing.Color.Transparent;
            this.btnLimpaP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpaP.Enabled = false;
            this.btnLimpaP.FlatAppearance.BorderSize = 0;
            this.btnLimpaP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpaP.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpaP.Image")));
            this.btnLimpaP.Location = new System.Drawing.Point(509, 228);
            this.btnLimpaP.Name = "btnLimpaP";
            this.btnLimpaP.Size = new System.Drawing.Size(51, 47);
            this.btnLimpaP.TabIndex = 28;
            this.btnLimpaP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpaP.UseVisualStyleBackColor = false;
            this.btnLimpaP.Click += new System.EventHandler(this.btnLimpaP_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblPlaca);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.lblPlacal1);
            this.panel4.Location = new System.Drawing.Point(117, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(471, 131);
            this.panel4.TabIndex = 27;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkBlue;
            this.panel5.Controls.Add(this.lblMarca);
            this.panel5.Controls.Add(this.lblTipo);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(487, 28);
            this.panel5.TabIndex = 16;
            // 
            // lblMarca
            // 
            this.lblMarca.BackColor = System.Drawing.Color.Transparent;
            this.lblMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(192, -1);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMarca.Size = new System.Drawing.Size(278, 29);
            this.lblMarca.TabIndex = 26;
            this.lblMarca.Text = "Marca";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPlacal1
            // 
            this.lblPlacal1.AutoSize = true;
            this.lblPlacal1.BackColor = System.Drawing.Color.Transparent;
            this.lblPlacal1.Font = new System.Drawing.Font("Arial Narrow", 51.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlacal1.Location = new System.Drawing.Point(-11, 37);
            this.lblPlacal1.Name = "lblPlacal1";
            this.lblPlacal1.Size = new System.Drawing.Size(0, 81);
            this.lblPlacal1.TabIndex = 30;
            // 
            // btnConfig
            // 
            this.btnConfig.BackColor = System.Drawing.Color.Transparent;
            this.btnConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfig.BackgroundImage")));
            this.btnConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfig.FlatAppearance.BorderSize = 0;
            this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfig.Location = new System.Drawing.Point(1217, 2);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(120, 85);
            this.btnConfig.TabIndex = 21;
            this.btnConfig.UseVisualStyleBackColor = false;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Silver;
            this.panel6.Controls.Add(this.btnCadastros);
            this.panel6.Controls.Add(this.btnSair);
            this.panel6.Controls.Add(this.button8);
            this.panel6.Controls.Add(this.button6);
            this.panel6.Controls.Add(this.btnConfig);
            this.panel6.Controls.Add(this.btnEncerrar);
            this.panel6.Controls.Add(this.btnIniciar);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 612);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1346, 88);
            this.panel6.TabIndex = 24;
            // 
            // btnCadastros
            // 
            this.btnCadastros.BackColor = System.Drawing.Color.Transparent;
            this.btnCadastros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCadastros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastros.FlatAppearance.BorderSize = 0;
            this.btnCadastros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastros.Image = ((System.Drawing.Image)(resources.GetObject("btnCadastros.Image")));
            this.btnCadastros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCadastros.Location = new System.Drawing.Point(772, 0);
            this.btnCadastros.Name = "btnCadastros";
            this.btnCadastros.Size = new System.Drawing.Size(212, 85);
            this.btnCadastros.TabIndex = 27;
            this.btnCadastros.Text = "Cadastros";
            this.btnCadastros.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCadastros.UseVisualStyleBackColor = false;
            this.btnCadastros.Click += new System.EventHandler(this.btnCadastros_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.Transparent;
            this.btnSair.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.Location = new System.Drawing.Point(8, 2);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(120, 85);
            this.btnSair.TabIndex = 26;
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.button4_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(994, 0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(213, 85);
            this.button8.TabIndex = 25;
            this.button8.Text = "Relatórios";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(586, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(176, 85);
            this.button6.TabIndex = 24;
            this.button6.Text = "Tickets";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblData,
            this.lblHora,
            this.toolStripStatusLabel1,
            this.lblUsername});
            this.statusStrip1.Location = new System.Drawing.Point(0, 700);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1346, 25);
            this.statusStrip1.TabIndex = 27;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblData
            // 
            this.lblData.BackColor = System.Drawing.Color.Transparent;
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(42, 20);
            this.lblData.Text = "Data";
            // 
            // lblHora
            // 
            this.lblHora.BackColor = System.Drawing.Color.Transparent;
            this.lblHora.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(62, 20);
            this.lblHora.Text = "Horario";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 20);
            this.toolStripStatusLabel1.Text = "Usuário:";
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(63, 20);
            this.lblUsername.Text = "Usuario";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Firebrick;
            this.label13.Location = new System.Drawing.Point(5, 79);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label13.Size = new System.Drawing.Size(357, 31);
            this.label13.TabIndex = 28;
            this.label13.Text = "* Informações obrigatórias";
            // 
            // HoraData
            // 
            this.HoraData.Interval = 1000;
            this.HoraData.Tick += new System.EventHandler(this.HoraData_Tick);
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Silver;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Enabled = false;
            this.splitter2.Location = new System.Drawing.Point(0, 609);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1346, 3);
            this.splitter2.TabIndex = 31;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkBlue;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 599);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1346, 10);
            this.panel3.TabIndex = 32;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Silver;
            this.panel7.Controls.Add(this.btnPesquisaTicket);
            this.panel7.Controls.Add(this.mskTelefone);
            this.panel7.Controls.Add(this.cmbTipo);
            this.panel7.Controls.Add(this.cmbMarca);
            this.panel7.Controls.Add(this.txtNome);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.txtPlaca);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1346, 115);
            this.panel7.TabIndex = 33;
            // 
            // btnPesquisaTicket
            // 
            this.btnPesquisaTicket.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaTicket.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPesquisaTicket.BackgroundImage")));
            this.btnPesquisaTicket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPesquisaTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaTicket.Enabled = false;
            this.btnPesquisaTicket.FlatAppearance.BorderSize = 0;
            this.btnPesquisaTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaTicket.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisaTicket.Image")));
            this.btnPesquisaTicket.Location = new System.Drawing.Point(3, 10);
            this.btnPesquisaTicket.Name = "btnPesquisaTicket";
            this.btnPesquisaTicket.Size = new System.Drawing.Size(72, 69);
            this.btnPesquisaTicket.TabIndex = 30;
            this.btnPesquisaTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPesquisaTicket.UseVisualStyleBackColor = false;
            this.btnPesquisaTicket.Click += new System.EventHandler(this.btnPesquisaTicket_Click_2);
            // 
            // mskTelefone
            // 
            this.mskTelefone.BackColor = System.Drawing.Color.White;
            this.mskTelefone.Enabled = false;
            this.mskTelefone.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskTelefone.Location = new System.Drawing.Point(1137, 41);
            this.mskTelefone.Mask = "(99)00000-0000";
            this.mskTelefone.Name = "mskTelefone";
            this.mskTelefone.Size = new System.Drawing.Size(202, 35);
            this.mskTelefone.TabIndex = 4;
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.Color.Silver;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Enabled = false;
            this.splitter3.Location = new System.Drawing.Point(0, 115);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(1346, 3);
            this.splitter3.TabIndex = 34;
            this.splitter3.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.DarkBlue;
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 118);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1346, 10);
            this.panel8.TabIndex = 35;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.White;
            this.panel10.Controls.Add(this.panel2);
            this.panel10.Controls.Add(this.panel1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 128);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1346, 471);
            this.panel10.TabIndex = 36;
            // 
            // FrmTelaOperacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1346, 725);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "FrmTelaOperacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Principal - ParkManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTelaOperacao_FormClosing);
            this.Load += new System.EventHandler(this.FrmTelaOperacao_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.ComboBox cmbMarca;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnEncerrar;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.TextBox txtNomeP;
        private System.Windows.Forms.TextBox txtTelefoneP;
        private System.Windows.Forms.Label lblHrEntrada;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label lblQtdTicket;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.ToolStripStatusLabel lblUsername;
        private System.Windows.Forms.ToolStripStatusLabel lblData;
        private System.Windows.Forms.Timer HoraData;
        private System.Windows.Forms.ToolStripStatusLabel lblHora;
        private System.Windows.Forms.Button btnLimpaP;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.MaskedTextBox mskTelefone;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lblHoraEntradaVisual;
        private System.Windows.Forms.Button btnPesquisaTicket;
        private System.Windows.Forms.Button btnCadastros;
        private System.Windows.Forms.Label lblPlacal1;
    }
}


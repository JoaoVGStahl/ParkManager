
namespace Teste
{
    partial class FrmTelaRelatorios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTelaRelatorios));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblTop4 = new System.Windows.Forms.Label();
            this.lblTop3 = new System.Windows.Forms.Label();
            this.lblTop2 = new System.Windows.Forms.Label();
            this.lblMarca2 = new System.Windows.Forms.Label();
            this.lblMarca1 = new System.Windows.Forms.Label();
            this.lblMarca3 = new System.Windows.Forms.Label();
            this.lblCountMarca = new System.Windows.Forms.Label();
            this.lblMarca4 = new System.Windows.Forms.Label();
            this.lblTop1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTicket = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chtRelatorio = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblNada = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnCliente = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnFinanceiro = new System.Windows.Forms.Button();
            this.btnVeiculo = new System.Windows.Forms.Button();
            this.btnFluxo = new System.Windows.Forms.Button();
            this.dtpInicial = new System.Windows.Forms.DateTimePicker();
            this.dtpFinal = new System.Windows.Forms.DateTimePicker();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtRelatorio)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblTicket);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 699);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblMarca);
            this.panel2.Controls.Add(this.lblTotal);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.lblTop4);
            this.panel2.Controls.Add(this.lblTop3);
            this.panel2.Controls.Add(this.lblTop2);
            this.panel2.Controls.Add(this.lblMarca2);
            this.panel2.Controls.Add(this.lblMarca1);
            this.panel2.Controls.Add(this.lblMarca3);
            this.panel2.Controls.Add(this.lblCountMarca);
            this.panel2.Controls.Add(this.lblMarca4);
            this.panel2.Controls.Add(this.lblTop1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(0, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 459);
            this.panel2.TabIndex = 23;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblMarca
            // 
            this.lblMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.Location = new System.Drawing.Point(3, 34);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(241, 38);
            this.lblMarca.TabIndex = 14;
            this.lblMarca.Text = "Marca";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMarca.Click += new System.EventHandler(this.lblMarca_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(5, 375);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(239, 33);
            this.lblTotal.TabIndex = 13;
            this.lblTotal.Text = "0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(80, 332);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 37);
            this.label15.TabIndex = 11;
            this.label15.Text = "Total";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // lblTop4
            // 
            this.lblTop4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTop4.Location = new System.Drawing.Point(140, 277);
            this.lblTop4.Name = "lblTop4";
            this.lblTop4.Size = new System.Drawing.Size(86, 24);
            this.lblTop4.TabIndex = 10;
            this.lblTop4.Text = "0";
            // 
            // lblTop3
            // 
            this.lblTop3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTop3.Location = new System.Drawing.Point(140, 241);
            this.lblTop3.Name = "lblTop3";
            this.lblTop3.Size = new System.Drawing.Size(86, 24);
            this.lblTop3.TabIndex = 9;
            this.lblTop3.Text = "0";
            this.lblTop3.Click += new System.EventHandler(this.label13_Click);
            // 
            // lblTop2
            // 
            this.lblTop2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTop2.Location = new System.Drawing.Point(140, 205);
            this.lblTop2.Name = "lblTop2";
            this.lblTop2.Size = new System.Drawing.Size(86, 24);
            this.lblTop2.TabIndex = 8;
            this.lblTop2.Text = "0";
            this.lblTop2.Click += new System.EventHandler(this.lblTop2_Click);
            // 
            // lblMarca2
            // 
            this.lblMarca2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca2.Location = new System.Drawing.Point(9, 204);
            this.lblMarca2.Name = "lblMarca2";
            this.lblMarca2.Size = new System.Drawing.Size(121, 25);
            this.lblMarca2.TabIndex = 7;
            this.lblMarca2.Text = "TOP 2:";
            this.lblMarca2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMarca1
            // 
            this.lblMarca1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca1.Location = new System.Drawing.Point(9, 169);
            this.lblMarca1.Name = "lblMarca1";
            this.lblMarca1.Size = new System.Drawing.Size(121, 25);
            this.lblMarca1.TabIndex = 6;
            this.lblMarca1.Text = "TOP 1:";
            this.lblMarca1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMarca3
            // 
            this.lblMarca3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca3.Location = new System.Drawing.Point(9, 240);
            this.lblMarca3.Name = "lblMarca3";
            this.lblMarca3.Size = new System.Drawing.Size(121, 25);
            this.lblMarca3.TabIndex = 5;
            this.lblMarca3.Text = "TOP 3:";
            this.lblMarca3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCountMarca
            // 
            this.lblCountMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountMarca.Location = new System.Drawing.Point(4, 72);
            this.lblCountMarca.Name = "lblCountMarca";
            this.lblCountMarca.Size = new System.Drawing.Size(239, 33);
            this.lblCountMarca.TabIndex = 4;
            this.lblCountMarca.Text = "0";
            this.lblCountMarca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCountMarca.Click += new System.EventHandler(this.lblCountMarca_Click);
            // 
            // lblMarca4
            // 
            this.lblMarca4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca4.Location = new System.Drawing.Point(9, 276);
            this.lblMarca4.Name = "lblMarca4";
            this.lblMarca4.Size = new System.Drawing.Size(121, 25);
            this.lblMarca4.TabIndex = 3;
            this.lblMarca4.Text = "TOP 4:";
            this.lblMarca4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTop1
            // 
            this.lblTop1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTop1.Location = new System.Drawing.Point(140, 170);
            this.lblTop1.Name = "lblTop1";
            this.lblTop1.Size = new System.Drawing.Size(86, 24);
            this.lblTop1.TabIndex = 2;
            this.lblTop1.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Quantidade de Veiculos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Marca mais frequentada";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblTicket
            // 
            this.lblTicket.AutoSize = true;
            this.lblTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicket.Location = new System.Drawing.Point(60, 35);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(118, 37);
            this.lblTicket.TabIndex = 5;
            this.lblTicket.Text = "Tickets";
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.Transparent;
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.Location = new System.Drawing.Point(3, 592);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(249, 77);
            this.btnSair.TabIndex = 22;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Silver;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(252, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 699);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkBlue;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(255, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 699);
            this.panel3.TabIndex = 4;
            // 
            // chtRelatorio
            // 
            this.chtRelatorio.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chtRelatorio.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chtRelatorio.Legends.Add(legend1);
            this.chtRelatorio.Location = new System.Drawing.Point(0, 112);
            this.chtRelatorio.Name = "chtRelatorio";
            this.chtRelatorio.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chtRelatorio.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Crimson,
        System.Drawing.SystemColors.HotTrack,
        System.Drawing.Color.DarkBlue,
        System.Drawing.Color.Red};
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 4;
            this.chtRelatorio.Series.Add(series1);
            this.chtRelatorio.Size = new System.Drawing.Size(1052, 268);
            this.chtRelatorio.TabIndex = 30;
            this.chtRelatorio.Text = "chart1";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.lblNada);
            this.panel4.Controls.Add(this.btnGerar);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.dtpInicial);
            this.panel4.Controls.Add(this.dtpFinal);
            this.panel4.Controls.Add(this.chtRelatorio);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(265, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1055, 380);
            this.panel4.TabIndex = 34;
            // 
            // lblNada
            // 
            this.lblNada.AutoSize = true;
            this.lblNada.BackColor = System.Drawing.Color.Transparent;
            this.lblNada.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNada.ForeColor = System.Drawing.Color.DarkRed;
            this.lblNada.Location = new System.Drawing.Point(291, 219);
            this.lblNada.Name = "lblNada";
            this.lblNada.Size = new System.Drawing.Size(442, 37);
            this.lblNada.TabIndex = 24;
            this.lblNada.Text = "< Não há dados para exibir! >";
            this.lblNada.Visible = false;
            // 
            // btnGerar
            // 
            this.btnGerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerar.Image = ((System.Drawing.Image)(resources.GetObject("btnGerar.Image")));
            this.btnGerar.Location = new System.Drawing.Point(769, 70);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(132, 36);
            this.btnGerar.TabIndex = 39;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Silver;
            this.panel7.Controls.Add(this.btnCliente);
            this.panel7.Controls.Add(this.button5);
            this.panel7.Controls.Add(this.btnFinanceiro);
            this.panel7.Controls.Add(this.btnVeiculo);
            this.panel7.Controls.Add(this.btnFluxo);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1051, 52);
            this.panel7.TabIndex = 1;
            // 
            // btnCliente
            // 
            this.btnCliente.FlatAppearance.BorderSize = 0;
            this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnCliente.Image")));
            this.btnCliente.Location = new System.Drawing.Point(467, 0);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(133, 52);
            this.btnCliente.TabIndex = 44;
            this.btnCliente.Text = "Cliente";
            this.btnCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(892, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(149, 48);
            this.button5.TabIndex = 39;
            this.button5.Text = "Imprimir";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // btnFinanceiro
            // 
            this.btnFinanceiro.FlatAppearance.BorderSize = 0;
            this.btnFinanceiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinanceiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinanceiro.Image = ((System.Drawing.Image)(resources.GetObject("btnFinanceiro.Image")));
            this.btnFinanceiro.Location = new System.Drawing.Point(334, 0);
            this.btnFinanceiro.Name = "btnFinanceiro";
            this.btnFinanceiro.Size = new System.Drawing.Size(133, 52);
            this.btnFinanceiro.TabIndex = 45;
            this.btnFinanceiro.Text = "Financeiro";
            this.btnFinanceiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFinanceiro.UseVisualStyleBackColor = true;
            this.btnFinanceiro.Click += new System.EventHandler(this.btnFinanceiro_Click);
            // 
            // btnVeiculo
            // 
            this.btnVeiculo.FlatAppearance.BorderSize = 0;
            this.btnVeiculo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVeiculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVeiculo.Image = ((System.Drawing.Image)(resources.GetObject("btnVeiculo.Image")));
            this.btnVeiculo.Location = new System.Drawing.Point(600, 0);
            this.btnVeiculo.Name = "btnVeiculo";
            this.btnVeiculo.Size = new System.Drawing.Size(133, 52);
            this.btnVeiculo.TabIndex = 46;
            this.btnVeiculo.Text = "Veículo";
            this.btnVeiculo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVeiculo.UseVisualStyleBackColor = true;
            this.btnVeiculo.Click += new System.EventHandler(this.btnVeiculo_Click);
            // 
            // btnFluxo
            // 
            this.btnFluxo.FlatAppearance.BorderSize = 0;
            this.btnFluxo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFluxo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFluxo.Image = ((System.Drawing.Image)(resources.GetObject("btnFluxo.Image")));
            this.btnFluxo.Location = new System.Drawing.Point(201, 0);
            this.btnFluxo.Name = "btnFluxo";
            this.btnFluxo.Size = new System.Drawing.Size(133, 52);
            this.btnFluxo.TabIndex = 43;
            this.btnFluxo.Text = "Fluxo";
            this.btnFluxo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFluxo.UseVisualStyleBackColor = true;
            this.btnFluxo.Click += new System.EventHandler(this.btnFluxo_Click);
            // 
            // dtpInicial
            // 
            this.dtpInicial.CustomFormat = "dd \'de\'MMMM\'de\' yyyy";
            this.dtpInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInicial.Location = new System.Drawing.Point(201, 77);
            this.dtpInicial.Name = "dtpInicial";
            this.dtpInicial.Size = new System.Drawing.Size(238, 29);
            this.dtpInicial.TabIndex = 38;
            this.dtpInicial.Value = new System.DateTime(2021, 11, 20, 0, 0, 0, 0);
            // 
            // dtpFinal
            // 
            this.dtpFinal.CustomFormat = "dd \'de\'MMMM\'de\' yyyy";
            this.dtpFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFinal.Location = new System.Drawing.Point(513, 77);
            this.dtpFinal.Name = "dtpFinal";
            this.dtpFinal.Size = new System.Drawing.Size(238, 29);
            this.dtpFinal.TabIndex = 37;
            this.dtpFinal.Value = new System.DateTime(2021, 11, 20, 0, 0, 0, 0);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(265, 390);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1055, 309);
            this.panel5.TabIndex = 35;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1055, 309);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.DarkBlue;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(265, 380);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1055, 10);
            this.panel6.TabIndex = 38;
            // 
            // FrmTelaRelatorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1320, 699);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1336, 738);
            this.MinimumSize = new System.Drawing.Size(1336, 726);
            this.Name = "FrmTelaRelatorios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTelaRelatorios";
            this.Load += new System.EventHandler(this.FrmTelaRelatorios_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtRelatorio)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label lblTicket;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtRelatorio;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblTop4;
        private System.Windows.Forms.Label lblTop3;
        private System.Windows.Forms.Label lblTop2;
        private System.Windows.Forms.Label lblMarca2;
        private System.Windows.Forms.Label lblMarca1;
        private System.Windows.Forms.Label lblMarca3;
        private System.Windows.Forms.Label lblCountMarca;
        private System.Windows.Forms.Label lblMarca4;
        private System.Windows.Forms.Label lblTop1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DateTimePicker dtpInicial;
        private System.Windows.Forms.DateTimePicker dtpFinal;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnFinanceiro;
        private System.Windows.Forms.Button btnVeiculo;
        private System.Windows.Forms.Button btnFluxo;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Label lblNada;
    }
}
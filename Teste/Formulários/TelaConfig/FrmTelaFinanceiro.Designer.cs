
namespace Teste
{
    partial class FrmTelaFinanceiro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTelaFinanceiro));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtId = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ckValorUnico = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrecoHora = new System.Windows.Forms.TextBox();
            this.txtCobrancaMinima = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrecoHora = new System.Windows.Forms.Label();
            this.lblPrecoUnico = new System.Windows.Forms.Label();
            this.txtValorUnico = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPrecoMin = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTolerancia = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTolerancia = new System.Windows.Forms.TrackBar();
            this.lblCont = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTolerancia)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.txtId);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 85);
            this.panel1.TabIndex = 0;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(311, 33);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(156, 20);
            this.txtId.TabIndex = 0;
            this.txtId.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btnEditar);
            this.panel2.Controls.Add(this.btnSalvar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 582);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(814, 85);
            this.panel2.TabIndex = 1;
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnEditar.BackColor = System.Drawing.Color.Transparent;
            this.btnEditar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(442, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(174, 79);
            this.btnEditar.TabIndex = 31;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSalvar.BackColor = System.Drawing.Color.Transparent;
            this.btnSalvar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(198, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(174, 79);
            this.btnSalvar.TabIndex = 30;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Silver;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(0, 85);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(814, 3);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Silver;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Enabled = false;
            this.splitter2.Location = new System.Drawing.Point(0, 579);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(814, 3);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkBlue;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(814, 10);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 569);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(814, 10);
            this.panel4.TabIndex = 5;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "Preço Hora(R$)";
            // 
            // ckValorUnico
            // 
            this.ckValorUnico.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckValorUnico.BackColor = System.Drawing.Color.DarkGray;
            this.ckValorUnico.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ckValorUnico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckValorUnico.Enabled = false;
            this.ckValorUnico.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ckValorUnico.FlatAppearance.BorderSize = 2;
            this.ckValorUnico.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkBlue;
            this.ckValorUnico.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.ckValorUnico.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.ckValorUnico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckValorUnico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckValorUnico.ForeColor = System.Drawing.Color.Black;
            this.ckValorUnico.Location = new System.Drawing.Point(322, 387);
            this.ckValorUnico.Name = "ckValorUnico";
            this.ckValorUnico.Size = new System.Drawing.Size(135, 35);
            this.ckValorUnico.TabIndex = 7;
            this.ckValorUnico.Text = "Desativado";
            this.ckValorUnico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckValorUnico.UseMnemonic = false;
            this.ckValorUnico.UseVisualStyleBackColor = false;
            this.ckValorUnico.CheckedChanged += new System.EventHandler(this.ckValorUnico_CheckedChanged);
            this.ckValorUnico.CheckStateChanged += new System.EventHandler(this.ckValorUnico_CheckStateChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cobrança Mínima(R$)";
            // 
            // txtPrecoHora
            // 
            this.txtPrecoHora.Enabled = false;
            this.txtPrecoHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecoHora.Location = new System.Drawing.Point(27, 168);
            this.txtPrecoHora.Name = "txtPrecoHora";
            this.txtPrecoHora.Size = new System.Drawing.Size(232, 44);
            this.txtPrecoHora.TabIndex = 9;
            this.txtPrecoHora.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecoHora.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecoHora_KeyPress);
            this.txtPrecoHora.Leave += new System.EventHandler(this.txtPrecoHora_Leave);
            // 
            // txtCobrancaMinima
            // 
            this.txtCobrancaMinima.Enabled = false;
            this.txtCobrancaMinima.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCobrancaMinima.Location = new System.Drawing.Point(27, 277);
            this.txtCobrancaMinima.Name = "txtCobrancaMinima";
            this.txtCobrancaMinima.Size = new System.Drawing.Size(232, 44);
            this.txtCobrancaMinima.TabIndex = 10;
            this.txtCobrancaMinima.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCobrancaMinima.TextChanged += new System.EventHandler(this.txtCobrancaMinima_TextChanged);
            this.txtCobrancaMinima.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCobrancaMinima_KeyPress);
            this.txtCobrancaMinima.Leave += new System.EventHandler(this.txtCobrancaMinima_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 29);
            this.label3.TabIndex = 11;
            this.label3.Text = "Preço Hora Atual";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 29);
            this.label4.TabIndex = 12;
            this.label4.Text = "Valor Único Atual";
            // 
            // lblPrecoHora
            // 
            this.lblPrecoHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecoHora.Location = new System.Drawing.Point(-1, 68);
            this.lblPrecoHora.Name = "lblPrecoHora";
            this.lblPrecoHora.Size = new System.Drawing.Size(268, 39);
            this.lblPrecoHora.TabIndex = 13;
            this.lblPrecoHora.Text = "R$ 0,00";
            this.lblPrecoHora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrecoUnico
            // 
            this.lblPrecoUnico.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecoUnico.Location = new System.Drawing.Point(-1, 286);
            this.lblPrecoUnico.Name = "lblPrecoUnico";
            this.lblPrecoUnico.Size = new System.Drawing.Size(268, 39);
            this.lblPrecoUnico.TabIndex = 14;
            this.lblPrecoUnico.Text = "R$ 0,00";
            this.lblPrecoUnico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtValorUnico
            // 
            this.txtValorUnico.Enabled = false;
            this.txtValorUnico.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorUnico.Location = new System.Drawing.Point(27, 386);
            this.txtValorUnico.Name = "txtValorUnico";
            this.txtValorUnico.Size = new System.Drawing.Size(232, 44);
            this.txtValorUnico.TabIndex = 16;
            this.txtValorUnico.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorUnico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorUnico_KeyPress);
            this.txtValorUnico.Leave += new System.EventHandler(this.txtValorUnico_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 338);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(207, 31);
            this.label7.TabIndex = 15;
            this.label7.Text = "Valor Único(R$)";
            // 
            // lblPrecoMin
            // 
            this.lblPrecoMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecoMin.Location = new System.Drawing.Point(-1, 177);
            this.lblPrecoMin.Name = "lblPrecoMin";
            this.lblPrecoMin.Size = new System.Drawing.Size(268, 39);
            this.lblPrecoMin.TabIndex = 19;
            this.lblPrecoMin.Text = "R$ 0,00";
            this.lblPrecoMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(221, 29);
            this.label9.TabIndex = 18;
            this.label9.Text = "Preço Mínimo Atual";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.lblTolerancia);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.lblPrecoMin);
            this.panel6.Controls.Add(this.lblPrecoHora);
            this.panel6.Controls.Add(this.lblPrecoUnico);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(543, 98);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(271, 471);
            this.panel6.TabIndex = 20;
            // 
            // lblTolerancia
            // 
            this.lblTolerancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTolerancia.Location = new System.Drawing.Point(-1, 393);
            this.lblTolerancia.Name = "lblTolerancia";
            this.lblTolerancia.Size = new System.Drawing.Size(268, 39);
            this.lblTolerancia.TabIndex = 21;
            this.lblTolerancia.Text = "00 Minutos";
            this.lblTolerancia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(-2, 350);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(271, 29);
            this.label10.TabIndex = 20;
            this.label10.Text = "Tempo Tolerância Atual";
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Enabled = false;
            this.splitter3.Location = new System.Drawing.Point(540, 98);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 471);
            this.splitter3.TabIndex = 21;
            this.splitter3.TabStop = false;
            this.splitter3.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter3_SplitterMoved);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkBlue;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(530, 98);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 471);
            this.panel5.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(318, 355);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 29);
            this.label5.TabIndex = 20;
            this.label5.Text = "Modo Unico";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 447);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(300, 31);
            this.label6.TabIndex = 23;
            this.label6.Text = "Tempo Tolerância (min)";
            // 
            // tbTolerancia
            // 
            this.tbTolerancia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbTolerancia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbTolerancia.Enabled = false;
            this.tbTolerancia.Location = new System.Drawing.Point(27, 495);
            this.tbTolerancia.Maximum = 58;
            this.tbTolerancia.Name = "tbTolerancia";
            this.tbTolerancia.Size = new System.Drawing.Size(232, 45);
            this.tbTolerancia.SmallChange = 3;
            this.tbTolerancia.TabIndex = 24;
            this.tbTolerancia.TickFrequency = 5;
            this.tbTolerancia.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbTolerancia.Value = 1;
            this.tbTolerancia.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblCont
            // 
            this.lblCont.AutoSize = true;
            this.lblCont.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCont.Location = new System.Drawing.Point(262, 499);
            this.lblCont.Name = "lblCont";
            this.lblCont.Size = new System.Drawing.Size(29, 31);
            this.lblCont.TabIndex = 25;
            this.lblCont.Text = "0";
            // 
            // FrmTelaFinanceiro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(814, 667);
            this.Controls.Add(this.lblCont);
            this.Controls.Add(this.tbTolerancia);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.txtValorUnico);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCobrancaMinima);
            this.Controls.Add(this.txtPrecoHora);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ckValorUnico);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTelaFinanceiro";
            this.Text = "FrmTelaFinanceiro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTelaFinanceiro_FormClosing);
            this.Load += new System.EventHandler(this.FrmTelaFinanceiro_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTolerancia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckValorUnico;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrecoHora;
        private System.Windows.Forms.TextBox txtCobrancaMinima;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPrecoHora;
        private System.Windows.Forms.Label lblPrecoUnico;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox txtValorUnico;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPrecoMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar tbTolerancia;
        private System.Windows.Forms.Label lblCont;
        private System.Windows.Forms.Label lblTolerancia;
        private System.Windows.Forms.Label label10;
    }
}
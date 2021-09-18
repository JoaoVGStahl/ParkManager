
namespace Teste
{
    partial class FrmTelaConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTelaConfig));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGeral = new System.Windows.Forms.Button();
            this.btnDev = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnEstacionamento = new System.Windows.Forms.Button();
            this.btnPrecos = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnGeral);
            this.panel1.Controls.Add(this.btnDev);
            this.panel1.Controls.Add(this.btnUsuarios);
            this.panel1.Controls.Add(this.btnEstacionamento);
            this.panel1.Controls.Add(this.btnPrecos);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.lblUsuario);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 679);
            this.panel1.TabIndex = 0;
            // 
            // btnGeral
            // 
            this.btnGeral.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGeral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeral.FlatAppearance.BorderSize = 0;
            this.btnGeral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeral.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeral.Image = ((System.Drawing.Image)(resources.GetObject("btnGeral.Image")));
            this.btnGeral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGeral.Location = new System.Drawing.Point(0, 147);
            this.btnGeral.Name = "btnGeral";
            this.btnGeral.Size = new System.Drawing.Size(255, 77);
            this.btnGeral.TabIndex = 15;
            this.btnGeral.Text = "Geral";
            this.btnGeral.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGeral.UseVisualStyleBackColor = false;
            this.btnGeral.Click += new System.EventHandler(this.btnGeral_Click);
            // 
            // btnDev
            // 
            this.btnDev.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDev.FlatAppearance.BorderSize = 0;
            this.btnDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDev.Image = ((System.Drawing.Image)(resources.GetObject("btnDev.Image")));
            this.btnDev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDev.Location = new System.Drawing.Point(0, 455);
            this.btnDev.Name = "btnDev";
            this.btnDev.Size = new System.Drawing.Size(255, 77);
            this.btnDev.TabIndex = 19;
            this.btnDev.Text = "Opções do Desenvolvedor";
            this.btnDev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDev.UseVisualStyleBackColor = false;
            this.btnDev.Visible = false;
            this.btnDev.Click += new System.EventHandler(this.btnDev_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarios.Image = ((System.Drawing.Image)(resources.GetObject("btnUsuarios.Image")));
            this.btnUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 378);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(255, 77);
            this.btnUsuarios.TabIndex = 18;
            this.btnUsuarios.Text = "Usuários";
            this.btnUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUsuarios.UseVisualStyleBackColor = false;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnEstacionamento
            // 
            this.btnEstacionamento.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEstacionamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstacionamento.FlatAppearance.BorderSize = 0;
            this.btnEstacionamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstacionamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstacionamento.Image = ((System.Drawing.Image)(resources.GetObject("btnEstacionamento.Image")));
            this.btnEstacionamento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstacionamento.Location = new System.Drawing.Point(0, 224);
            this.btnEstacionamento.Name = "btnEstacionamento";
            this.btnEstacionamento.Size = new System.Drawing.Size(255, 77);
            this.btnEstacionamento.TabIndex = 17;
            this.btnEstacionamento.Text = "Estacionamento";
            this.btnEstacionamento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEstacionamento.UseVisualStyleBackColor = false;
            this.btnEstacionamento.Click += new System.EventHandler(this.btnEstacionamento_Click);
            // 
            // btnPrecos
            // 
            this.btnPrecos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrecos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrecos.FlatAppearance.BorderSize = 0;
            this.btnPrecos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrecos.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrecos.Image = ((System.Drawing.Image)(resources.GetObject("btnPrecos.Image")));
            this.btnPrecos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrecos.Location = new System.Drawing.Point(0, 301);
            this.btnPrecos.Name = "btnPrecos";
            this.btnPrecos.Size = new System.Drawing.Size(255, 77);
            this.btnPrecos.TabIndex = 16;
            this.btnPrecos.Text = "Financeiro";
            this.btnPrecos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrecos.UseVisualStyleBackColor = false;
            this.btnPrecos.Click += new System.EventHandler(this.btnPrecos_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.Transparent;
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.Location = new System.Drawing.Point(3, 565);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(246, 77);
            this.btnSair.TabIndex = 21;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(3, 89);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(249, 31);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuário";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(54, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(147, 105);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(252, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 679);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // FrmTelaConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 679);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "FrmTelaConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações - ParkManager";
            this.Load += new System.EventHandler(this.FrmTelaConfig_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnDev;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnEstacionamento;
        private System.Windows.Forms.Button btnPrecos;
        private System.Windows.Forms.Button btnGeral;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Splitter splitter1;
    }
}
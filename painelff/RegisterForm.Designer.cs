namespace painelff
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label statusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblUsuario = new Label();
            lblSenha = new Label();
            lblKey = new Label();
            txtUsuario = new TextBox();
            txtSenha = new TextBox();
            txtKey = new TextBox();
            btnRegistrar = new Button();
            statusLabel = new Label();
            SuspendLayout();
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(30, 20);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(47, 15);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuário";
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.Location = new Point(30, 70);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(39, 15);
            lblSenha.TabIndex = 2;
            lblSenha.Text = "Senha";
            // 
            // lblKey
            // 
            lblKey.AutoSize = true;
            lblKey.Location = new Point(30, 120);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(47, 15);
            lblKey.TabIndex = 4;
            lblKey.Text = "Licença";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(30, 40);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(200, 23);
            txtUsuario.TabIndex = 1;
            // 
            // txtSenha
            // 
            txtSenha.Location = new Point(30, 90);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '●';
            txtSenha.Size = new Size(200, 23);
            txtSenha.TabIndex = 3;
            // 
            // txtKey
            // 
            txtKey.Location = new Point(30, 140);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(200, 23);
            txtKey.TabIndex = 5;
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.DarkBlue;
            btnRegistrar.ForeColor = SystemColors.Control;
            btnRegistrar.Location = new Point(30, 180);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(200, 30);
            btnRegistrar.TabIndex = 6;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(30, 220);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 15);
            statusLabel.TabIndex = 7;
            // 
            // RegisterForm
            // 
            AcceptButton = btnRegistrar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(264, 261);
            Controls.Add(statusLabel);
            Controls.Add(btnRegistrar);
            Controls.Add(txtKey);
            Controls.Add(lblKey);
            Controls.Add(txtSenha);
            Controls.Add(lblSenha);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            ForeColor = SystemColors.ControlLightLight;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registrar Usuário";
            ResumeLayout(false);
            PerformLayout();
        }
    }
} 
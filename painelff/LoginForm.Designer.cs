namespace painelff
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.CheckBox rememberCheckBox;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Timer ProgressTimer;
        private System.Windows.Forms.ProgressBar guna2ProgressBar1;
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
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            lblUsuario = new Label();
            lblSenha = new Label();
            txtUsuario = new TextBox();
            txtSenha = new TextBox();
            btnEntrar = new Button();
            rememberCheckBox = new CheckBox();
            btnRegistrar = new Button();
            ProgressTimer = new System.Windows.Forms.Timer(components);
            guna2ProgressBar1 = new ProgressBar();
            statusLabel = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 150, 255);
            lblTitle.Location = new Point(60, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(243, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🔐 LOGIN PAINEL";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.Silver;
            lblUsuario.Location = new Point(40, 90);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(63, 20);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "Usuário";
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSenha.ForeColor = Color.Silver;
            lblSenha.Location = new Point(40, 155);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(51, 20);
            lblSenha.TabIndex = 3;
            lblSenha.Text = "Senha";
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.Location = new Point(40, 115);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(260, 27);
            txtUsuario.TabIndex = 2;
            // 
            // txtSenha
            // 
            txtSenha.Font = new Font("Segoe UI", 11F);
            txtSenha.Location = new Point(40, 180);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '●';
            txtSenha.Size = new Size(260, 27);
            txtSenha.TabIndex = 4;
            // 
            // btnEntrar
            // 
            btnEntrar.BackColor = Color.Navy;
            btnEntrar.FlatAppearance.BorderSize = 0;
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEntrar.ForeColor = Color.White;
            btnEntrar.Location = new Point(40, 250);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(260, 35);
            btnEntrar.TabIndex = 6;
            btnEntrar.Text = "Entrar";
            btnEntrar.UseVisualStyleBackColor = false;
            // 
            // rememberCheckBox
            // 
            rememberCheckBox.AutoSize = true;
            rememberCheckBox.Font = new Font("Segoe UI", 9F);
            rememberCheckBox.ForeColor = Color.Silver;
            rememberCheckBox.Location = new Point(40, 215);
            rememberCheckBox.Name = "rememberCheckBox";
            rememberCheckBox.Size = new Size(100, 19);
            rememberCheckBox.TabIndex = 5;
            rememberCheckBox.Text = "Lembrar login";
            rememberCheckBox.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.FromArgb(255, 150, 0);
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(40, 295);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(260, 35);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Registrar-se";
            btnRegistrar.UseVisualStyleBackColor = false;
            // 
            // guna2ProgressBar1
            // 
            guna2ProgressBar1.Location = new Point(40, 345);
            guna2ProgressBar1.Name = "guna2ProgressBar1";
            guna2ProgressBar1.Size = new Size(260, 10);
            guna2ProgressBar1.TabIndex = 8;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            statusLabel.ForeColor = Color.FromArgb(0, 200, 100);
            statusLabel.Location = new Point(40, 360);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 15);
            statusLabel.TabIndex = 9;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(340, 400);
            Controls.Add(lblTitle);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblSenha);
            Controls.Add(txtSenha);
            Controls.Add(rememberCheckBox);
            Controls.Add(btnEntrar);
            Controls.Add(btnRegistrar);
            Controls.Add(guna2ProgressBar1);
            Controls.Add(statusLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - Painel";
            ResumeLayout(false);
            PerformLayout();
        }
    }
} 
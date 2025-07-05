namespace painelff
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnActive = new Button();
            btnToggleAimbot = new Button();
            btnStatus = new Button();
            btnNoRecoil = new Button();
            btnVisionHack = new Button();
            btnWallHack = new Button();
            lblTitle = new Label();
            lblInfo = new Label();
            SuspendLayout();
            // 
            // btnActive
            // 
            btnActive.BackColor = Color.FromArgb(64, 64, 64);
            btnActive.FlatStyle = FlatStyle.Flat;
            btnActive.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnActive.ForeColor = Color.White;
            btnActive.Location = new Point(50, 120);
            btnActive.Name = "btnActive";
            btnActive.Size = new Size(200, 50);
            btnActive.TabIndex = 0;
            btnActive.Text = "Ativar Aimbot";
            btnActive.UseVisualStyleBackColor = false;
            btnActive.Click += btnActive_Click;
            // 
            // btnToggleAimbot
            // 
            btnToggleAimbot.BackColor = Color.FromArgb(45, 45, 45);
            btnToggleAimbot.Enabled = false;
            btnToggleAimbot.FlatStyle = FlatStyle.Flat;
            btnToggleAimbot.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnToggleAimbot.ForeColor = Color.White;
            btnToggleAimbot.Location = new Point(50, 180);
            btnToggleAimbot.Name = "btnToggleAimbot";
            btnToggleAimbot.Size = new Size(200, 40);
            btnToggleAimbot.TabIndex = 1;
            btnToggleAimbot.Text = "Aplicar Novamente";
            btnToggleAimbot.UseVisualStyleBackColor = false;
            btnToggleAimbot.Click += btnToggleAimbot_Click;
            // 
            // btnStatus
            // 
            btnStatus.BackColor = Color.FromArgb(45, 45, 45);
            btnStatus.FlatStyle = FlatStyle.Flat;
            btnStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnStatus.ForeColor = Color.White;
            btnStatus.Location = new Point(50, 230);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(200, 35);
            btnStatus.TabIndex = 2;
            btnStatus.Text = "Status do Sistema";
            btnStatus.UseVisualStyleBackColor = false;
            btnStatus.Click += btnStatus_Click;
            // 
            // btnNoRecoil
            // 
            btnNoRecoil.BackColor = Color.FromArgb(45, 45, 45);
            btnNoRecoil.FlatStyle = FlatStyle.Flat;
            btnNoRecoil.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnNoRecoil.ForeColor = Color.White;
            btnNoRecoil.Location = new Point(50, 275);
            btnNoRecoil.Name = "btnNoRecoil";
            btnNoRecoil.Size = new Size(200, 40);
            btnNoRecoil.TabIndex = 5;
            btnNoRecoil.Text = "Ativar No Recoil";
            btnNoRecoil.UseVisualStyleBackColor = false;
            btnNoRecoil.Click += btnNoRecoil_Click;
            // 
            // btnVisionHack
            // 
            btnVisionHack.BackColor = Color.FromArgb(45, 45, 45);
            btnVisionHack.FlatStyle = FlatStyle.Flat;
            btnVisionHack.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnVisionHack.ForeColor = Color.White;
            btnVisionHack.Location = new Point(50, 325);
            btnVisionHack.Name = "btnVisionHack";
            btnVisionHack.Size = new Size(200, 40);
            btnVisionHack.TabIndex = 6;
            btnVisionHack.Text = "Ativar Vision Hack";
            btnVisionHack.UseVisualStyleBackColor = false;
            btnVisionHack.Click += btnVisionHack_Click;
            // 
            // btnWallHack
            // 
            btnWallHack.BackColor = Color.FromArgb(45, 45, 45);
            btnWallHack.FlatStyle = FlatStyle.Flat;
            btnWallHack.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnWallHack.ForeColor = Color.White;
            btnWallHack.Location = new Point(50, 375);
            btnWallHack.Name = "btnWallHack";
            btnWallHack.Size = new Size(200, 40);
            btnWallHack.TabIndex = 7;
            btnWallHack.Text = "Ativar Wall Hack";
            btnWallHack.UseVisualStyleBackColor = false;
            btnWallHack.Click += btnWallHack_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(50, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 30);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "Free Fire Aimbot";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblInfo.ForeColor = Color.Silver;
            lblInfo.Location = new Point(50, 60);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(200, 45);
            lblInfo.TabIndex = 4;
            lblInfo.Text = "Aimbot otimizado para Free Fire\nno BlueStacks 4\nVersão x64";
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(300, 400);
            Controls.Add(lblInfo);
            Controls.Add(lblTitle);
            Controls.Add(btnStatus);
            Controls.Add(btnToggleAimbot);
            Controls.Add(btnActive);
            Controls.Add(btnNoRecoil);
            Controls.Add(btnVisionHack);
            Controls.Add(btnWallHack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Free Fire Aimbot v2.0";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnActive;
        private Button btnToggleAimbot;
        private Button btnStatus;
        private Button btnNoRecoil;
        private Button btnVisionHack;
        private Button btnWallHack;
        private Label lblTitle;
        private Label lblInfo;
    }
}

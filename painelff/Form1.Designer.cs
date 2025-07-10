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
            panelHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            panelMain = new Panel();
            panelStatus = new Panel();
            lblStatusInfo = new Label();
            btnStatus = new Button();
            lblStatusTitle = new Label();
            panelHacks = new Panel();
            btnWallHack = new Button();
            btnVisionHack = new Button();
            btnNoRecoil = new Button();
            lblHacksTitle = new Label();
            panelAimbot = new Panel();
            btnToggleAimbot = new Button();
            btnActive = new Button();
            lblAimbotTitle = new Label();
            panelFooter = new Panel();
            lblVersion = new Label();
            panelHeader.SuspendLayout();
            panelMain.SuspendLayout();
            panelStatus.SuspendLayout();
            panelHacks.SuspendLayout();
            panelAimbot.SuspendLayout();
            panelFooter.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.Black;
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(400, 100);
            panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.Silver;
            lblSubtitle.Location = new Point(82, 55);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(219, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "⚡ Otimizado - Ultra Performance";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 150, 255);
            lblTitle.Location = new Point(68, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(233, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🎯 DANZIN XITS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Click += lblTitle_Click;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(30, 30, 40);
            panelMain.Controls.Add(panelStatus);
            panelMain.Controls.Add(panelHacks);
            panelMain.Controls.Add(panelAimbot);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 100);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(400, 500);
            panelMain.TabIndex = 1;
            // 
            // panelStatus
            // 
            panelStatus.BackColor = Color.Black;
            panelStatus.BorderStyle = BorderStyle.FixedSingle;
            panelStatus.Controls.Add(lblStatusInfo);
            panelStatus.Controls.Add(btnStatus);
            panelStatus.Controls.Add(lblStatusTitle);
            panelStatus.Location = new Point(20, 380);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(360, 100);
            panelStatus.TabIndex = 2;
            // 
            // lblStatusInfo
            // 
            lblStatusInfo.AutoSize = true;
            lblStatusInfo.Font = new Font("Segoe UI", 8F);
            lblStatusInfo.ForeColor = Color.Silver;
            lblStatusInfo.Location = new Point(15, 80);
            lblStatusInfo.Name = "lblStatusInfo";
            lblStatusInfo.Size = new Size(265, 13);
            lblStatusInfo.TabIndex = 2;
            lblStatusInfo.Text = "Clique para verificar o status de todos os módulos";
            // 
            // btnStatus
            // 
            btnStatus.BackColor = Color.FromArgb(100, 100, 110);
            btnStatus.FlatAppearance.BorderSize = 0;
            btnStatus.FlatStyle = FlatStyle.Flat;
            btnStatus.Font = new Font("Segoe UI", 9F);
            btnStatus.ForeColor = Color.White;
            btnStatus.Location = new Point(15, 45);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(330, 30);
            btnStatus.TabIndex = 1;
            btnStatus.Text = "🔍 VERIFICAR STATUS DO SISTEMA";
            btnStatus.UseVisualStyleBackColor = false;
            btnStatus.Click += btnStatus_Click;
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.AutoSize = true;
            lblStatusTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblStatusTitle.ForeColor = Color.FromArgb(150, 150, 255);
            lblStatusTitle.Location = new Point(15, 15);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(94, 21);
            lblStatusTitle.TabIndex = 0;
            lblStatusTitle.Text = "📊 STATUS";
            // 
            // panelHacks
            // 
            panelHacks.BackColor = Color.Black;
            panelHacks.BorderStyle = BorderStyle.FixedSingle;
            panelHacks.Controls.Add(btnWallHack);
            panelHacks.Controls.Add(btnVisionHack);
            panelHacks.Controls.Add(btnNoRecoil);
            panelHacks.Controls.Add(lblHacksTitle);
            panelHacks.Location = new Point(20, 160);
            panelHacks.Name = "panelHacks";
            panelHacks.Size = new Size(360, 200);
            panelHacks.TabIndex = 1;
            // 
            // btnWallHack
            // 
            btnWallHack.BackColor = Color.FromArgb(80, 80, 90);
            btnWallHack.FlatAppearance.BorderSize = 0;
            btnWallHack.FlatStyle = FlatStyle.Flat;
            btnWallHack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnWallHack.ForeColor = Color.White;
            btnWallHack.Location = new Point(15, 125);
            btnWallHack.Name = "btnWallHack";
            btnWallHack.Size = new Size(330, 35);
            btnWallHack.TabIndex = 3;
            btnWallHack.Text = "\U0001f9f1 WALL HACK";
            btnWallHack.UseVisualStyleBackColor = false;
            btnWallHack.Click += btnWallHack_Click;
            // 
            // btnVisionHack
            // 
            btnVisionHack.BackColor = Color.FromArgb(80, 80, 90);
            btnVisionHack.FlatAppearance.BorderSize = 0;
            btnVisionHack.FlatStyle = FlatStyle.Flat;
            btnVisionHack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnVisionHack.ForeColor = Color.White;
            btnVisionHack.Location = new Point(15, 85);
            btnVisionHack.Name = "btnVisionHack";
            btnVisionHack.Size = new Size(330, 35);
            btnVisionHack.TabIndex = 2;
            btnVisionHack.Text = "👁️ VISION HACK";
            btnVisionHack.UseVisualStyleBackColor = false;
            btnVisionHack.Click += btnVisionHack_Click;
            // 
            // btnNoRecoil
            // 
            btnNoRecoil.BackColor = Color.FromArgb(80, 80, 90);
            btnNoRecoil.FlatAppearance.BorderSize = 0;
            btnNoRecoil.FlatStyle = FlatStyle.Flat;
            btnNoRecoil.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNoRecoil.ForeColor = Color.White;
            btnNoRecoil.Location = new Point(15, 45);
            btnNoRecoil.Name = "btnNoRecoil";
            btnNoRecoil.Size = new Size(330, 35);
            btnNoRecoil.TabIndex = 1;
            btnNoRecoil.Text = "🎯 NO RECOIL";
            btnNoRecoil.UseVisualStyleBackColor = false;
            btnNoRecoil.Click += btnNoRecoil_Click;
            // 
            // lblHacksTitle
            // 
            lblHacksTitle.AutoSize = true;
            lblHacksTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHacksTitle.ForeColor = Color.FromArgb(255, 150, 0);
            lblHacksTitle.Location = new Point(15, 15);
            lblHacksTitle.Name = "lblHacksTitle";
            lblHacksTitle.Size = new Size(125, 21);
            lblHacksTitle.TabIndex = 0;
            lblHacksTitle.Text = "⚡ HACKS PRO";
            // 
            // panelAimbot
            // 
            panelAimbot.BackColor = Color.Black;
            panelAimbot.BorderStyle = BorderStyle.FixedSingle;
            panelAimbot.Controls.Add(btnToggleAimbot);
            panelAimbot.Controls.Add(btnActive);
            panelAimbot.Controls.Add(lblAimbotTitle);
            panelAimbot.Location = new Point(20, 20);
            panelAimbot.Name = "panelAimbot";
            panelAimbot.Size = new Size(360, 120);
            panelAimbot.TabIndex = 0;
            // 
            // btnToggleAimbot
            // 
            btnToggleAimbot.BackColor = Color.FromArgb(60, 60, 70);
            btnToggleAimbot.Enabled = false;
            btnToggleAimbot.FlatAppearance.BorderSize = 0;
            btnToggleAimbot.FlatStyle = FlatStyle.Flat;
            btnToggleAimbot.Font = new Font("Segoe UI", 9F);
            btnToggleAimbot.ForeColor = Color.Silver;
            btnToggleAimbot.Location = new Point(185, 45);
            btnToggleAimbot.Name = "btnToggleAimbot";
            btnToggleAimbot.Size = new Size(160, 40);
            btnToggleAimbot.TabIndex = 2;
            btnToggleAimbot.Text = "🔄 APLICAR NOVAMENTE";
            btnToggleAimbot.UseVisualStyleBackColor = false;
            btnToggleAimbot.Click += btnToggleAimbot_Click;
            // 
            // btnActive
            // 
            btnActive.BackColor = Color.FromArgb(0, 120, 215);
            btnActive.FlatAppearance.BorderSize = 0;
            btnActive.FlatStyle = FlatStyle.Flat;
            btnActive.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnActive.ForeColor = Color.White;
            btnActive.Location = new Point(15, 45);
            btnActive.Name = "btnActive";
            btnActive.Size = new Size(160, 40);
            btnActive.TabIndex = 1;
            btnActive.Text = "🚀 ATIVAR AIMBOT";
            btnActive.UseVisualStyleBackColor = false;
            btnActive.Click += btnActive_Click;
            // 
            // lblAimbotTitle
            // 
            lblAimbotTitle.AutoSize = true;
            lblAimbotTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAimbotTitle.ForeColor = Color.FromArgb(0, 200, 100);
            lblAimbotTitle.Location = new Point(15, 15);
            lblAimbotTitle.Name = "lblAimbotTitle";
            lblAimbotTitle.Size = new Size(163, 21);
            lblAimbotTitle.TabIndex = 0;
            lblAimbotTitle.Text = "🎯 AIMBOT SYSTEM";
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.FromArgb(20, 20, 30);
            panelFooter.Controls.Add(lblVersion);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(0, 600);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(400, 30);
            panelFooter.TabIndex = 2;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.ForeColor = Color.Gray;
            lblVersion.Location = new Point(47, 8);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(306, 13);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "© 2024 Danzin XITS Pro • Versão 1.0 • Desenvolvido com ❤️";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(400, 630);
            Controls.Add(panelMain);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "🎯 Danzin XITS v1.0";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelMain.ResumeLayout(false);
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            panelHacks.ResumeLayout(false);
            panelHacks.PerformLayout();
            panelAimbot.ResumeLayout(false);
            panelAimbot.PerformLayout();
            panelFooter.ResumeLayout(false);
            panelFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelMain;
        private Panel panelAimbot;
        private Label lblAimbotTitle;
        private Button btnActive;
        private Button btnToggleAimbot;
        private Panel panelHacks;
        private Label lblHacksTitle;
        private Button btnNoRecoil;
        private Button btnVisionHack;
        private Button btnWallHack;
        private Panel panelStatus;
        private Label lblStatusTitle;
        private Button btnStatus;
        private Label lblStatusInfo;
        private Panel panelFooter;
        private Label lblVersion;
    }
}
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
            btnAntiCheat = new Button();
            lblStatusTitle = new Label();
            panelAimbot = new Panel();
            lblAimbotTitle = new Label();
            btnAimbotAtualizado = new Button();
            panelFooter = new Panel();
            lblVersion = new Label();
            panelSidebar = new Panel();
            btnConfig = new Button();
            btnHacksPro = new Button();
            btnVoltarSidebar = new Button();
            panelConfigOptions = new Panel();
            btnDestruct = new Button();
            btnLimparLogs = new Button();
            btnFechar = new Button();
            panelHeader.SuspendLayout();
            panelMain.SuspendLayout();
            panelStatus.SuspendLayout();
            panelAimbot.SuspendLayout();
            panelFooter.SuspendLayout();
            panelSidebar.SuspendLayout();
            panelConfigOptions.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.Black;
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(60, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(340, 100);
            panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.Silver;
            lblSubtitle.Location = new Point(63, 54);
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
            lblTitle.Location = new Point(52, 17);
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
            panelMain.Controls.Add(panelAimbot);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(60, 100);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(340, 540);
            panelMain.TabIndex = 1;
            // 
            // panelStatus
            // 
            panelStatus.BackColor = Color.Black;
            panelStatus.BorderStyle = BorderStyle.FixedSingle;
            panelStatus.Controls.Add(lblStatusInfo);
            panelStatus.Controls.Add(btnStatus);
            panelStatus.Controls.Add(btnAntiCheat);
            panelStatus.Controls.Add(lblStatusTitle);
            panelStatus.Location = new Point(22, 316);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(295, 196);
            panelStatus.TabIndex = 2;
            // 
            // lblStatusInfo
            // 
            lblStatusInfo.AutoSize = true;
            lblStatusInfo.Font = new Font("Segoe UI", 8F);
            lblStatusInfo.ForeColor = Color.Silver;
            lblStatusInfo.Location = new Point(13, 82);
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
            btnStatus.Location = new Point(32, 42);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(230, 28);
            btnStatus.TabIndex = 1;
            btnStatus.Text = "🔍 VERIFICAR STATUS DO SISTEMA";
            btnStatus.UseVisualStyleBackColor = false;
            btnStatus.Click += btnStatus_Click;
            // 
            // btnAntiCheat
            // 
            btnAntiCheat.BackColor = Color.FromArgb(80, 80, 120);
            btnAntiCheat.FlatAppearance.BorderSize = 0;
            btnAntiCheat.FlatStyle = FlatStyle.Flat;
            btnAntiCheat.Font = new Font("Segoe UI", 9F);
            btnAntiCheat.ForeColor = Color.White;
            btnAntiCheat.Location = new Point(32, 108);
            btnAntiCheat.Name = "btnAntiCheat";
            btnAntiCheat.Size = new Size(230, 50);
            btnAntiCheat.TabIndex = 2;
            btnAntiCheat.Text = "🛡️ GERENCIADOR ANTI-CHEAT";
            btnAntiCheat.UseVisualStyleBackColor = false;
            btnAntiCheat.Click += BtnAntiCheat_Click;
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.AutoSize = true;
            lblStatusTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblStatusTitle.ForeColor = Color.FromArgb(150, 150, 255);
            lblStatusTitle.Location = new Point(103, 18);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(94, 21);
            lblStatusTitle.TabIndex = 0;
            lblStatusTitle.Text = "📊 STATUS";
            // 
            // panelAimbot
            // 
            panelAimbot.BackColor = Color.Black;
            panelAimbot.BorderStyle = BorderStyle.FixedSingle;
            panelAimbot.Controls.Add(lblAimbotTitle);
            panelAimbot.Controls.Add(btnAimbotAtualizado);
            panelAimbot.Location = new Point(26, 6);
            panelAimbot.Name = "panelAimbot";
            panelAimbot.Size = new Size(275, 120);
            panelAimbot.TabIndex = 0;
            // 
            // lblAimbotTitle
            // 
            lblAimbotTitle.AutoSize = true;
            lblAimbotTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAimbotTitle.ForeColor = Color.FromArgb(0, 200, 100);
            lblAimbotTitle.Location = new Point(51, 13);
            lblAimbotTitle.Name = "lblAimbotTitle";
            lblAimbotTitle.Size = new Size(163, 21);
            lblAimbotTitle.TabIndex = 0;
            lblAimbotTitle.Text = "🎯 AIMBOT SYSTEM";
            // 
            // btnAimbotAtualizado
            // 
            btnAimbotAtualizado.BackColor = Color.FromArgb(120, 60, 120);
            btnAimbotAtualizado.FlatAppearance.BorderSize = 0;
            btnAimbotAtualizado.FlatStyle = FlatStyle.Flat;
            btnAimbotAtualizado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAimbotAtualizado.ForeColor = Color.White;
            btnAimbotAtualizado.Location = new Point(41, 47);
            btnAimbotAtualizado.Name = "btnAimbotAtualizado";
            btnAimbotAtualizado.Size = new Size(189, 38);
            btnAimbotAtualizado.TabIndex = 5;
            btnAimbotAtualizado.Text = "🔄 AIMBOT ATUALIZADO";
            btnAimbotAtualizado.UseVisualStyleBackColor = false;
            btnAimbotAtualizado.Click += btnAimbotAtualizado_Click;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.FromArgb(20, 20, 30);
            panelFooter.Controls.Add(lblVersion);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(60, 640);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(340, 30);
            panelFooter.TabIndex = 2;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.ForeColor = Color.Gray;
            lblVersion.Location = new Point(11, 8);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(306, 13);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "© 2024 Danzin XITS Pro • Versão 1.0 • Desenvolvido com ❤️";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(20, 20, 30);
            panelSidebar.Controls.Add(btnConfig);
            panelSidebar.Controls.Add(btnHacksPro);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(60, 670);
            panelSidebar.TabIndex = 100;
            // 
            // btnConfig
            // 
            btnConfig.BackColor = Color.FromArgb(30, 30, 40);
            btnConfig.Cursor = Cursors.Hand;
            btnConfig.FlatAppearance.BorderSize = 0;
            btnConfig.FlatStyle = FlatStyle.Flat;
            btnConfig.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnConfig.ForeColor = Color.White;
            btnConfig.Location = new Point(12, 66);
            btnConfig.Name = "btnConfig";
            btnConfig.Size = new Size(36, 36);
            btnConfig.TabIndex = 101;
            btnConfig.Text = "⚙";
            btnConfig.UseVisualStyleBackColor = false;
            // 
            // btnHacksPro
            // 
            btnHacksPro.BackColor = Color.FromArgb(30, 30, 40);
            btnHacksPro.Cursor = Cursors.Hand;
            btnHacksPro.FlatAppearance.BorderSize = 0;
            btnHacksPro.FlatStyle = FlatStyle.Flat;
            btnHacksPro.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnHacksPro.ForeColor = Color.Orange;
            btnHacksPro.Location = new Point(12, 20);
            btnHacksPro.Name = "btnHacksPro";
            btnHacksPro.Size = new Size(36, 36);
            btnHacksPro.TabIndex = 102;
            btnHacksPro.Text = "⚡";
            btnHacksPro.UseVisualStyleBackColor = false;
            // 
            // btnVoltarSidebar
            // 
            btnVoltarSidebar.Location = new Point(0, 0);
            btnVoltarSidebar.Name = "btnVoltarSidebar";
            btnVoltarSidebar.Size = new Size(75, 23);
            btnVoltarSidebar.TabIndex = 0;
            // 
            // panelConfigOptions
            // 
            panelConfigOptions.BackColor = Color.FromArgb(35, 35, 45);
            panelConfigOptions.BorderStyle = BorderStyle.FixedSingle;
            panelConfigOptions.Controls.Add(btnDestruct);
            panelConfigOptions.Controls.Add(btnLimparLogs);
            panelConfigOptions.Controls.Add(btnFechar);
            panelConfigOptions.Location = new Point(284, 261);
            panelConfigOptions.Name = "panelConfigOptions";
            panelConfigOptions.Size = new Size(260, 200);
            panelConfigOptions.TabIndex = 101;
            panelConfigOptions.Visible = false;
            // 
            // btnDestruct
            // 
            btnDestruct.BackColor = Color.FromArgb(200, 50, 50);
            btnDestruct.FlatAppearance.BorderSize = 0;
            btnDestruct.FlatStyle = FlatStyle.Flat;
            btnDestruct.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDestruct.ForeColor = Color.White;
            btnDestruct.Location = new Point(20, 20);
            btnDestruct.Name = "btnDestruct";
            btnDestruct.Size = new Size(220, 40);
            btnDestruct.TabIndex = 0;
            btnDestruct.Text = "Destruct Painel";
            btnDestruct.UseVisualStyleBackColor = false;
            // 
            // btnLimparLogs
            // 
            btnLimparLogs.BackColor = Color.FromArgb(80, 80, 90);
            btnLimparLogs.FlatAppearance.BorderSize = 0;
            btnLimparLogs.FlatStyle = FlatStyle.Flat;
            btnLimparLogs.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLimparLogs.ForeColor = Color.White;
            btnLimparLogs.Location = new Point(20, 70);
            btnLimparLogs.Name = "btnLimparLogs";
            btnLimparLogs.Size = new Size(220, 40);
            btnLimparLogs.TabIndex = 1;
            btnLimparLogs.Text = "Limpar Logs";
            btnLimparLogs.UseVisualStyleBackColor = false;
            // 
            // btnFechar
            // 
            btnFechar.BackColor = Color.FromArgb(60, 60, 70);
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.FlatStyle = FlatStyle.Flat;
            btnFechar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFechar.ForeColor = Color.White;
            btnFechar.Location = new Point(20, 120);
            btnFechar.Name = "btnFechar";
            btnFechar.Size = new Size(220, 40);
            btnFechar.TabIndex = 2;
            btnFechar.Text = "Fechar";
            btnFechar.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(400, 670);
            Controls.Add(panelMain);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);
            Controls.Add(panelSidebar);
            Controls.Add(panelConfigOptions);
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
            panelAimbot.ResumeLayout(false);
            panelAimbot.PerformLayout();
            panelFooter.ResumeLayout(false);
            panelFooter.PerformLayout();
            panelSidebar.ResumeLayout(false);
            panelConfigOptions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelMain;
        private Panel panelAimbot;
        private Label lblAimbotTitle;


        private Panel panelStatus;
        private Label lblStatusTitle;
        private Button btnStatus;
        private Label lblStatusInfo;
        private Panel panelFooter;
        private Label lblVersion;
        private Panel panelSidebar;
        private Button btnConfig;
        private Panel panelConfigOptions;
        private Button btnDestruct;
        private Button btnLimparLogs;
        private Button btnFechar;
        private Button btnHacksPro;


        private Button btnAimbotAtualizado;
        private Button btnAntiCheat;
    }
}
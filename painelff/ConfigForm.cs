using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace painelff
{
    public class ConfigForm : Form
    {
        public Button btnDestruct;
        public Button btnLimparLogs;
        public Button btnFechar;

        public ConfigForm()
        {
            this.Text = "Configurações";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(320, 280);
            this.BackColor = Color.FromArgb(35, 35, 45);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            btnDestruct = new Button();
            btnDestruct.Text = "Destruct Painel";
            btnDestruct.Size = new Size(240, 45);
            btnDestruct.Location = new Point(40, 30);
            btnDestruct.BackColor = Color.FromArgb(200, 50, 50);
            btnDestruct.ForeColor = Color.White;
            btnDestruct.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDestruct.FlatStyle = FlatStyle.Flat;
            btnDestruct.FlatAppearance.BorderSize = 0;

            btnLimparLogs = new Button();
            btnLimparLogs.Text = "Limpar Logs";
            btnLimparLogs.Size = new Size(240, 45);
            btnLimparLogs.Location = new Point(40, 90);
            btnLimparLogs.BackColor = Color.FromArgb(80, 80, 90);
            btnLimparLogs.ForeColor = Color.White;
            btnLimparLogs.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLimparLogs.FlatStyle = FlatStyle.Flat;
            btnLimparLogs.FlatAppearance.BorderSize = 0;

            btnFechar = new Button();
            btnFechar.Text = "Fechar";
            btnFechar.Size = new Size(240, 45);
            btnFechar.Location = new Point(40, 150);
            btnFechar.BackColor = Color.FromArgb(60, 60, 70);
            btnFechar.ForeColor = Color.White;
            btnFechar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFechar.FlatStyle = FlatStyle.Flat;
            btnFechar.FlatAppearance.BorderSize = 0;

            btnDestruct.Click += (s, e) => {
                try
                {
                    // Apagar arquivo de configuração
                    if (File.Exists("login_config.json"))
                        File.Delete("login_config.json");
                    // Apagar pasta de logs local
                    if (Directory.Exists("Logs"))
                        Directory.Delete("Logs", true);
                    // Apagar pasta KeyAuth no AppData
                    string appDataKeyAuth = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeyAuth");
                    if (Directory.Exists(appDataKeyAuth))
                        Directory.Delete(appDataKeyAuth, true);
                    // Apagar outros rastros temporários
                    // (adicione aqui outros arquivos/pastas se necessário)
                    MessageBox.Show("Todos os arquivos do painel foram removidos!", "Destruição Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao destruir arquivos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Application.Exit();
            };
            btnLimparLogs.Click += (s, e) => {
                try
                {
                    // Apagar pasta de logs local
                    if (Directory.Exists("Logs"))
                        Directory.Delete("Logs", true);
                    // Apagar pasta KeyAuth no AppData
                    string appDataKeyAuth = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeyAuth");
                    if (Directory.Exists(appDataKeyAuth))
                        Directory.Delete(appDataKeyAuth, true);
                    MessageBox.Show("Todos os logs e rastros temporários foram removidos!", "Limpeza Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao limpar logs: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            btnFechar.Click += (s, e) => { this.Close(); };

            this.Controls.Add(btnDestruct);
            this.Controls.Add(btnLimparLogs);
            this.Controls.Add(btnFechar);
        }
    }
} 
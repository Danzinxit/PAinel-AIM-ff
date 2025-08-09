using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace painelff
{
    public partial class AntiCheatForm : Form
    {
        private ListView listViewProtecoes;
        private Button btnAplicarTodas;
        private Button btnAplicarSelecionadas;
        private Button btnVerificarStatus;
        private Button btnFechar;
        private Label lblStatus;
        private ProgressBar progressBar;
        private System.Windows.Forms.Timer updateTimer;

        public AntiCheatForm()
        {
            InitializeComponent();
            SetupEventHandlers();
            LoadProtecoes();
            StartUpdateTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Sistema Anti-Cheat - Gerenciador de Proteções";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(30, 30, 40);
            this.ForeColor = Color.White;

            // Label de status
            lblStatus = new Label
            {
                Text = "Status: Inicializando...",
                Location = new Point(20, 20),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Orange
            };

            // Progress bar
            progressBar = new ProgressBar
            {
                Location = new Point(20, 50),
                Size = new Size(400, 20),
                Style = ProgressBarStyle.Continuous
            };

            // ListView para proteções
            listViewProtecoes = new ListView
            {
                Location = new Point(20, 90),
                Size = new Size(500, 400),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.FromArgb(40, 40, 50),
                ForeColor = Color.White
            };

            listViewProtecoes.Columns.Add("Proteção", 120);
            listViewProtecoes.Columns.Add("Status", 80);
            listViewProtecoes.Columns.Add("Última Verificação", 150);
            listViewProtecoes.Columns.Add("Detalhes", 150);

            // Botões
            btnAplicarTodas = new Button
            {
                Text = "Aplicar Todas",
                Location = new Point(540, 90),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(0, 120, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            btnAplicarSelecionadas = new Button
            {
                Text = "Aplicar Selecionadas",
                Location = new Point(540, 135),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(0, 100, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            btnVerificarStatus = new Button
            {
                Text = "Verificar Status",
                Location = new Point(540, 180),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(120, 120, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            btnFechar = new Button
            {
                Text = "Fechar",
                Location = new Point(540, 450),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(120, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            // Adicionar controles
            this.Controls.AddRange(new Control[]
            {
                lblStatus,
                progressBar,
                listViewProtecoes,
                btnAplicarTodas,
                btnAplicarSelecionadas,
                btnVerificarStatus,
                btnFechar
            });
        }

        private void SetupEventHandlers()
        {
            btnAplicarTodas.Click += BtnAplicarTodas_Click;
            btnAplicarSelecionadas.Click += BtnAplicarSelecionadas_Click;
            btnVerificarStatus.Click += BtnVerificarStatus_Click;
            btnFechar.Click += BtnFechar_Click;
        }

        private void LoadProtecoes()
        {
            try
            {
                listViewProtecoes.Items.Clear();
                var status = AntiCheat.GetProtectionStatus();

                foreach (var protection in status)
                {
                    var item = new ListViewItem(protection.Key);
                    item.SubItems.Add(protection.Value ? "Ativa" : "Inativa");
                    item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
                    item.SubItems.Add(protection.Value ? "Protegido" : "Não protegido");

                    if (protection.Value)
                    {
                        item.BackColor = Color.FromArgb(0, 80, 0);
                    }
                    else
                    {
                        item.BackColor = Color.FromArgb(80, 0, 0);
                    }

                    listViewProtecoes.Items.Add(item);
                }

                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar proteções: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusLabel()
        {
            try
            {
                var status = AntiCheat.GetProtectionStatus();
                var ativas = status.Count(s => s.Value);
                var total = status.Count;

                lblStatus.Text = $"Status: {ativas}/{total} proteções ativas";
                progressBar.Value = total > 0 ? (int)((double)ativas / total * 100) : 0;

                if (ativas == total && total > 0)
                {
                    lblStatus.ForeColor = Color.Lime;
                }
                else if (ativas > total / 2)
                {
                    lblStatus.ForeColor = Color.Yellow;
                }
                else
                {
                    lblStatus.ForeColor = Color.Red;
                }
            }
            catch
            {
                lblStatus.Text = "Status: Erro ao verificar";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private async void BtnAplicarTodas_Click(object sender, EventArgs e)
        {
            try
            {
                btnAplicarTodas.Enabled = false;
                btnAplicarTodas.Text = "Aplicando...";

                await AntiCheat.ApplyAllProtectionsAsync();
                LoadProtecoes();

                MessageBox.Show("Todas as proteções foram aplicadas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar proteções: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAplicarTodas.Enabled = true;
                btnAplicarTodas.Text = "Aplicar Todas";
            }
        }

        private async void BtnAplicarSelecionadas_Click(object sender, EventArgs e)
        {
            if (listViewProtecoes.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos uma proteção para aplicar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnAplicarSelecionadas.Enabled = false;
                btnAplicarSelecionadas.Text = "Aplicando...";

                int aplicadas = 0;
                foreach (ListViewItem item in listViewProtecoes.SelectedItems)
                {
                    var protectionName = item.Text;
                    var success = await AntiCheat.ApplyProtection(protectionName);
                    if (success) aplicadas++;
                }

                LoadProtecoes();
                MessageBox.Show($"{aplicadas} proteções foram aplicadas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar proteções selecionadas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAplicarSelecionadas.Enabled = true;
                btnAplicarSelecionadas.Text = "Aplicar Selecionadas";
            }
        }

        private void BtnVerificarStatus_Click(object sender, EventArgs e)
        {
            LoadProtecoes();
            MessageBox.Show("Status das proteções foi atualizado!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartUpdateTimer()
        {
            updateTimer = new System.Windows.Forms.Timer
            {
                Interval = 10000 // 10 segundos
            };
            updateTimer.Tick += (s, e) => UpdateStatusLabel();
            updateTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            updateTimer?.Stop();
            updateTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}

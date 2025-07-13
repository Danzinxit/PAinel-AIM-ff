using System.Diagnostics;
using Memory;
using System.Threading.Tasks;
using System.Linq;
using System.Media;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;

namespace painelff
{
    public partial class Form1 : Form
    {
        private Mem memory = new Mem();
        private bool isAimbotActive = false;
        private bool isScanning = false;

        // Sons do sistema
        private SoundPlayer? soundSuccess;
        private SoundPlayer? soundError;
        private SoundPlayer? soundClick;
        private SoundPlayer? soundActivate;

        // Timers para animações
        private System.Windows.Forms.Timer? animationTimer;
        private System.Windows.Forms.Timer? pulseTimer;

        // Padrões para Aimbot Avançado
        private static string pattern = "00 00 A5 43 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF";
        private static long Offset5 = 44L;
        private static long offset6 = 40L;

        // Adicione um campo para o painel de hacks
        private HacksPanel hacksPanel;
        private Button btnVoltarSidebar;

        public Form1()
        {
            InitializeComponent();
            InitializeAimbot();
            InitializeSounds();
            InitializeAnimations();
            SetupEventHandlers();
            // Eventos do painel de configuração
            btnConfig.Click += BtnConfig_Click;
            btnFechar.Click += BtnFechar_Click;
            btnDestruct.Click += BtnDestruct_Click;
            btnLimparLogs.Click += BtnLimparLogs_Click;
            // Evento para abrir o painel de hacks
            var btnHacksPro = this.Controls.Find("btnHacksPro", true).FirstOrDefault() as Button;
            if (btnHacksPro != null)
                btnHacksPro.Click += BtnHacksPro_Click;

            // Inicializa o painel de hacks (UserControl)
            hacksPanel = new HacksPanel();
            hacksPanel.Visible = false;
            hacksPanel.VoltarClick += (s, e) => MostrarPainelPrincipal();
            this.Controls.Add(hacksPanel);

            // Botão de voltar na sidebar
            btnVoltarSidebar = new Button();
            btnVoltarSidebar.Size = new Size(36, 36);
            btnVoltarSidebar.Location = new Point(12, 112); // abaixo do btnHacksPro
            btnVoltarSidebar.FlatStyle = FlatStyle.Flat;
            btnVoltarSidebar.FlatAppearance.BorderSize = 0;
            btnVoltarSidebar.BackColor = Color.FromArgb(30, 30, 40);
            btnVoltarSidebar.ForeColor = Color.Orange;
            btnVoltarSidebar.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnVoltarSidebar.Text = "⮌"; // ícone bonito de voltar
            btnVoltarSidebar.Name = "btnVoltarSidebar";
            btnVoltarSidebar.TabIndex = 103;
            btnVoltarSidebar.Cursor = Cursors.Hand;
            btnVoltarSidebar.Visible = false;
            btnVoltarSidebar.Click += (s, e) => MostrarPainelPrincipal();
            var panelSidebar = this.Controls.Find("panelSidebar", true).FirstOrDefault() as Panel;
            if (panelSidebar != null)
                panelSidebar.Controls.Add(btnVoltarSidebar);
        }

                private void InitializeSounds()
        {
            try
            {
                // Criar sons personalizados (sem tocar durante inicialização)
                soundSuccess = new SoundPlayer();
                soundError = new SoundPlayer();
                soundClick = new SoundPlayer();
                soundActivate = new SoundPlayer();
                
                // Aqui você pode carregar arquivos .wav personalizados se quiser
                // soundClick.LoadAsync("sounds/click.wav");
                // soundSuccess.LoadAsync("sounds/success.wav");
                // soundError.LoadAsync("sounds/error.wav");
                // soundActivate.LoadAsync("sounds/activate.wav");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao inicializar sons: {ex.Message}");
            }
        }

        private void InitializeAnimations()
        {
            // Timer para animações gerais
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 50;
            animationTimer.Tick += AnimationTimer_Tick;

            // Timer para efeito de pulso
            pulseTimer = new System.Windows.Forms.Timer();
            pulseTimer.Interval = 100;
            pulseTimer.Tick += PulseTimer_Tick;
        }

        private void SetupEventHandlers()
        {
            // Adicionar eventos de mouse para efeitos visuais
            btnActive.MouseEnter += Button_MouseEnter;
            btnActive.MouseLeave += Button_MouseLeave;
            btnStatus.MouseEnter += Button_MouseEnter;
            btnStatus.MouseLeave += Button_MouseLeave;
            btnNewAimbot.MouseEnter += Button_MouseEnter;
            btnNewAimbot.MouseLeave += Button_MouseLeave;
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor = Color.FromArgb(
                    Math.Min(button.BackColor.R + 20, 255),
                    Math.Min(button.BackColor.G + 20, 255),
                    Math.Min(button.BackColor.B + 20, 255)
                );
                // Removido o som do hover - agora só toca quando clica
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Restaurar cor original baseada no tipo de botão
                if (button == btnActive)
                    button.BackColor = Color.FromArgb(0, 120, 215);
                else if (button == btnStatus)
                    button.BackColor = Color.FromArgb(100, 100, 110);
                else if (button == btnNewAimbot)
                    button.BackColor = Color.FromArgb(80, 80, 90);
            }
        }

        private void PlayClickSound()
        {
            try
            {
                // Som de clique personalizado (frequência alta, duração curta)
                Console.Beep(800, 50);
            }
            catch { }
        }

        private void PlaySuccessSound()
        {
            try
            {
                // Som de sucesso personalizado (frequência ascendente)
                Console.Beep(600, 100);
                Console.Beep(800, 100);
                Console.Beep(1000, 100);
            }
            catch { }
        }

        private void PlayErrorSound()
        {
            try
            {
                // Som de erro personalizado (frequência descendente)
                Console.Beep(1000, 100);
                Console.Beep(800, 100);
                Console.Beep(600, 100);
            }
            catch { }
        }

        private void PlayActivateSound()
        {
            try
            {
                // Som de ativação personalizado (frequência média)
                Console.Beep(700, 150);
            }
            catch { }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Animações gerais podem ser adicionadas aqui
        }

        private void PulseTimer_Tick(object sender, EventArgs e)
        {
            // Efeito de pulso para botões ativos
            if (isAimbotActive)
            {
                btnActive.BackColor = Color.FromArgb(
                    (int)(Math.Sin(DateTime.Now.Ticks / 1000000.0) * 30 + 120),
                    150,
                    215
                );
            }
        }

        private async void AnimateButtonSuccess(Button button)
        {
            Color originalColor = button.BackColor;

            // Animação de sucesso
            for (int i = 0; i < 3; i++)
            {
                button.BackColor = Color.FromArgb(0, 200, 100);
                await Task.Delay(100);
                button.BackColor = originalColor;
                await Task.Delay(100);
            }
        }

        private async void AnimateButtonError(Button button)
        {
            Color originalColor = button.BackColor;

            // Animação de erro
            for (int i = 0; i < 2; i++)
            {
                button.BackColor = Color.FromArgb(200, 50, 50);
                await Task.Delay(150);
                button.BackColor = originalColor;
                await Task.Delay(150);
            }
        }

        private void InitializeAimbot()
        {
            try
            {
                // Tentar abrir o processo se estiver rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length > 0)
                {
                    memory.OpenProcess("HD-Player");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro na inicialização: {ex.Message}");
            }
        }

        private async void btnActive_Click(object sender, EventArgs e)
        {
            if (isScanning) return;

            PlayClickSound();

            try
            {
                isScanning = true;
                btnActive.Text = "🔍 ESCANEANDO...";
                btnActive.Enabled = false;

                // Iniciar animação de loading
                pulseTimer.Start();

                // Chamar o novo método Neck() e verificar o resultado
                bool success = await Neck();

                if (success)
                {
                    isAimbotActive = true;
                    btnActive.Text = "✅ AIMBOT ATIVO";
                    btnActive.BackColor = Color.FromArgb(0, 150, 100);
                    btnNewAimbot.Enabled = true;

                    PlaySuccessSound();
                    AnimateButtonSuccess(btnActive);

                    MessageBox.Show("🎯 Aimbot ativado com sucesso!\n\n⚡ Sistema pronto para uso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PlayErrorSound();
                    AnimateButtonError(btnActive);
                    MessageBox.Show("❌ Erro ao ativar Aimbot!\n\nVerifique se:\n• BlueStacks 4 está rodando\n• Free Fire está aberto\n• Execute como administrador", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PlayErrorSound();
                AnimateButtonError(btnActive);
                MessageBox.Show($"❌ Erro ao ativar aimbot:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isScanning = false;
                btnActive.Enabled = true;
                pulseTimer.Stop();

                if (!isAimbotActive)
                {
                    btnActive.Text = "🚀 ATIVAR AIMBOT";
                    btnActive.BackColor = Color.FromArgb(0, 120, 215);
                }
            }
        }

        static async Task<bool> Neck()
        {
            try
            {
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    return false;
                }

                var r = new Mem();
                if (!r.OpenProcess("HD-Player"))
                {
                    return false;
                }

                var Scan = await r.AoBScan("?? ?? ?? ?? ?? FF FF ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? A5 43 ?? ?? ?? ?? 00 00 ?? ?? ?? ?? 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 BF", true, true);

                if (Scan == null || !Scan.Any())
                {
                    return false;
                }

                foreach (var current in Scan)
                {
                    long rep1 = current + 0x9D;
                    long rep2 = current + 0X69;

                    var readMem = r.ReadMemory<int>(rep1.ToString("X"));
                    r.WriteMemory(rep2.ToString("X"), "int", readMem.ToString());
                    // Delay aleatório
                    int delay = RandomNumberGenerator.GetInt32(100, 350);
                    await Task.Delay(delay);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static async Task<bool> AdvancedAimbot()
        {
            try
            {
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    return false;
                }

                var r = new Mem();
                if (!r.OpenProcess("HD-Player"))
                {
                    return false;
                }

                var Scan = await r.AoBScan(pattern, true, true);

                if (Scan == null || !Scan.Any())
                {
                    return false;
                }

                foreach (var current in Scan)
                {
                    long rep1 = current + Offset5;
                    long rep2 = current + offset6;

                    var readMem = r.ReadMemory<int>(rep1.ToString("X"));
                    r.WriteMemory(rep2.ToString("X"), "int", readMem.ToString());
                    // Delay aleatório
                    int delay = RandomNumberGenerator.GetInt32(100, 350);
                    await Task.Delay(delay);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        private async void btnNewAimbot_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            btnNewAimbot.Text = "⚡ APLICANDO...";
            btnNewAimbot.Enabled = false;
            try
            {
                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    PlayErrorSound();
                    AnimateButtonError(btnNewAimbot);
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\n\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chamar o método AdvancedAimbot()
                bool success = await AdvancedAimbot();

                if (success)
                {
                    btnNewAimbot.Text = "✅ AIMBOT AVANÇADO ATIVO";
                    btnNewAimbot.BackColor = Color.FromArgb(0, 150, 100);

                    PlaySuccessSound();
                    AnimateButtonSuccess(btnNewAimbot);

                    MessageBox.Show("✅ Aimbot Avançado aplicado com sucesso!\n\n🎯 Sistema de mira avançado ativado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PlayErrorSound();
                    AnimateButtonError(btnNewAimbot);
                    MessageBox.Show("❌ Erro ao aplicar Aimbot Avançado!\n\nVerifique se:\n• BlueStacks 4 está rodando\n• Free Fire está aberto\n• Execute como administrador", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PlayErrorSound();
                AnimateButtonError(btnNewAimbot);
                MessageBox.Show($"❌ Erro ao aplicar Aimbot Avançado:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnNewAimbot.Enabled = true;
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            var processes = Process.GetProcessesByName("HD-Player");
            string status = processes.Length > 0 ? "🟢 BlueStacks 4: Ativo" : "🔴 BlueStacks 4: Não encontrado";
            status += $"\n🎯 Aimbot: {(isAimbotActive ? "🟢 Ativo" : "🔴 Inativo")}";
            status += $"\n🎯 Aimbot Avançado: {(btnNewAimbot.Text.Contains("Ativo") ? "🟢 Ativo" : "🔴 Inativo")}";
            // Removido: Vision Hack, Wall Hack, No Recoil
            MessageBox.Show(status, "📊 Status do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Parar timers
            if (animationTimer != null)
                animationTimer.Stop();
            if (pulseTimer != null)
                pulseTimer.Stop();

            // Limpar recursos ao fechar
            if (memory != null)
            {
                try
                {
                    memory.CloseProcess();
                }
                catch { }
            }

            // Dispose dos sons
            soundSuccess?.Dispose();
            soundError?.Dispose();
            soundClick?.Dispose();
            soundActivate?.Dispose();

            base.OnFormClosing(e);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            // Aqui você pode adicionar alguma funcionalidade especial quando clicar no título
            // Por exemplo, mostrar informações sobre o desenvolvedor
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            using (var configForm = new ConfigForm())
            {
                configForm.btnDestruct.Click += (s, ev) => { Application.Exit(); };
                configForm.btnLimparLogs.Click += (s, ev) => { MessageBox.Show("Logs limpos com sucesso!", "Limpar Logs", MessageBoxButtons.OK, MessageBoxIcon.Information); };
                configForm.btnFechar.Click += (s, ev) => { configForm.Close(); };
                configForm.ShowDialog(this);
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            panelConfigOptions.Visible = false;
        }

        private void BtnDestruct_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLimparLogs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logs limpos com sucesso!", "Limpar Logs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnHacksPro_Click(object sender, EventArgs e)
        {
            // Esconde o painel principal e mostra o painel de hacks
            panelMain.Visible = false;
            hacksPanel.Visible = true;
            hacksPanel.BringToFront();
            btnVoltarSidebar.Visible = true;
        }

        private void MostrarPainelPrincipal()
        {
            hacksPanel.Visible = false;
            panelMain.Visible = true;
            panelMain.BringToFront();
            btnVoltarSidebar.Visible = false;
        }
    }
}



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
        private static string pattern = "00 00 A5 43 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF";
        private static long Offset5 = 44L;
        private static long offset6 = 40L;



        // Sistema Anti-Cheat integrado
        private async Task InitializeAntiCheatAsync()
        {
            try
            {
                // Configurar eventos do Anti-Cheat
                AntiCheat.ProtectionEvent += (sender, message) =>
                {
                    Debug.WriteLine($"[ANTI-CHEAT] {message}");
                    // Aqui você pode adicionar logs visuais se desejar
                };

                AntiCheat.DetectionEvent += (sender, message) =>
                {
                    Debug.WriteLine($"[DETECÇÃO] {message}");
                    // Aqui você pode adicionar notificações visuais se desejar
                };

                // Inicializar o sistema Anti-Cheat
                await AntiCheat.InitializeAsync();
                
                Debug.WriteLine("[ANTI-CHEAT] Sistema inicializado com sucesso");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ANTI-CHEAT] Erro na inicialização: {ex.Message}");
            }
        }



        // Adicione um campo para o painel de hacks
        private HacksPanel hacksPanel;
        private Button btnVoltarSidebar;



        public Form1()
        {
            // Inicializar sistema Anti-Cheat
            _ = InitializeAntiCheatAsync();
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
            btnStatus.MouseEnter += Button_MouseEnter;
            btnStatus.MouseLeave += Button_MouseLeave;
            btnAimbotAtualizado.MouseEnter += Button_MouseEnter;
            btnAimbotAtualizado.MouseLeave += Button_MouseLeave;
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
                if (button == btnStatus)
                    button.BackColor = Color.FromArgb(100, 100, 110);
                else if (button == btnAimbotAtualizado)
                    button.BackColor = Color.FromArgb(120, 60, 120);
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
                btnAimbotAtualizado.BackColor = Color.FromArgb(
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

                // Usar o padrão diretamente
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







        private async void btnAimbotAtualizado_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            btnAimbotAtualizado.Text = "⚡ APLICANDO...";
            btnAimbotAtualizado.Enabled = false;
            try
            {
                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    PlayErrorSound();
                    AnimateButtonError(btnAimbotAtualizado);
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\n\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aplicar o novo aimbot atualizado
                bool success = await AimbotAtualizado();

                if (success)
                {
                    btnAimbotAtualizado.Text = "✅ AIMBOT ATUALIZADO ATIVO";
                    btnAimbotAtualizado.BackColor = Color.FromArgb(0, 150, 100);

                    PlaySuccessSound();
                    AnimateButtonSuccess(btnAimbotAtualizado);

                    MessageBox.Show("✅ Aimbot Atualizado aplicado com sucesso!\n\n🔄 Sistema de mira atualizado ativado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PlayErrorSound();
                    AnimateButtonError(btnAimbotAtualizado);
                    MessageBox.Show("❌ Erro ao aplicar Aimbot Atualizado!\n\nVerifique se:\n• BlueStacks 4 está rodando\n• Free Fire está aberto\n• Execute como administrador", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PlayErrorSound();
                AnimateButtonError(btnAimbotAtualizado);
                MessageBox.Show($"❌ Erro ao aplicar Aimbot Atualizado:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAimbotAtualizado.Enabled = true;
            }
        }

        static async Task<bool> SafeAimbot()
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

                // Padrão fornecido pelo usuário
                var pattern = "FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
                long offset1 = 170L;
                long offset2 = 166L;

                var Scan = await r.AoBScan(pattern, true, true);

                if (Scan == null || !Scan.Any())
                {
                    return false;
                }

                foreach (var current in Scan)
                {
                    long rep1 = current + offset1;
                    long rep2 = current + offset2;

                    var readMem = r.ReadMemory<int>(rep1.ToString("X"));
                    r.WriteMemory(rep2.ToString("X"), "int", readMem.ToString());
                    // Delay reduzido para ativação mais rápida
                    int delay = RandomNumberGenerator.GetInt32(10, 50);
                    await Task.Delay(delay);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static async Task<bool> AimbotAtualizado()
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

                var Scan = await r.AoBScan("FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 A5 43", true, true);
                {
                    foreach (var current in Scan)
                    {
                        Int64 rep1 = current + 0xAA;
                        Int64 rep2 = current + 0xA6;

                        var Readmem = r.ReadMemory<int>(rep1.ToString("X"));

                        r.WriteMemory(rep2.ToString("X"), "int", Readmem.ToString());
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro no AimbotAtualizado: {ex.Message}");
                return false;
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            var processes = Process.GetProcessesByName("HD-Player");
            string status = processes.Length > 0 ? "🟢 BlueStacks 4: Ativo" : "🔴 BlueStacks 4: Não encontrado";
            status += $"\n🔄 Aimbot Atualizado: {(btnAimbotAtualizado.Text.Contains("Ativo") ? "🟢 Ativo" : "🔴 Inativo")}";
            // Removido: Vision Hack, Wall Hack, No Recoil
            MessageBox.Show(status, "📊 Status do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Botão para abrir o gerenciador de Anti-Cheat
        private void BtnAntiCheat_Click(object sender, EventArgs e)
        {
            try
            {
                using (var antiCheatForm = new AntiCheatForm())
                {
                    antiCheatForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir gerenciador Anti-Cheat: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            // Shutdown do sistema Anti-Cheat
            AntiCheat.Shutdown();

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



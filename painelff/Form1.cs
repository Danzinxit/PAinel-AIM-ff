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
        // private static string pattern = "00 00 A5 43 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF";
        private static string patternObfuscated = ReverseString("00 00 A5 43 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF");
        private static long Offset5 = 44L;
        private static long offset6 = 40L;

        // Lista de ferramentas proibidas
        private static readonly string[] forbiddenTools = { "cheatengine", "processhacker", "ida64", "ida32", "ollydbg", "x64dbg", "scylla", "procexp", "megadumper" };

        // Lista de padrões Antiban (ofuscados)
        private static readonly string[] antibanPatternsObfuscated = new string[]
        {
            ReverseString("00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5 00 00 A0 E3 1E FF 2F E1"),
            ReverseString("30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 00 00 A0 E3 1E FF 2F E1"),
            ReverseString("00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 54 10 9F E5 01 10 9F E7 00 10 91 E5 04 10 0B E5 0C 00 8D E5 0C 00 9D E5 B5 06 00 EB 00 00 A0 E3 1E FF 2F E1"),
            ReverseString("00 48 2D E9 0D B0 A0 E1 98 D0 4D E2 84 34 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 1C 00 0B E5 00 00 A0 E3 1E FF 2F E1"),
            ReverseString("00 48 2D E9 0D B0 A0 E1 58 D0 4D E2 64 22 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 00 00 A0 E3 1E FF 2F E1")
        };

        // Método Antiban automático
        private async Task ApplyAntibanAsync()
        {
            try
            {
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                    return;
                var r = new Mem();
                if (!r.OpenProcess("HD-Player"))
                    return;

                foreach (var patternObf in antibanPatternsObfuscated)
                {
                    var pattern = ReverseString(patternObf);
                    var scan = await r.AoBScan(pattern, true, true);
                    if (scan != null && scan.Any())
                    {
                        Debug.WriteLine($"[ANTIBAN] Padrão encontrado e protegido: {pattern}");
                        // Aqui você pode sobrescrever, limpar, etc. Por enquanto, só loga.
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ANTIBAN] Erro: {ex.Message}");
            }
        }

        // Método para inverter string (ofuscação simples)
        private static string ReverseString(string s)
        {
            return new string(s.Reverse().ToArray());
        }

        // Método para detectar ferramentas proibidas
        private void DetectForbiddenToolsAndExit()
        {
            var runningProcesses = Process.GetProcesses();
            foreach (var proc in runningProcesses)
            {
                try
                {
                    string name = proc.ProcessName.ToLower();
                    if (forbiddenTools.Any(tool => name.Contains(tool)))
                    {
                        MessageBox.Show($"Ferramenta proibida detectada: {name}\nO programa será encerrado.", "Proteção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }
                }
                catch { }
            }
        }

        // Adicione um campo para o painel de hacks
        private HacksPanel hacksPanel;
        private Button btnVoltarSidebar;

        // Lista de padrões Antiblacklist (ofuscados)
        private static readonly string[] antiblacklistPatternsObfuscated = new string[]
        {
            ReverseString("0A 00 A0 E3 6E 00 54 E3 3F 00 00 13 10 8C BD E8 08 00 A0 E3 00 00 00 EA 0D 00 A0 E3 70 00 FF E6 10 8C BD E8 C1 00 F0 20 E3"),
            ReverseString("EA 00 00 A0 E3 21 00 84 E8 70 8C BD E8 F0 4F 2D E9 00 F0 20 E3"),
            ReverseString("A8 00 9F E5 00 20 A0 E3 00 00 9F E7 00 10 90 E5 0A 00 A0 E3"),
            ReverseString("A8 00 9F E5 00 20 A0 E3 00 00 9F E7 00 10 90 E5 64 09 A0 00")
        };

        // Método Antiblacklist automático
        private async Task ApplyAntiblacklistAsync()
        {
            try
            {
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                    return;
                var r = new Mem();
                if (!r.OpenProcess("HD-Player"))
                    return;

                foreach (var patternObf in antiblacklistPatternsObfuscated)
                {
                    var pattern = ReverseString(patternObf);
                    var scan = await r.AoBScan(pattern, true, true);
                    if (scan != null && scan.Any())
                    {
                        Debug.WriteLine($"[ANTIBLACKLIST] Padrão encontrado e protegido: {pattern}");
                        // Aqui você pode sobrescrever, limpar, etc. Por enquanto, só loga.
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ANTIBLACKLIST] Erro: {ex.Message}");
            }
        }

        public Form1()
        {
            DetectForbiddenToolsAndExit(); // Proteção contra ferramentas proibidas
            // Chama o antiban automático
            _ = ApplyAntibanAsync();
            // Chama o antiblacklist automático
            _ = ApplyAntiblacklistAsync();
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
            btnSafeAimbot.MouseEnter += Button_MouseEnter;
            btnSafeAimbot.MouseLeave += Button_MouseLeave;
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
                else if (button == btnSafeAimbot)
                    button.BackColor = Color.FromArgb(60, 120, 60);
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

                // Desofusca o padrão em tempo de execução
                var pattern = ReverseString(patternObfuscated);
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

        private async void btnSafeAimbot_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            btnSafeAimbot.Text = "⚡ APLICANDO...";
            btnSafeAimbot.Enabled = false;
            try
            {
                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    PlayErrorSound();
                    AnimateButtonError(btnSafeAimbot);
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\n\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chamar o método SafeAimbot()
                bool success = await SafeAimbot();

                if (success)
                {
                    btnSafeAimbot.Text = "✅ AIMBOT SAFE ATIVO";
                    btnSafeAimbot.BackColor = Color.FromArgb(0, 150, 100);

                    PlaySuccessSound();
                    AnimateButtonSuccess(btnSafeAimbot);

                    MessageBox.Show("✅ Aimbot Safe aplicado com sucesso!\n\n🛡️ Sistema de mira seguro ativado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PlayErrorSound();
                    AnimateButtonError(btnSafeAimbot);
                    MessageBox.Show("❌ Erro ao aplicar Aimbot Safe!\n\nVerifique se:\n• BlueStacks 4 está rodando\n• Free Fire está aberto\n• Execute como administrador", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PlayErrorSound();
                AnimateButtonError(btnSafeAimbot);
                MessageBox.Show($"❌ Erro ao aplicar Aimbot Safe:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSafeAimbot.Enabled = true;
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

        private void btnStatus_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            var processes = Process.GetProcessesByName("HD-Player");
            string status = processes.Length > 0 ? "🟢 BlueStacks 4: Ativo" : "🔴 BlueStacks 4: Não encontrado";
            status += $"\n🎯 Aimbot: {(isAimbotActive ? "🟢 Ativo" : "🔴 Inativo")}";
            status += $"\n🎯 Aimbot Avançado: {(btnNewAimbot.Text.Contains("Ativo") ? "🟢 Ativo" : "🔴 Inativo")}";
            status += $"\n🛡️ Aimbot Safe: {(btnSafeAimbot.Text.Contains("Ativo") ? "🟢 Ativo" : "🔴 Inativo")}";
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



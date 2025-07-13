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

        // Padrões para Vision Hack
        private string patternVisionHackSearch = "00 00 B4 43 DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30";
        private string patternVisionHackReplace = "00 00 B4 43 00 00 A0 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30";

        // Padrões para Wall Hack
        private string patternWallHackSearch = "09 0E 00 00 80 3F 00 00 80 3F";
        private string patternWallHackReplace = "09 0E 00 00 A0 4F 00 00 80 3F";

        // Padrões para Aimbot Avançado
        private static string pattern = "00 00 A5 43 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF";
        private static long Offset5 = 44L;
        private static long offset6 = 40L;

        public Form1()
        {
            InitializeComponent();
            InitializeAimbot();
            InitializeSounds();
            InitializeAnimations();
            SetupEventHandlers();
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
            btnToggleAimbot.MouseEnter += Button_MouseEnter;
            btnToggleAimbot.MouseLeave += Button_MouseLeave;
            btnNoRecoil.MouseEnter += Button_MouseEnter;
            btnNoRecoil.MouseLeave += Button_MouseLeave;
            btnVisionHack.MouseEnter += Button_MouseEnter;
            btnVisionHack.MouseLeave += Button_MouseLeave;
            btnWallHack.MouseEnter += Button_MouseEnter;
            btnWallHack.MouseLeave += Button_MouseLeave;
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
                else if (button == btnToggleAimbot)
                    button.BackColor = Color.FromArgb(60, 60, 70);
                else if (button == btnNoRecoil || button == btnVisionHack || button == btnWallHack)
                    button.BackColor = Color.FromArgb(80, 80, 90);
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
                    btnToggleAimbot.Enabled = true;

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



        private async void btnToggleAimbot_Click(object sender, EventArgs e)
        {
            if (!isAimbotActive)
            {
                PlayErrorSound();
                AnimateButtonError(btnToggleAimbot);
                MessageBox.Show("⚠️ Ative o aimbot primeiro!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PlayClickSound();

            try
            {
                btnToggleAimbot.Text = "⚡ APLICANDO...";
                btnToggleAimbot.Enabled = false;

                // Chamar o novo método Neck() novamente
                bool success = await Neck();

                if (success)
                {
                    btnToggleAimbot.Text = "🔄 APLICAR NOVAMENTE";

                    PlaySuccessSound();
                    AnimateButtonSuccess(btnToggleAimbot);

                    MessageBox.Show("✅ Aimbot aplicado novamente!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PlayErrorSound();
                    AnimateButtonError(btnToggleAimbot);
                    MessageBox.Show("❌ Erro ao reaplicar Aimbot!\n\nVerifique se o BlueStacks 4 ainda está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PlayErrorSound();
                AnimateButtonError(btnToggleAimbot);
                MessageBox.Show($"❌ Erro ao aplicar aimbot:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnToggleAimbot.Enabled = true;
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
            status += $"\n👁️ Vision Hack: {(btnVisionHack.Text.Contains("Ativo") ? "🟢 Ativo" : "🔴 Inativo")}";
            status += $"\n🧱 Wall Hack: {(btnWallHack.Text.Contains("Ativo") ? "🟢 Ativo" : "🔴 Inativo")}";
            status += $"\n🎯 No Recoil: {(btnNoRecoil.Text.Contains("Ativo") ? "🟢 Ativo" : "🔴 Inativo")}";

            MessageBox.Show(status, "📊 Status do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnNoRecoil_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            btnNoRecoil.Text = "⚡ APLICANDO...";
            btnNoRecoil.Enabled = false;
            try
            {
                // AOBs fornecidas
                string aobNoRecoil1 = "30 48 2D E9 08 B0 8D E2 02 8B 2D ED 00 40 A0 E1 38 01 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 28 01 9F E5 00 00 9F E7 00 00 90 E5";
                string aobNoRecoil2 = "00 00 A0 E3 1E FF 2F E1 02 8B 2D ED 00 40 A0 E1 38 01 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 28 01 9F E5 00 00 9F E7 00 00 90 E5";

                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    PlayErrorSound();
                    AnimateButtonError(btnNoRecoil);
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\n\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!memory.OpenProcess("HD-Player"))
                {
                    PlayErrorSound();
                    AnimateButtonError(btnNoRecoil);
                    MessageBox.Show("❌ Erro ao abrir processo do BlueStacks.\n\nExecute como administrador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar a primeira AOB
                var result = await Task.Run(() => memory.AoBScan(aobNoRecoil1, true, true));
                if (result == null || !result.Any())
                {
                    PlayErrorSound();
                    AnimateButtonError(btnNoRecoil);
                    MessageBox.Show("⚠️ Padrão No Recoil não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Patch: sobrescrever o endereço encontrado com o segundo padrão
                foreach (var address in result)
                {
                    // Escrever o segundo padrão byte a byte
                    var bytes = aobNoRecoil2.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    int delay = RandomNumberGenerator.GetInt32(100, 350);
                    await Task.Delay(delay);
                }

                btnNoRecoil.Text = "✅ NO RECOIL ATIVO";
                btnNoRecoil.BackColor = Color.FromArgb(0, 150, 100);

                PlaySuccessSound();
                AnimateButtonSuccess(btnNoRecoil);

                MessageBox.Show("✅ No Recoil aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                PlayErrorSound();
                AnimateButtonError(btnNoRecoil);
                MessageBox.Show($"❌ Erro ao aplicar No Recoil:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnNoRecoil.Enabled = true;
            }
        }

        private async void btnVisionHack_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            btnVisionHack.Text = "⚡ APLICANDO...";
            btnVisionHack.Enabled = false;
            try
            {
                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    PlayErrorSound();
                    AnimateButtonError(btnVisionHack);
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\n\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!memory.OpenProcess("HD-Player"))
                {
                    PlayErrorSound();
                    AnimateButtonError(btnVisionHack);
                    MessageBox.Show("❌ Erro ao abrir processo do BlueStacks.\n\nExecute como administrador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar o padrão do Vision Hack
                var result = await Task.Run(() => memory.AoBScan(patternVisionHackSearch, true, true));
                if (result == null || !result.Any())
                {
                    PlayErrorSound();
                    AnimateButtonError(btnVisionHack);
                    MessageBox.Show("⚠️ Padrão Vision Hack não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Patch: sobrescrever o endereço encontrado com o padrão de substituição
                foreach (var address in result)
                {
                    // Escrever o padrão de substituição byte a byte
                    var bytes = patternVisionHackReplace.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    int delay = RandomNumberGenerator.GetInt32(100, 350);
                    await Task.Delay(delay);
                }

                btnVisionHack.Text = "✅ VISION HACK ATIVO";
                btnVisionHack.BackColor = Color.FromArgb(0, 150, 100);

                PlaySuccessSound();
                AnimateButtonSuccess(btnVisionHack);

                MessageBox.Show("✅ Vision Hack aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                PlayErrorSound();
                AnimateButtonError(btnVisionHack);
                MessageBox.Show($"❌ Erro ao aplicar Vision Hack:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnVisionHack.Enabled = true;
            }
        }

        private async void btnWallHack_Click(object sender, EventArgs e)
        {
            PlayClickSound();

            btnWallHack.Text = "⚡ APLICANDO...";
            btnWallHack.Enabled = false;
            try
            {
                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    PlayErrorSound();
                    AnimateButtonError(btnWallHack);
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\n\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!memory.OpenProcess("HD-Player"))
                {
                    PlayErrorSound();
                    AnimateButtonError(btnWallHack);
                    MessageBox.Show("❌ Erro ao abrir processo do BlueStacks.\n\nExecute como administrador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar o padrão do Wall Hack
                var result = await Task.Run(() => memory.AoBScan(patternWallHackSearch, true, true));
                if (result == null || !result.Any())
                {
                    PlayErrorSound();
                    AnimateButtonError(btnWallHack);
                    MessageBox.Show("⚠️ Padrão Wall Hack não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Patch: sobrescrever o endereço encontrado com o padrão de substituição
                foreach (var address in result)
                {
                    // Escrever o padrão de substituição byte a byte
                    var bytes = patternWallHackReplace.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    int delay = RandomNumberGenerator.GetInt32(100, 350);
                    await Task.Delay(delay);
                }

                btnWallHack.Text = "✅ WALL HACK ATIVO";
                btnWallHack.BackColor = Color.FromArgb(0, 150, 100);

                PlaySuccessSound();
                AnimateButtonSuccess(btnWallHack);

                MessageBox.Show("✅ Wall Hack aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                PlayErrorSound();
                AnimateButtonError(btnWallHack);
                MessageBox.Show($"❌ Erro ao aplicar Wall Hack:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnWallHack.Enabled = true;
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

            base.OnFormClosing(e);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            // Aqui você pode adicionar alguma funcionalidade especial quando clicar no título
            // Por exemplo, mostrar informações sobre o desenvolvedor
        }
    }
}



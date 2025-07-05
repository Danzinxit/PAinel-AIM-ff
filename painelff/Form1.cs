using System.Diagnostics;
using Memory;
using System.Threading.Tasks;

namespace painelff
{
    public partial class Form1 : Form
    {
        private Mem memory = new Mem();
        private bool isAimbotActive = false;
        private bool isScanning = false;
        private List<long> aimbotAddresses = new List<long>();
        
        // Offsets para diferentes partes do corpo
        private readonly Dictionary<string, int> bodyOffsets = new Dictionary<string, int>
        {
            {"Neck", 0x6D},           // 109 decimal
            {"NeckLeft", 0x9D},       // 157 decimal  
            {"NeckRight", 0x99},      // 153 decimal
            {"LeftShoulder", 0xA9},   // 169 decimal
            {"RightShoulder", 0xAD}   // 173 decimal
        };

        // Valor de escrita para todas as partes (105 decimal = 0x69)
        private const int WRITE_VALUE = 0x69;

        // Novo padrão e offsets para o aimbot
        private string patternAimbot = "00 00 A5 43 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF";
        private long offset5 = 44L;
        private long offset6 = 40L;

        // Padrões para Vision Hack
        private string patternVisionHackSearch = "00 00 B4 43 DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30";
        private string patternVisionHackReplace = "00 00 B4 43 00 00 A0 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30";

        // Padrões para Wall Hack
        private string patternWallHackSearch = "09 0E 00 00 80 3F 00 00 80 3F";
        private string patternWallHackReplace = "09 0E 00 00 A0 4F 00 00 80 3F";

        public Form1()
        {
            InitializeComponent();
            InitializeAimbot();
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

            try
            {
                isScanning = true;
                btnActive.Text = "Escaneando...";
                btnActive.Enabled = false;

                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    MessageBox.Show("BlueStacks 4 não encontrado! Certifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Abrir processo se não estiver aberto
                if (!memory.OpenProcess("HD-Player"))
                {
                    MessageBox.Show("Erro ao abrir processo do BlueStacks. Execute como administrador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Usar o novo padrão para o aimbot
                var scanResults = await Task.Run(() => memory.AoBScan(patternAimbot, true, true));

                if (scanResults != null && scanResults.Any())
                {
                    aimbotAddresses.Clear();
                    aimbotAddresses.AddRange(scanResults);

                    // Aplicar aimbot nos dois offsets
                    foreach (var baseAddress in aimbotAddresses)
                    {
                        try
                        {
                            long addr5 = baseAddress + offset5;
                            long addr6 = baseAddress + offset6;
                            memory.WriteMemory(addr5.ToString("X"), "int", WRITE_VALUE.ToString());
                            memory.WriteMemory(addr6.ToString("X"), "int", WRITE_VALUE.ToString());
                            await Task.Delay(10);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Erro ao aplicar aimbot: {ex.Message}");
                        }
                    }

                    isAimbotActive = true;
                    btnActive.Text = "Aimbot Ativo";
                    btnActive.BackColor = Color.LightGreen;
                    btnToggleAimbot.Enabled = true;
                    MessageBox.Show($"Aimbot ativado com sucesso!\nEndereços encontrados: {aimbotAddresses.Count}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum endereço encontrado. Verifique se o Free Fire está rodando no BlueStacks 4.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ativar aimbot: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isScanning = false;
                btnActive.Enabled = true;
            }
        }

        private async Task ApplyAimbotToAllParts()
        {
            foreach (var baseAddress in aimbotAddresses)
            {
                try
                {
                    // Aplicar aimbot para cada parte do corpo
                    foreach (var offset in bodyOffsets)
                    {
                        long targetAddress = baseAddress + offset.Value;
                        
                        // Escrever novo valor (105 decimal = 0x69)
                        memory.WriteMemory(targetAddress.ToString("X"), "int", WRITE_VALUE.ToString());
                        
                        // Pequena pausa para evitar detecção
                        await Task.Delay(10);
                    }
                }
                catch (Exception ex)
                {
                    // Log silencioso de erros para evitar interrupção
                    Debug.WriteLine($"Erro ao processar endereço {baseAddress:X}: {ex.Message}");
                }
            }
        }

        private async void btnToggleAimbot_Click(object sender, EventArgs e)
        {
            if (!isAimbotActive || aimbotAddresses.Count == 0)
            {
                MessageBox.Show("Ative o aimbot primeiro!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnToggleAimbot.Text = "Aplicando...";
                btnToggleAimbot.Enabled = false;

                await ApplyAimbotToAllParts();

                btnToggleAimbot.Text = "Aplicar Novamente";
                MessageBox.Show("Aimbot aplicado novamente!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar aimbot: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnToggleAimbot.Enabled = true;
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            var processes = Process.GetProcessesByName("HD-Player");
            string status = processes.Length > 0 ? "BlueStacks 4: Ativo" : "BlueStacks 4: Não encontrado";
            status += $"\nAimbot: {(isAimbotActive ? "Ativo" : "Inativo")}";
            status += $"\nEndereços encontrados: {aimbotAddresses.Count}";
            status += $"\nVision Hack: {(btnVisionHack.Text.Contains("Ativo") ? "Ativo" : "Inativo")}";
            status += $"\nWall Hack: {(btnWallHack.Text.Contains("Ativo") ? "Ativo" : "Inativo")}";
            status += $"\nNo Recoil: {(btnNoRecoil.Text.Contains("Ativo") ? "Ativo" : "Inativo")}";
            
            MessageBox.Show(status, "Status do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnNoRecoil_Click(object sender, EventArgs e)
        {
            btnNoRecoil.Text = "Aplicando...";
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
                    MessageBox.Show("BlueStacks 4 não encontrado! Certifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!memory.OpenProcess("HD-Player"))
                {
                    MessageBox.Show("Erro ao abrir processo do BlueStacks. Execute como administrador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar a primeira AOB
                var result = await Task.Run(() => memory.AoBScan(aobNoRecoil1, true, true));
                if (result == null || !result.Any())
                {
                    MessageBox.Show("Padrão No Recoil não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Patch: sobrescrever o endereço encontrado com o segundo padrão
                foreach (var address in result)
                {
                    // Escrever o segundo padrão byte a byte
                    var bytes = aobNoRecoil2.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    await Task.Delay(10);
                }
                btnNoRecoil.Text = "No Recoil Ativo";
                btnNoRecoil.BackColor = Color.LightGreen;
                MessageBox.Show("No Recoil aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar No Recoil: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnNoRecoil.Enabled = true;
            }
        }

        private async void btnVisionHack_Click(object sender, EventArgs e)
        {
            btnVisionHack.Text = "Aplicando...";
            btnVisionHack.Enabled = false;
            try
            {
                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    MessageBox.Show("BlueStacks 4 não encontrado! Certifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!memory.OpenProcess("HD-Player"))
                {
                    MessageBox.Show("Erro ao abrir processo do BlueStacks. Execute como administrador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar o padrão do Vision Hack
                var result = await Task.Run(() => memory.AoBScan(patternVisionHackSearch, true, true));
                if (result == null || !result.Any())
                {
                    MessageBox.Show("Padrão Vision Hack não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Patch: sobrescrever o endereço encontrado com o padrão de substituição
                foreach (var address in result)
                {
                    // Escrever o padrão de substituição byte a byte
                    var bytes = patternVisionHackReplace.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    await Task.Delay(10);
                }
                btnVisionHack.Text = "Vision Hack Ativo";
                btnVisionHack.BackColor = Color.LightGreen;
                MessageBox.Show("Vision Hack aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar Vision Hack: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnVisionHack.Enabled = true;
            }
        }

        private async void btnWallHack_Click(object sender, EventArgs e)
        {
            btnWallHack.Text = "Aplicando...";
            btnWallHack.Enabled = false;
            try
            {
                // Verificar se o processo está rodando
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0)
                {
                    MessageBox.Show("BlueStacks 4 não encontrado! Certifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!memory.OpenProcess("HD-Player"))
                {
                    MessageBox.Show("Erro ao abrir processo do BlueStacks. Execute como administrador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar o padrão do Wall Hack
                var result = await Task.Run(() => memory.AoBScan(patternWallHackSearch, true, true));
                if (result == null || !result.Any())
                {
                    MessageBox.Show("Padrão Wall Hack não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Patch: sobrescrever o endereço encontrado com o padrão de substituição
                foreach (var address in result)
                {
                    // Escrever o padrão de substituição byte a byte
                    var bytes = patternWallHackReplace.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    await Task.Delay(10);
                }
                btnWallHack.Text = "Wall Hack Ativo";
                btnWallHack.BackColor = Color.LightGreen;
                MessageBox.Show("Wall Hack aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar Wall Hack: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnWallHack.Enabled = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Limpar recursos ao fechar
            if (memory != null)
            {
                try
                {
                    memory.CloseProcess();
                }
                catch { }
            }
            base.OnFormClosing(e);
        }
    }
}



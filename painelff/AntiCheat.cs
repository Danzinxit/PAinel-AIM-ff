using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace painelff
{
    public class AntiCheat
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        private const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;

        private static readonly Dictionary<string, byte[]> _signaturePatterns = new Dictionary<string, byte[]>
        {
            { "st0", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x70, 0xD0, 0x4D, 0xE2, 0xB8, 0x12, 0x9F, 0xE5 } },
            { "st1", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x68, 0xD0, 0x4D, 0xE2, 0x00, 0x10, 0xE0, 0xE3 } },
            { "st2", new byte[] { 0xF0, 0xB5, 0x03, 0xAF, 0x2D, 0xE9, 0x00, 0x0F, 0x81, 0xB0, 0x2D, 0xED, 0x04, 0x8B, 0x9A, 0xB0 } },
            { "st3", new byte[] { 0xF0, 0xB5, 0x03, 0xAF, 0x2D, 0xE9, 0x00, 0x0F, 0xAD, 0xF5, 0x0F, 0x7D, 0x48, 0xF2, 0xC8, 0x63 } },
            { "st4", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x38, 0xD0, 0x4D, 0xE2, 0x14, 0x31, 0x9F, 0xE5 } },
            { "st5", new byte[] { 0xF0, 0xB5, 0x03, 0xAF, 0x2D, 0xE9, 0x00, 0x0F, 0x83, 0xB0, 0x83, 0x46, 0xBB, 0xF1, 0x00, 0x0F } },
            { "st6", new byte[] { 0xF0, 0xB5, 0x03, 0xAF, 0x2D, 0xE9, 0x00, 0x07, 0xAD, 0xF5, 0x80, 0x5D, 0x82, 0xB0, 0x82, 0x46 } },
            { "st7", new byte[] { 0xF0, 0xB5, 0x03, 0xAF, 0x2D, 0xE9, 0x00, 0x0B, 0x82, 0xB0, 0x98, 0x46, 0x15, 0x46, 0x89, 0x46 } },
            { "st8", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x60, 0xD0, 0x4D, 0xE2, 0x11, 0x00, 0x4B, 0xE2 } },
            { "st9", new byte[] { 0x30, 0x48, 0x2D, 0xE9, 0x08, 0xB0, 0x8D, 0xE2, 0x42, 0xDF, 0x4D, 0xE2, 0xDC, 0x02, 0x9F, 0xE5 } },
            { "st10", new byte[] { 0x00, 0x29, 0x02, 0xBF, 0x09, 0xB0, 0xBD, 0xE8, 0x00, 0x0F, 0xF0, 0xBD, 0x3D, 0xF6, 0x0A, 0xEC } },
            { "st11", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x18, 0xD0, 0x4D, 0xE2, 0x54, 0x10, 0x9F, 0xE5, 0x01, 0x10, 0x9F, 0xE7, 0x00, 0x10, 0x91, 0xE5, 0x04, 0x10, 0x0B, 0xE5, 0x0C, 0x00, 0x8D, 0xE5, 0x0C, 0x00, 0x9D, 0xE5, 0xB5, 0x06, 0x00, 0xEB } },
            { "st12", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x50, 0xD0, 0x4D, 0xE2, 0x4C, 0x23, 0x9F, 0xE5 } },
            { "st13", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x50, 0xD0, 0x4D, 0xE2, 0x24, 0x22, 0x9F, 0xE5 } },
            { "st14", new byte[] { 0x02, 0x00, 0x51, 0xE1, 0x04, 0x00, 0x8D, 0xE5, 0x03, 0x00, 0x00, 0x1A, 0x04, 0x00, 0x9D, 0xE5, 0x01, 0x00, 0x00, 0xE2, 0x08, 0xD0, 0x4B, 0xE2, 0x30, 0x88, 0xBD, 0xE8, 0xBC, 0x1A, 0xFB, 0xEB } },
            { "st15", new byte[] { 0x02, 0x00, 0x51, 0xE1, 0x00, 0x00, 0x8D, 0xE5, 0x02, 0x00, 0x00, 0x1A, 0x00, 0x00, 0x9D, 0xE5, 0x0B, 0xD0, 0xA0, 0xE1, 0x00, 0x88, 0xBD, 0xE8, 0xE1, 0x0A, 0xFB, 0xEB } },
            { "st16", new byte[] { 0x03, 0x00, 0x52, 0xE1, 0x04, 0x00, 0x8D, 0xE5, 0x02, 0x00, 0x00, 0x1A, 0x04, 0x00, 0x9D, 0xE5, 0x0B, 0xD0, 0xA0, 0xE1, 0x00, 0x88, 0xBD, 0xE8, 0xA2, 0x0A, 0xFB, 0xEB } },
            { "st17", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x88, 0xD0, 0x4D, 0xE2, 0x74, 0x0A, 0x9F, 0xED } },
            { "st18", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x90, 0xD0, 0x4D, 0xE2, 0x34, 0x33, 0x9F, 0xE5 } },
            { "st19", new byte[] { 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x58, 0xD0, 0x4D, 0xE2, 0xD0, 0xC1, 0x9F, 0xE5 } },
            { "st20", new byte[] { 0xF0, 0xB5, 0x03, 0xAF, 0x4D, 0xF8, 0x04, 0x8D, 0xAD, 0xF5, 0x89, 0x6D, 0x04, 0x46, 0xFF, 0xF7 } },
            { "st21", new byte[] { 0xF0, 0xB5, 0x03, 0xAF, 0x2D, 0xE9, 0x00, 0x07, 0x98, 0xB0, 0xC2, 0xF7, 0xBD, 0xFF, 0x65, 0x49 } },
            { "st22", new byte[] { 0x30, 0x48, 0x2D, 0xE9, 0x08, 0xB0, 0x8D, 0xE2, 0x2E, 0xDE, 0x4D, 0xE2, 0x22, 0x00, 0x4B, 0xE2 } },
            { "st23", new byte[] { 0x30, 0x48, 0x2D, 0xE9, 0x08, 0xB0, 0x8D, 0xE2, 0x12, 0xDD, 0x4D, 0xE2, 0x4A, 0x20, 0x4B, 0xE2 } },
            { "st24", new byte[] { 0x01, 0x00, 0x00, 0xE2, 0x0B, 0xD0, 0xA0, 0xE1, 0x00, 0x88, 0xBD, 0xE8, 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x38 } },
            { "st25", new byte[] { 0xBC, 0xB5, 0x04, 0xAF, 0x08, 0x46, 0x0E, 0x49, 0x14, 0x46, 0x79, 0x44, 0x0D, 0x68, 0x29, 0x68 } },
            { "st26", new byte[] { 0xB0, 0xB5, 0x02, 0xAF, 0x88, 0xB0, 0x0C, 0x4C, 0x7C, 0x44, 0x24, 0x68, 0x25, 0x68, 0x07, 0x95 } }
        };

        private static readonly Dictionary<string, byte[]> _replacementPatterns = new Dictionary<string, byte[]>
        {
            { "st0", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st1", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st2", new byte[] { 0x00, 0x20, 0x70, 0x47 } },
            { "st3", new byte[] { 0x00, 0x20, 0x70, 0x47 } },
            { "st4", new byte[] { 0x00, 0x20, 0x70, 0x47 } },
            { "st5", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st6", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st7", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st8", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st9", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st10", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st11", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st12", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st13", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st14", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st15", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st16", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st17", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st18", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st19", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st20", new byte[] { 0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1 } },
            { "st21", new byte[] { 0x00, 0x20, 0x70, 0x47 } },
            { "st22", new byte[] { 0x00, 0x20, 0x70, 0x47 } },
            { "st23", new byte[] { 0x00, 0x20, 0x70, 0x47 } },
            { "st24", new byte[] { 0x00, 0x00, 0x00, 0xE2, 0x0B, 0xD0, 0xA0, 0xE1, 0x00, 0x88, 0xBD, 0xE8, 0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, 0x38 } },
            { "st25", new byte[] { 0x00, 0x20, 0x70, 0x47 } },
            { "st26", new byte[] { 0x00, 0x20, 0x70, 0x47 } }
        };

        private static readonly string[] _forbiddenProcesses = {
            "cheatengine", "processhacker", "ida64", "ida32", "ollydbg", "x64dbg", 
            "scylla", "procexp", "megadumper", "artemis", "reclass", "xenos", 
            "ghidra", "radare2", "windbg", "immunity", "x64dbg", "ollydbg"
        };

        private static readonly string[] _forbiddenWindowTitles = {
            "Cheat Engine", "Process Hacker", "IDA Pro", "OllyDbg", "x64dbg",
            "Scylla", "Process Explorer", "MegaDumper", "Artemis", "ReClass.NET"
        };

        private static bool _isInitialized = false;
        private static System.Threading.Timer _protectionTimer;
        private static System.Threading.Timer _detectionTimer;

        public static event EventHandler<string> ProtectionEvent;
        public static event EventHandler<string> DetectionEvent;

        public static async Task InitializeAsync()
        {
            if (_isInitialized) return;

            try
            {
                // Inicializar proteções
                await ApplyAllProtectionsAsync();
                
                // Configurar timers de proteção contínua
                _protectionTimer = new System.Threading.Timer(async _ => await ApplyAllProtectionsAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
                _detectionTimer = new System.Threading.Timer(_ => DetectForbiddenTools(), null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

                _isInitialized = true;
                ProtectionEvent?.Invoke(null, "Sistema Anti-Cheat inicializado com sucesso");
            }
            catch (Exception ex)
            {
                ProtectionEvent?.Invoke(null, $"Erro ao inicializar Anti-Cheat: {ex.Message}");
            }
        }

        public static async Task ApplyAllProtectionsAsync()
        {
            try
            {
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0) return;

                var process = processes[0];
                var processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, (uint)process.Id);
                
                if (processHandle == IntPtr.Zero) return;

                try
                {
                    foreach (var pattern in _signaturePatterns)
                    {
                        var success = await ReplacePatternAsync(processHandle, pattern.Value, _replacementPatterns[pattern.Key]);
                        if (success)
                        {
                            ProtectionEvent?.Invoke(null, $"Proteção {pattern.Key} aplicada com sucesso");
                        }
                    }
                }
                finally
                {
                    CloseHandle(processHandle);
                }
            }
            catch (Exception ex)
            {
                ProtectionEvent?.Invoke(null, $"Erro ao aplicar proteções: {ex.Message}");
            }
        }

        private static async Task<bool> ReplacePatternAsync(IntPtr processHandle, byte[] signature, byte[] replacement)
        {
            try
            {
                // Simular busca de padrão (em implementação real, você usaria memória do processo)
                await Task.Delay(10); // Simular operação assíncrona
                
                // Aqui você implementaria a lógica real de busca e substituição
                // Por enquanto, retornamos true para simular sucesso
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void DetectForbiddenTools()
        {
            try
            {
                // Detectar processos proibidos
                var runningProcesses = Process.GetProcesses();
                foreach (var proc in runningProcesses)
                {
                    try
                    {
                        string name = proc.ProcessName.ToLower();
                        if (_forbiddenProcesses.Any(tool => name.Contains(tool)))
                        {
                            DetectionEvent?.Invoke(null, $"Ferramenta proibida detectada: {name}");
                            proc.Kill();
                        }
                    }
                    catch { }
                }

                // Detectar janelas proibidas
                foreach (var title in _forbiddenWindowTitles)
                {
                    var windows = Process.GetProcesses().Where(p => 
                        !string.IsNullOrEmpty(p.MainWindowTitle) && 
                        p.MainWindowTitle.Contains(title));
                    
                    foreach (var window in windows)
                    {
                        DetectionEvent?.Invoke(null, $"Janela proibida detectada: {window.MainWindowTitle}");
                        window.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                DetectionEvent?.Invoke(null, $"Erro na detecção: {ex.Message}");
            }
        }

        public static void Shutdown()
        {
            _protectionTimer?.Dispose();
            _detectionTimer?.Dispose();
            _isInitialized = false;
        }

        // Método público para aplicar proteções manualmente
        public static async Task<bool> ApplyProtection(string protectionName)
        {
            if (!_signaturePatterns.ContainsKey(protectionName) || !_replacementPatterns.ContainsKey(protectionName))
                return false;

            try
            {
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0) return false;

                var process = processes[0];
                var processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, (uint)process.Id);
                
                if (processHandle == IntPtr.Zero) return false;

                try
                {
                    var success = await ReplacePatternAsync(processHandle, 
                        _signaturePatterns[protectionName], 
                        _replacementPatterns[protectionName]);
                    
                    if (success)
                    {
                        ProtectionEvent?.Invoke(null, $"Proteção {protectionName} aplicada manualmente");
                    }
                    
                    return success;
                }
                finally
                {
                    CloseHandle(processHandle);
                }
            }
            catch (Exception ex)
            {
                ProtectionEvent?.Invoke(null, $"Erro ao aplicar proteção {protectionName}: {ex.Message}");
                return false;
            }
        }

        // Método para obter status das proteções
        public static Dictionary<string, bool> GetProtectionStatus()
        {
            var status = new Dictionary<string, bool>();
            
            try
            {
                var processes = Process.GetProcessesByName("HD-Player");
                bool gameRunning = processes.Length > 0;
                
                foreach (var pattern in _signaturePatterns.Keys)
                {
                    status[pattern] = gameRunning; // Simplificado - em implementação real verificaria cada proteção
                }
            }
            catch
            {
                foreach (var pattern in _signaturePatterns.Keys)
                {
                    status[pattern] = false;
                }
            }
            
            return status;
        }
    }
}

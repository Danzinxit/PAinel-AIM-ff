using System;
using System.Windows.Forms;
using KeyAuth;
using System.IO;
using System.Text.Json;

namespace painelff
{
    public partial class LoginForm : Form
    {
        public static api KeyAuthApp = new api(
            name: "Danielvieiraxbh30's Application",
            ownerid: "eOgmD1CNum",
            secret: "011142891941ca201362bc731d01323f1863170b7e541fb2f37bdfa5e20b6e2c", // Substitua pelo seu secret real
            version: "1.0"
        );

        private const string CONFIG_FILE = "login_config.json";
        private int loginAttempts = 0;
        private const int MAX_LOGIN_ATTEMPTS = 3;
        private System.Windows.Forms.Timer resetAttemptsTimer;

        public LoginForm()
        {
            InitializeComponent();
            btnRegistrar.Click += btnRegistrar_Click;
            btnEntrar.Click += btnEntrar_Click;
            this.Load += LoginForm_Load;
            
            // Inicializar timer para resetar tentativas
            resetAttemptsTimer = new System.Windows.Forms.Timer();
            resetAttemptsTimer.Interval = 300000; // 5 minutos
            resetAttemptsTimer.Tick += ResetAttemptsTimer_Tick;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                ProgressTimer.Interval = 20;
                ProgressTimer.Tick += ProgressTimer_Tick;
                guna2ProgressBar1.Value = 0;
                
                // Inicializar KeyAuth
                KeyAuthApp.init();
                
                // Verificar se a inicialização foi bem-sucedida
                if (KeyAuthApp.response.success)
                {
                    statusLabel.Text = "Sistema inicializado com sucesso";
                    LoadRememberedLogin();
                }
                else
                {
                    statusLabel.Text = "Erro na inicialização: " + KeyAuthApp.response.message;
                    MessageBox.Show("Erro na inicialização do sistema: " + KeyAuthApp.response.message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Erro na inicialização";
                MessageBox.Show("Erro durante a inicialização: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Verificar se os campos estão preenchidos
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar se excedeu o número máximo de tentativas
            if (loginAttempts >= MAX_LOGIN_ATTEMPTS)
            {
                MessageBox.Show($"Você excedeu o número máximo de tentativas ({MAX_LOGIN_ATTEMPTS}).\nAguarde alguns minutos antes de tentar novamente.", 
                    "Muitas tentativas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                statusLabel.Text = "Tentando fazer login...";
                btnEntrar.Enabled = false; // Desabilitar botão durante o login
                
                KeyAuthApp.login(txtUsuario.Text, txtSenha.Text);
                
                // Debug: Mostrar resposta completa
                string debugInfo = $"Success: {KeyAuthApp.response.success}\nMessage: {KeyAuthApp.response.message}";
                Console.WriteLine(debugInfo);
                
                if (KeyAuthApp.response.success)
                {
                    // Salvar credenciais se "Lembrar login" estiver marcado
                    if (rememberCheckBox.Checked)
                    {
                        SaveRememberedLogin(txtUsuario.Text, txtSenha.Text);
                    }
                    else
                    {
                        // Se não estiver marcado, remover credenciais salvas
                        RemoveRememberedLogin();
                    }

                    loginAttempts = 0; // Resetar tentativas
                    statusLabel.Text = "Login bem-sucedido! Carregando painel...";
                    guna2ProgressBar1.Value = 0;
                    ProgressTimer.Start();
                }
                else
                {
                    loginAttempts++;
                    int remainingAttempts = MAX_LOGIN_ATTEMPTS - loginAttempts;
                    
                    string errorMessage = GetErrorMessage(KeyAuthApp.response.message);
                    
                    statusLabel.Text = $"Login falhou! Tentativas restantes: {remainingAttempts}";
                    
                    if (remainingAttempts > 0)
                    {
                        MessageBox.Show($"{errorMessage}\n\nTentativas restantes: {remainingAttempts}", 
                            "Login Falhou", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"{errorMessage}\n\nVocê excedeu o número máximo de tentativas.\nAguarde 5 minutos antes de tentar novamente.", 
                            "Muitas tentativas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                        // Iniciar timer para resetar tentativas
                        resetAttemptsTimer.Start();
                        statusLabel.Text = "Aguarde 5 minutos para tentar novamente...";
                    }
                    
                    // Limpar senha em caso de erro
                    txtSenha.Clear();
                    txtSenha.Focus();
                }
            }
            catch (Exception ex)
            {
                loginAttempts++;
                MessageBox.Show("Erro durante o login: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Erro durante o login";
                Console.WriteLine($"Erro no login: {ex}");
                
                // Limpar senha em caso de erro
                txtSenha.Clear();
                txtSenha.Focus();
            }
            finally
            {
                btnEntrar.Enabled = true; // Reabilitar botão
            }
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            guna2ProgressBar1.Value += 4;
            statusLabel.Text = $"Carregando painel... {guna2ProgressBar1.Value}%";
            
            if (guna2ProgressBar1.Value >= 100)
            {
                ProgressTimer.Stop();
                statusLabel.Text = "Abrindo painel principal...";
                LoginSucces();
            }
        }

        private void LoginSucces()
        {
            try
            {
                Form1 main = new Form1();
                main.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir o painel principal: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            using (var regForm = new RegisterForm())
            {
                if (regForm.ShowDialog() == DialogResult.OK)
                {
                    txtUsuario.Text = regForm.UsuarioRegistrado;
                    txtSenha.Text = regForm.SenhaRegistrada;
                    MessageBox.Show("Usuário registrado com sucesso! Agora faça login.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnEntrar.Focus();
                }
            }
        }

        // Métodos para gerenciar "Lembrar login"
        private void SaveRememberedLogin(string username, string password)
        {
            try
            {
                var config = new
                {
                    Username = username,
                    Password = password,
                    RememberLogin = true,
                    SavedAt = DateTime.Now
                };

                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(CONFIG_FILE, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar configuração: {ex.Message}");
            }
        }

        private void LoadRememberedLogin()
        {
            try
            {
                if (File.Exists(CONFIG_FILE))
                {
                    string json = File.ReadAllText(CONFIG_FILE);
                    var config = JsonSerializer.Deserialize<dynamic>(json);

                    if (config != null)
                    {
                        txtUsuario.Text = config.GetProperty("Username").GetString() ?? "";
                        txtSenha.Text = config.GetProperty("Password").GetString() ?? "";
                        rememberCheckBox.Checked = config.GetProperty("RememberLogin").GetBoolean();

                        if (rememberCheckBox.Checked && !string.IsNullOrEmpty(txtUsuario.Text))
                        {
                            statusLabel.Text = "Credenciais carregadas automaticamente";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar configuração: {ex.Message}");
                // Se houver erro, remover arquivo corrompido
                try { File.Delete(CONFIG_FILE); } catch { }
            }
        }

        private void RemoveRememberedLogin()
        {
            try
            {
                if (File.Exists(CONFIG_FILE))
                {
                    File.Delete(CONFIG_FILE);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover configuração: {ex.Message}");
            }
        }

        private string GetErrorMessage(string keyAuthMessage)
        {
            // Traduzir mensagens do KeyAuth para português
            return keyAuthMessage.ToLower() switch
            {
                var msg when msg.Contains("invalid") || msg.Contains("incorrect") => "Usuário ou senha incorretos!",
                var msg when msg.Contains("not found") => "Usuário não encontrado!",
                var msg when msg.Contains("banned") || msg.Contains("blacklisted") => "Conta banida ou bloqueada!",
                var msg when msg.Contains("expired") => "Sua assinatura expirou!",
                var msg when msg.Contains("hwid") => "HWID não autorizado para esta conta!",
                var msg when msg.Contains("timeout") || msg.Contains("connection") => "Erro de conexão com o servidor!",
                _ => $"Erro no login: {keyAuthMessage}"
            };
        }

        private void ResetAttemptsTimer_Tick(object sender, EventArgs e)
        {
            loginAttempts = 0;
            resetAttemptsTimer.Stop();
            statusLabel.Text = "Sistema inicializado com sucesso";
            btnEntrar.Enabled = true;
        }
    }
} 
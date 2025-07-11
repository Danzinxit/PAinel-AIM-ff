using System;
using System.Windows.Forms;
using KeyAuth;

namespace painelff
{
    public partial class RegisterForm : Form
    {
        public string UsuarioRegistrado { get; private set; }
        public string SenhaRegistrada { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
            this.Load += RegisterForm_Load;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            LoginForm.KeyAuthApp.init();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            LoginForm.KeyAuthApp.register(txtUsuario.Text, txtSenha.Text, txtKey.Text);
            if (LoginForm.KeyAuthApp.response.success)
            {
                statusLabel.Text = "Usuário registrado com sucesso!";
                UsuarioRegistrado = txtUsuario.Text;
                SenhaRegistrada = txtSenha.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                statusLabel.Text = "Erro: " + LoginForm.KeyAuthApp.response.message;
            }
        }
    }
} 
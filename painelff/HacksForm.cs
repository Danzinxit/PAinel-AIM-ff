using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using Memory;

namespace painelff
{
    public class HacksPanel : UserControl
    {
        private Button btnNoRecoil;
        private Button btnVisionHack;
        private Button btnWallHack;
        private Label lblHacksTitle;
        private Button btnVoltar;
        private Mem memory = new Mem();
        private string patternVisionHackSearch = "00 00 B4 43 DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30";
        private string patternVisionHackReplace = "00 00 B4 43 00 00 A0 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30";
        private string patternWallHackSearch = "09 0E 00 00 80 3F 00 00 80 3F";
        private string patternWallHackReplace = "09 0E 00 00 A0 4F 00 00 80 3F";

        public event EventHandler VoltarClick;

        public HacksPanel()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(30, 30, 40);

            lblHacksTitle = new Label();
            lblHacksTitle.Text = "⚡ HACKS PRO";
            lblHacksTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblHacksTitle.ForeColor = Color.Orange;
            lblHacksTitle.Location = new Point(20, 18);
            lblHacksTitle.Size = new Size(200, 28);

            btnNoRecoil = new Button();
            btnNoRecoil.Text = "🎯 NO RECOIL";
            btnNoRecoil.Size = new Size(240, 38);
            btnNoRecoil.Location = new Point(35, 60);
            btnNoRecoil.BackColor = Color.FromArgb(80, 80, 90);
            btnNoRecoil.ForeColor = Color.White;
            btnNoRecoil.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnNoRecoil.FlatStyle = FlatStyle.Flat;
            btnNoRecoil.FlatAppearance.BorderSize = 0;
            btnNoRecoil.Click += BtnNoRecoil_Click;

            btnVisionHack = new Button();
            btnVisionHack.Text = "👁️ VISION HACK";
            btnVisionHack.Size = new Size(240, 38);
            btnVisionHack.Location = new Point(35, 108);
            btnVisionHack.BackColor = Color.FromArgb(80, 80, 90);
            btnVisionHack.ForeColor = Color.White;
            btnVisionHack.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnVisionHack.FlatStyle = FlatStyle.Flat;
            btnVisionHack.FlatAppearance.BorderSize = 0;
            btnVisionHack.Click += BtnVisionHack_Click;

            btnWallHack = new Button();
            btnWallHack.Text = "🧱 WALL HACK";
            btnWallHack.Size = new Size(240, 38);
            btnWallHack.Location = new Point(35, 156);
            btnWallHack.BackColor = Color.FromArgb(80, 80, 90);
            btnWallHack.ForeColor = Color.White;
            btnWallHack.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnWallHack.FlatStyle = FlatStyle.Flat;
            btnWallHack.FlatAppearance.BorderSize = 0;
            btnWallHack.Click += BtnWallHack_Click;

            btnVoltar = new Button();
            btnVoltar.Text = "←";
            btnVoltar.Size = new Size(36, 36);
            btnVoltar.Location = new Point(12, 20);
            btnVoltar.FlatStyle = FlatStyle.Flat;
            btnVoltar.FlatAppearance.BorderSize = 0;
            btnVoltar.BackColor = Color.FromArgb(30, 30, 40);
            btnVoltar.ForeColor = Color.Orange;
            btnVoltar.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnVoltar.Cursor = Cursors.Hand;
            btnVoltar.Click += (s, e) => VoltarClick?.Invoke(this, EventArgs.Empty);

            this.Controls.Add(lblHacksTitle);
            this.Controls.Add(btnNoRecoil);
            this.Controls.Add(btnVisionHack);
            this.Controls.Add(btnWallHack);
            this.Controls.Add(btnVoltar);
        }

        private async void BtnNoRecoil_Click(object sender, EventArgs e)
        {
            btnNoRecoil.Text = "⚡ APLICANDO...";
            btnNoRecoil.Enabled = false;
            try
            {
                string aobNoRecoil1 = "30 48 2D E9 08 B0 8D E2 02 8B 2D ED 00 40 A0 E1 38 01 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 28 01 9F E5 00 00 9F E7 00 00 90 E5";
                string aobNoRecoil2 = "00 00 A0 E3 1E FF 2F E1 02 8B 2D ED 00 40 A0 E1 38 01 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 28 01 9F E5 00 00 9F E7 00 00 90 E5";
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0 || !memory.OpenProcess("HD-Player"))
                {
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var result = await System.Threading.Tasks.Task.Run(() => memory.AoBScan(aobNoRecoil1, true, true));
                if (result == null || !result.Any())
                {
                    MessageBox.Show("⚠️ Padrão No Recoil não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (var address in result)
                {
                    var bytes = aobNoRecoil2.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    int delay = RandomNumberGenerator.GetInt32(100, 350);
                    await System.Threading.Tasks.Task.Delay(delay);
                }
                btnNoRecoil.Text = "✅ NO RECOIL ATIVO";
                btnNoRecoil.BackColor = Color.FromArgb(0, 150, 100);
                MessageBox.Show("✅ No Recoil aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao aplicar No Recoil:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnNoRecoil.Enabled = true;
            }
        }

        private async void BtnVisionHack_Click(object sender, EventArgs e)
        {
            btnVisionHack.Text = "⚡ APLICANDO...";
            btnVisionHack.Enabled = false;
            try
            {
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0 || !memory.OpenProcess("HD-Player"))
                {
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var result = await System.Threading.Tasks.Task.Run(() => memory.AoBScan(patternVisionHackSearch, true, true));
                if (result == null || !result.Any())
                {
                    MessageBox.Show("⚠️ Padrão Vision Hack não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (var address in result)
                {
                    var bytes = patternVisionHackReplace.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    int delay = RandomNumberGenerator.GetInt32(100, 350);
                    await System.Threading.Tasks.Task.Delay(delay);
                }
                btnVisionHack.Text = "✅ VISION HACK ATIVO";
                btnVisionHack.BackColor = Color.FromArgb(0, 150, 100);
                MessageBox.Show("✅ Vision Hack aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao aplicar Vision Hack:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnVisionHack.Enabled = true;
            }
        }

        private async void BtnWallHack_Click(object sender, EventArgs e)
        {
            btnWallHack.Text = "⚡ APLICANDO...";
            btnWallHack.Enabled = false;
            try
            {
                var processes = Process.GetProcessesByName("HD-Player");
                if (processes.Length == 0 || !memory.OpenProcess("HD-Player"))
                {
                    MessageBox.Show("❌ BlueStacks 4 não encontrado!\nCertifique-se de que o Free Fire está rodando.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var result = await System.Threading.Tasks.Task.Run(() => memory.AoBScan(patternWallHackSearch, true, true));
                if (result == null || !result.Any())
                {
                    MessageBox.Show("⚠️ Padrão Wall Hack não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (var address in result)
                {
                    var bytes = patternWallHackReplace.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();
                    memory.WriteBytes(address.ToString("X"), bytes);
                    int delay = RandomNumberGenerator.GetInt32(100, 350);
                    await System.Threading.Tasks.Task.Delay(delay);
                }
                btnWallHack.Text = "✅ WALL HACK ATIVO";
                btnWallHack.BackColor = Color.FromArgb(0, 150, 100);
                MessageBox.Show("✅ Wall Hack aplicado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao aplicar Wall Hack:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnWallHack.Enabled = true;
            }
        }
    }
} 
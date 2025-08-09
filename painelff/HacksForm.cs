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
        private Label lblHacksTitle;
        private Button btnVoltar;

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
            this.Controls.Add(btnVoltar);
        }
    }
} 
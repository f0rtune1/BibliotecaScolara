using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmLogin : Form
    {
        public Utilizator UtilizatorAutentificat { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                Mesaje.Validare("Introduceti numele de utilizator!");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtParola.Text))
            {
                Mesaje.Validare("Introduceti parola!");
                txtParola.Focus();
                return;
            }

            try
            {
                Utilizator user = UtilizatorManager.Autentifica(txtUsername.Text.Trim(), txtParola.Text);
                if (user != null)
                {
                    UtilizatorAutentificat = user;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Mesaje.Eroare("Utilizator sau parola incorecta!", "Autentificare esuata");
                    txtParola.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la autentificare: " + ex.Message);
            }
        }

        private void BtnAnuleaza_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TxtParola_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                BtnLogin_Click(sender, e);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new Panel();
            this.lblTitlu = new Label();
            this.lblSubtitlu = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblParola = new Label();
            this.txtParola = new TextBox();
            this.btnLogin = new Button();
            this.btnAnuleaza = new Button();
            this.lblInfo = new Label();

            this.panelHeader.SuspendLayout();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = Color.FromArgb(26, 82, 118);
            this.panelHeader.Controls.Add(this.lblTitlu);
            this.panelHeader.Controls.Add(this.lblSubtitlu);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Location = new Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new Size(380, 90);
            this.panelHeader.TabIndex = 0;

            // lblTitlu
            this.lblTitlu.AutoSize = true;
            this.lblTitlu.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitlu.ForeColor = Color.White;
            this.lblTitlu.Location = new Point(15, 15);
            this.lblTitlu.Name = "lblTitlu";
            this.lblTitlu.Text = "Biblioteca Scolara";

            // lblSubtitlu
            this.lblSubtitlu.AutoSize = true;
            this.lblSubtitlu.Font = new Font("Segoe UI", 9F);
            this.lblSubtitlu.ForeColor = Color.FromArgb(174, 214, 241);
            this.lblSubtitlu.Location = new Point(18, 52);
            this.lblSubtitlu.Name = "lblSubtitlu";
            this.lblSubtitlu.Text = "Sistem de Gestiune - Autentificare";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 9F);
            this.lblUsername.Location = new Point(40, 115);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Text = "Utilizator:";

            // txtUsername
            this.txtUsername.Font = new Font("Segoe UI", 10F);
            this.txtUsername.Location = new Point(40, 135);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(300, 26);
            this.txtUsername.TabIndex = 1;

            // lblParola
            this.lblParola.AutoSize = true;
            this.lblParola.Font = new Font("Segoe UI", 9F);
            this.lblParola.Location = new Point(40, 175);
            this.lblParola.Name = "lblParola";
            this.lblParola.Text = "Parola:";

            // txtParola
            this.txtParola.Font = new Font("Segoe UI", 10F);
            this.txtParola.Location = new Point(40, 195);
            this.txtParola.Name = "txtParola";
            this.txtParola.PasswordChar = '*';
            this.txtParola.Size = new Size(300, 26);
            this.txtParola.TabIndex = 2;
            this.txtParola.KeyPress += new KeyPressEventHandler(this.TxtParola_KeyPress);

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(26, 82, 118);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(40, 245);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(130, 35);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Autentificare";
            this.btnLogin.Click += new EventHandler(this.BtnLogin_Click);

            // btnAnuleaza
            this.btnAnuleaza.BackColor = Color.FromArgb(189, 195, 199);
            this.btnAnuleaza.FlatStyle = FlatStyle.Flat;
            this.btnAnuleaza.Font = new Font("Segoe UI", 10F);
            this.btnAnuleaza.Location = new Point(210, 245);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new Size(130, 35);
            this.btnAnuleaza.TabIndex = 4;
            this.btnAnuleaza.Text = "Anulare";
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            // lblInfo
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new Font("Segoe UI", 8F);
            this.lblInfo.ForeColor = Color.Gray;
            this.lblInfo.Location = new Point(40, 295);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Text = "Utilizator implicit: admin / Parola: admin123";

            // FrmLogin
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(380, 320);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtParola);
            this.Controls.Add(this.lblParola);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Autentificare - Biblioteca Scolara";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Panel panelHeader;
        private Label lblTitlu;
        private Label lblSubtitlu;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblParola;
        private TextBox txtParola;
        private Button btnLogin;
        private Button btnAnuleaza;
        private Label lblInfo;
    }
}

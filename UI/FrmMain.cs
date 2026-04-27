using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmMain : Form
    {
        private Utilizator utilizatorCurent;

        public FrmMain(Utilizator utilizator = null)
        {
            InitializeComponent();
            utilizatorCurent = utilizator;
            ApplyTheme();
            if (utilizatorCurent != null)
            {
                this.Text = "Biblioteca Școlară - " + utilizatorCurent.NumeUtilizator +
                            " [" + utilizatorCurent.Rol + "]";
                statusLabelUser.Text = "Utilizator: " + utilizatorCurent.NumeUtilizator +
                                       "  |  Rol: " + utilizatorCurent.Rol;
            }
            statusLabelDate.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        }

        private void ApplyTheme()
        {
            this.BackColor = ThemeHelper.FormBackground;
            menuStrip.BackColor = ThemeHelper.PrimaryBlue;
            menuStrip.ForeColor = Color.White;
            menuStrip.Font = new Font("Segoe UI", 9F);
            foreach (ToolStripItem item in menuStrip.Items)
            {
                item.ForeColor = Color.White;
            }
            statusStrip.BackColor = ThemeHelper.PrimaryBlue;
            statusStrip.ForeColor = Color.White;
            statusLabelUser.ForeColor = Color.White;
            statusLabelDate.ForeColor = Color.FromArgb(174, 214, 241);
        }

        private void AutoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAutori frm = new FrmAutori();
            frm.MdiParent = this;
            frm.Show();
        }

        private void EditoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEdituri frm = new FrmEdituri();
            frm.MdiParent = this;
            frm.Show();
        }

        private void CategoriiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategorii frm = new FrmCategorii();
            frm.MdiParent = this;
            frm.Show();
        }

        private void CartiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCarti frm = new FrmCarti();
            frm.MdiParent = this;
            frm.Show();
        }

        private void EleviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmElevi frm = new FrmElevi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ImprumuturiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmImprumturi frm = new FrmImprumturi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ExemplareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExemplare frm = new FrmExemplare();
            frm.MdiParent = this;
            frm.Show();
        }

        private void RaportIntarziateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var intarziate = ImprumutManager.GetOverdueLoans();
                FrmRaportIntarziate frm = new FrmRaportIntarziate(intarziate);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la generarea raportului: " + ex.Message);
            }
        }

        private void IesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.menuStrip = new MenuStrip();
            this.administrareToolStripMenuItem = new ToolStripMenuItem();
            this.autoToolStripMenuItem = new ToolStripMenuItem();
            this.editori = new ToolStripMenuItem();
            this.categorii = new ToolStripMenuItem();
            this.separatorMenu1 = new ToolStripSeparator();
            this.carti = new ToolStripMenuItem();
            this.exemplare = new ToolStripMenuItem();
            this.managementToolStripMenuItem = new ToolStripMenuItem();
            this.elevi = new ToolStripMenuItem();
            this.imprumturi = new ToolStripMenuItem();
            this.separatorMenu2 = new ToolStripSeparator();
            this.rapoarteToolStripMenuItem = new ToolStripMenuItem();
            this.raportIntarziate = new ToolStripMenuItem();
            this.separatorMenu3 = new ToolStripSeparator();
            this.iesire = new ToolStripMenuItem();
            this.statusStrip = new StatusStrip();
            this.statusLabelUser = new ToolStripStatusLabel();
            this.statusSpring = new ToolStripStatusLabel();
            this.statusLabelDate = new ToolStripStatusLabel();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new ToolStripItem[] {
                this.administrareToolStripMenuItem,
                this.managementToolStripMenuItem,
                this.rapoarteToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";

            // administrareToolStripMenuItem
            this.administrareToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.autoToolStripMenuItem,
                this.editori,
                this.categorii,
                this.separatorMenu1,
                this.carti,
                this.exemplare});
            this.administrareToolStripMenuItem.Name = "administrareToolStripMenuItem";
            this.administrareToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.administrareToolStripMenuItem.Text = "&Administrare";

            // autoToolStripMenuItem
            this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
            this.autoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.autoToolStripMenuItem.Text = "&Autori";
            this.autoToolStripMenuItem.Click += new EventHandler(this.AutoriToolStripMenuItem_Click);

            // editori
            this.editori.Name = "editori";
            this.editori.Size = new System.Drawing.Size(150, 22);
            this.editori.Text = "&Edituri";
            this.editori.Click += new EventHandler(this.EditoriToolStripMenuItem_Click);

            // categorii
            this.categorii.Name = "categorii";
            this.categorii.Size = new System.Drawing.Size(150, 22);
            this.categorii.Text = "&Categorii";
            this.categorii.Click += new EventHandler(this.CategoriiToolStripMenuItem_Click);

            // separatorMenu1
            this.separatorMenu1.Name = "separatorMenu1";
            this.separatorMenu1.Size = new System.Drawing.Size(147, 6);

            // carti
            this.carti.Name = "carti";
            this.carti.Size = new System.Drawing.Size(150, 22);
            this.carti.Text = "&Cărți";
            this.carti.Click += new EventHandler(this.CartiToolStripMenuItem_Click);

            // exemplare
            this.exemplare.Name = "exemplare";
            this.exemplare.Size = new System.Drawing.Size(150, 22);
            this.exemplare.Text = "&Exemplare";
            this.exemplare.Click += new EventHandler(this.ExemplareToolStripMenuItem_Click);

            // managementToolStripMenuItem
            this.managementToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.elevi,
                this.imprumturi,
                this.separatorMenu2,
                this.iesire});
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            this.managementToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.managementToolStripMenuItem.Text = "&Management";

            // elevi
            this.elevi.Name = "elevi";
            this.elevi.Size = new System.Drawing.Size(150, 22);
            this.elevi.Text = "&Elevi";
            this.elevi.Click += new EventHandler(this.EleviToolStripMenuItem_Click);

            // imprumturi
            this.imprumturi.Name = "imprumturi";
            this.imprumturi.Size = new System.Drawing.Size(150, 22);
            this.imprumturi.Text = "Î&mprumturi";
            this.imprumturi.Click += new EventHandler(this.ImprumuturiToolStripMenuItem_Click);

            // separatorMenu2
            this.separatorMenu2.Name = "separatorMenu2";
            this.separatorMenu2.Size = new System.Drawing.Size(147, 6);

            // iesire
            this.iesire.Name = "iesire";
            this.iesire.Size = new System.Drawing.Size(150, 22);
            this.iesire.Text = "&Ieșire";
            this.iesire.Click += new EventHandler(this.IesireToolStripMenuItem_Click);

            // rapoarteToolStripMenuItem
            this.rapoarteToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.raportIntarziate,
                this.separatorMenu3});
            this.rapoarteToolStripMenuItem.Name = "rapoarteToolStripMenuItem";
            this.rapoarteToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.rapoarteToolStripMenuItem.Text = "&Rapoarte";

            // raportIntarziate
            this.raportIntarziate.Name = "raportIntarziate";
            this.raportIntarziate.Size = new System.Drawing.Size(200, 22);
            this.raportIntarziate.Text = "Cărți Î&ntârziate";
            this.raportIntarziate.Click += new EventHandler(this.RaportIntarziateToolStripMenuItem_Click);

            // separatorMenu3
            this.separatorMenu3.Name = "separatorMenu3";
            this.separatorMenu3.Size = new System.Drawing.Size(197, 6);

            // statusStrip
            this.statusStrip.Items.AddRange(new ToolStripItem[] {
                this.statusLabelUser,
                this.statusSpring,
                this.statusLabelDate});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 1;

            // statusLabelUser
            this.statusLabelUser.Name = "statusLabelUser";
            this.statusLabelUser.Text = "Utilizator: -";

            // statusSpring
            this.statusSpring.Name = "statusSpring";
            this.statusSpring.Spring = true;

            // statusLabelDate
            this.statusLabelDate.Name = "statusLabelDate";
            this.statusLabelDate.Text = "";

            // FrmMain
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmMain";
            this.Text = "Biblioteca Școlară - Sistem de Gestionare";
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private MenuStrip menuStrip;
        private ToolStripMenuItem administrareToolStripMenuItem;
        private ToolStripMenuItem autoToolStripMenuItem;
        private ToolStripMenuItem editori;
        private ToolStripMenuItem categorii;
        private ToolStripSeparator separatorMenu1;
        private ToolStripMenuItem carti;
        private ToolStripMenuItem exemplare;
        private ToolStripMenuItem managementToolStripMenuItem;
        private ToolStripMenuItem elevi;
        private ToolStripMenuItem imprumturi;
        private ToolStripSeparator separatorMenu2;
        private ToolStripMenuItem rapoarteToolStripMenuItem;
        private ToolStripMenuItem raportIntarziate;
        private ToolStripSeparator separatorMenu3;
        private ToolStripMenuItem iesire;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabelUser;
        private ToolStripStatusLabel statusSpring;
        private ToolStripStatusLabel statusLabelDate;
    }
}

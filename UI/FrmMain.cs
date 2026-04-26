using System;
using System.Windows.Forms;
using BibliotecaScolara.Database;
using BibliotecaScolara.Utilities;
using BibliotecaScolara.UI;

namespace BibliotecaScolara
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            TestDatabase();
        }

        private void TestDatabase()
        {
            if (!DatabaseConnection.TestConnection())
            {
                Mesaje.Eroare("Nu s-a putut conecta la baza de date!\n\nVerifică configurația din App.config");
                Application.Exit();
            }
        }

        private void BtnAutori_Click(object sender, EventArgs e)
        {
            FrmAutori frm = new FrmAutori();
            frm.ShowDialog();
        }

        private void BtnEdituri_Click(object sender, EventArgs e)
        {
            FrmEdituri frm = new FrmEdituri();
            frm.ShowDialog();
        }

        private void BtnCarti_Click(object sender, EventArgs e)
        {
            FrmCarti frm = new FrmCarti();
            frm.ShowDialog();
        }

        private void BtnElevi_Click(object sender, EventArgs e)
        {
            FrmElevi frm = new FrmElevi();
            frm.ShowDialog();
        }

        private void BtnImprumturi_Click(object sender, EventArgs e)
        {
            FrmImprumturi frm = new FrmImprumturi();
            frm.ShowDialog();
        }

        private void BtnStatistici_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = ImprumutManager.GetStatistics();
                if (dt.Rows.Count > 0)
                {
                    string message = $"Statistici Biblioteca:\n\n" +
                                   $"Total Împrumuturi: {dt.Rows[0]["TotalImprumturi"]}\n" +
                                   $"Împrumuturi Active: {dt.Rows[0]["ImprumturiActive"]}\n" +
                                   $"Împrumuturi Întârziate: {dt.Rows[0]["ImprumturiIntarziate"]}\n" +
                                   $"Împrumuturi Încheiata: {dt.Rows[0]["ImprumturiIncheiata"]}";
                    Mesaje.Informare(message, "Statistici");
                }
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare: " + ex.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (Mesaje.Confirmare("Doriți să ieșiți din aplicație?") == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void InitializeComponent()
        {
            this.btnAutori = new Button();
            this.btnEdituri = new Button();
            this.btnCarti = new Button();
            this.btnElevi = new Button();
            this.btnImprumturi = new Button();
            this.btnStatistici = new Button();
            this.btnExit = new Button();
            this.lblTitle = new Label();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(244, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Biblioteca Școlară v1.0";

            this.btnAutori.Location = new System.Drawing.Point(50, 70);
            this.btnAutori.Name = "btnAutori";
            this.btnAutori.Size = new System.Drawing.Size(150, 50);
            this.btnAutori.TabIndex = 1;
            this.btnAutori.Text = "Gestionare Autori";
            this.btnAutori.UseVisualStyleBackColor = true;
            this.btnAutori.Click += new EventHandler(this.BtnAutori_Click);

            this.btnEdituri.Location = new System.Drawing.Point(220, 70);
            this.btnEdituri.Name = "btnEdituri";
            this.btnEdituri.Size = new System.Drawing.Size(150, 50);
            this.btnEdituri.TabIndex = 2;
            this.btnEdituri.Text = "Gestionare Edituri";
            this.btnEdituri.UseVisualStyleBackColor = true;
            this.btnEdituri.Click += new EventHandler(this.BtnEdituri_Click);

            this.btnCarti.Location = new System.Drawing.Point(50, 140);
            this.btnCarti.Name = "btnCarti";
            this.btnCarti.Size = new System.Drawing.Size(150, 50);
            this.btnCarti.TabIndex = 3;
            this.btnCarti.Text = "Gestionare Cărți";
            this.btnCarti.UseVisualStyleBackColor = true;
            this.btnCarti.Click += new EventHandler(this.BtnCarti_Click);

            this.btnElevi.Location = new System.Drawing.Point(220, 140);
            this.btnElevi.Name = "btnElevi";
            this.btnElevi.Size = new System.Drawing.Size(150, 50);
            this.btnElevi.TabIndex = 4;
            this.btnElevi.Text = "Gestionare Elevi";
            this.btnElevi.UseVisualStyleBackColor = true;
            this.btnElevi.Click += new EventHandler(this.BtnElevi_Click);

            this.btnImprumturi.Location = new System.Drawing.Point(50, 210);
            this.btnImprumturi.Name = "btnImprumturi";
            this.btnImprumturi.Size = new System.Drawing.Size(150, 50);
            this.btnImprumturi.TabIndex = 5;
            this.btnImprumturi.Text = "Gestionare Împrumuturi";
            this.btnImprumturi.UseVisualStyleBackColor = true;
            this.btnImprumturi.Click += new EventHandler(this.BtnImprumturi_Click);

            this.btnStatistici.Location = new System.Drawing.Point(220, 210);
            this.btnStatistici.Name = "btnStatistici";
            this.btnStatistici.Size = new System.Drawing.Size(150, 50);
            this.btnStatistici.TabIndex = 6;
            this.btnStatistici.Text = "Statistici";
            this.btnStatistici.UseVisualStyleBackColor = true;
            this.btnStatistici.Click += new EventHandler(this.BtnStatistici_Click);

            this.btnExit.Location = new System.Drawing.Point(135, 280);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 50);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Ieși din Aplicație";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new EventHandler(this.BtnExit_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 360);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStatistici);
            this.Controls.Add(this.btnImprumturi);
            this.Controls.Add(this.btnElevi);
            this.Controls.Add(this.btnCarti);
            this.Controls.Add(this.btnEdituri);
            this.Controls.Add(this.btnAutori);
            this.Controls.Add(this.lblTitle);
            this.Name = "FrmMain";
            this.Text = "Biblioteca Școlară";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button btnAutori;
        private Button btnEdituri;
        private Button btnCarti;
        private Button btnElevi;
        private Button btnImprumturi;
        private Button btnStatistici;
        private Button btnExit;
        private Label lblTitle;
    }
}
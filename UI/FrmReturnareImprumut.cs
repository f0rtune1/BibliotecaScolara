using System;
using System.Windows.Forms;
using BibliotecaScolara.Models;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmReturnareImprumut : Form
    {
        private Imprumut imprumut;

        public FrmReturnareImprumut(Imprumut imp)
        {
            InitializeComponent();
            imprumut = imp;
            PopulateForm();
        }

        private void PopulateForm()
        {
            lblElev.Text = $"Elev: {imprumut.NumeElev}";
            lblCarte.Text = $"Carte: {imprumut.TitluCarte}";
            lblDataImprumut.Text = $"Data Împrumut: {imprumut.DataImprumut:dd.MM.yyyy}";
            lblDataScadenta.Text = $"Data Scadență: {imprumut.DataScadenta:dd.MM.yyyy}";
            
            int zileIntarziere = DateTime.Today > imprumut.DataScadenta ? (DateTime.Today - imprumut.DataScadenta).Days : 0;
            if (zileIntarziere > 0)
                lblIntarziere.Text = $"Zile de Întârziere: {zileIntarziere}";
            else
                lblIntarziere.Text = "Carte returnată la timp!";
        }

        private void BtnConfirma_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImprumutManager.ReturnBook(imprumut.IDImprumut, txtObservatii.Text))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    Mesaje.Eroare("Nu s-a putut înregistra returnarea!");
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare: " + ex.Message);
            }
        }

        private void BtnAnuleaza_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.lblElev = new Label();
            this.lblCarte = new Label();
            this.lblDataImprumut = new Label();
            this.lblDataScadenta = new Label();
            this.lblIntarziere = new Label();
            this.lblObservatii = new Label();
            this.txtObservatii = new TextBox();
            this.btnConfirma = new Button();
            this.btnAnuleaza = new Button();
            this.SuspendLayout();

            this.lblElev.AutoSize = true;
            this.lblElev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblElev.Location = new System.Drawing.Point(12, 15);
            this.lblElev.Name = "lblElev";
            this.lblElev.Size = new System.Drawing.Size(38, 15);
            this.lblElev.TabIndex = 0;
            this.lblElev.Text = "Elev:";

            this.lblCarte.AutoSize = true;
            this.lblCarte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCarte.Location = new System.Drawing.Point(12, 40);
            this.lblCarte.Name = "lblCarte";
            this.lblCarte.Size = new System.Drawing.Size(38, 15);
            this.lblCarte.TabIndex = 1;
            this.lblCarte.Text = "Carte:";

            this.lblDataImprumut.AutoSize = true;
            this.lblDataImprumut.Location = new System.Drawing.Point(12, 65);
            this.lblDataImprumut.Name = "lblDataImprumut";
            this.lblDataImprumut.Size = new System.Drawing.Size(81, 13);
            this.lblDataImprumut.TabIndex = 2;
            this.lblDataImprumut.Text = "Data Împrumut:";

            this.lblDataScadenta.AutoSize = true;
            this.lblDataScadenta.Location = new System.Drawing.Point(12, 90);
            this.lblDataScadenta.Name = "lblDataScadenta";
            this.lblDataScadenta.Size = new System.Drawing.Size(81, 13);
            this.lblDataScadenta.TabIndex = 3;
            this.lblDataScadenta.Text = "Data Scadență:";

            this.lblIntarziere.AutoSize = true;
            this.lblIntarziere.ForeColor = System.Drawing.Color.Red;
            this.lblIntarziere.Location = new System.Drawing.Point(12, 115);
            this.lblIntarziere.Name = "lblIntarziere";
            this.lblIntarziere.Size = new System.Drawing.Size(100, 13);
            this.lblIntarziere.TabIndex = 4;
            this.lblIntarziere.Text = "Zile Întârziere: 0";

            this.lblObservatii.AutoSize = true;
            this.lblObservatii.Location = new System.Drawing.Point(12, 140);
            this.lblObservatii.Name = "lblObservatii";
            this.lblObservatii.Size = new System.Drawing.Size(65, 13);
            this.lblObservatii.TabIndex = 5;
            this.lblObservatii.Text = "Observații:";

            this.txtObservatii.Location = new System.Drawing.Point(12, 156);
            this.txtObservatii.Multiline = true;
            this.txtObservatii.Name = "txtObservatii";
            this.txtObservatii.Size = new System.Drawing.Size(340, 80);
            this.txtObservatii.TabIndex = 6;

            this.btnConfirma.Location = new System.Drawing.Point(197, 250);
            this.btnConfirma.Name = "btnConfirma";
            this.btnConfirma.Size = new System.Drawing.Size(75, 23);
            this.btnConfirma.TabIndex = 7;
            this.btnConfirma.Text = "Confirma";
            this.btnConfirma.UseVisualStyleBackColor = true;
            this.btnConfirma.Click += new EventHandler(this.BtnConfirma_Click);

            this.btnAnuleaza.Location = new System.Drawing.Point(278, 250);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 23);
            this.btnAnuleaza.TabIndex = 8;
            this.btnAnuleaza.Text = "Anuleaza";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 290);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnConfirma);
            this.Controls.Add(this.txtObservatii);
            this.Controls.Add(this.lblObservatii);
            this.Controls.Add(this.lblIntarziere);
            this.Controls.Add(this.lblDataScadenta);
            this.Controls.Add(this.lblDataImprumut);
            this.Controls.Add(this.lblCarte);
            this.Controls.Add(this.lblElev);
            this.Name = "FrmReturnareImprumut";
            this.Text = "Returnare Carte";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblElev;
        private Label lblCarte;
        private Label lblDataImprumut;
        private Label lblDataScadenta;
        private Label lblIntarziere;
        private Label lblObservatii;
        private TextBox txtObservatii;
        private Button btnConfirma;
        private Button btnAnuleaza;
    }
}
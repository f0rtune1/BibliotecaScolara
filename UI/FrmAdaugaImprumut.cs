using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmAdaugaImprumut : Form
    {
        private List<Elev> eleviList;
        private List<Carte> cartiList;
        private List<Exemplar> exemplareList;

        public FrmAdaugaImprumut()
        {
            InitializeComponent();
            ApplyTheme();
            LoadData();
        }

        private void ApplyTheme()
        {
            ThemeHelper.ApplyTheme(this);
            ThemeHelper.StyleButtonAdd(btnSalveaza);
            ThemeHelper.StyleButtonNeutral(btnAnuleaza);
        }

        private void LoadData()
        {
            try
            {
                eleviList = ElevManager.GetActive();
                cartiList = CarteManager.GetAll();

                cmbElev.DataSource = eleviList;
                cmbElev.DisplayMember = "NumeComplet";
                cmbElev.ValueMember = "IDElev";

                cmbCarte.DataSource = cartiList;
                cmbCarte.DisplayMember = "Titlu";
                cmbCarte.ValueMember = "IDCarte";
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la încărcarea datelor: " + ex.Message);
            }
        }

        private void CmbCarte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCarte.SelectedValue != null && int.TryParse(cmbCarte.SelectedValue.ToString(), out int carteId))
            {
                exemplareList = ExemplarManager.GetAvailableForCarte(carteId);
                cmbExemplar.DataSource = exemplareList;
                cmbExemplar.DisplayMember = "CoduInventar";
                cmbExemplar.ValueMember = "IDExemplar";
            }
        }

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (!Valideaza())
                return;

            try
            {
                var imprumut = new Imprumut
                {
                    IDElev = (int)cmbElev.SelectedValue,
                    IDExemplar = (int)cmbExemplar.SelectedValue,
                    DataImprumut = DateTime.Today
                };

                if (ImprumutManager.Insert(imprumut))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    Mesaje.Eroare("Nu s-a putut înregistra împrumutul!");
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

        private bool Valideaza()
        {
            if (cmbElev.SelectedValue == null)
            {
                Mesaje.Validare("Selectați un elev!");
                return false;
            }

            if (cmbCarte.SelectedValue == null)
            {
                Mesaje.Validare("Selectați o carte!");
                return false;
            }

            if (cmbExemplar.SelectedValue == null)
            {
                Mesaje.Validare("Nu sunt exemplare disponibile pentru această carte!");
                return false;
            }

            return true;
        }

        private void InitializeComponent()
        {
            this.lblElev = new Label();
            this.cmbElev = new ComboBox();
            this.lblCarte = new Label();
            this.cmbCarte = new ComboBox();
            this.lblExemplar = new Label();
            this.cmbExemplar = new ComboBox();
            this.btnSalveaza = new Button();
            this.btnAnuleaza = new Button();
            this.SuspendLayout();

            this.lblElev.AutoSize = true;
            this.lblElev.Location = new System.Drawing.Point(12, 15);
            this.lblElev.Name = "lblElev";
            this.lblElev.Size = new System.Drawing.Size(39, 13);
            this.lblElev.TabIndex = 0;
            this.lblElev.Text = "Elev:";

            this.cmbElev.FormattingEnabled = true;
            this.cmbElev.Location = new System.Drawing.Point(100, 12);
            this.cmbElev.Name = "cmbElev";
            this.cmbElev.Size = new System.Drawing.Size(250, 21);
            this.cmbElev.TabIndex = 1;

            this.lblCarte.AutoSize = true;
            this.lblCarte.Location = new System.Drawing.Point(12, 45);
            this.lblCarte.Name = "lblCarte";
            this.lblCarte.Size = new System.Drawing.Size(39, 13);
            this.lblCarte.TabIndex = 2;
            this.lblCarte.Text = "Carte:";

            this.cmbCarte.FormattingEnabled = true;
            this.cmbCarte.Location = new System.Drawing.Point(100, 42);
            this.cmbCarte.Name = "cmbCarte";
            this.cmbCarte.Size = new System.Drawing.Size(250, 21);
            this.cmbCarte.TabIndex = 3;
            this.cmbCarte.SelectedIndexChanged += new EventHandler(this.CmbCarte_SelectedIndexChanged);

            this.lblExemplar.AutoSize = true;
            this.lblExemplar.Location = new System.Drawing.Point(12, 75);
            this.lblExemplar.Name = "lblExemplar";
            this.lblExemplar.Size = new System.Drawing.Size(63, 13);
            this.lblExemplar.TabIndex = 4;
            this.lblExemplar.Text = "Exemplar:";

            this.cmbExemplar.FormattingEnabled = true;
            this.cmbExemplar.Location = new System.Drawing.Point(100, 72);
            this.cmbExemplar.Name = "cmbExemplar";
            this.cmbExemplar.Size = new System.Drawing.Size(250, 21);
            this.cmbExemplar.TabIndex = 5;

            this.btnSalveaza.Location = new System.Drawing.Point(100, 110);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(75, 23);
            this.btnSalveaza.TabIndex = 6;
            this.btnSalveaza.Text = "Imprumuta";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new EventHandler(this.BtnSalveaza_Click);

            this.btnAnuleaza.Location = new System.Drawing.Point(181, 110);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 23);
            this.btnAnuleaza.TabIndex = 7;
            this.btnAnuleaza.Text = "Anuleaza";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 150);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.cmbExemplar);
            this.Controls.Add(this.lblExemplar);
            this.Controls.Add(this.cmbCarte);
            this.Controls.Add(this.lblCarte);
            this.Controls.Add(this.cmbElev);
            this.Controls.Add(this.lblElev);
            this.Name = "FrmAdaugaImprumut";
            this.Text = "Înregistrare Împrumut Nou";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblElev;
        private ComboBox cmbElev;
        private Label lblCarte;
        private ComboBox cmbCarte;
        private Label lblExemplar;
        private ComboBox cmbExemplar;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}
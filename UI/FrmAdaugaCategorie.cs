using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmAdaugaCategorie : Form
    {
        private Categorie categorie;

        public FrmAdaugaCategorie(Categorie catEdit = null)
        {
            InitializeComponent();
            ApplyTheme();
            categorie = catEdit ?? new Categorie();
            if (catEdit != null)
            {
                PopulateForm();
                this.Text = "Editare Categorie";
            }
        }

        private void ApplyTheme()
        {
            ThemeHelper.ApplyTheme(this);
            ThemeHelper.StyleButtonPrimary(btnSalveaza);
            ThemeHelper.StyleButtonNeutral(btnAnuleaza);
        }

        private void PopulateForm()
        {
            txtNume.Text = categorie.NumeCategorie;
            txtDescriere.Text = categorie.Descriere;
        }

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (!Valideaza())
                return;

            categorie.NumeCategorie = txtNume.Text.Trim();
            categorie.Descriere = txtDescriere.Text.Trim();

            try
            {
                bool succes;
                if (categorie.IDCategorie == 0)
                    succes = CategorieManager.Insert(categorie);
                else
                    succes = CategorieManager.Update(categorie);

                if (succes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    Mesaje.Eroare("Operatia nu a reusit!");
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
            if (Validari.EsteGol(txtNume.Text))
            {
                Mesaje.Validare("Introduceti numele categoriei!");
                txtNume.Focus();
                return false;
            }

            if (!Validari.ValidareLungime(txtNume.Text, 2, 100))
            {
                Mesaje.Validare("Numele categoriei trebuie sa aiba intre 2 si 100 caractere!");
                return false;
            }

            return true;
        }

        private void InitializeComponent()
        {
            this.lblNume = new Label();
            this.txtNume = new TextBox();
            this.lblDescriere = new Label();
            this.txtDescriere = new TextBox();
            this.btnSalveaza = new Button();
            this.btnAnuleaza = new Button();
            this.SuspendLayout();

            this.lblNume.AutoSize = true;
            this.lblNume.Location = new System.Drawing.Point(12, 15);
            this.lblNume.Name = "lblNume";
            this.lblNume.Size = new System.Drawing.Size(38, 13);
            this.lblNume.TabIndex = 0;
            this.lblNume.Text = "Nume:";

            this.txtNume.Location = new System.Drawing.Point(100, 12);
            this.txtNume.Name = "txtNume";
            this.txtNume.Size = new System.Drawing.Size(250, 20);
            this.txtNume.TabIndex = 1;

            this.lblDescriere.AutoSize = true;
            this.lblDescriere.Location = new System.Drawing.Point(12, 45);
            this.lblDescriere.Name = "lblDescriere";
            this.lblDescriere.Size = new System.Drawing.Size(64, 13);
            this.lblDescriere.TabIndex = 2;
            this.lblDescriere.Text = "Descriere:";

            this.txtDescriere.Location = new System.Drawing.Point(100, 42);
            this.txtDescriere.Multiline = true;
            this.txtDescriere.Name = "txtDescriere";
            this.txtDescriere.Size = new System.Drawing.Size(250, 60);
            this.txtDescriere.TabIndex = 3;

            this.btnSalveaza.Location = new System.Drawing.Point(100, 115);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(75, 23);
            this.btnSalveaza.TabIndex = 4;
            this.btnSalveaza.Text = "Salveaza";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new EventHandler(this.BtnSalveaza_Click);

            this.btnAnuleaza.Location = new System.Drawing.Point(181, 115);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 23);
            this.btnAnuleaza.TabIndex = 5;
            this.btnAnuleaza.Text = "Anuleaza";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 155);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.txtDescriere);
            this.Controls.Add(this.lblDescriere);
            this.Controls.Add(this.txtNume);
            this.Controls.Add(this.lblNume);
            this.Name = "FrmAdaugaCategorie";
            this.Text = "Adauga Categorie";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblNume;
        private TextBox txtNume;
        private Label lblDescriere;
        private TextBox txtDescriere;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}

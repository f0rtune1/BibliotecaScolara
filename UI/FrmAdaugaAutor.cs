using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmAdaugaAutor : Form
    {
        private Autor autor;

        public FrmAdaugaAutor(Autor autorEdit = null)
        {
            InitializeComponent();
            ApplyTheme();
            autor = autorEdit ?? new Autor();
            if (autorEdit != null)
            {
                PopulateForm();
                this.Text = "Editare Autor";
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
            txtNume.Text = autor.Nume;
            txtPrenume.Text = autor.Prenume;
            txtNationalitate.Text = autor.Nationalitate;
            if (autor.DataNasterii.HasValue)
                dateTimePickerData.Value = autor.DataNasterii.Value;
            txtBiografie.Text = autor.BiografieBrieșă;
        }

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (!Valideaza())
                return;

            autor.Nume = txtNume.Text.Trim();
            autor.Prenume = txtPrenume.Text.Trim();
            autor.Nationalitate = txtNationalitate.Text.Trim();
            autor.DataNasterii = dateTimePickerData.Checked ? (DateTime?)dateTimePickerData.Value : null;
            autor.BiografieBrieșă = txtBiografie.Text.Trim();

            try
            {
                bool succes;
                if (autor.IDAutor == 0)
                    succes = AutorManager.Insert(autor);
                else
                    succes = AutorManager.Update(autor);

                if (succes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    Mesaje.Eroare("Operația nu a reușit!");
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
                Mesaje.Validare("Introduceti numele autorului!");
                txtNume.Focus();
                return false;
            }

            if (Validari.EsteGol(txtPrenume.Text))
            {
                Mesaje.Validare("Introduceti prenumele autorului!");
                txtPrenume.Focus();
                return false;
            }

            if (!Validari.ValidareLungime(txtNume.Text, 2, 100))
            {
                Mesaje.Validare("Numele trebuie sa aiba intre 2 si 100 caractere!");
                return false;
            }

            if (!Validari.ValidareLungime(txtPrenume.Text, 2, 100))
            {
                Mesaje.Validare("Prenumele trebuie sa aiba intre 2 si 100 caractere!");
                return false;
            }

            return true;
        }

        private void InitializeComponent()
        {
            this.lblNume = new Label();
            this.txtNume = new TextBox();
            this.lblPrenume = new Label();
            this.txtPrenume = new TextBox();
            this.lblData = new Label();
            this.dateTimePickerData = new DateTimePicker();
            this.lblNationalitate = new Label();
            this.txtNationalitate = new TextBox();
            this.lblBiografie = new Label();
            this.txtBiografie = new TextBox();
            this.btnSalveaza = new Button();
            this.btnAnuleaza = new Button();
            this.SuspendLayout();

            // lblNume
            this.lblNume.AutoSize = true;
            this.lblNume.Location = new System.Drawing.Point(12, 15);
            this.lblNume.Name = "lblNume";
            this.lblNume.Size = new System.Drawing.Size(38, 13);
            this.lblNume.TabIndex = 0;
            this.lblNume.Text = "Nume:";

            // txtNume
            this.txtNume.Location = new System.Drawing.Point(100, 12);
            this.txtNume.Name = "txtNume";
            this.txtNume.Size = new System.Drawing.Size(250, 20);
            this.txtNume.TabIndex = 1;

            // lblPrenume
            this.lblPrenume.AutoSize = true;
            this.lblPrenume.Location = new System.Drawing.Point(12, 45);
            this.lblPrenume.Name = "lblPrenume";
            this.lblPrenume.Size = new System.Drawing.Size(59, 13);
            this.lblPrenume.TabIndex = 2;
            this.lblPrenume.Text = "Prenume:";

            // txtPrenume
            this.txtPrenume.Location = new System.Drawing.Point(100, 42);
            this.txtPrenume.Name = "txtPrenume";
            this.txtPrenume.Size = new System.Drawing.Size(250, 20);
            this.txtPrenume.TabIndex = 3;

            // lblData
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(12, 75);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(84, 13);
            this.lblData.TabIndex = 4;
            this.lblData.Text = "Data nasterii:";

            // dateTimePickerData
            this.dateTimePickerData.Location = new System.Drawing.Point(100, 72);
            this.dateTimePickerData.Name = "dateTimePickerData";
            this.dateTimePickerData.Size = new System.Drawing.Size(250, 20);
            this.dateTimePickerData.TabIndex = 5;
            this.dateTimePickerData.Checked = false;
            this.dateTimePickerData.ShowCheckBox = true;

            // lblNationalitate
            this.lblNationalitate.AutoSize = true;
            this.lblNationalitate.Location = new System.Drawing.Point(12, 105);
            this.lblNationalitate.Name = "lblNationalitate";
            this.lblNationalitate.Size = new System.Drawing.Size(73, 13);
            this.lblNationalitate.TabIndex = 6;
            this.lblNationalitate.Text = "Nationalitate:";

            // txtNationalitate
            this.txtNationalitate.Location = new System.Drawing.Point(100, 102);
            this.txtNationalitate.Name = "txtNationalitate";
            this.txtNationalitate.Size = new System.Drawing.Size(250, 20);
            this.txtNationalitate.TabIndex = 7;

            // lblBiografie
            this.lblBiografie.AutoSize = true;
            this.lblBiografie.Location = new System.Drawing.Point(12, 135);
            this.lblBiografie.Name = "lblBiografie";
            this.lblBiografie.Size = new System.Drawing.Size(81, 13);
            this.lblBiografie.TabIndex = 8;
            this.lblBiografie.Text = "Biografie brieșă:";

            // txtBiografie
            this.txtBiografie.Location = new System.Drawing.Point(100, 132);
            this.txtBiografie.Multiline = true;
            this.txtBiografie.Name = "txtBiografie";
            this.txtBiografie.Size = new System.Drawing.Size(250, 80);
            this.txtBiografie.TabIndex = 9;

            // btnSalveaza
            this.btnSalveaza.Location = new System.Drawing.Point(100, 220);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(75, 23);
            this.btnSalveaza.TabIndex = 10;
            this.btnSalveaza.Text = "Salveaza";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new EventHandler(this.BtnSalveaza_Click);

            // btnAnuleaza
            this.btnAnuleaza.Location = new System.Drawing.Point(181, 220);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 23);
            this.btnAnuleaza.TabIndex = 11;
            this.btnAnuleaza.Text = "Anuleaza";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            // FrmAdaugaAutor
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 260);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.txtBiografie);
            this.Controls.Add(this.lblBiografie);
            this.Controls.Add(this.txtNationalitate);
            this.Controls.Add(this.lblNationalitate);
            this.Controls.Add(this.dateTimePickerData);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.txtPrenume);
            this.Controls.Add(this.lblPrenume);
            this.Controls.Add(this.txtNume);
            this.Controls.Add(this.lblNume);
            this.Name = "FrmAdaugaAutor";
            this.Text = "Adauga Autor";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblNume;
        private TextBox txtNume;
        private Label lblPrenume;
        private TextBox txtPrenume;
        private Label lblData;
        private DateTimePicker dateTimePickerData;
        private Label lblNationalitate;
        private TextBox txtNationalitate;
        private Label lblBiografie;
        private TextBox txtBiografie;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}
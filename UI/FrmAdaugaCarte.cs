using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmAdaugaCarte : Form
    {
        private Carte carte;
        private List<Autor> autoriList;
        private List<Editura> edituriList;
        private List<Categorie> categoriiList;

        public FrmAdaugaCarte(List<Autor> autori, List<Editura> edituri, List<Categorie> categorii, Carte carteEdit = null)
        {
            InitializeComponent();
            autoriList = autori;
            edituriList = edituri;
            categoriiList = categorii;
            carte = carteEdit ?? new Carte();
            LoadComboBoxes();
            if (carteEdit != null)
            {
                PopulateForm();
                this.Text = "Editare Carte";
            }
        }

        private void LoadComboBoxes()
        {
            cmbAutor.DataSource = autoriList;
            cmbAutor.DisplayMember = "Prenume";
            cmbAutor.ValueMember = "IDAutor";

            cmbEditura.DataSource = edituriList;
            cmbEditura.DisplayMember = "NumeEditura";
            cmbEditura.ValueMember = "IDEditura";

            cmbCategorie.DataSource = categoriiList;
            cmbCategorie.DisplayMember = "NumeCategorie";
            cmbCategorie.ValueMember = "IDCategorie";
        }

        private void PopulateForm()
        {
            txtTitlu.Text = carte.Titlu;
            cmbAutor.SelectedValue = carte.IDAutor;
            cmbEditura.SelectedValue = carte.IDEditura;
            cmbCategorie.SelectedValue = carte.IDCategorie;
            if (carte.AnPublicarii.HasValue)
                nudAn.Value = carte.AnPublicarii.Value;
            txtISBN.Text = carte.ISBN;
            if (carte.NrPagini.HasValue)
                nudPagini.Value = carte.NrPagini.Value;
        }

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (!Valideaza())
                return;

            carte.Titlu = txtTitlu.Text.Trim();
            carte.IDAutor = (int)cmbAutor.SelectedValue;
            carte.IDEditura = (int)cmbEditura.SelectedValue;
            carte.IDCategorie = (int)cmbCategorie.SelectedValue;
            carte.AnPublicarii = (int)nudAn.Value == 0 ? null : (int?)nudAn.Value;
            carte.ISBN = txtISBN.Text.Trim();
            carte.NrPagini = (int)nudPagini.Value == 0 ? null : (int?)nudPagini.Value;

            try
            {
                bool succes;
                if (carte.IDCarte == 0)
                    succes = CarteManager.Insert(carte);
                else
                    succes = CarteManager.Update(carte);

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
            if (Validari.EsteGol(txtTitlu.Text))
            {
                Mesaje.Validare("Introduceți titlul cărții!");
                txtTitlu.Focus();
                return false;
            }

            if (!Validari.ValidareLungime(txtTitlu.Text, 3, 200))
            {
                Mesaje.Validare("Titlul trebuie să aibă între 3 și 200 caractere!");
                return false;
            }

            if (cmbAutor.SelectedValue == null)
            {
                Mesaje.Validare("Selectați un autor!");
                return false;
            }

            if (cmbEditura.SelectedValue == null)
            {
                Mesaje.Validare("Selectați o editură!");
                return false;
            }

            if (cmbCategorie.SelectedValue == null)
            {
                Mesaje.Validare("Selectați o categorie!");
                return false;
            }

            if (!string.IsNullOrEmpty(txtISBN.Text) && !Validari.ValidareISBN(txtISBN.Text))
            {
                Mesaje.Validare("ISBN invalid!");
                return false;
            }

            if ((int)nudAn.Value != 0 && !Validari.ValidareAnPublicare((int)nudAn.Value))
            {
                Mesaje.Validare("Anul de publicare trebuie să fie între 1000 și anul curent!");
                return false;
            }

            if ((int)nudPagini.Value != 0 && !Validari.ValidareNrPagini((int)nudPagini.Value))
            {
                Mesaje.Validare("Numărul de pagini trebuie să fie între 1 și 9999!");
                return false;
            }

            return true;
        }

        private void InitializeComponent()
        {
            this.lblTitlu = new Label();
            this.txtTitlu = new TextBox();
            this.lblAutor = new Label();
            this.cmbAutor = new ComboBox();
            this.lblEditura = new Label();
            this.cmbEditura = new ComboBox();
            this.lblCategorie = new Label();
            this.cmbCategorie = new ComboBox();
            this.lblAn = new Label();
            this.nudAn = new NumericUpDown();
            this.lblISBN = new Label();
            this.txtISBN = new TextBox();
            this.lblPagini = new Label();
            this.nudPagini = new NumericUpDown();
            this.btnSalveaza = new Button();
            this.btnAnuleaza = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPagini)).BeginInit();
            this.SuspendLayout();

            this.lblTitlu.AutoSize = true;
            this.lblTitlu.Location = new System.Drawing.Point(12, 15);
            this.lblTitlu.Name = "lblTitlu";
            this.lblTitlu.Size = new System.Drawing.Size(39, 13);
            this.lblTitlu.TabIndex = 0;
            this.lblTitlu.Text = "Titlu:";

            this.txtTitlu.Location = new System.Drawing.Point(100, 12);
            this.txtTitlu.Name = "txtTitlu";
            this.txtTitlu.Size = new System.Drawing.Size(250, 20);
            this.txtTitlu.TabIndex = 1;

            this.lblAutor.AutoSize = true;
            this.lblAutor.Location = new System.Drawing.Point(12, 45);
            this.lblAutor.Name = "lblAutor";
            this.lblAutor.Size = new System.Drawing.Size(39, 13);
            this.lblAutor.TabIndex = 2;
            this.lblAutor.Text = "Autor:";

            this.cmbAutor.FormattingEnabled = true;
            this.cmbAutor.Location = new System.Drawing.Point(100, 42);
            this.cmbAutor.Name = "cmbAutor";
            this.cmbAutor.Size = new System.Drawing.Size(250, 21);
            this.cmbAutor.TabIndex = 3;

            this.lblEditura.AutoSize = true;
            this.lblEditura.Location = new System.Drawing.Point(12, 75);
            this.lblEditura.Name = "lblEditura";
            this.lblEditura.Size = new System.Drawing.Size(48, 13);
            this.lblEditura.TabIndex = 4;
            this.lblEditura.Text = "Editură:";

            this.cmbEditura.FormattingEnabled = true;
            this.cmbEditura.Location = new System.Drawing.Point(100, 72);
            this.cmbEditura.Name = "cmbEditura";
            this.cmbEditura.Size = new System.Drawing.Size(250, 21);
            this.cmbEditura.TabIndex = 5;

            this.lblCategorie.AutoSize = true;
            this.lblCategorie.Location = new System.Drawing.Point(12, 105);
            this.lblCategorie.Name = "lblCategorie";
            this.lblCategorie.Size = new System.Drawing.Size(62, 13);
            this.lblCategorie.TabIndex = 6;
            this.lblCategorie.Text = "Categorie:";

            this.cmbCategorie.FormattingEnabled = true;
            this.cmbCategorie.Location = new System.Drawing.Point(100, 102);
            this.cmbCategorie.Name = "cmbCategorie";
            this.cmbCategorie.Size = new System.Drawing.Size(250, 21);
            this.cmbCategorie.TabIndex = 7;

            this.lblAn.AutoSize = true;
            this.lblAn.Location = new System.Drawing.Point(12, 135);
            this.lblAn.Name = "lblAn";
            this.lblAn.Size = new System.Drawing.Size(68, 13);
            this.lblAn.TabIndex = 8;
            this.lblAn.Text = "An publicare:";

            this.nudAn.Location = new System.Drawing.Point(100, 132);
            this.nudAn.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            this.nudAn.Name = "nudAn";
            this.nudAn.Size = new System.Drawing.Size(100, 20);
            this.nudAn.TabIndex = 9;

            this.lblISBN.AutoSize = true;
            this.lblISBN.Location = new System.Drawing.Point(12, 165);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(38, 13);
            this.lblISBN.TabIndex = 10;
            this.lblISBN.Text = "ISBN:";

            this.txtISBN.Location = new System.Drawing.Point(100, 162);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(250, 20);
            this.txtISBN.TabIndex = 11;

            this.lblPagini.AutoSize = true;
            this.lblPagini.Location = new System.Drawing.Point(12, 195);
            this.lblPagini.Name = "lblPagini";
            this.lblPagini.Size = new System.Drawing.Size(71, 13);
            this.lblPagini.TabIndex = 12;
            this.lblPagini.Text = "Nr. Pagini:";

            this.nudPagini.Location = new System.Drawing.Point(100, 192);
            this.nudPagini.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudPagini.Name = "nudPagini";
            this.nudPagini.Size = new System.Drawing.Size(100, 20);
            this.nudPagini.TabIndex = 13;

            this.btnSalveaza.Location = new System.Drawing.Point(100, 230);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(75, 23);
            this.btnSalveaza.TabIndex = 14;
            this.btnSalveaza.Text = "Salveaza";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new EventHandler(this.BtnSalveaza_Click);

            this.btnAnuleaza.Location = new System.Drawing.Point(181, 230);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 23);
            this.btnAnuleaza.TabIndex = 15;
            this.btnAnuleaza.Text = "Anuleaza";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 270);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.nudPagini);
            this.Controls.Add(this.lblPagini);
            this.Controls.Add(this.txtISBN);
            this.Controls.Add(this.lblISBN);
            this.Controls.Add(this.nudAn);
            this.Controls.Add(this.lblAn);
            this.Controls.Add(this.cmbCategorie);
            this.Controls.Add(this.lblCategorie);
            this.Controls.Add(this.cmbEditura);
            this.Controls.Add(this.lblEditura);
            this.Controls.Add(this.cmbAutor);
            this.Controls.Add(this.lblAutor);
            this.Controls.Add(this.txtTitlu);
            this.Controls.Add(this.lblTitlu);
            this.Name = "FrmAdaugaCarte";
            this.Text = "Adauga Carte";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.nudAn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPagini)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblTitlu;
        private TextBox txtTitlu;
        private Label lblAutor;
        private ComboBox cmbAutor;
        private Label lblEditura;
        private ComboBox cmbEditura;
        private Label lblCategorie;
        private ComboBox cmbCategorie;
        private Label lblAn;
        private NumericUpDown nudAn;
        private Label lblISBN;
        private TextBox txtISBN;
        private Label lblPagini;
        private NumericUpDown nudPagini;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}
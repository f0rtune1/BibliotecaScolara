using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmCategorii : Form
    {
        private List<Categorie> categoriiList;

        public FrmCategorii()
        {
            InitializeComponent();
            LoadCategorii();
        }

        private void LoadCategorii()
        {
            try
            {
                categoriiList = CategorieManager.GetAll();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la incarcarea categoriilor: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dataGridViewCategorii.DataSource = null;
            dataGridViewCategorii.DataSource = categoriiList;
            AdjustColumns();
        }

        private void AdjustColumns()
        {
            if (dataGridViewCategorii.Columns.Count > 0)
            {
                if (dataGridViewCategorii.Columns.Contains("IDCategorie"))
                    dataGridViewCategorii.Columns["IDCategorie"].Width = 60;
                if (dataGridViewCategorii.Columns.Contains("NumeCategorie"))
                    dataGridViewCategorii.Columns["NumeCategorie"].Width = 200;
                if (dataGridViewCategorii.Columns.Contains("Descriere"))
                    dataGridViewCategorii.Columns["Descriere"].Width = 400;
            }
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            FrmAdaugaCategorie frm = new FrmAdaugaCategorie();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCategorii();
                Mesaje.Succes("Categoria a fost adaugata cu succes!");
            }
        }

        private void BtnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridViewCategorii.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectati o categorie!");
                return;
            }

            Categorie cat = (Categorie)dataGridViewCategorii.SelectedRows[0].DataBoundItem;
            FrmAdaugaCategorie frm = new FrmAdaugaCategorie(cat);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCategorii();
                Mesaje.Succes("Categoria a fost actualizata cu succes!");
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewCategorii.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectati o categorie!");
                return;
            }

            Categorie cat = (Categorie)dataGridViewCategorii.SelectedRows[0].DataBoundItem;
            if (Mesaje.Confirmare("Doriti sa stergeti categoria '" + cat.NumeCategorie + "'?\nAtentie: nu puteti sterge o categorie care are carti asociate!") == DialogResult.Yes)
            {
                try
                {
                    if (CategorieManager.Delete(cat.IDCategorie))
                    {
                        LoadCategorii();
                        Mesaje.Succes("Categoria a fost stearsa cu succes!");
                    }
                    else
                        Mesaje.Eroare("Nu s-a putut sterge categoria!");
                }
                catch (Exception ex)
                {
                    Mesaje.Eroare("Eroare: " + ex.Message);
                }
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewCategorii = new DataGridView();
            this.btnAdauga = new Button();
            this.btnEditeaza = new Button();
            this.btnSterge = new Button();
            this.lblTitlu = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCategorii)).BeginInit();
            this.SuspendLayout();

            this.dataGridViewCategorii.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCategorii.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewCategorii.Name = "dataGridViewCategorii";
            this.dataGridViewCategorii.ReadOnly = true;
            this.dataGridViewCategorii.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCategorii.Size = new System.Drawing.Size(760, 340);
            this.dataGridViewCategorii.TabIndex = 0;

            this.lblTitlu.AutoSize = true;
            this.lblTitlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitlu.Location = new System.Drawing.Point(12, 15);
            this.lblTitlu.Name = "lblTitlu";
            this.lblTitlu.TabIndex = 1;
            this.lblTitlu.Text = "Categorii de carti";

            this.btnAdauga.Location = new System.Drawing.Point(12, 400);
            this.btnAdauga.Name = "btnAdauga";
            this.btnAdauga.Size = new System.Drawing.Size(75, 23);
            this.btnAdauga.TabIndex = 2;
            this.btnAdauga.Text = "Adauga";
            this.btnAdauga.UseVisualStyleBackColor = true;
            this.btnAdauga.Click += new EventHandler(this.BtnAdauga_Click);

            this.btnEditeaza.Location = new System.Drawing.Point(93, 400);
            this.btnEditeaza.Name = "btnEditeaza";
            this.btnEditeaza.Size = new System.Drawing.Size(75, 23);
            this.btnEditeaza.TabIndex = 3;
            this.btnEditeaza.Text = "Editeaza";
            this.btnEditeaza.UseVisualStyleBackColor = true;
            this.btnEditeaza.Click += new EventHandler(this.BtnEditeaza_Click);

            this.btnSterge.Location = new System.Drawing.Point(174, 400);
            this.btnSterge.Name = "btnSterge";
            this.btnSterge.Size = new System.Drawing.Size(75, 23);
            this.btnSterge.TabIndex = 4;
            this.btnSterge.Text = "Sterge";
            this.btnSterge.UseVisualStyleBackColor = true;
            this.btnSterge.Click += new EventHandler(this.BtnSterge_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 435);
            this.Controls.Add(this.btnSterge);
            this.Controls.Add(this.btnEditeaza);
            this.Controls.Add(this.btnAdauga);
            this.Controls.Add(this.lblTitlu);
            this.Controls.Add(this.dataGridViewCategorii);
            this.Name = "FrmCategorii";
            this.Text = "Gestionare Categorii";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCategorii)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dataGridViewCategorii;
        private Button btnAdauga;
        private Button btnEditeaza;
        private Button btnSterge;
        private Label lblTitlu;
    }
}

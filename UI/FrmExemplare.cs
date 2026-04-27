using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmExemplare : Form
    {
        private int carteId;
        private List<Exemplar> exemplareList;

        // No-arg constructor - shows all exemplars
        public FrmExemplare() : this(0) { }

        public FrmExemplare(int idCarte)
        {
            InitializeComponent();
            ApplyTheme();
            carteId = idCarte;
            LoadExemplare();
        }

        private void ApplyTheme()
        {
            ThemeHelper.ApplyTheme(this);
            ThemeHelper.StyleGrid(dataGridViewExemplare);
            ThemeHelper.StyleButtonAdd(btnAdauga);
            ThemeHelper.StyleButtonEdit(btnEditeaza);
            ThemeHelper.StyleButtonDelete(btnSterge);
            ThemeHelper.StyleButtonNeutral(btnInchide);
        }

        private void LoadExemplare()
        {
            try
            {
                if (carteId > 0)
                    exemplareList = ExemplarManager.GetByCarte(carteId);
                else
                    exemplareList = ExemplarManager.GetAll();
                RefreshGrid();
                if (carteId > 0 && exemplareList.Count > 0)
                    this.Text = exemplareList[0].TitluCarte + " - Exemplare";
                else if (carteId == 0)
                    this.Text = "Toate Exemplarele";
                else
                    this.Text = "Exemplare";
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la încărcarea exemplarelor: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dataGridViewExemplare.DataSource = exemplareList;
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            FrmAdaugaExemplar frm = new FrmAdaugaExemplar(carteId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadExemplare();
                Mesaje.Succes("Exemplarul a fost adăugat cu succes!");
            }
        }

        private void BtnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridViewExemplare.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un exemplar!");
                return;
            }

            Exemplar exemplar = (Exemplar)dataGridViewExemplare.SelectedRows[0].DataBoundItem;
            FrmAdaugaExemplar frm = new FrmAdaugaExemplar(carteId, exemplar);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadExemplare();
                Mesaje.Succes("Exemplarul a fost actualizat cu succes!");
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewExemplare.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un exemplar!");
                return;
            }

            if (Mesaje.ConfirmareSt() == DialogResult.Yes)
            {
                Exemplar exemplar = (Exemplar)dataGridViewExemplare.SelectedRows[0].DataBoundItem;
                try
                {
                    if (ExemplarManager.Delete(exemplar.IDExemplar))
                    {
                        LoadExemplare();
                        Mesaje.Succes("Exemplarul a fost șters cu succes!");
                    }
                    else
                        Mesaje.Eroare("Nu s-a putut șterge exemplarul!");
                }
                catch (Exception ex)
                {
                    Mesaje.Eroare(ex.Message);
                }
            }
        }

        private void BtnInchide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.dataGridViewExemplare = new DataGridView();
            this.btnAdauga = new Button();
            this.btnEditeaza = new Button();
            this.btnSterge = new Button();
            this.btnInchide = new Button();
            this.lblTotal = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExemplare)).BeginInit();
            this.SuspendLayout();

            this.dataGridViewExemplare.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExemplare.Location = new System.Drawing.Point(12, 40);
            this.dataGridViewExemplare.Name = "dataGridViewExemplare";
            this.dataGridViewExemplare.ReadOnly = true;
            this.dataGridViewExemplare.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewExemplare.Size = new System.Drawing.Size(760, 330);
            this.dataGridViewExemplare.TabIndex = 0;

            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(100, 13);
            this.lblTotal.TabIndex = 1;

            this.btnAdauga.Location = new System.Drawing.Point(12, 380);
            this.btnAdauga.Name = "btnAdauga";
            this.btnAdauga.Size = new System.Drawing.Size(75, 23);
            this.btnAdauga.TabIndex = 2;
            this.btnAdauga.Text = "Adauga";
            this.btnAdauga.UseVisualStyleBackColor = true;
            this.btnAdauga.Click += new EventHandler(this.BtnAdauga_Click);

            this.btnEditeaza.Location = new System.Drawing.Point(93, 380);
            this.btnEditeaza.Name = "btnEditeaza";
            this.btnEditeaza.Size = new System.Drawing.Size(75, 23);
            this.btnEditeaza.TabIndex = 3;
            this.btnEditeaza.Text = "Editeaza";
            this.btnEditeaza.UseVisualStyleBackColor = true;
            this.btnEditeaza.Click += new EventHandler(this.BtnEditeaza_Click);

            this.btnSterge.Location = new System.Drawing.Point(174, 380);
            this.btnSterge.Name = "btnSterge";
            this.btnSterge.Size = new System.Drawing.Size(75, 23);
            this.btnSterge.TabIndex = 4;
            this.btnSterge.Text = "Sterge";
            this.btnSterge.UseVisualStyleBackColor = true;
            this.btnSterge.Click += new EventHandler(this.BtnSterge_Click);

            this.btnInchide.Location = new System.Drawing.Point(697, 380);
            this.btnInchide.Name = "btnInchide";
            this.btnInchide.Size = new System.Drawing.Size(75, 23);
            this.btnInchide.TabIndex = 5;
            this.btnInchide.Text = "Inchide";
            this.btnInchide.UseVisualStyleBackColor = true;
            this.btnInchide.Click += new EventHandler(this.BtnInchide_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 415);
            this.Controls.Add(this.btnInchide);
            this.Controls.Add(this.btnSterge);
            this.Controls.Add(this.btnEditeaza);
            this.Controls.Add(this.btnAdauga);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dataGridViewExemplare);
            this.Name = "FrmExemplare";
            this.Text = "Gestionare Exemplare";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExemplare)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dataGridViewExemplare;
        private Button btnAdauga;
        private Button btnEditeaza;
        private Button btnSterge;
        private Button btnInchide;
        private Label lblTotal;
    }
}
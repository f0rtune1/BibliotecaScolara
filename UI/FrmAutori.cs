using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmAutori : Form
    {
        private List<Autor> autoriList;

        public FrmAutori()
        {
            InitializeComponent();
            ApplyTheme();
            LoadAutori();
        }

        private void ApplyTheme()
        {
            ThemeHelper.ApplyTheme(this);
            ThemeHelper.StyleGrid(dataGridViewAutori);
            ThemeHelper.StyleButtonAdd(btnAdauga);
            ThemeHelper.StyleButtonEdit(btnEditeaza);
            ThemeHelper.StyleButtonDelete(btnSterge);

            Panel header = ThemeHelper.CreateHeaderPanel(this, "Gestionare Autori");
            this.Controls.Add(header);
            header.BringToFront();
            dataGridViewAutori.Location = new System.Drawing.Point(12, 80);
        }

        private void LoadAutori()
        {
            try
            {
                autoriList = AutorManager.GetAll();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la încărcarea autorilor: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dataGridViewAutori.DataSource = autoriList;
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            FrmAdaugaAutor frm = new FrmAdaugaAutor();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadAutori();
                Mesaje.Succes("Autorul a fost adăugat cu succes!");
            }
        }

        private void BtnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridViewAutori.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un autor!");
                return;
            }

            Autor autor = (Autor)dataGridViewAutori.SelectedRows[0].DataBoundItem;
            FrmAdaugaAutor frm = new FrmAdaugaAutor(autor);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadAutori();
                Mesaje.Succes("Autorul a fost actualizat cu succes!");
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewAutori.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un autor!");
                return;
            }

            if (Mesaje.ConfirmareSt() == DialogResult.Yes)
            {
                Autor autor = (Autor)dataGridViewAutori.SelectedRows[0].DataBoundItem;
                try
                {
                    if (AutorManager.Delete(autor.IDAutor))
                    {
                        LoadAutori();
                        Mesaje.Succes("Autorul a fost șters cu succes!");
                    }
                    else
                    {
                        Mesaje.Eroare("Nu s-a putut șterge autorul!");
                    }
                }
                catch (Exception ex)
                {
                    Mesaje.Eroare(ex.Message);
                }
            }
        }

        private void TxtCauta_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCauta.Text))
            {
                RefreshGrid();
            }
            else
            {
                autoriList = AutorManager.Search(txtCauta.Text);
                RefreshGrid();
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewAutori = new DataGridView();
            this.btnAdauga = new Button();
            this.btnEditeaza = new Button();
            this.btnSterge = new Button();
            this.txtCauta = new TextBox();
            this.lblCauta = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAutori)).BeginInit();
            this.SuspendLayout();

            // dataGridViewAutori
            this.dataGridViewAutori.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAutori.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewAutori.Name = "dataGridViewAutori";
            this.dataGridViewAutori.ReadOnly = true;
            this.dataGridViewAutori.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAutori.Size = new System.Drawing.Size(760, 330);
            this.dataGridViewAutori.TabIndex = 0;

            // txtCauta
            this.txtCauta.Location = new System.Drawing.Point(100, 20);
            this.txtCauta.Name = "txtCauta";
            this.txtCauta.Size = new System.Drawing.Size(250, 20);
            this.txtCauta.TabIndex = 1;
            this.txtCauta.TextChanged += new EventHandler(this.TxtCauta_TextChanged);

            // lblCauta
            this.lblCauta.AutoSize = true;
            this.lblCauta.Location = new System.Drawing.Point(12, 23);
            this.lblCauta.Name = "lblCauta";
            this.lblCauta.Size = new System.Drawing.Size(71, 13);
            this.lblCauta.TabIndex = 2;
            this.lblCauta.Text = "Cauta autor:";

            // btnAdauga
            this.btnAdauga.Location = new System.Drawing.Point(12, 410);
            this.btnAdauga.Name = "btnAdauga";
            this.btnAdauga.Size = new System.Drawing.Size(75, 23);
            this.btnAdauga.TabIndex = 3;
            this.btnAdauga.Text = "Adauga";
            this.btnAdauga.UseVisualStyleBackColor = true;
            this.btnAdauga.Click += new EventHandler(this.BtnAdauga_Click);

            // btnEditeaza
            this.btnEditeaza.Location = new System.Drawing.Point(93, 410);
            this.btnEditeaza.Name = "btnEditeaza";
            this.btnEditeaza.Size = new System.Drawing.Size(75, 23);
            this.btnEditeaza.TabIndex = 4;
            this.btnEditeaza.Text = "Editeaza";
            this.btnEditeaza.UseVisualStyleBackColor = true;
            this.btnEditeaza.Click += new EventHandler(this.BtnEditeaza_Click);

            // btnSterge
            this.btnSterge.Location = new System.Drawing.Point(174, 410);
            this.btnSterge.Name = "btnSterge";
            this.btnSterge.Size = new System.Drawing.Size(75, 23);
            this.btnSterge.TabIndex = 5;
            this.btnSterge.Text = "Sterge";
            this.btnSterge.UseVisualStyleBackColor = true;
            this.btnSterge.Click += new EventHandler(this.BtnSterge_Click);

            // FrmAutori
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 445);
            this.Controls.Add(this.btnSterge);
            this.Controls.Add(this.btnEditeaza);
            this.Controls.Add(this.btnAdauga);
            this.Controls.Add(this.lblCauta);
            this.Controls.Add(this.txtCauta);
            this.Controls.Add(this.dataGridViewAutori);
            this.Name = "FrmAutori";
            this.Text = "Gestionare Autori";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAutori)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dataGridViewAutori;
        private Button btnAdauga;
        private Button btnEditeaza;
        private Button btnSterge;
        private TextBox txtCauta;
        private Label lblCauta;
    }
}
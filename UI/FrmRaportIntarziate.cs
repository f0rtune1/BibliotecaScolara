using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliotecaScolara.Models;

namespace BibliotecaScolara.UI
{
    public partial class FrmRaportIntarziate : Form
    {
        private List<Imprumut> imprumturiIntarziate;

        public FrmRaportIntarziate(List<Imprumut> intarziate)
        {
            InitializeComponent();
            imprumturiIntarziate = intarziate;
            LoadData();
        }

        private void LoadData()
        {
            dataGridViewIntarziate.DataSource = imprumturiIntarziate;
            AdjustColumns();
            lblTotal.Text = $"Total cărți întârziate: {imprumturiIntarziate.Count}";
        }

        private void AdjustColumns()
        {
            if (dataGridViewIntarziate.Columns.Count > 0)
            {
                dataGridViewIntarziate.Columns["NumeElev"].Width = 150;
                dataGridViewIntarziate.Columns["TitluCarte"].Width = 200;
                dataGridViewIntarziate.Columns["DataScadenta"].Width = 100;
                dataGridViewIntarziate.Columns["ZileRamase"].Width = 100;
            }
        }

        private void BtnInchide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.dataGridViewIntarziate = new DataGridView();
            this.lblTotal = new Label();
            this.btnInchide = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIntarziate)).BeginInit();
            this.SuspendLayout();

            this.dataGridViewIntarziate.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIntarziate.Location = new System.Drawing.Point(12, 40);
            this.dataGridViewIntarziate.Name = "dataGridViewIntarziate";
            this.dataGridViewIntarziate.ReadOnly = true;
            this.dataGridViewIntarziate.Size = new System.Drawing.Size(760, 370);
            this.dataGridViewIntarziate.TabIndex = 0;

            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(65, 13);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total: 0";

            this.btnInchide.Location = new System.Drawing.Point(697, 420);
            this.btnInchide.Name = "btnInchide";
            this.btnInchide.Size = new System.Drawing.Size(75, 23);
            this.btnInchide.TabIndex = 2;
            this.btnInchide.Text = "Inchide";
            this.btnInchide.UseVisualStyleBackColor = true;
            this.btnInchide.Click += new EventHandler(this.BtnInchide_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 455);
            this.Controls.Add(this.btnInchide);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dataGridViewIntarziate);
            this.Name = "FrmRaportIntarziate";
            this.Text = "Raport Cărți Întârziate";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIntarziate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dataGridViewIntarziate;
        private Label lblTotal;
        private Button btnInchide;
    }
}
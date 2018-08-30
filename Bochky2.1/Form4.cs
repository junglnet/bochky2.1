using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Bochky2._1
{
    public partial class Form4 : Form
    {
        OleDbConnection CONNECTION;
        OleDbCommand COMMAND;
        OleDbDataAdapter dataAdapter;
        OleDbCommandBuilder dbBuilder;
        Order order;
        DataGridView ModelGrid;
        DataGridView SetsGrid;
        DataSet dtmodel;
        DataSet dtsets;
        Model currentSets;


        public Form4(OleDbConnection CONNECTION, Order order)
        {
            InitializeComponent();
            this.CONNECTION = CONNECTION;
            this.order = order;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoSize = false;
            this.Text = "Редактирование и создание изделий и спецификаций";
            this.Visible = true;
        
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.ModelGrid = new DataGridView();
            COMMAND = new OleDbCommand();
            dtmodel = new DataSet();
            currentSets = new Model(CONNECTION);
            //try
            //{
            //    CONNECTION.Open();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
                
            //}
            
            panel1.Controls.Add(this.ModelGrid);


            this.ModelGrid.BackgroundColor = Color.FromArgb(255, 255, 255);
            this.ModelGrid.Width = panel1.Width;
            this.ModelGrid.Height = panel1.Height;
            this.ModelGrid.RowHeadersVisible = false;
            this.ModelGrid.DataSource = currentSets.GetDataSetCategory();
            // Почему в ручную??
            this.ModelGrid.DataMember = "Model";

            this.ModelGrid.Columns[0].Width = panel1.Width / 2;
            this.ModelGrid.Columns[1].Width = (panel1.Width / 2) - 3;
            //.Columns(2).Visible = False
            //'.Columns(3).Visible = False
            this.ModelGrid.ReadOnly = true;

            this.ModelGrid.DoubleClick += new EventHandler(ModelGrid_DoubleClick);
            this.ModelGrid.Click += new EventHandler(ModelGrid_Click);

            
        }
        private void Form4_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          //  CONNECTION.Close();
        }

        private void ModelGrid_Click(object sender, System.EventArgs e)
        {
            panel2.Controls.Clear();
        }
        private void ModelGrid_DoubleClick(object sender, System.EventArgs e)
        {
            this.SetsGrid = new DataGridView();

            string setsTableName = "Specification";
            try
            {
                panel2.Controls.Add(this.SetsGrid);
                // Почему сдесь 1
                this.SetsGrid.DataSource = currentSets.GetDataSetFromModelID("1");
                this.SetsGrid.DataMember = setsTableName;

                this.SetsGrid.BackgroundColor = Color.FromArgb(255, 255, 255);
                this.SetsGrid.Width = this.panel2.Width;
                this.SetsGrid.Height = this.panel2.Height;
                this.SetsGrid.RowHeadersVisible = false;
                //'.Columns(0).Width = .Width / 2
                //'.Columns(1).Width = (.Width / 2) - 7
                this.SetsGrid.ReadOnly = true;
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Сохранить базу моделей
            dbBuilder = new OleDbCommandBuilder(dataAdapter);
            try
            {
                if (dataAdapter.Update(dtmodel, "Model") > 0) {
                    MessageBox.Show("Таблица " + ModelGrid.DataMember + " Сохранена");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }       
        }
    }
}

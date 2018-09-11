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

namespace Bochky2
{
    public partial class EnteryOptionForms : Form
    {
        DataSet specificationList;

        public EnteryOptionForms(DataSet specificationList)
        {
            InitializeComponent();           
            this.specificationList = specificationList;
            this.Show();
        }

        private void EnteryOptionFormcs_Load(object sender, EventArgs e)
        {
            dataGridView1.BackgroundColor = Color.FromArgb(255, 255, 255);            
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.DataSource = specificationList.Tables[1];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["nom_id"].Width = 30;
            dataGridView1.Columns["nom_id"].ReadOnly = true;
            dataGridView1.Columns["group_id"].Visible = false;

            dataGridView1.Click += new EventHandler(dataGridView1_Click);
        }

        private void dataGridView1_Click(object sender, System.EventArgs e)
        {
            dataGridView2.BackgroundColor = Color.FromArgb(255, 255, 255);
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.DataSource = specificationList.Tables[2];
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
           
        }

    }
}

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
    public partial class Form2 : Form
    {
        OleDbConnection CONNECTION;
        OleDbCommand COMMAND;
        Order order;
        string[] arrModel;

        public Form2(OleDbConnection CONNECTION, Order order)
        {
            InitializeComponent();
            this.CONNECTION = CONNECTION;
            this.order = order;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoSize = false;
            this.Text = "Выберете изделие";
            this.Visible = true;
            this.FormClosed += new FormClosedEventHandler(Form2_Closed);

            try
            {
                //CONNECTION.Open();
                order.GetCategoryArr();
                comboBox1.DataSource = order.categoryArr;
                comboBox1.Text = order.categoryArr[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
        private void Form2_Load(object sender, EventArgs e)
        {
           
        }
        // Не работает.
        private void Form2_Closed(object sender, EventArgs e)
        {
            // MessageBox.Show("отработал");
           // CONNECTION.Close();
            
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            order.GetModelArr(comboBox1.Text.ToString());
            comboBox2.DataSource = order.modelArr;
            comboBox2.Text = order.modelArr[0];            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            order.try_RecordData(comboBox1.Text, comboBox2.Text);
            Close();
        }
    }
}

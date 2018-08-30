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
    public partial class Form1 : Form  
    {
        OleDbConnection CONNECTION;
        Order order;
        

        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show("test");
            this.FormClosed += new FormClosedEventHandler(Form1_Closed);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoSize = false;
            this.Text = "Программа создания договоров и спецификаций";
            CONNECTION = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;" + "data source=Database3.accdb");
           
            try
            {
                CONNECTION.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            order = new Order(CONNECTION);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void Form1_Closed(object sender, EventArgs e)
        {
            
            CONNECTION.Close();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 newForm2 = new Form2(CONNECTION, order);
            newForm2.Visible = true;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(order.chosenCategory);
            MessageBox.Show(order.chosenModel);
        }

        private void настройкиБазыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 newForm3 = new Form3(CONNECTION);
            newForm3.Visible = true;
        }

        private void моделиИСпецификацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 newForm4 = new Form4(CONNECTION, order);
            newForm4.Visible = true;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

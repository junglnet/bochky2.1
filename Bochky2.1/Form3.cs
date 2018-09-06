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
    public partial class Form3 : Form
    {
        OleDbConnection CONNECTION;
        OleDbDataAdapter dataAdapter;
        OleDbCommandBuilder dbBuilder;
        DataGridView[] dataGr;
        public Form3(OleDbConnection CONNECTION)
        {
            InitializeComponent();
            this.CONNECTION = CONNECTION;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoSize = false;
            this.Text = "Редактирование основной базы";
            this.Visible = true;
        
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            OleDbCommand COMMAND = new OleDbCommand();
            DataSet dtable;
            int i = 0;
        //try
        //    {
        //        CONNECTION.Open();
        //    }

        //catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        throw;
        //    }


            COMMAND.Connection = CONNECTION;
        
        foreach (DataRow dataRow in CONNECTION.GetSchema("Tables").Rows) {                
                if (dataRow["TABLE_TYPE"].ToString() == "TABLE") {
                    dtable = new DataSet();
                try {
                        tabControl1.TabPages.Add(dataRow["TABLE_NAME"].ToString());

                        string queryString = "Select * From " + dataRow["TABLE_NAME"].ToString() + "";

                        dataAdapter = new OleDbDataAdapter(queryString, CONNECTION);
                        dataAdapter.Fill(dtable, dataRow["TABLE_NAME"].ToString());
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        throw;
                    }

                    this.dataGr = new DataGridView[0];
                    this.dataGr[i] = new DataGridView();

                    Array.Resize(ref this.dataGr, this.dataGr.Length + i);

                    this.dataGr[i].Name = dataRow["TABLE_NAME"].ToString();

                    tabControl1.TabPages[i].Controls.Add(this.dataGr[i]);


                    this.dataGr[i].Width = this.Width;
                    this.dataGr[i].Height = this.Height - 30;
                    this.dataGr[i].DataSource = dtable;
                    this.dataGr[i].DataMember = dataRow["TABLE_NAME"].ToString();

                    i++;
                }                
           
            }       
        }
        private void Form3_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // CONNECTION.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string queryString = "Select * From " + this.dataGr[tabControl1.SelectedIndex].DataMember + "";

            

            dataAdapter = new OleDbDataAdapter(queryString, CONNECTION);

            //dbBuilder = new OleDbCommandBuilder(dataAdapter);
            //DataSet saveDS = new DataSet();
            //BindingSource bs = (BindingSource)this.dataGr[tabControl1.SelectedIndex].DataSource;
            //DataTable tCxC = (DataTable)bs.DataSource;
            //saveDS.Tables.Add(tCxC);
            try
            {
                
                if (dataAdapter.Update(this.dataGr[tabControl1.SelectedIndex].DataSource as DataSet, this.dataGr[tabControl1.SelectedIndex].DataMember) > 0)
                {
                    MessageBox.Show("Таблица " + this.dataGr[tabControl1.SelectedIndex].DataMember + " обновлена");
                }                   
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            this.Close();
        }
    }
}

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
    public partial class EnteryOptionForms : Form
    {
        DataSet optionSet;
        string tableName;
        string[] PropertyTableHeaders;

        public EnteryOptionForms(DataSet optionSet, string tableName)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.optionSet = optionSet;
            this.tableName = tableName;
            PropertyTableHeaders = new string[2] { "ID", "Название" };
            this.Show();
        }

        private void EnteryOptionFormcs_Load(object sender, EventArgs e)
        {
            
        }

        private void OptionGrid_DoubleClick(object sender, System.EventArgs e)
        {
            ChooseOption();
        }

        private void ChooseOption()
        {

        }
    }
}

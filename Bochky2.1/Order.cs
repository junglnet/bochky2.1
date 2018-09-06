using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Bochky2
{
    public class Order
    {
        public string[] categoryArr = new string[1];
        public string[] modelArr = new string[1];
        
        public string chosenCategory;
        public string chosenModel;

        string categoryTableName ;
        string modelTableName;

        OleDbConnection CONNECTION;
        OleDbCommand COMMAND;

        public Order(OleDbConnection CONNECTION)
        {
            this.CONNECTION = CONNECTION;
            categoryTableName = "Category";
            modelTableName = "Model";
            categoryArr[0] = "Нет категорий для отображения";
            modelArr[0] = "Нет моделей для отображения";
        }
        public void GetCategoryArr()
        {
            COMMAND = new OleDbCommand();
            int i = 0;
            OleDbDataReader readerCategory;
            COMMAND.Connection = CONNECTION;
            COMMAND.CommandText = "Select category_name From " + categoryTableName + "";
            readerCategory = COMMAND.ExecuteReader();

            while (readerCategory.Read()) {
                //categoryArr[i + 1];
                Array.Resize(ref categoryArr, categoryArr.Length + i);

                categoryArr[i] = readerCategory.GetValue(0).ToString();

                i++;
            }  
            readerCategory.Close();
        }

        public void GetModelArr(string selectedCategory)
        {
            COMMAND = new OleDbCommand();
            OleDbDataReader readerModel;
            int i = 0;
            COMMAND.Connection = CONNECTION;
            try
            {
                COMMAND.CommandText = "SELECT " + modelTableName + ".model_name FROM " + modelTableName + " INNER JOIN " + categoryTableName + " On " + categoryTableName + ".category_id = " + modelTableName + ".category_id WHERE " + categoryTableName + ".category_name Like '%" + selectedCategory + "%'";
                readerModel = COMMAND.ExecuteReader();
                while (readerModel.Read())
                {
                    Array.Resize(ref modelArr, modelArr.Length + i);
                    modelArr[i] = readerModel.GetValue(0).ToString();
                    i++;
                }
                readerModel.Close();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        public bool try_RecordData(string argsCategory, string argsModel)
        {
            if (argsCategory != "" && argsModel != "")
            {
                chosenCategory = argsCategory;
                chosenModel = argsModel;
                return true;
            }
            else return false;
        }
    }
}

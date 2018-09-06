using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace Bochky2._1
{
    public class Model
    {
        OleDbConnection CONNECTION;
        OleDbDataAdapter dataAdapter;

        public int CurrentModelId { get; set; }
        public string CategoryTableName { get; set; }
        public string ModelTableName { get; set; }       
        public string[] ModelTableHeaders { get; private set; }
       

        public Model(OleDbConnection CONNECTION)
        {
            this.CONNECTION = CONNECTION;
            CategoryTableName = "Category";
            ModelTableName = "Model";
            ModelTableHeaders = new string[3] { "ID", "Модель", "Категория" };
        }

        // получить перечень всех моделей
        public DataSet GetModelDataSet()
        {
            DataSet ModelDataSet = new DataSet();
            dataAdapter = new OleDbDataAdapter("Select " + ModelTableName + ".model_id, " + ModelTableName + ".model_name, " +
                "" + CategoryTableName + ".category_name FROM " + ModelTableName + " INNER JOIN " + CategoryTableName + " On " +
                "" + CategoryTableName + ".category_id = " + ModelTableName + ".category_id", CONNECTION);
            dataAdapter.Fill(ModelDataSet, ModelTableName);
            return ModelDataSet;
        }

    }
}

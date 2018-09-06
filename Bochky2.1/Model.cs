using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace Bochky2
{
    public class Model
    {
        OleDbConnection CONNECTION;
        OleDbDataAdapter dAdapterModel;
        OleDbDataAdapter dAdapterCategory;

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
            dAdapterModel = new OleDbDataAdapter("SELECT * FROM " + ModelTableName + "", CONNECTION);
            dAdapterCategory = new OleDbDataAdapter("SELECT * FROM " + CategoryTableName + "", CONNECTION);

            dAdapterModel.Fill(ModelDataSet, ModelTableName);
            dAdapterCategory.Fill(ModelDataSet, CategoryTableName);

            ModelDataSet.Relations.Add("Cat_idToModel", ModelDataSet.Tables[CategoryTableName].Columns["category_id"],
                ModelDataSet.Tables[ModelTableName].Columns["category_id"]);

            ModelDataSet.Tables[ModelTableName].Columns.Add("category_name", typeof(string), "Parent(Cat_idToModel).category_name");
                       
            ModelDataSet.Tables[0].Columns["category_name"].SetOrdinal(2);
            ModelDataSet.Tables[0].Columns["category_name"].Caption = "Что это";

            return ModelDataSet;
        }

    }
}

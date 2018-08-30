using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace Bochky2._1
{
    public class Model
    {
        OleDbConnection CONNECTION;
        string tableName;
        string setModelID;
        string categoryTableName;
        string modelTableName;
        DataSet SetsdbTemplate;
        OleDbCommandBuilder dbBuilder;

        OleDbDataAdapter dataAdapter;

        // Конструктор по-умолчанию
        public Model(OleDbConnection CONNECTION)
        {
            this.CONNECTION = CONNECTION;
            categoryTableName = "Category";
            modelTableName = "Model";
        }
        public DataSet GetDataSetCategory()
        {
            DataSet GetDataSetCategory = new DataSet();
            dataAdapter = new OleDbDataAdapter("Select " + modelTableName + ".model_name, " + categoryTableName + ".category_name FROM " + modelTableName + " INNER JOIN " + categoryTableName + " On " + categoryTableName + ".category_id = " + modelTableName + ".category_id", CONNECTION);
            dataAdapter.Fill(GetDataSetCategory, modelTableName);
            return GetDataSetCategory;
        }
        
         // получить выборку спецификаций по модели
        public DataSet GetDataSetFromModelID(string setModelID)
        {
            this.setModelID = setModelID;
            DataSet GetDataSetFromModelID = new DataSet();
            try
            {
                dataAdapter = new OleDbDataAdapter("SELECT Nomenclature.nom_name, Property.property_name, Specification.spec_amount, Model.model_name "
                                                      + "FROM ((Specification INNER JOIN Model On Model.model_id = Specification.model_id) "
                                                      + "INNER JOIN Nomenclature On Nomenclature.nom_id = Specification.nom_id) "
                                                      + "INNER JOIN Property On Property.property_id = Specification.property_id WHERE Specification.model_id = 6", CONNECTION);
                dataAdapter.Fill(GetDataSetFromModelID, "Specification");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            return GetDataSetFromModelID;
        }       
       
        
    }
}

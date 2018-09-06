using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace Bochky2._1
{
    public class ModelSpecification
    {
        public string SpecificationTableName { get; set; }
        public string NomenclatureTableName { get; set; }
        public string PropertyTableName { get; set; }
        public string GroupTableName { get; set; }
        public string[] SpecificationTableHeaders { get; private set; }
        public string[] PropertyTableHeaders { get; private set; }


        Model modelList;
        //DataSet SetsdbTemplate;
        //OleDbCommandBuilder dbBuilder;
        OleDbConnection CONNECTION;
        OleDbDataAdapter dataAdapter;
        DataSet SpecificationDataSet;

        // Конструктор по-умолчанию.
        public ModelSpecification(OleDbConnection CONNECTION, Model modelList)
        {
            this.CONNECTION = CONNECTION;
            this.modelList = modelList;           
            SpecificationTableName = "Specification";
            NomenclatureTableName = "Nomenclature";
            PropertyTableName = "NomProperty";
            GroupTableName = "NomGroup";
            SpecificationTableHeaders = new string[5] { "ID", "Номенклатура", "Группа", "Характеристика", "Кол-во" };
            PropertyTableHeaders = new string[2] { "ID", "Название"};
        }       
                
        // получить спецификацию в виде dataset.
        public DataSet GetSpecificationDataSet()
        {
            SpecificationDataSet = new DataSet();
            try
            {
                
                dataAdapter = new OleDbDataAdapter("SELECT " + NomenclatureTableName + ".nom_id, " + NomenclatureTableName + ".nom_name, " + GroupTableName + ".group_name, " 
                                                      + PropertyTableName + ".property_name, " + SpecificationTableName + ".spec_amount, " + SpecificationTableName + ".property_id " 
                                                      + "FROM (((" + SpecificationTableName + " INNER JOIN Model On Model.model_id = " + SpecificationTableName + ".model_id) "
                                                      + "INNER JOIN " + NomenclatureTableName + " On " + NomenclatureTableName + ".nom_id = " + SpecificationTableName + ".nom_id) "
                                                      + "INNER JOIN " + GroupTableName + " On " + GroupTableName + ".group_id = " + NomenclatureTableName + ".group_id) "
                                                      + "INNER JOIN " + PropertyTableName + " On " + PropertyTableName + ".property_id = " + SpecificationTableName + ".property_id WHERE " 
                                                      + SpecificationTableName + ".model_id = " + modelList.CurrentModelId  + "", CONNECTION);
                dataAdapter.Fill(SpecificationDataSet, "Specification");

                MessageBox.Show(SpecificationDataSet.Tables[0].TableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            return SpecificationDataSet;
        }
        public void SaveSpecification ()
        {
            OleDbCommandBuilder dbBuilder;
            // Сохранить базу моделей
            dbBuilder = new OleDbCommandBuilder(dataAdapter);
            try
            {
                if (dataAdapter.Update(SpecificationDataSet, "Specification") > 0)
                {
                    MessageBox.Show("Таблица Сохранена");
                }
                else
                {
                    MessageBox.Show("Таблица не сохранена");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void SetCellValue (int model_id, int col)
        {
            
        }
        

    }
}

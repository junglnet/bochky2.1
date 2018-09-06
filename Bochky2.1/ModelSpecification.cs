using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace Bochky2
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
        OleDbDataAdapter dAdapterSpecification;
        OleDbDataAdapter dAdapterNomenclature;
        OleDbDataAdapter dAdapterNomProperty;
        OleDbDataAdapter dAdapterNomGroup;

        DataSet commonDataSet;

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
            commonDataSet = new DataSet();
            try
            {
                // получаем нужные таблицы
                dAdapterSpecification = new OleDbDataAdapter("SELECT * FROM " + SpecificationTableName + " WHERE " 
                    + SpecificationTableName + ".model_id = " + modelList.CurrentModelId  + "", CONNECTION);
                dAdapterSpecification.Fill(commonDataSet, SpecificationTableName);

                dAdapterNomenclature = new OleDbDataAdapter("SELECT * FROM " + NomenclatureTableName + "", CONNECTION);
                dAdapterNomenclature.Fill(commonDataSet, NomenclatureTableName);

                dAdapterNomProperty = new OleDbDataAdapter("SELECT * FROM " + PropertyTableName + "", CONNECTION);
                dAdapterNomProperty.Fill(commonDataSet, PropertyTableName);

                dAdapterNomGroup = new OleDbDataAdapter("SELECT * FROM " + GroupTableName + "", CONNECTION);
                dAdapterNomGroup.Fill(commonDataSet, GroupTableName);
                                
                // устанавливаем связь
                commonDataSet.Relations.Add("Nom_idToSpec", commonDataSet.Tables[NomenclatureTableName].Columns["nom_id"],
                    commonDataSet.Tables[SpecificationTableName].Columns["nom_id"]);
                commonDataSet.Relations.Add("Prop_idToSpec", commonDataSet.Tables[PropertyTableName].Columns["property_id"],
                    commonDataSet.Tables[SpecificationTableName].Columns["property_id"]);

                commonDataSet.Relations.Add("Group_idToNom", commonDataSet.Tables[GroupTableName].Columns["group_id"],
                    commonDataSet.Tables[NomenclatureTableName].Columns["group_id"]);


                commonDataSet.Tables[NomenclatureTableName].Columns.Add("group_name", typeof(string), "Parent(Group_idToNom).group_name");

                commonDataSet.Tables[SpecificationTableName].Columns.Add("group_name", typeof(string), "Parent(Nom_idToSpec).group_name");
                commonDataSet.Tables[SpecificationTableName].Columns.Add("nom_name", typeof(string), "Parent(Nom_idToSpec).nom_name");
                commonDataSet.Tables[SpecificationTableName].Columns.Add("property_name", typeof(string), "Parent(Prop_idToSpec).property_name");
                
                commonDataSet.Tables[0].Columns["nom_name"].SetOrdinal(1);
                commonDataSet.Tables[0].Columns["group_name"].SetOrdinal(2);
                commonDataSet.Tables[0].Columns["property_name"].SetOrdinal(3);
                commonDataSet.Tables[0].Columns["spec_amount"].SetOrdinal(4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            return commonDataSet;
        }
        public void SaveSpecification ()
        {
            OleDbCommandBuilder dbBuilder;
            // Сохранить таблицу спецификаций
            dbBuilder = new OleDbCommandBuilder(dAdapterSpecification);
            try
            {
                if (dAdapterSpecification.Update(commonDataSet, "Specification") > 0)
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

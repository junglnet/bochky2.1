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
using Translate;

namespace Bochky2
{
    public partial class Form4 : Form
    {
        
        OleDbConnection CONNECTION;        
        OleDbDataAdapter dataAdapter;
        OleDbCommandBuilder dbBuilder;
        Order order;
        DataGridView ModelGrid;
        DataGridView SetsGrid;
        DataSet dtmodel;
        EnteryOptionForms optionForms;
        // DataSet dtsets;

        ModelSpecification currentSpecification;
        Model modelList;
        Label TextNotification = new Label();
        
        public Form4(OleDbConnection CONNECTION, Order order)
        {
            TranslateBase lang = new TranslateBase();
            InitializeComponent();
            this.CONNECTION = CONNECTION;
            this.order = order;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoSize = false;
            this.Text = "Редактирование и создание изделий и спецификаций";
            this.Visible = true;

            saveToolStripMenuItem.Text = lang._SAVE_;

        }

        private void Form4_Load(object sender, EventArgs e)
        {
           
            ModelGrid = new DataGridView();         

            // Отрисовка формы.            
            splitContainer1.Panel1.Controls.Add(ModelGrid);
            ModelGrid.BackgroundColor = Color.FromArgb(255, 255, 255);            
            ModelGrid.Dock = DockStyle.Fill;
            ModelGrid.Height = splitContainer1.Panel1.Height;
            ModelGrid.RowHeadersVisible = false;
            ModelGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // заполнение моделями
            GetModelListOnDataGrid();
                       
            TextNotification.Text = "Дважды нажмите на моделе для отображения спецификации";
            TextNotification.Location = new Point(300, 100);
            TextNotification.Anchor = AnchorStyles.Right;
            TextNotification.Width = 250;
            TextNotification.Height = 200;
            TextNotification.TextAlign = ContentAlignment.MiddleCenter;
            splitContainer1.Panel2.Controls.Add(TextNotification);
            TextNotification.Visible = true;

        }      

        private void ModelGrid_Click(object sender, System.EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(TextNotification);
            TextNotification.Visible = true;
        }

        // Двойной клик на форме выбора моделей.
        private void ModelGrid_DoubleClick(object sender, System.EventArgs e)
        {          
           
            TextNotification.Visible = false;

            // Заполнение спецификации
            GetSpecificationOnDataGrid();
           
        }

        private void SetsGrid_DoubleClick(object sender, System.EventArgs e)
        {
            //currentSpecification.SetCellValue(Convert.ToInt32(SetsGrid[0, SetsGrid.CurrentCell.RowIndex].Value.ToString()), SetsGrid.CurrentCell.ColumnIndex);           
            //OleDbDataAdapter daForCellValue = new OleDbDataAdapter();
            //DataSet optionSet = new DataSet();
            
            //int model_id = Convert.ToInt32(SetsGrid[0, SetsGrid.CurrentCell.RowIndex].Value.ToString());
            //daForCellValue = new OleDbDataAdapter("SELECT " + currentSpecification.PropertyTableName + ".property_id, " + currentSpecification.PropertyTableName + ".property_name, " + currentSpecification.PropertyTableName + ".nom_id FROM "
            //                                  + currentSpecification.PropertyTableName + " WHERE " + currentSpecification.PropertyTableName + ".nom_id = " + model_id + "", CONNECTION);
            //daForCellValue.Fill(optionSet, "Property");
            //optionForms = new EnteryOptionForms(optionSet, "Property");

            //DataGridView OptionGrid = new DataGridView
            //{
            //    Dock = DockStyle.Top,
            //    RowHeadersVisible = false,
            //    DataSource = optionSet,
            //    DataMember = "Property",
            //    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            //};

            //optionForms.Controls.Add(OptionGrid);
            //OptionGrid.Columns[0].Width = 30;
            //OptionGrid.Columns[OptionGrid.ColumnCount - 1].Visible = false;
            //for (int i = 0; i < currentSpecification.PropertyTableHeaders.Count(); i++)
            //{
            //    OptionGrid.Columns[i].HeaderText = currentSpecification.PropertyTableHeaders[i];
            //}

            //// Обработчики событий.
            //OptionGrid.DoubleClick += new EventHandler(OptionGrid_DoubleClick);


            ////SetsGrid.CurrentCell.Value = "dsdfdsdfs";
        }

        // Вспомогательная форма обработчик событий

        private void OptionGrid_DoubleClick(object sender, System.EventArgs e)
        {
            SetsGrid.CurrentCell.Value = "dsdfdsdfs";
            optionForms.Close();
        }


        // Получение спецификации в DataGrid.

        private void GetModelListOnDataGrid()
        {
            modelList = new Model(CONNECTION);            
            dtmodel = new DataSet();

            // Назначение datasourse.
            ModelGrid.DataSource = modelList.GetModelDataSet().Tables[0];
            
            // Отрисовка полей.            
            ModelGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ModelGrid.Columns["model_id"].Width = 30;
            ModelGrid.Columns["model_id"].ReadOnly = true;
            ModelGrid.Columns["category_id"].Visible = false;

            // Установка заголовка колонок.
            for (int i = 0; i < modelList.ModelTableHeaders.Count(); i++)
            {
                ModelGrid.Columns[i].HeaderText = modelList.ModelTableHeaders[i];
            }


            // Обработчики событий
            ModelGrid.DoubleClick += new EventHandler(ModelGrid_DoubleClick);
            ModelGrid.Click += new EventHandler(ModelGrid_Click);
        }

        private void GetSpecificationOnDataGrid()
        {
            SetsGrid = new DataGridView();
            SetsGrid.DoubleClick += new EventHandler(SetsGrid_DoubleClick);

            // Запись ID выбранной модели.
            modelList.CurrentModelId = Convert.ToInt32(ModelGrid[0, ModelGrid.CurrentCell.RowIndex].Value.ToString());
            currentSpecification = new ModelSpecification(CONNECTION, modelList);
            
            // Отрисовка контрола.
            splitContainer1.Panel2.Controls.Add(SetsGrid);
            SetsGrid.BackgroundColor = Color.FromArgb(255, 255, 255);
            SetsGrid.Dock = DockStyle.Fill;
            SetsGrid.Height = splitContainer1.Panel2.Height;
            SetsGrid.RowHeadersVisible = false;

            // Назначение Datasourse.
            SetsGrid.DataSource = currentSpecification.GetSpecificationDataSet().Tables[0];

            //скрыть ключи
            SetsGrid.Columns["nom_id"].Visible = false;
            SetsGrid.Columns["property_id"].Visible = false;
            SetsGrid.Columns["model_id"].Visible = false;
            SetsGrid.Columns["spec_id"].ReadOnly = true;           

            // переименование столбцов
            for (int i = 0; i < currentSpecification.SpecificationTableHeaders.Count(); i++)
            {
                SetsGrid.Columns[i].HeaderText = currentSpecification.SpecificationTableHeaders[i];
            }

            //отрисовака полей
            SetsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SetsGrid.Columns["spec_id"].Width = 30;
            SetsGrid.Columns["spec_amount"].Width = 50;
        }


        private void Form4_Resize(object sender, EventArgs e)
        {
           

        }

        private void Form4_ResizeEnd(object sender, EventArgs e)
        {

           
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void saveSpecificationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentSpecification.SaveSpecification();
        }
    }
}

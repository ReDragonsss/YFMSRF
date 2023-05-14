using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace YFMSRF
{
    public partial class BazaDan : MetroFramework.Forms.MetroForm
    {
        public BazaDan()
        {
            InitializeComponent();
        }
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private SQLiteDataAdapter MyDA = new SQLiteDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        protected BindingSource bSource;
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //Представляет одну таблицу данных в памяти.
        private DataTable table;
        //Переменная для ID записи в БД, выбранной в гриде. Пока она не содердит значения, лучше его инициализировать с 0
        //что бы в БД не отправлялся null
        string id_selected_rows = null;
        public void ManagerRole(int role)
        {
            switch (role)
            {
                case 1://отдел депортации
                    metroButton3.Enabled = true;
                    metroButton1.Enabled = false;
                    metroButton3.Visible = true;
                    metroButton1.Visible = false;
                    break;
                case 2:// разрешительно-визовой работы
                    metroButton3.Enabled = false;
                    metroButton1.Enabled = true;
                    metroButton3.Visible = false;
                    metroButton1.Visible = true;
                    break;
                default: //off
                    metroButton1.Enabled = true;
                    metroButton3.Enabled = true;
                    break;
            }
        }
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
        }
        public bool DeleteInfo()// Запрос на удаление
        {
            PCS.ControlData.conn.Open();
            int InsertCount = 0;
            bool result = false;
            string SqlDelete = $"DELETE FROM {Dell.dell} WHERE fam ='" + id_selected_rows + "'";
            try
            {
                SQLiteCommand command = new SQLiteCommand(SqlDelete, PCS.ControlData.conn);
                InsertCount = command.ExecuteNonQuery();
            }
            catch
            {
                //Если возникла ошибка, то запрос не вставит ни одной строки
                InsertCount = 0;
            }
            finally
            {
                PCS.ControlData.conn.Close();
                //Ессли количество вставленных строк было не 0, то есть вставлена хотя бы 1 строка
                if (InsertCount != 0)
                {
                    MessageBox.Show($"Успешное удаление данных");
                    reload_list();
                }
            }
            return result;
        }
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            if(table != null)
            {
                table.Clear();
                dataGridView1.DataSource = bSource;
            }
        }
        public void GetListUsers(string commandStr)
        {
            table = new DataTable();
            bSource = new BindingSource();
            //Запрос для вывода строк в БД
            //Открываем соединение
            PCS.ControlData.conn.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new SQLiteCommand(commandStr, PCS.ControlData.conn);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            PCS.ControlData.conn.Close();
        }
        private void metroButton3_Click(object sender, EventArgs e)
        {
            // доступ высшее начальство
            // доступ отдел по вопросам депортации
            reload_list();
            GetListUsers("SELECT fam AS'Фамилия',ima AS'Имя',otech AS'Отчество',data_rojden AS'Дата рождения',grajdanstvo AS'Гражданство',seria_and_nomer_pasporta AS'Серия и номер паспорта' FROM deportiruuchi");
            Dell.dell = "deportiruuchi";
            dataGridView1.Columns[0].FillWeight = 9;
            dataGridView1.Columns[1].FillWeight = 9;
            dataGridView1.Columns[2].FillWeight = 10;
            dataGridView1.Columns[3].FillWeight = 14;
            dataGridView1.Columns[4].FillWeight = 13;
            dataGridView1.Columns[5].FillWeight = 15;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            // доступ высшее начальство
            // доступ отдел по вопросам гражданства
            reload_list();
            GetListUsers($"SELECT fam AS'Фамилия',name AS'Имя',otchestv AS'Отчество',pol AS'Пол',data_rojdenia AS'Дата рождения',mesto_rojden AS'Место рождения',kod_inostr AS'Код иностранца' FROM Osnov_dannie_inostr");
            Dell.dell = "Osnov_dannie_inostr";
            dataGridView1.Columns[0].FillWeight = 8;
            dataGridView1.Columns[1].FillWeight = 8;
            dataGridView1.Columns[2].FillWeight = 8;
            dataGridView1.Columns[3].FillWeight = 6;
            dataGridView1.Columns[4].FillWeight = 9;
            dataGridView1.Columns[5].FillWeight = 10;
            dataGridView1.Columns[6].FillWeight = 5;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void metroButton6_Click(object sender, EventArgs e)
        {
            reload_list();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
           DeleteInfo();
        }
        private void BazaDan_Load(object sender, EventArgs e)
        {
            ManagerRole(Auth.auth_role);
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            {
                if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
                {
                    dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                    dataGridView1.CurrentCell.Selected = true;
                    //Метод получения ID выделенной строки в глобальную переменную
                    GetSelectedIDString();
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            //    dataGridView1.Rows[i].Visible = dataGridView1[10, i].Value.ToString() == ;
        }
        //private void ChangeColorDGV()
        //{
        //    //Отражаем количество записей в ДатаГриде
        //    int count_rows = dataGridView1.RowCount - 1;
        //    toolStripLabel2.Text = (count_rows).ToString();
        //    //Проходимся по ДатаГриду и красим строки в нужные нам цвета, в зависимости от статуса студента
        //    for (int i = 0; i < count_rows; i++)
        //    {

        //        //статус конкретного студента в Базе данных, на основании индекса строки
        //        int id_selected_status = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
        //        //Логический блок для определения цветности
        //        if (id_selected_status == 1)
        //        {
        //            //Красим в красный
        //            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
        //        }
        //        if (id_selected_status == 2)
        //        {
        //            //Красим в зелёный
        //            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
        //        }
        //        if (id_selected_status == 3)
        //        {
        //            //Красим в желтый
        //            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
        //        }
        //    }
        //}

    }
}

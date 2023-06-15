using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using MySql.Data.MySqlClient;

namespace YFMSRF
{
    public partial class BazaDan : MetroFramework.Forms.MetroForm
    {
        public BazaDan()
        {
            InitializeComponent();
        }
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
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
                    metroButton8.Enabled = false;
                    metroButton8.Visible = false;
                    break;
                case 2:// разрешительно-визовой работы
                    metroButton3.Enabled = false;
                    metroButton1.Enabled = true;
                    metroButton3.Visible = false;
                    metroButton1.Visible = true;
                    metroButton8.Enabled = false;
                    metroButton8.Visible = false;
                    break;
                case 3:// разрешительно-визовой работы
                    metroButton8.Enabled = true;
                    metroButton8.Visible = true;
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
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[5].Value.ToString();
        }
        public bool DeleteInfo()// Запрос на удаление
        {
            PCS.ControlData.conn.Open();
            int InsertCount = 0;
            bool result = false;
            string SqlDelete = $"DELETE FROM {Dell.dell} WHERE id_viz ='" + id_selected_rows + "'";
            try
            {
                MySqlCommand command = new MySqlCommand(SqlDelete, PCS.ControlData.conn);
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
            if (table != null)
            {
                table.Clear();
                dataGridView1.DataSource = bSource;
                GetListUsers($"SELECT id_viz, data_vidachi, na_srock, grajdanstv, fio, nomber_pass, data_rojd, pol, prinim_organiz, dopol_sveden FROM {Dell.dell}");
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
            MyDA.SelectCommand = new MySqlCommand(commandStr, PCS.ControlData.conn);
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
            GetListUsers($"SELECT id_viz, data_vidachi, na_srock, grajdanstv, fio, nomber_pass, data_rojd, pol, prinim_organiz, dopol_sveden FROM viza");
            Dell.dell = "viza";
            dataGridView1.Columns[0].FillWeight = 5;
            dataGridView1.Columns[1].FillWeight = 11;
            dataGridView1.Columns[2].FillWeight = 11;
            dataGridView1.Columns[3].FillWeight = 12;
            dataGridView1.Columns[4].FillWeight = 16;
            dataGridView1.Columns[5].FillWeight = 11;
            dataGridView1.Columns[6].FillWeight = 12;
            dataGridView1.Columns[7].FillWeight = 11;
            dataGridView1.Columns[8].FillWeight = 11;
            dataGridView1.Columns[9].FillWeight = 11;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            Action.action = "Удалил информацию о иностранце id: " + id_selected_rows + "";
            Aud instance = new Aud();
            bool auditResult = instance.Audit();
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
                    string index_selected_rows;
                    //Индекс выбранной строки
                    index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
                    //ID конкретной записи в Базе данных, на основании индекса строки
                    id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Dell.id = id_selected_rows;
            if (string.IsNullOrEmpty(id_selected_rows))
            {
                MessageBox.Show("Вы не выбрали поле");
            }
            else
            {
                Form1 f1 = new Form1();
                f1.Show();
            }


        }
    }
}

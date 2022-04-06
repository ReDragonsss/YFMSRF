using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace YFMSRF
{
    public partial class BazaDan : MetroFramework.Forms.MetroForm
    {
        public BazaDan()
        {
            InitializeComponent();
        }
        public string avtosalon = "Comp1";
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
        private void Form8_Load(object sender, EventArgs e)
        {
        
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
            string SqlDelete = $"DELETE FROM {avtosalon} WHERE kod_inostr ='" + id_selected_rows + "'";
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
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                dataGridView1.CurrentCell.Selected = true;
                //Метод получения ID выделенной строки в глобальную переменную
                GetSelectedIDString();
            }
        }
        private void metroButton3_Click(object sender, EventArgs e)
        {
            // доступ высшее начальство
            // доступ отдел по вопросам депортации
            reload_list();
            GetListUsers("SELECT fam,ima,otech,data_rojden,grajdanstvo,seria_and_nomer_pasporta FROM deportiruuchi");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            // доступ высшее начальство
            // доступ отдел по вопросам гражданства
            reload_list();
            GetListUsers($"SELECT fam AS'Фамилия',name,otchestv,pol,data_rojdenia,mesto_rojden FROM Osnov_dannie_inostr");
        }
        // а где 2-ая кнопка
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

        private void metroButton7_Click(object sender, EventArgs e)
        {
            // доступ отдел по вопросам гражданства
            // доступ отдел по вопросам депортации
            // доступ высшее начальство
        }
    }
}

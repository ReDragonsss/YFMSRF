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
        MySqlConnection conn;
        public string avtosalon = "Comp1";
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        //Переменная для ID записи в БД, выбранной в гриде. Пока она не содердит значения, лучше его инициализировать с 0
        //что бы в БД не отправлялся null
        string id_selected_rows = null;
        private void Form8_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_2_21_19;database=st_2_21_19;password=30518003";
            conn=new MySqlConnection(connStr);
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
        }
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            //Указываем ID выделенной строки в метке
            //toolStripLabel2.Text = id_selected_rows;
            //ControlData.ID_PC = id_selected_rows;
        }
        public void reload_list()
        {
            table.Clear();
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
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //ControlData.ComboId=toolStripComboBox1.Text;
            //ControlData.ID_PC = id_selected_rows;
            mainmenu form1 = new mainmenu();
            form1.ShowDialog();
            reload_list();
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //ControlData.ComboId=toolStripComboBox1.Text;
            //ControlData.ID_PC = id_selected_rows;
            mainmenu form1 = new mainmenu();
            form1.ShowDialog();
            reload_list();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            // доступ высшее начальство
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            // доступ высшее начальство
            // доступ отдел по вопросам гражданства
            // доступ отдел по вопросам депортации
            reload_list();
            string commandStr = $"SELECT fam,name,otchestv,pol,data_rojdenia,mesto_rojden FROM Osnov_dannie_inostr";
            conn.Open();
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            conn.Close();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            // доступ высшее начальство
            // доступ отдел по вопросам депортации
            reload_list();
            string commandStr = $"SELECT doljnost,famil_sotr,inicial_sotr,spec_zvan,grajdan,ima,fam,otch,obstoatel_osn,poloj_fed FROM nerazrehviezde";
            conn.Open();
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            conn.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            // доступ высшее начальство
            // доступ отдел по вопросам гражданства
            reload_list();
            string commandStr = $"SELECT data_vidachi,na_srock,grajdanstv,fio,nomber_pass,data_rojd,pol,prinim_organiz,dopol_sveden FROM viza";
            conn.Open();
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            conn.Close();
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
           // DeleteInfo();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            // доступ отдел по вопросам гражданства
            // доступ отдел по вопросам депортации
            // доступ высшее начальство
        }
    }
}


//public bool DeleteInfo()// Запрос на удаление
//{
//    conn.Open();
//    int InsertCount = 0;
//    bool result = false;
//    string SqlDelete = $"DELETE FROM {avtosalon} WHERE name_pc ='" + id_selected_rows + "'";
//    try
//    {
//        MySqlCommand command = new MySqlCommand(SqlDelete, conn);
//        InsertCount = command.ExecuteNonQuery();
//    }
//    catch
//    {
//        //Если возникла ошибка, то запрос не вставит ни одной строки
//        InsertCount = 0;
//    }
//    finally
//    {
//        conn.Close();
//        //Ессли количество вставленных строк было не 0, то есть вставлена хотя бы 1 строка
//        if (InsertCount != 0)
//        {
//            MessageBox.Show($"Успешное удаление данных");
//            reload_list();
//        }
//    }
//    return result;
//}
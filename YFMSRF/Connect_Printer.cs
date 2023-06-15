using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing;
using MySql.Data.MySqlClient;

namespace YFMSRF
{
    public partial class Connect_Printer : MetroFramework.Forms.MetroForm
    {
        protected BindingSource bSource;
        private DataTable table;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        string id_selected_rows = null;
        public Connect_Printer()
        {
            InitializeComponent();
            dataGridView1.ContextMenuStrip = metroContextMenu1;
        }
        public void ManagerRole(int role)
        {
            switch (role)
            {
                case 1://отдел депортации
                    comboBox1.Enabled = false;
                    comboBox1.Visible = false;
                    break;
                case 2:// разрешительно-визовой работы
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = false;
                    comboBox1.Visible = true;
                    comboBox2.Visible = false;
                    break;
                case 3:// Начальство
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox1.Visible = true;
                    comboBox2.Visible = true;
                    break;
                default: //off
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    break;
            }
        }
        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }
        void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            if (Auth.auth_role == 2 || Auth.auth_role == 3)
            {
                Getinfo1();
                Getinfo2();
                Getinfo3();
                string fam = Inostranci.inostr_fam;
                string name = Inostranci.inostr_ima;
                string otch = Inostranci.inostr_otch;
                string dater = Inostranci.inostr_datar;
                string datem = Inostranci.inostr_mestr;
                string pol = Inostranci.inostr_pol;
                string grajdan = Inostranci.inostr_grajd;
                string seria = Inostranci.inostr_seria;
                string nomer = Inostranci.inostr_pass;
                DateTime date = DateTime.Today;
                // Формируем текст заявления об увольнении
                string text = string.Format("ВИЗОВАЯ АНКЕТА\n\n" +
                                     "Код: ______________ \n" +
                                     "Фамилия: {0}\n" +
                                     "Имя(имена): {1}\n" +
                                     "Отчество: {2}\n" +
                                     "Дата рождения:{3} Место рождения:{4} Пол:{5}\n" +
                                     "Гражданство: {6}\n" +
                                     "Cерия {7} Номер {8}\n" +
                                     "___________________________   ______________________\n" +
                                     "        (подпись)                                 (фамилия и инициалы)\n\n" +
                                 "Начальник отдела по вопросам паспортно-визовой работы: Райдер.А.В\n\n" +
                                 "Дата: {9}", fam, name, otch, dater, datem, pol, grajdan, seria, nomer, date.ToShortDateString(), "Райдер.А.В", date.ToShortDateString());

                // Установка параметров для печати
                Font fontText = new Font("Times New Roman", 14, FontStyle.Regular);
                Font fontTitle = new Font("Times New Roman", 24);
                Font bodyFont = new Font("Times New Roman", 14);
                Brush brush = Brushes.Black;
                float x = e.MarginBounds.Left;
                float y = e.MarginBounds.Top;
                // Настройка расположения для заголовка
                SizeF textSize = e.Graphics.MeasureString("ВИЗОВАЯ АНКЕТА", fontTitle);
                float xTitle = (e.PageBounds.Width - textSize.Width) / 2;
                float yTitle = y;
                e.Graphics.DrawString("ВИЗОВАЯ АНКЕТА", fontTitle, brush, xTitle, yTitle);
                // Печать текста
                int spacing = 30;
                float yPos = yTitle + textSize.Height + spacing;
                yPos += spacing;
                e.Graphics.DrawString("Код: ______________ ", fontText, brush, x, yPos);
                yPos += spacing;
                e.Graphics.DrawString("Фамилия: " + fam, fontText, brush, x, yPos);
                yPos += spacing;
                e.Graphics.DrawString("Имя(имена): " + name, fontText, brush, x, yPos);
                yPos += spacing;
                e.Graphics.DrawString("Отчество: " + otch, fontText, brush, x, yPos);
                yPos += spacing;
                e.Graphics.DrawString("Дата рождения: " + dater + " Место рождения: " + datem + "  Пол: " + pol, fontText, brush, x, yPos);
                yPos += spacing;
                e.Graphics.DrawString("Гражданство: " + grajdan, fontText, brush, x, yPos);
                yPos += spacing;
                e.Graphics.DrawString("Cерия " + seria + " Номер " + nomer, fontText, brush, x, yPos);
                yPos += spacing;
                // Настройка расположения для подписи руководителя
                SizeF bodySize = e.Graphics.MeasureString("(Фамилия и инициалы, Подпись)", bodyFont);
                float xSignature = e.PageBounds.Right - bodySize.Width - 45;
                float ySignature = e.PageBounds.Bottom - bodySize.Height - 50;
                float ySignat = e.PageBounds.Bottom - bodySize.Height - 75;
                // Печать подписи руководителя
                e.Graphics.DrawString("___________________________", bodyFont, brush, xSignature, ySignat);
                e.Graphics.DrawString("(Фамилия и инициалы, Подпись)", bodyFont, brush, xSignature, ySignature);
                // Печать заголовка для подписи руководителя
                string signatureTitle = "Начальник паспортно-визовой работы: Райдер.А.В";
                SizeF signatureTitleSize = e.Graphics.MeasureString(signatureTitle, fontText);
                float xSignatureTitle = xSignature - signatureTitleSize.Width - 50;
                float ySignatureTitle = ySignature + bodySize.Height / 2 - signatureTitleSize.Height / 2;
                e.Graphics.DrawString(signatureTitle, fontText, brush, xSignatureTitle, ySignatureTitle);
                // Печать даты
                string dateText = "Дата: " + DateTime.Today.ToShortDateString();
                SizeF dateSize = e.Graphics.MeasureString(dateText, fontText);
                float xDate = xSignatureTitle - dateSize.Width - 0;
                float yDate = ySignatureTitle + signatureTitleSize.Height + spacing;
                e.Graphics.DrawString(dateText, fontText, brush, xDate, yDate);
            }
            else
            {
                Bitmap txt = new Bitmap(dataGridView1.Size.Width + 0, dataGridView1.Size.Width + 10);// Мы создаем новый экземпляр класса
                dataGridView1.DrawToBitmap(txt, dataGridView1.Bounds);//подготовка к разметке страниц
                e.Graphics.DrawImage(txt, 100, 100); // разметка в дюймах
            }
           
        }
        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Test_Connect_Printer_Load(object sender, EventArgs e)
        {
            ManagerRole(Auth.auth_role);
        }
        public void Getinfo1()//метод для получения гражданства иностранца
        {
            PCS.ControlData.conn.Open();
            string sql = $"SELECT grajdanstvo FROM vizov_anketa Where kod_inostr='{id_selected_rows}'";
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Inostranci.inostr_grajd = reader[0].ToString();

            }
            reader.Close();
            PCS.ControlData.conn.Close();
        }
        public void Getinfo2()//метод для получения онсовной информации иностранца
        {
            PCS.ControlData.conn.Open();
            string sql = $"SELECT fam,name,otchestv,pol,data_rojdenia, mesto_rojden FROM Osnov_dannie_inostr Where kod_inostr='{id_selected_rows}'";
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Inostranci.inostr_fam = reader[0].ToString();
                Inostranci.inostr_ima = reader[1].ToString();
                Inostranci.inostr_otch = reader[2].ToString();
                Inostranci.inostr_pol = reader[3].ToString();
                Inostranci.inostr_datar = reader[4].ToString();
                Inostranci.inostr_mestr = reader[5].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
        }
        public void Getinfo3()//метод для получения номера паспорта иностранца
        {
            PCS.ControlData.conn.Open();
            string sql = $"SELECT seria_pass, nomer_pass FROM pass Where kod_inostr ='{id_selected_rows}'";
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Inostranci.inostr_seria = reader[0].ToString();
                Inostranci.inostr_pass = reader[1].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         GetListUsers($"SELECT fam AS'Фамилия',name AS'Имя',otchestv AS'Отчество',pol AS'Пол',data_rojdenia AS'Дата рождения',mesto_rojden AS'Место рождения', kod_inostr AS'Уникальный номер' FROM Osnov_dannie_inostr");
            dataGridView1.Columns[0].FillWeight = 9;
            dataGridView1.Columns[1].FillWeight = 9;
            dataGridView1.Columns[2].FillWeight = 10;
            dataGridView1.Columns[3].FillWeight = 10;
            dataGridView1.Columns[4].FillWeight = 13;
            dataGridView1.Columns[5].FillWeight = 14;
            dataGridView1.Columns[6].FillWeight = 5;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
         GetListUsers($"SELECT id_document,famil_sotr,inicial_sotr,grajdan,ima,fam,otch,data_pribit,data_podpisan,data_polych,obstoatel_osn,poloj_fed FROM nerazrehviezde");
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
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
                    id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[6].Value.ToString();
                }
            }
        }
    }

}

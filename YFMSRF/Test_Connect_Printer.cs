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
using MySql.Data.MySqlClient;

namespace YFMSRF
{
    public partial class Test_Connect_Printer : MetroFramework.Forms.MetroForm
    {
        protected BindingSource bSource;
        private DataTable table;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        public Test_Connect_Printer()
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
                    comboBox2.Enabled = true;
                    comboBox1.Visible = false;
                    comboBox2.Visible = true;
                    break;
                case 2:// разрешительно-визовой работы
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = false;
                    comboBox1.Visible = true;
                    comboBox2.Visible = false;
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
            Bitmap txt = new Bitmap(dataGridView1.Size.Width + 10, dataGridView1.Size.Width + 10);// Мы создаем новый экземпляр класса
            dataGridView1.DrawToBitmap(txt, dataGridView1.Bounds);// незнаю что это.. хотя скорее всего подготовка к разметке страниц
            e.Graphics.DrawImage(txt, 0,0); // разметка в дюймах выше все наприсано 
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         GetListUsers($"SELECT fam AS'Фамилия',name,otchestv,pol,data_rojdenia,mesto_rojden FROM Osnov_dannie_inostr");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
         GetListUsers("SELECT fam,ima,otech,data_rojden,grajdanstvo,seria_and_nomer_pasporta FROM deportiruuchi");
        }
    }

}
// можно в будущем добавить удаление за определенный срок, таким образом можно создать аккаунт начальника отдела и повысить 
// функционал программы или реализовать выборку здесь по дате
//
//                            Для печати нужны printDialog и printDocument
//
//                                 это коректировка размера распечатки
//public void SetResolution (float xDpi, float yDpi); 

//                                           Параметры
//xDpi
//Single
//Горизонтальное разрешение изображения Bitmap в точках на дюйм.
//yDpi
//Single
//Вертикальное разрешение изображения Bitmap в пикселях на дюйм.

//                                            Пример 
// Bitmap bitmap = new Bitmap(100, 100);
// bitmap.SetResolution(96.0F, 96.0F);

//                                        Сайты с инфой 
// https://vscode.ru/prog-lessons/pechat-dokumentov-na-c-sharp.html еще пример возможной конструкции, между прочем хороший сайт с объеснением. И еще самый товповый видос https://www.youtube.com/watch?v=XlRCBaxJRcM&t=446s он есть на сайте
// https://docs.microsoft.com/ru-ru/dotnet/api/system.drawing.bitmap.setresolution?view=netframework-4.7 просто обьеснение что такое bitmap
// https://www.youtube.com/watch?v=-znftdtV1sk&t=126s видеоурок где, незнаю говорит он или нет но я по нему делал
// https://docs.microsoft.com/ru-ru/dotnet/api/system.drawing.bitmap.-ctor?view=windowsdesktop-5.0 растовые изображения 
// https://docs.microsoft.com/ru-ru/dotnet/api/system.drawing.printing.printdocument?view=net-5.0 PrintDocument
//

//if (ControlData.ComboId == "Changan")
//{
//    Arhiv = "";
//}
//else if (ControlData.ComboId == "Kia")
//{
//    Arhiv = "";
//}
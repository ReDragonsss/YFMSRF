using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YFMSRF
{
    public partial class Test_Connect_Printer : MetroFramework.Forms.MetroForm
    {
        public Test_Connect_Printer()
        {
            InitializeComponent();
            dataGridView1.ContextMenuStrip = metroContextMenu1;
        }
        private void Test_Connect_Printer_Load(object sender, EventArgs e)
        {

        }

        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap txt = new Bitmap(dataGridView1.Size.Width+10, dataGridView1.Size.Width+10);// Мы создаем новый экземпляр класса
            dataGridView1.DrawToBitmap(txt, dataGridView1.Bounds);// незнаю что это.. хотя скорее всего подготовка к разметке страниц
            e.Graphics.DrawImage(txt, 0, 0); // разметка в дюймах выше все наприсано 
      
        }

    private void metroContextMenu1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
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
//                                       Предупреждение
// Если берете код, то разберитесь сначала с темой, не надо копировать структуру или полностью все. 
// Все в инете есть так что удачи с печатками.

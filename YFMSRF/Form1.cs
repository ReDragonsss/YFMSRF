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
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ManagerRole(int role)
        {
            switch (role)
            {
                case 1://отдел депортации
                    metroButton1.Enabled = true;
                    metroButton2.Enabled = true;
                    metroButton3.Enabled = true;//false
                    break;
                case 2:// разрешительно-визовой работы
                    metroButton1.Enabled = false;
                    metroButton2.Enabled = false;
                    metroButton3.Enabled = true;
                    break;
                case 3:// Начальство 
                    metroButton1.Enabled = false;
                    metroButton2.Enabled = false;
                    metroButton3.Enabled = false;
                    break;
                default: //off
                    metroLabel1.Text="Неизвестный пользователь";
                    metroLabel2.Text="Неизвестный пользователь";
                    metroButton1.Enabled = false;
                    metroButton2.Enabled = false;
                    metroButton3.Enabled = false;
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          this.Hide();
            Form2 avtoriz = new Form2();
            avtoriz.ShowDialog();
            if (Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                //Вытаскиваем из класса поля в label'ы
                metroLabel1.Text = Auth.auth_id;
                metroLabel2.Text = Auth.auth_fio;
                ManagerRole(Auth.auth_role);
                metroLabel3.Text = Auth.auth_sotr;
            }
            else
            {
                this.Close();
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test_Connect_Printer t = new Test_Connect_Printer();
            t.Show();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 avtoriz = new Form2();
            avtoriz.ShowDialog();
            if (Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                //Вытаскиваем из класса поля в label'ы
                metroLabel1.Text = Auth.auth_id;
                metroLabel2.Text = Auth.auth_fio;
                ManagerRole(Auth.auth_role);
                metroLabel3.Text = Auth.auth_sotr;
            }

        }
    }
}

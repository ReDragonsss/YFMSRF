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
    public partial class mainmenu : MetroFramework.Forms.MetroForm
    {
        public mainmenu()
        {
            InitializeComponent();
        }
        public void ManagerRole(int role)
        {
            switch (role)
            {
                case 1://отдел депортации
                    metroButton1.Enabled = true;
                    metroButton3.Enabled = false;
                    metroButton7.Enabled = false;
                    metroButton1.Visible = true;
                    metroButton3.Visible = false;
                    metroButton7.Visible = false;
                    metroButton2.Enabled = false;
                    metroButton2.Visible = false;
                    break;
                case 2:// разрешительно-визовой работы
                    metroButton1.Enabled = false;
                    metroButton3.Enabled = true;
                    metroButton7.Enabled = false;
                    metroButton1.Visible = false;
                    metroButton3.Visible = true;
                    metroButton7.Visible = false;
                    metroButton2.Enabled = false;
                    metroButton2.Visible = false;
                    break;
                case 3:// Начальство 
                    metroButton1.Enabled = false;
                    metroButton1.Visible = false;
                    metroButton7.Visible = true;
                    metroButton3.Enabled = true;
                    metroButton7.Enabled = true;
                    metroButton3.Visible = true;
                    metroButton7.Visible = true;
                    break;
                default: //off
                    metroLabel1.Text="Неизвестный пользователь";
                    metroLabel2.Text="Неизвестный пользователь";
                    metroButton1.Enabled = true;
                    metroButton3.Enabled = true;
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          this.Hide();
            autoriz avtoriz = new autoriz();
            avtoriz.ShowDialog();
            if (Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                //Вытаскиваем из класса поля в label'ы
                metroLabel1.Text = sotrudnik.auth_Fam;
                metroLabel2.Text = sotrudnik.auth_Ima;
                ManagerRole(Auth.auth_role);
                metroLabel3.Text = sotrudnik.auth_Otch;
                metroLabel5.Text = zvanie.auth_Zvan;
                Action.action = "Авторизовался";
                Aud instance = new Aud();
                bool auditResult = instance.Audit();
            }
            else
            {
                this.Close();
            }
        }
     
        private void metroButton1_Click(object sender, EventArgs e)
        {
            otddeport f7 = new otddeport();
            f7.Show();
        }
        private void metroButton3_Click(object sender, EventArgs e)
        {
            createviz f3 = new createviz();
            f3.Show();
        }
        private void metroButton4_Click(object sender, EventArgs e)
        {
            BazaDan f8 = new BazaDan();
            f8.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            TechHelp th = new TechHelp();
            th.Show();
        }
        private void metroButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            Connect_Printer t = new Connect_Printer();
            t.Show();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            autoriz avtoriz = new autoriz();
            avtoriz.ShowDialog();
            if (Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                //Вытаскиваем из класса поля в label'ы
                metroLabel1.Text = sotrudnik.auth_Fam;
                metroLabel2.Text = sotrudnik.auth_Ima;
                ManagerRole(Auth.auth_role);
                metroLabel3.Text = sotrudnik.auth_Otch;
                metroLabel5.Text = zvanie.auth_Zvan;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Audit t = new Audit();
            t.Show();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            otddeport f7 = new otddeport();
            f7.Show();
        }
    }
}

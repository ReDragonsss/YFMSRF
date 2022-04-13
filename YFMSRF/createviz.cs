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
    public partial class createviz : MetroFramework.Forms.MetroForm
    {
        public createviz()
        {
            InitializeComponent();
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Show();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            string p1 = metroTextBox1.Text;
            string p2 = metroTextBox2.Text;
            string p3 = metroTextBox3.Text;
            string p4 = metroTextBox4.Text;
            string p5 = metroTextBox5.Text;
            string p6 = metroTextBox6.Text;
            string p7 = metroTextBox7.Text;
            string p8 = metroTextBox8.Text;
            string p9 = metroTextBox9.Text;
            Insertin(p1, p2, p3, p4, p5, p6, p7, p8, p9);
        }
        public void Getinfo1()//метод для получения гражданства иностранца
        {
            PCS.ControlData.conn.Open();
            string sql = $"SELECT grajdanstvo FROM vizov_anketa Where kod_inostr='{Inostranci.inostr_id}'";
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
            string sql = $"SELECT fam,name,otch,pol,data_rojdenia FROM Osnov_dannie_inostr Where kod_inostr='{Inostranci.inostr_id}'";
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Inostranci.inostr_fam = reader[0].ToString();
                Inostranci.inostr_ima = reader[1].ToString();
                Inostranci.inostr_otch = reader[2].ToString();
                Inostranci.inostr_pol = reader[3].ToString();
                Inostranci.inostr_datar = reader[4].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
        }
        public void Getinfo3()//метод для получения номера паспорта иностранца
        {
            string sql = $"SELECT nomer_pass FROM pass Where kod_inostr='{Inostranci.inostr_id}'";
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Inostranci.inostr_pass = reader[0].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
        }
        
          public bool Insertin(string datav, string nasrock, string grajd, string fio, string nomberp, string datar, string pol, string prinimo, string dopols)
          {
            bool result = false;
            int InsertCount = 0;
            PCS.ControlData.conn.Open();
            string sql = $"INSERT INTO viza (data_vidachi,na_srock,grajdanstv,fio,nomber_pass,data_rojd,pol,prinim_organiz,dopol_sveden) VALUES ('{datav}','{nasrock}','{grajd}','{fio}','{nomberp}','{datar}','{pol}','{prinimo}','{dopols}')";
            try
            {
                MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
                InsertCount = command.ExecuteNonQuery();
            }
            catch (Exception osh)
            {
                //Если возникла ошибка, то запрос не вставит ни одной строки
                InsertCount = 0;
                MessageBox.Show($"Неповезло" + osh);
            }
            finally
            {
                //Ессли количество вставленных строк было не 0, то есть вставлена хотя бы 1 строка
                if (InsertCount != 0)
                {
                    result = true;
                }
                PCS.ControlData.conn.Close();
            }
            return result;
          }

        private void metroButton6_Click(object sender, EventArgs e)
        {//метод для обновления данных в тексбоксах
            Getinfo1();//нужен для переменной гражданство
            Getinfo2();//нужен для переменной фамилиля имя отчества пола даты рождения
            Getinfo3();//нужен для переменной паспорта
            metroTextBox3.Text = Inostranci.inostr_grajd;
            metroTextBox4.Text = $"{Inostranci.inostr_fam} {Inostranci.inostr_ima} {Inostranci.inostr_otch}";
            metroTextBox5.Text = Inostranci.inostr_pass;
            metroTextBox6.Text = Inostranci.inostr_datar;
            metroTextBox7.Text = Inostranci.inostr_pol;
        }
    }
}

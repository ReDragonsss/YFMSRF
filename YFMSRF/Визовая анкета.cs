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
    public partial class Form5 : MetroFramework.Forms.MetroForm
    {
        public Form5()
        {
            InitializeComponent();
        }
        MySqlConnection conn;

        private void Form5_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_2_21_19;database=st_2_21_19;password=30518003";
            conn=new MySqlConnection(connStr);
        }
        public bool InsertComp(string strana,string grad, string fam, string vid, string num, string kod, string home, string srok, string proff, string cel, string pred, string data, string mest, string ikod)//strana grad fam vid num kod home srok proff cel pred data mest
        { 
            bool result = false;
            int InsertCount = 0;
            conn.Open();
            string sql = $"INSERT INTO vizov_anketa (strana_rojdenia,grajdanstvo,family_polojenie,vid_zagran_pass,number_zagran_pass,srok_zagran_pass,kod_gosydarstv,home_adress,profession_deutelnost,cel_prebiv,predastav_otpechatki,data_one_vezda_and_viezda,mest_podpis_vizi,kod_inostr) VALUES ('{strana}','{grad}','{fam}','{vid}','{num}','{kod}','{home}','{srok}','{proff}','{cel}','{pred}','{data}','{mest}','{ikod}')";
            try
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
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
                conn.Close();
            }
            return result;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string p1 = metroTextBox1.Text;
            string p2 = metroTextBox2.Text;
            string p3 = metroTextBox3.Text;
            string p4 = metroComboBox1.Text;
            string p5 = metroTextBox4.Text;
            string p6 = metroTextBox5.Text;
            string p7 = metroTextBox6.Text;
            string p8 = metroTextBox7.Text;
            string p9 = metroTextBox8.Text;
            string p10 = metroTextBox9.Text;
            string p11 = metroComboBox2.Text;
            string p12 = metroTextBox10.Text;
            string p13 = metroTextBox11.Text;
            string p14 = Inostranci.inostr_id;
            InsertComp(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

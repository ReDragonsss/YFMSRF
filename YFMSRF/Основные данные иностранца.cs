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
    public partial class Form4 : MetroFramework.Forms.MetroForm
    {
        public Form4()
        {
            InitializeComponent();
        }
        MySqlConnection conn;

        public bool InsertComp(string famil, string name, string otcestv, string pol, string rojd, string mestro)
        {
            bool result = false;
            int InsertCount = 0;
            conn.Open();
            string sql = $"INSERT INTO Osnov_dannie_inostr (fam, name, otchestv, pol, data_rojdenia, mestro_rojden) VALUES ('{famil}', '{name}', '{otcestv}', '{pol}', '{rojd}','{mestro}')";
            try
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                InsertCount = command.ExecuteNonQuery();
            }
            catch
            {
                //Если возникла ошибка, то запрос не вставит ни одной строки
                InsertCount = 0;
            }
            finally
            {
                conn.Close();
                //Ессли количество вставленных строк было не 0, то есть вставлена хотя бы 1 строка
                if (InsertCount !=0)
                {
                    result = true;
                }
            }
            return result;
        }
            
            private void Form4_Load(object sender, EventArgs e)
            {
                string connStr = "server=caseum.ru;port=33333;user=st_2_21_19;database=st_2_21_19;password=30518003";
                conn=new MySqlConnection(connStr);
            }

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string famil = metroTextBox1.Text;
            string name = metroTextBox2.Text;
            string otcestv = metroTextBox3.Text;
            string pol = metroTextBox4.Text;
            string rojd= metroTextBox5.Text;
            string mestro = metroTextBox6.Text;
            InsertComp(famil, name, otcestv, pol, rojd, mestro);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
                try
                {
                    conn.Open();
                    MessageBox.Show("База данных работает стабильно");
                    conn.Close();
                }
                catch (Exception osh)
                {
                    MessageBox.Show("Произошла ошибка"+ osh);
                    conn.Close();
                }
        }
    }
}

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

        public bool InsertComp(string famil, string name, string otcestv, string pol, string rojd, string mestro)
        {
            bool result = false;
            int InsertCount = 0;
            PCS.ControlData.conn.Open();
            string sql = $"INSERT INTO Osnov_dannie_inostr (fam, name, otchestv, pol, data_rojdenia, mesto_rojden) VALUES ('{famil}','{name}','{otcestv}','{pol}','{rojd}','{mestro}')";
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
                if (InsertCount !=0)
                {
                    result = true;
                }
                PCS.ControlData.conn.Close();
            }
            return result;
        }

        private void Form4_Load(object sender, EventArgs e)
            {
            }

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string p1 = metroTextBox1.Text;
            string p2 = metroTextBox2.Text;
            string p3 = metroTextBox3.Text;
            string p4 = metroTextBox4.Text;
            string p5 = metroTextBox5.Text;
            string p6 = metroTextBox6.Text;
            InsertComp(p1,p2,p3,p4,p5,p6);
            Getinfo();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
                try
                {
                    PCS.ControlData.conn.Open();
                    MessageBox.Show("База данных работает стабильно");
                    PCS.ControlData.conn.Close();
                }
                catch (Exception osh)
                {
                    MessageBox.Show("Произошла ошибка"+ osh);
                    PCS.ControlData.conn.Close();
                }
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Getinfo()
        {
            string o = metroTextBox1.Text;
            PCS.ControlData.conn.Open();
            string sql2 = $"SELECT kod_inostr FROM Osnov_dannie_inostr Where fam='{o}'";
            MySqlCommand command = new MySqlCommand(sql2, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Inostranci.inostr_id = reader[0].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            createviz viz = new createviz();
            viz.metroTextBox4.Text = metroTextBox1.Text;
        }
    }
}

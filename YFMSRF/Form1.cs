using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using YFMSRF;
using MetroFramework.Controls;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace YFMSRF
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void GetListUsers(string commandStr)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.HorizontalScrollbar = false;
            PCS.ControlData.conn.Open();
            string sql = "SELECT data_vidachi, na_srock, grajdanstv, fio, nomber_pass, data_rojd, pol, prinim_organiz, dopol_sveden FROM viza WHERE id_viz = " + Dell.id + "";
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
              viza.data_vidachi = reader[0].ToString();
              viza.na_srock = reader[1].ToString();
              viza.grajdanstv = reader[2].ToString();
              viza.fio = reader[3].ToString();
              viza.nomber_pass = reader[4].ToString();
              viza.data_rojd = reader[5].ToString();
              viza.pol = reader[6].ToString();
              viza.prinim_organiz = reader[7].ToString();
              viza.dopol_sveden = reader[8].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
            metroTextBox1.Text = viza.data_vidachi;
            metroTextBox2.Text = viza.na_srock;
            metroTextBox3.Text = viza.grajdanstv;
            metroTextBox4.Text = viza.fio;
            metroTextBox5.Text = viza.nomber_pass;
            metroTextBox6.Text = viza.data_rojd;
            metroTextBox7.Text = viza.pol;
            metroTextBox8.Text = viza.prinim_organiz;
            listBox1.Items.Add(viza.dopol_sveden);
        }
       
        private void metroTextBox3_Click(object sender, EventArgs e)
        {
            
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            string p1 = metroTextBox1.Text;
            string p2 = metroTextBox2.Text;
            string p3 = metroTextBox3.Text;
            string p4 = metroTextBox4.Text;
            string p5 = metroTextBox5.Text;
            string p6 = metroTextBox6.Text;
            string p7 = metroTextBox7.Text;
            string p8 = metroTextBox8.Text;
            string p9 = (string)listBox1.Items[0];
            Update(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            Action.action = "изменил информацию о " + viza.fio + "";
            Aud instance = new Aud();
            bool auditResult = instance.Audit();
            this.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public bool Update(string data, string srock, string grajd, string fio, string num, string rojd, string pol, string organiz, string sveden)
        {
            bool result = false;
            int InsertCount = 0;
            PCS.ControlData.conn.Open();
            string sql = $"UPDATE viza SET data_vidachi ='{data}', na_srock='{srock}', grajdanstv='{grajd}', fio='{fio}', nomber_pass='{num}', data_rojd='{rojd}', pol='{pol}', prinim_organiz='{organiz}', dopol_sveden='{sveden}' WHERE id_viz = " + Dell.id + "";
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
    }
}

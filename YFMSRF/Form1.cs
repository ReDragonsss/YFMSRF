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
            PCS.ControlData.conn.Open();
            string sql = "SELECT fam,name,otchestv,pol,data_rojdenia,mesto_rojden,kod_inostr FROM Osnov_dannie_inostr WHERE kod_inostr = " + Dell.id + "";
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Inostranci.inostr_fam = reader[0].ToString();
                Inostranci.inostr_ima = reader[1].ToString();
                Inostranci.inostr_otch = reader[2].ToString();
                Inostranci.inostr_datar = reader[3].ToString();
                Inostranci.inostr_grajd = reader[4].ToString();
                Inostranci.inostr_pass = reader[5].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
            metroTextBox1.Text = Inostranci.inostr_fam;
            metroTextBox2.Text = Inostranci.inostr_ima;
            metroTextBox3.Text = Inostranci.inostr_otch;
            metroTextBox4.Text = Inostranci.inostr_datar;
            metroTextBox5.Text = Inostranci.inostr_grajd;
            metroTextBox6.Text = Inostranci.inostr_pass;
        }
        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox4_Click(object sender, EventArgs e)
        {

        }
    }
}

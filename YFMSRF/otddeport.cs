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
    public partial class otddeport : MetroFramework.Forms.MetroForm
    {
        public otddeport()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string p1 = metroTextBox1.Text;
            string p2 = metroTextBox2.Text;
            string p3 = metroTextBox3.Text;
            string p4 = metroTextBox4.Text;
            string p5 = metroTextBox13.Text;
            string p6 = metroTextBox12.Text;
            string p7 = metroTextBox11.Text;
            string p8 = metroTextBox10.Text;
            string p9 = metroTextBox9.Text;
            string p10 = metroTextBox5.Text;
            string p11 = metroTextBox6.Text;
            string p12 = metroTextBox8.Text;
            string p13 = metroTextBox14.Text;
            InsertComp(p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13);

        }
        public bool InsertComp(string dolj, string fams, string inics, string specz, string graj, string ima, string fam, string otch, string data_pribitstring,string datapod, string datapol, string obsto, string poloj)
        {
            bool result = false;
            int InsertCount = 0;
            PCS.ControlData.conn.Open();
            string sql = $"INSERT INTO nerazrehviezde (doljnost, famil_sotr, inicial_sotr, spec_zvan, grajdan, ima, fam, otch, data_pribit, data_podpisan, data_polych, obstoatel_osn, poloj_fed) VALUES ('{dolj}','{fams}','{inics}','{specz}','{graj}','{ima}','{fam}','{otch}','{data_pribitstring}','{datapod}','{datapol}','{obsto}','{poloj}')";
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

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

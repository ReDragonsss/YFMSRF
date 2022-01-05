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
        public bool InsertComp(string spass, string npass, string dvidach, string pvidach, string ovidpass, string kpodraz, string strrojd, string godrojd, string vidpropis, string dataprop, string regpop, string gorprop, string naselpunkt, string ylica, string dom, string stroen, string kvart, string organ, string vidregistr)
        {
            bool result = false;
            int InsertCount = 0;
            conn.Open();
            string sql = $"INSERT INTO pass (seria_pass,nomer_pass,data_vidachi,punkt_vidachi,nazvan_organa,kod_podrazd,strana_rojden,gorod_rojden,naselen_punkt_rojden,vid_propiski_ycheta,data_propiski,region_propiski,gorod_propiski,naselenni_punkt,ylica,dom,stroenie,kvartira,organ_ycheta) VALUES ('{spass}','{npass}','{dvidach}','{pvidach}','{ovidpass}','{kpodraz}','{strrojd}','{godrojd}','{vidpropis}','{dataprop}','{regpop}','{gorprop}','{naselpunkt}','{ylica}','{dom}','{stroen}','{kvart}','{organ}''{vidregistr}')";
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

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }
    }
}

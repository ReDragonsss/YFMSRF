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
    public partial class Form6 : MetroFramework.Forms.MetroForm
    {
        public Form6()
        {
            InitializeComponent();
        }
        public bool InsertComp(string spass, string npass, string dvidach, string pvidach, string norgan, string kpodraz,string strrojd, string godrojd, string naselpunkt, string vidprop, string dprop, string regprop, string gprop, string rnaselpunkt, string ylica, string dom, string stroen, string kvartira, string organych,string ikod)//spass npass dvidach pvidach ovidpass kpodraz strrojd godrojd vidpropis dataprop regpop gorprop naselpunkt ylica dom stroen kvart organ vidregistr ikod
        {
            bool result = false;
            int InsertCount = 0;
            PCS.ControlData.conn.Open();
            string sql = $"INSERT INTO pass (seria_pass,nomer_pass,data_vidachi,punkt_vidachi,nazvan_organa,kod_podrazd,strana_rojdenia,god_vidach,naselen_punkt_rojden,vid_propiski_ycheta,data_propiski,region_propiski,gorod_propiski,naselenni_punkt,ylica,dom,stroenie,kvartira,organ_ycheta,kod_inostr) VALUES ('{spass}','{npass}','{dvidach}','{pvidach}','{norgan}','{kpodraz}','{strrojd}','{godrojd}','{naselpunkt}','{vidprop}','{dprop}','{regprop}','{gprop}','{rnaselpunkt}','{ylica}','{dom}','{stroen}','{kvartira}','{organych}','{ikod}')";
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

        private void Form6_Load(object sender, EventArgs e)
        {
        }

        private void metroTextBox15_Click(object sender, EventArgs e)
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
            string p7 = metroTextBox7.Text;
            string p8 = metroTextBox8.Text;
            string p9 = metroTextBox9.Text;
            string p10 = metroComboBox1.Text;
            string p11 = metroTextBox11.Text;
            string p12 = metroTextBox12.Text;
            string p13 = metroTextBox13.Text;
            string p14 = metroTextBox14.Text;
            string p15 = metroTextBox15.Text;
            string p16 = metroTextBox16.Text;
            string p17 = metroTextBox17.Text;
            string p18 = metroTextBox18.Text;
            string p19 = metroTextBox10.Text;
            string p20 = Inostranci.inostr_id;
            InsertComp(p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,p15,p16,p17,p18,p19,p20);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

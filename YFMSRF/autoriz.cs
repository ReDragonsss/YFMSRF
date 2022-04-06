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
    public partial class autoriz : MetroFramework.Forms.MetroForm
    {
        static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public void GetUserInfo(string login)
        {
            PCS.ControlData.conn.Open();
            string sql = $"SELECT * FROM Auto WHERE login='{login}'";
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Auth.auth_id = reader[0].ToString();
                Auth.auth_login = reader[1].ToString();
                Auth.auth_role = Convert.ToInt32(reader[3].ToString());
                Auth.auth_sotr= reader[4].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
            GetSotrydInfo();
            GetZvanieinfo();
        }
        public autoriz()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            metroTextBox3.Visible = false;
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Auto WHERE login = @un and password= @up";
            PCS.ControlData.conn.Open();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(sql, PCS.ControlData.conn);
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            command.Parameters.Add("@up", MySqlDbType.VarChar, 25);
            command.Parameters["@un"].Value = metroTextBox1.Text;
            command.Parameters["@up"].Value = sha256(metroTextBox2.Text);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            PCS.ControlData.conn.Close();
            if (table.Rows.Count > 0)
            {
                Auth.auth = true;
                GetUserInfo(metroTextBox1.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверные данные авторизации!");
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void metroTextBox2_TextChanged(object sender, EventArgs e)
        {
            metroTextBox3.Text = sha256(metroTextBox2.Text);
        }
        public void GetSotrydInfo()
        {
            PCS.ControlData.conn.Open();
            string sql1 = $"SELECT famil, ima, otchestv, id_zvanie  FROM sotrudnik Where id_sotrud='{Auth.auth_sotr}'";
            MySqlCommand command = new MySqlCommand(sql1, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                sotrudnik.auth_Ima = reader[0].ToString();
                sotrudnik.auth_Fam = reader[1].ToString();
                sotrudnik.auth_Otch = reader[2].ToString();
                sotrudnik.auth_idZvan = reader[3].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
        }
        public void GetZvanieinfo()
        {
            PCS.ControlData.conn.Open();
            string sql2 = $"SELECT zvanie FROM zvanie Where id_zvanie='{sotrudnik.auth_idZvan}'";
            MySqlCommand command = new MySqlCommand(sql2, PCS.ControlData.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                zvanie.auth_Zvan = reader[0].ToString();
            }
            reader.Close();
            PCS.ControlData.conn.Close();
        }
    }
}

//public void GetSotrydInfo()
//{
//    string sql1 = "SELECT * FROM sotrudnik";
//    MySqlCommand command = new MySqlCommand(sql1, conn);
//    MySqlDataReader reader = command.ExecuteReader();
//    while (reader.Read())
//    {
//        sotrudnik.auth_Ima=reader[0].ToString();
//        sotrudnik.auth_Fam=reader[1].ToString();
//        sotrudnik.auth_Otch=reader[2].ToString();
//        sotrudnik.auth_idZvan=reader[6].ToString();

//    }
//}
//public void GetZvanieinfo()
//{
//    string sql2 = "SELECT * FROM zvanie";
//    MySqlCommand command = new MySqlCommand(sql2, conn);
//    MySqlDataReader reader = command.ExecuteReader();
//    while (reader.Read())
//    {
//        zvanie.auth_Zvan=reader[1].ToString();
//    }
//}
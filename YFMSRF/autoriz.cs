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
        string connStr = "server=caseum.ru;port=33333;user=st_2_21_19;database=st_2_21_19;password=30518003";
        MySqlConnection conn;
        static string sha256(string randomString)
        {
            //Тут происходит криптографическая магия. Смысл данного метода заключается в том, что строка залетает в метод
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
            conn.Open();
            string sql = $"SELECT * FROM Auto WHERE login='{login}'";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Auth.auth_id = reader[0].ToString();
                Auth.auth_login = reader[1].ToString();
                Auth.auth_role = Convert.ToInt32(reader[3].ToString());
                Auth.auth_sotr= reader[4].ToString();
            }
            reader.Close(); 
            conn.Close();
            {
               // SELECT sotrudnik.id_zvanie
                //FROM zvanie INNER JOIN sotrudnik ON zvanie.id_zvanie = sotrudnik.id_zvanie;
            }
        }

        public autoriz()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Auto WHERE login = @un and password= @up";
            conn.Open();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            command.Parameters.Add("@up", MySqlDbType.VarChar, 25);
            command.Parameters["@un"].Value = metroTextBox1.Text;
            command.Parameters["@up"].Value = sha256(metroTextBox2.Text);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            conn.Close();
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

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }
    }
}

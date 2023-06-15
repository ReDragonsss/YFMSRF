using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace YFMSRF
{
    public partial class Audit : MetroFramework.Forms.MetroForm
    {
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        protected BindingSource bSource;
        private DataTable table;
        public Audit()
        {
            InitializeComponent();
        }

        private void Audit_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            bSource = new BindingSource();
            string commandStr = $"SELECT id_actions, name, fam, otch, auth_zvan, actions, times FROM audit_log";
            PCS.ControlData.conn.Open();
            MyDA.SelectCommand = new MySqlCommand(commandStr, PCS.ControlData.conn);
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            PCS.ControlData.conn.Close();
            dataGridView1.Columns[0].FillWeight = 6;
            dataGridView1.Columns[1].FillWeight = 11;
            dataGridView1.Columns[2].FillWeight = 12;
            dataGridView1.Columns[3].FillWeight = 12;
            dataGridView1.Columns[4].FillWeight = 15;
            dataGridView1.Columns[5].FillWeight = 29;
            dataGridView1.Columns[6].FillWeight = 15;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTextBox5_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

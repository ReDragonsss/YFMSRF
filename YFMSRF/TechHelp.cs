using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YFMSRF
{
    public partial class TechHelp : MetroFramework.Forms.MetroForm
    {
        public TechHelp()
        {
            InitializeComponent();
        }

        private void TechHelp_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ReDragonsss/Prog_Reginas");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/redragonss");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://disk.yandex.ru/client/disk/%D0%9F%D1%80%D0%BE%D0%B3%D0%B0");
        }
    }
}

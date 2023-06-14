using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace YFMSRF.PCS
{
    class ControlData
    {
        public static string ComboId = "0";
        private const string host = "chuc.sdlik.ru";
        private const string port = "33333";
        private const string database = "VKR_TitovD";
        private const string username = "VKR_TitovD";
        private const string password = "iVr7Kpzjp6-LpZ]A";
        //Объявляем и инициализируем соединение
        public static readonly MySqlConnection conn = new MySqlConnection($"server={host};port={port};user={username};database={database};password={password};");
    }

}
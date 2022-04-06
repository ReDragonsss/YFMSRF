using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace YFMSRF.PCS
{

    class ControlData
    {
        public static string ComboId = "0";
        private const string host = "chuc.caseum.ru";
        private const string port = "33333";
        private const string database = "is_2_19_st21_KURS";
        private const string username = "st_2_19_21";
        private const string password = "70964010";
        //Объявляем и инициализируем соединение
        public static readonly MySqlConnection conn = new MySqlConnection($"server={host};port={port};user={username};database={database};password={password};");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YFMSRF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainmenu());
        }
    }
    static class Auth
    {
        //Статичное поле, которое хранит значение статуса авторизации
        public static bool auth = false;
        //Статичное поле, которое хранит значения ID пользователя
        public static string auth_id = null;
        //Статичное поле, которое хранит значения ФИО пользователя
        public static string auth_login = null;
        //статик поле для данных сотрудника
        public static string auth_sotr = null;
        //Статичное поле, которое хранит количество привелегий пользователя
        public static int auth_role = 0;
    }
    static class sotrudnik
    {
        public static string auth_Ima = null;// Имя сотрудника
        public static string auth_Fam = null;// Фамилия
        public static string auth_Otch = null;// Отчество
        public static string auth_idZvan = null;// ИдЗван
    }
    static class zvanie
    {
        public static string auth_Zvan = null;// Зван
    }
    static class Inostranci
    {
        public static string inostr_id = null;// хронит в сердце своем уникальный номер иностранца без визы 
    }
}


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
    public static class Auth// данные авторизации
    {
        public static bool auth = true;//Статичное поле, которое хранит значение статуса авторизации
        public static string auth_id = null;//Статичное поле, которое хранит значения ID пользователя
        public static string auth_login = null;//Статичное поле, которое хранит значения ФИО пользователя
        public static string auth_sotr = null;//статик поле для данных сотрудника
        public static int auth_role = 0;//Статичное поле, которое хранит количество привелегий пользователя
    }
    static class sotrudnik// данные сотрудника
    {
        public static string auth_Ima = null;// Имя сотрудника
        public static string auth_Fam = null;// Фамилия
        public static string auth_Otch = null;// Отчество
        public static string auth_idZvan = null;// ИдЗван
    }
    static class zvanie//данные звания
    {
        public static string auth_Zvan = null;// Зван
    }
    static class Inostranci//данные иностранца
    {
        public static string inostr_id = null;// хранит в сердце своем уникальный номер иностранца без визы 
        public static string inostr_ima = null; //Статичное поле, которое хранит значение имени
        public static string inostr_fam = null; //Статичное поле, которое хранит значение фамилии
        public static string inostr_otch = null; //Статичное поле, которое хранит значение отчества
        public static string inostr_pass = null; //Статичное поле, которое хранит значение паспорта
        public static string inostr_datar = null; //Статичное поле, которое хранит значение даты 
        public static string inostr_pol = null; //Статичное поле, которое хранит значение пола
        public static string inostr_grajd = null;//Статичное поле, которое хранит значение гражданства
    }
    public static class Dell// данные авторизации
    {
        public static string dell = null;//Статичное поле, которое хранит значение статуса авторизации
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using PCBuilder.Localization;

namespace PCBuilder.Utilites
{
    public class LocalizationManager
    {
        private static object _temp0;
        private static object _temp1;

        public static LocalizationManager Instance { get; private set; }

        private Application _app;

        static LocalizationManager()
        {
            _temp0 = new object();
            _temp1 = new object();
        }

        public LocalizationManager(Application app)
        {
            _app = app;

            foreach (var item in typeof(Eng).GetProperties())
            {
                if (item.Name.StartsWith("Loc"))
                {
                    _app.Resources.Add(item.Name, "INIT");
                }
            }

            string culture = CultureInfo.CurrentCulture.Name;

            Language cur = culture == "ru-RU" || culture == "ru-BY" ? Language.RUS : Language.ENG;

            SetLang(cur);
        }

        public void SetLang(Language language)
        {
            PropertyInfo[] properties;

            switch (language)
            {
                case Language.ENG:
                    properties = typeof(Eng).GetProperties();
                    break;
                case Language.RUS:
                    properties = typeof(Rus).GetProperties();
                    break;
                default:
                    properties = typeof(Eng).GetProperties();
                    break;
            }

            foreach (var item in properties)
            {
                if (item.Name.StartsWith("Loc"))
                {
                    _app.Resources[item.Name] = (string)item.GetValue(typeof(Eng));
                }
            }
        }

        public static void CreateInstance(Application app)
        {
            lock (_temp0)
            {
                if (Instance == null)
                {
                    lock (_temp1)
                    {
                        Instance = new LocalizationManager(app);
                    }
                }
            }
        }
    }

    public enum Language
    {
        ENG, RUS
    }
}

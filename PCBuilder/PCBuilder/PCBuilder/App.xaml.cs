using PCBuilder.Localization;
using PCBuilder.Utilites;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace PCBuilder
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LocalizationManager.CreateInstance(this);

            //LocalizationManager.Instance.SetLang(Language.ENG);
        }
    }
}

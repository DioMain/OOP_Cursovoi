using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.Utilites;
using PCBuilder.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCBuilder.View
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private string _hash0, _hash1;

        public TestWindow()
        {
            InitializeComponent();

            DataContext = this;

            t0.Text = "null";
            t1.Text = "null";
            t2.Text = "false";

            _hash0 = "";
            _hash1 = "";
        }

        private BaseCommand ClickCommand;
        public ICommand Click
        {
            get
            {
                if (ClickCommand == null)
                    ClickCommand = new BaseCommand(ClickExecuted);

                return ClickCommand;
            }
        }
        private void ClickExecuted(object obj)
        {
            MessageBox.Show("Work");
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _hash0 = PasswordHasher.GetHash(tb0.Text);

            t0.Text = _hash0;

            CheckHash();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            _hash1 = "a";

            t1.Text = Image.SourceProperty.PropertyType.Name;

            CheckHash();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();

            Close();
        }

        private void CheckHash()
        {
            t2.Text = PasswordHasher.Compare(_hash0, _hash1).ToString();
        }
    }
}

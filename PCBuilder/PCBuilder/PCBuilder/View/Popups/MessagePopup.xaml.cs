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
using System.Windows.Shapes;

namespace PCBuilder.View.Popups
{
    /// <summary>
    /// Логика взаимодействия для MessagePopup.xaml
    /// </summary>
    public partial class MessagePopup : Window
    {
        public MessagePopup(string tittle, string message, bool isCenter = false)
        {
            InitializeComponent();

            if (isCenter)
                messageBox.TextAlignment = TextAlignment.Center;

            titleBox.Text = tittle;
            messageBox.Text = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

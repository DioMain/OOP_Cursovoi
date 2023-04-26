using PCBuilder.Commands;
using PCBuilder.Repositories;
using PCBuilder.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PCBuilder.ViewModel
{
    public class LoginWindowVM : WindowViewModel<LoginWindow>
    {
        public LoginWindowVM(LoginWindow owner) : base(owner)
        {
            SetState();

            DataBaseManager.CreateInstance();

            DataBaseManager.Instance.OnConnected += DataBase_OnConnected;
            DataBaseManager.Instance.OnError += DataBase_OnError;

            DataBaseManager.Instance.ConnectAsync();
        }

        private void DataBase_OnError(string message, DataBaseErrorType errorType)
        {
            SetState(1);
        }
        private void DataBase_OnConnected()
        {
            ThicknessAnimation maranim = new ThicknessAnimation();

            maranim.Duration = TimeSpan.FromSeconds(0.5d);
            maranim.From = new Thickness(30, 85, 40, 0);
            maranim.To = Owner.Main.Margin;
            maranim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 2 };

            SetState(2);
            Owner.Main.BeginAnimation(FrameworkElement.MarginProperty, maranim);
        }

        public void SetState(int state = 0)
        {
            switch (state)
            {
                case 1:
                    Owner.Failed.Visibility = Visibility.Visible;
                    Owner.Loading.Visibility = Visibility.Collapsed;
                    Owner.Main.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    Owner.Failed.Visibility = Visibility.Collapsed;
                    Owner.Loading.Visibility = Visibility.Collapsed;
                    Owner.Main.Visibility = Visibility.Visible;
                    break;
                default:
                    Owner.Failed.Visibility = Visibility.Collapsed;
                    Owner.Loading.Visibility = Visibility.Visible;
                    Owner.Main.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        #region Commands

        #region To regitration

        private BaseCommand ToRegistrationCommand;
        public ICommand ToRegistration
        {
            get
            {
                if (ToRegistrationCommand == null)
                    ToRegistrationCommand = new BaseCommand(ToRegistrationInvoke);

                return ToRegistrationCommand;
            }
        }
        private void ToRegistrationInvoke(object obj)
        {
            new RegistrationWindow().Show();

            Owner.Close();
        }

        #endregion

        #endregion

        public override void Dispose()
        {
            DataBaseManager.Instance.OnConnected -= DataBase_OnConnected;
            DataBaseManager.Instance.OnError -= DataBase_OnError;
        }
    }
}

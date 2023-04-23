using PCBuilder.Repositories;
using PCBuilder.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.ViewModel
{
    public class LoginWindowVM : BaseViewModel<LoginWindow>
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
            SetState(2);
        }

        public void SetState(int state = 0)
        {
            switch (state)
            {
                case 1:
                    Owner.Failed.Visibility = System.Windows.Visibility.Visible;
                    Owner.Loading.Visibility = System.Windows.Visibility.Hidden;
                    Owner.Main.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 2:
                    Owner.Failed.Visibility = System.Windows.Visibility.Hidden;
                    Owner.Loading.Visibility = System.Windows.Visibility.Hidden;
                    Owner.Main.Visibility = System.Windows.Visibility.Visible;
                    break;
                default:
                    Owner.Failed.Visibility = System.Windows.Visibility.Hidden;
                    Owner.Loading.Visibility = System.Windows.Visibility.Visible;
                    Owner.Main.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
        }

        #region Commands

        #endregion

        public override void Dispose()
        {
            DataBaseManager.Instance.OnConnected -= DataBase_OnConnected;
            DataBaseManager.Instance.OnError -= DataBase_OnError;
        }
    }
}

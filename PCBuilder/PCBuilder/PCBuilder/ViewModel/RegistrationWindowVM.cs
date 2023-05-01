using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.View;
using PCBuilder.View.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class RegistrationWindowVM : WindowViewModel<RegistrationWindow>
    {
        public RegistrationWindowVM(RegistrationWindow owner) : base(owner)
        {
            DropErrors();
        }

        public void DropErrors()
        {
            Owner.addressError.Visibility = Visibility.Hidden;
            Owner.contNumError.Visibility = Visibility.Hidden;
            Owner.emailError.Visibility = Visibility.Hidden;
            Owner.FNameError.Visibility = Visibility.Hidden;
            Owner.LNameError.Visibility = Visibility.Hidden;
            Owner.passError.Visibility = Visibility.Hidden;
            Owner.repPassError.Visibility = Visibility.Hidden;
        }

        #region Commands

        #region Submit

        private BaseCommand SubmitCommand;
        public ICommand Submit
        {
            get
            {
                if (SubmitCommand == null)
                    SubmitCommand = new BaseCommand(SubmitExecuted, SubmitCanExecuted);

                return SubmitCommand;
            }
        }
        public bool SubmitCanExecuted(object obj)
        {
            DropErrors();

            bool alright = true;

            Regex emailRegex = new Regex(@"^(\w|\.)+@(\w)+\.(\w)+$");
            Regex numberRegex = new Regex(@"^\+(\d){6,14}$");

            if (Owner.emailField.Text == null || Owner.emailField.Text == string.Empty)
            {
                alright = false;

                Owner.emailError.Text = (string)Application.Current.Resources["Loc_Global_EmptyField"];
                Owner.emailError.Visibility = Visibility.Visible;
            }
            else if (DataBaseManager.Instance.Users.GetByEmail(Owner.emailField.Text) != null)
            {
                alright = false;

                Owner.emailError.Text = (string)Application.Current.Resources["Loc_Reg_Email_Err0"];
                Owner.emailError.Visibility = Visibility.Visible; 
            }
            else if (!emailRegex.IsMatch(Owner.emailField.Text))
            {
                alright = false;

                Owner.emailError.Text = (string)Application.Current.Resources["Loc_Global_NotRightFormat"];
                Owner.emailError.Visibility = Visibility.Visible;
            }

            if (Owner.passField.Password == null || Owner.passField.Password == string.Empty)
            {
                alright = false;

                Owner.passError.Text = (string)Application.Current.Resources["Loc_Global_EmptyField"];
                Owner.passError.Visibility = Visibility.Visible;
            }

            if (Owner.repPassField.Password == null || Owner.repPassField.Password == string.Empty)
            {
                alright = false;

                Owner.repPassError.Text = (string)Application.Current.Resources["Loc_Global_EmptyField"];
                Owner.repPassError.Visibility = Visibility.Visible;
            }

            if (alright && Owner.repPassField.Password != Owner.passField.Password)
            {
                alright = false;

                Owner.passError.Text = (string)Application.Current.Resources["Loc_Reg_Pass_Err"];
                Owner.passError.Visibility = Visibility.Visible;
            }

            if (Owner.fnameField.Text == null || Owner.fnameField.Text == string.Empty)
            {
                alright = false;

                Owner.FNameError.Text = (string)Application.Current.Resources["Loc_Global_EmptyField"];
                Owner.FNameError.Visibility = Visibility.Visible;
            }
            if (Owner.lnameField.Text == null || Owner.lnameField.Text == string.Empty)
            {
                alright = false;

                Owner.LNameError.Text = (string)Application.Current.Resources["Loc_Global_EmptyField"];
                Owner.LNameError.Visibility = Visibility.Visible;
            }

            if (Owner.addressField.Text == null || Owner.addressField.Text == string.Empty)
            {
                alright = false;

                Owner.addressError.Text = (string)Application.Current.Resources["Loc_Global_EmptyField"];
                Owner.addressError.Visibility = Visibility.Visible;
            }

            if (Owner.contNumField.Text == null || Owner.contNumField.Text == string.Empty)
            {
                alright = false;

                Owner.contNumError.Text = (string)Application.Current.Resources["Loc_Global_EmptyField"];
                Owner.contNumError.Visibility = Visibility.Visible;
            }
            else if (!numberRegex.IsMatch(Owner.contNumField.Text))
            {
                alright = false;

                Owner.contNumError.Text = (string)Application.Current.Resources["Loc_Global_NotRightFormat"];
                Owner.contNumError.Visibility = Visibility.Visible;
            }

            return alright;
        }
        private void SubmitExecuted(object obj)
        {
            User nuser = new User()
            {
                Email = Owner.emailField.Text,
                Password = PasswordHasher.GetHash(Owner.passField.Password),
                FirstName = Owner.fnameField.Text,
                LastName = Owner.lnameField.Text,
                Address = Owner.addressField.Text,
                LastAuthDate = DateTime.Now,
                ContactNumber = Owner.contNumField.Text,
                Rights = "USER"
            };

            DataBaseManager.Instance.Users.Add(nuser);

            new MessagePopup(
                (string)Application.Current.Resources["Loc_Reg_Popup_Title"],
                (string)Application.Current.Resources["Loc_Reg_Popup_Message"],
                true
                ).ShowDialog();

            LoginWindow login = new LoginWindow();
            login.Show();

            Owner.Close();
        }

        #endregion

        #region Back to login

        private BaseCommand BackCommand;
        public ICommand Back
        {
            get
            {
                if (BackCommand == null)
                    BackCommand = new BaseCommand(BackExecuted);

                return BackCommand;
            }
        }
        private void BackExecuted(object obj)
        {
            new LoginWindow().Show();

            Owner.Close();
        }

        #endregion

        #endregion
    }
}

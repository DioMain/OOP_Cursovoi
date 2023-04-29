using PCBuilder.Commands;
using PCBuilder.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class RegistrationWindowVM : WindowViewModel<RegistrationWindow>
    {
        #region Props

        private bool isError;
        public bool IsError
        {
            get => isError;
            set
            {
                isError = value;
                OnPropertyChanged(nameof(IsError));
            }
        }

        #endregion

        public RegistrationWindowVM(RegistrationWindow owner) : base(owner)
        {
            isError = false;
        }



        #region Commands

        #region Submit

        private BaseCommand SubmitCommand;
        public ICommand Submit
        {
            get
            {
                if (SubmitCommand == null)
                    SubmitCommand = new BaseCommand(SubmitExecuted);

                return SubmitCommand;
            }
        }
        private void SubmitExecuted(object obj)
        {

        }

        #endregion

        #endregion
    }
}

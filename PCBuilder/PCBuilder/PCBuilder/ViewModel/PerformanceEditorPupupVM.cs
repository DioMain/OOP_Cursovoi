using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.View.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class PerformanceEditorPupupVM : WindowViewModel<PerformanceEditorPopup>
    {
        public string[] Types { get => Enum.GetNames(typeof(PerformanceDependency)); }

        public Performance Result;

        public PerformanceEditorPupupVM(PerformanceEditorPopup owner, Performance performance = null) 
            : base(owner)
        {
            Result = performance;

            if (performance != null)
            {
                Owner.nameBox.Text = Result.Name;
                Owner.tagBox.Text = Result.Tag;
                Owner.valueBox.Text = Result.Value;

                for (int i = 0; i < Types.Length; i++)
                {
                    if (Result.Dependency == Types[i])
                    {
                        Owner.combo.SelectedIndex = i;
                        break;
                    }
                }
            }
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
            if (Result == null)
                Result = new Performance();

            Result.Name = Owner.nameBox.Text;
            Result.Tag = Owner.tagBox.Text;
            Result.Value = Owner.valueBox.Text;

            Result.Dependency = Owner.combo.Text;

            Owner.Close();
        }

        #endregion

        #endregion
    }
}

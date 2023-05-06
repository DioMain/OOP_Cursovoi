using PCBuilder.Commands;
using PCBuilder.Utilites;
using PCBuilder.View;
using PCBuilder.View.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class OptionFrameVM : FrameViewModel<OptionFrame, MainWindow>
    {
        public string[] Items
        {
            get => Enum.GetNames(typeof(Language));
        }

        public OptionFrameVM(OptionFrame owner, MainWindow window) : base(owner, window)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (LocalizationManager.Instance.Current.ToString() == Items[i])
                    owner.combo.SelectedIndex = i;
            }

            AnimateAwake(owner.container);
        }

        #region Commands

        private BaseCommand LangSellectCommand;
        public ICommand LangSellect
        {
            get
            {
                return LangSellectCommand ?? (LangSellectCommand = new BaseCommand(LangSellectExecuted));
            }
        }
        private void LangSellectExecuted(object obj)
        {
            LocalizationManager.Instance.SetLang((Language)Enum.Parse(typeof(Language), (string)Owner.combo.SelectedValue));
        }

        #endregion
    }
}

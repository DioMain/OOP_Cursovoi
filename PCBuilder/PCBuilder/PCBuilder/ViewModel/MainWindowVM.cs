using PCBuilder.Commands;
using PCBuilder.CustomUI;
using PCBuilder.Model;
using PCBuilder.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class MainWindowVM : WindowViewModel<MainWindow>
    {
        #region Properties

        private int sectionId;
        /// <summary>
        /// 0 - user; 1 - basket; 2 - catalog; 3 - editor; 4 - options; 5 - admin
        /// </summary>
        public int SectionId
        {
            get => sectionId;
            private set
            {
                sectionId = value;
                OnPropertyChanged(nameof(SectionId));
            }
        }

        #endregion

        public MainWindowVM(MainWindow owner) : base(owner)
        {
            owner.adminBut.Visibility = User.Current.Rights == "ADMIN" ? Visibility.Visible : Visibility.Collapsed;

            ChangeSection(2);
        }


        /// <summary>
        /// Set selection visual effect
        /// </summary>
        /// <param name="sectionId">0 - user; 1 - basket; 2 - catalog; 3 - editor; 4 - options; 5 - admin</param>
        public void ChangeSeletedButton(int sectionId)
        {
            Owner.basketBut.IsSelected = false;
            Owner.userBut.IsSelected = false;
            Owner.catalogBut.IsSelected = false;
            Owner.adminBut.IsSelected = false;
            Owner.editorBut.IsSelected = false;
            Owner.optionsBut.IsSelected = false;

            switch (sectionId)
            {
                case 0:
                    Owner.userBut.IsSelected = true;
                    break;
                case 1:
                    Owner.basketBut.IsSelected = true;
                    break;
                case 2:
                    Owner.catalogBut.IsSelected = true;
                    break;
                case 3:
                    Owner.editorBut.IsSelected = true;
                    break;
                case 4:
                    Owner.optionsBut.IsSelected = true;
                    break;
                case 5:
                    Owner.adminBut.IsSelected = true;
                    break;
            }
        }
        /// <summary>
        /// Change current section
        /// </summary>
        /// <param name="sectionId">0 - user; 1 - basket; 2 - catalog; 3 - editor; 4 - options; 5 - admin</param>
        public void ChangeSection(int sectionId)
        {
            SectionId = sectionId;

            ChangeSeletedButton(sectionId);

            switch (sectionId)
            {
                case 0:
                    
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }

        #region Commands

        #region SetSection

        private BaseCommand SetSectionCommand;
        public ICommand SetSection
        {
            get
            {
                if (SetSectionCommand == null)
                    SetSectionCommand = new BaseCommand(SetSectionExecuted);

                return SetSectionCommand;
            }
        }
        private void SetSectionExecuted(object obj)
        {
            CircleButton circleButton = (CircleButton)obj;

            ChangeSection(circleButton.Id);
        }

        #endregion

        #endregion
    }
}

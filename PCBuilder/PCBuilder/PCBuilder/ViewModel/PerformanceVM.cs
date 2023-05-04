using PCBuilder.Commands;
using PCBuilder.CustomUI;
using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace PCBuilder.ViewModel
{
    public class PerformanceVM : BaseViewModel
    {
        private Performance _performance;
        public Performance Performance
        { 
            get { return _performance; } 
            set 
            { 
                _performance = value; 
                OnPropertyChanged(nameof(Performance));
            }
        }

        private object _owner;

        public string Name { get => _performance.Name; }
        public string Value { get => _performance.Value; }
        public string Tag { get => _performance.Tag; }
        public string Dependency { get => _performance.Dependency; }

        public PerformanceVM(Performance performance, object owner = null)
        {
            _performance = performance;
            _owner = owner;
        }

        #region Commnads

        #region Init

        private BaseCommand InitCommand;
        public ICommand Init
        {
            get
            {
                if (InitCommand == null)
                    InitCommand = new BaseCommand(InitExecuted);

                return InitCommand;
            }
        }
        private void InitExecuted(object obj)
        {
            if (_owner is ProductEditorFrameVM)
            {
                FrameworkElement element = (FrameworkElement)obj;

                FrameworkElement but0 = element.FindName("editButton") as FrameworkElement;
                FrameworkElement but1 = element.FindName("deleteButton") as FrameworkElement;

                but0.Style = Application.Current.Resources["CB_DEF"] as Style;
                but1.Style = Application.Current.Resources["CB_RED"] as Style;
            }
        }

        #endregion

        #region Delete

        private BaseCommand DeleteCommand;
        public ICommand Delete
        {
            get
            {
                if (DeleteCommand == null)
                    DeleteCommand = new BaseCommand(DeleteExecuted);

                return DeleteCommand;
            }
        }
        private void DeleteExecuted(object obj)
        {
            if (_owner is ProductEditorFrameVM)
            {
                ProductEditorFrameVM element = (ProductEditorFrameVM)_owner;

                element.DeleteProp(this);
            }
        }
        #endregion

        #region Edit

        private BaseCommand EditCommand;
        public ICommand Edit
        {
            get
            {
                if (EditCommand == null)
                    EditCommand = new BaseCommand(EditExecuted);

                return EditCommand;
            }
        }
        private void EditExecuted(object obj)
        {
            if (_owner is ProductEditorFrameVM)
            {
                ProductEditorFrameVM element = (ProductEditorFrameVM)_owner;

                element.EditProp(this);
            }
        }

        #endregion

        #endregion
    }
}

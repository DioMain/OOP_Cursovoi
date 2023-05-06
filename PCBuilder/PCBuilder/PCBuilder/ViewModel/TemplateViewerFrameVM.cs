using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.View;
using PCBuilder.View.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class TemplateViewerFrameVM : FrameViewModel<TemplateViewerFrame, MainWindow>
    {
        private List<TemplateVM> _templates;

        private List<TemplateVM> _textFilteredTemplates;
        private List<TemplateVM> _viewTemplates;

        public List<TemplateVM> Templates
        {
            get => _viewTemplates;
        }

        public TemplateViewerFrameVM(TemplateViewerFrame owner, MainWindow window) : base(owner, window)
        {
            _templates = new List<TemplateVM>();

            SyncData();

            AnimateAwake(owner.container);
        }

        private void TextFilter()
        {
            _textFilteredTemplates = _templates.Where(i =>
            {
                Regex regex = new Regex(@"(\w)*" + Owner.textFilterBox.Text + @"(\w)*");

                return regex.IsMatch(i.Template.Name);
            }).ToList();

            _viewTemplates = new List<TemplateVM>(_textFilteredTemplates);
        }

        public void SyncData()
        {
            _templates.Clear();

            foreach (Template item in DataBaseManager.Instance.Templates.GetAll())
            {
                _templates.Add(new TemplateVM(item, this));
            }

            TextFilter();

            OnPropertyChanged(nameof(Templates));
        }

        public void Delete(TemplateVM template)
        {

        }

        #region Commands

        #region Create

        private BaseCommand CreateCommand;
        public ICommand Create
        {
            get => CreateCommand ?? (CreateCommand = new BaseCommand(CreateExecuted));
        }
        public void CreateExecuted(object obj)
        {
            ((MainWindowVM)OwnerWindow.DataContext).PushSubPage(new TemplateEditorFrame(OwnerWindow));
        }

        #endregion

        #region ClickAll

        private BaseCommand ClickAllCommand;
        public ICommand ClickAll
        {
            get => ClickAllCommand ?? (ClickAllCommand = new BaseCommand(ClickAllExecuted));
        }
        public void ClickAllExecuted(object obj)
        {
            SyncData();
        }

        #endregion

        #region ClickMy

        private BaseCommand ClickMyCommand;
        public ICommand ClickMy
        {
            get => ClickMyCommand ?? (ClickMyCommand = new BaseCommand(ClickMyExecuted));
        }
        public void ClickMyExecuted(object obj)
        {
            _viewTemplates = _textFilteredTemplates.Where(i => i.Template.CreatorId == User.Current.Id).ToList();

            OnPropertyChanged(nameof(Templates));
        }

        #endregion

        #endregion
    }
}

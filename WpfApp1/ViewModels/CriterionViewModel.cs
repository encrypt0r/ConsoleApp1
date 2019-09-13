using ConsoleApp1;
using System;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public abstract class CriterionViewModel
    {
        public CriterionViewModel()
        {
            CloseCommand = new DelegateCommand(p => OnCloseClicked());
        }

        public string Header { get; protected set; }
        public string Title { get; set; }
        public abstract Criterion Inflate();

        public ICommand CloseCommand { get; }

        public event EventHandler CloseClicked;

        protected void OnCloseClicked()
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}

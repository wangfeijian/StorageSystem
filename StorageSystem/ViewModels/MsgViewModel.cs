using MaterialDesignThemes.Wpf;
using StorageSystem.Common;

namespace StorageSystem.ViewModels
{
    public class MsgViewModel : BindableBase, IDialogHostAware
    {
        public MsgViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK));
            }
        }

        public string DialogHostName { get; set; } = "Root";
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Title"))
                Title = parameters.GetValue<string>("Title");

            if (parameters.ContainsKey("Content"))
                Content = parameters.GetValue<string>("Content");
        }
    }
}

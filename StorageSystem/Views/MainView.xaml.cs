using MaterialDesignThemes.Wpf;
using StorageSystem.Common;
using StorageSystem.Extensions;
using StorageSystem.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace StorageSystem.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IDialogHostService dialogHostService;
        public MainView(IEventAggregator aggregator, IDialogHostService dialogHostService)
        {
            InitializeComponent();
            SkinViewModel.ModifyTheme(theme => theme.SetBaseTheme(AppSettingsManager.IsDarkMode ? BaseTheme.Dark : BaseTheme.Light));
            SkinViewModel.ChangeHue(AppSettingsManager.Color);
            //注册提示消息
            aggregator.ResgiterMessage(Snackbar.MessageQueue.Enqueue);

            //注册等待消息窗口
            aggregator.Resgiter(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;

                if (DialogHost.IsOpen)
                    DialogHost.DialogContent = new ProgressView();
            });

            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            };
            btnClose.Click += async (s, e) =>
            {
                var dialogResult = await dialogHostService.Question("温馨提示", "确认退出系统?");
                if (dialogResult.Result != ButtonResult.OK) return;
                this.Close();
            };
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            };

            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
            this.dialogHostService = dialogHostService;
        }
    }
}
using StorageSystem.Common;
using StorageSystem.Common.Models;
using StorageSystem.Extensions;
using StorageSystem.Service;
using System.Collections.ObjectModel;

namespace StorageSystem.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        public DelegateCommand LoginOutCommand { get; private set; }

        public MainViewModel(IContainerProvider containerProvider,
            IRegionManager regionManager, ILoginService service, IStorageStatusService storageStatusService)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                    journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                    journal.GoForward();
            });
            LoginOutCommand = new DelegateCommand(() =>
              {
                  //注销当前用户
                  App.LoginOut(containerProvider);
              });
            this.containerProvider = containerProvider;
            this.regionManager = regionManager;
            this.service = service;
            this.storageStatusService = storageStatusService;
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
             {
                 journal = back.Context.NavigationService.Journal;
             });
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        private ObservableCollection<MenuBar> menuBars;
        private readonly IContainerProvider containerProvider;
        private readonly IRegionManager regionManager;
        private readonly ILoginService service;
        private readonly IStorageStatusService storageStatusService;
        private IRegionNavigationJournal journal;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }


        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "StoreOutline", Title = "仓位浏览", NameSpace = "StorageBrowseView" });
            MenuBars.Add(new MenuBar() { Icon = "StoreEdit", Title = "仓位定义", NameSpace = "StorageDefView" });
            MenuBars.Add(new MenuBar() { Icon = "BasketFill", Title = "物料入仓", NameSpace = "StorageInView" });
            MenuBars.Add(new MenuBar() { Icon = "BasketUnfill", Title = "物料出仓", NameSpace = "StorageOutView" });
            MenuBars.Add(new MenuBar() { Icon = "StoreSearch", Title = "仓位查询", NameSpace = "StorageSearchView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }

        /// <summary>
        /// 配置首页初始化参数
        /// </summary>
        public void Configure()
        {
            UserName = AppSession.UserName;
            CreateMenuBar();
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
        }
    }
}

using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using StorageSystem.Common;
using StorageSystem.Service;
using StorageSystem.ViewModels;
using StorageSystem.Views;
using StorageSystem.Views.Dialog;
using System.IO;
using System.Windows;

namespace StorageSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();

            var dialog = containerProvider.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                Current.MainWindow.Show();
            });
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var config = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText("appsettings.json"));
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(config["WebUrl"].ToString(), serviceKey: "webUrl");

            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IStorageStatusService, StorageStatusService>();
            containerRegistry.Register<IStorageDetailService, StorageDetailService>();
            containerRegistry.Register<IStorageOutDetailService, StorageOutDetailService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();

            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<StorageBrowseView, StorageBrowseViewModel>();
            containerRegistry.RegisterForNavigation<StorageDefView, StorageDefViewModel>();
            containerRegistry.RegisterForNavigation<StorageInView, StorageInViewModel>();
            containerRegistry.RegisterForNavigation<StorageSearchView, StorageSearchViewModel>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();

        }

        protected override void OnInitialized()
        {
            var wait = new WaitView();
            wait.Show();
            Task.Run(InitStorageData).Wait();
            wait.Close();
            var dialog = Container.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                var service = App.Current.MainWindow.DataContext as IConfigureService;
                if (service != null)
                    service.Configure();

                base.OnInitialized();
            });
        }

        private async Task InitStorageData()
        {
            var service = Container.Resolve<IStorageStatusService>();

            try
            {
                if (AppSettingsManager.InitEnable)
                {
                    while (true)
                    {
                        var result = await service.GetAllAsync(new Shared.Parameters.QueryParameter() { PageIndex = 0, PageSize = 100, Search = string.Empty });
                        if (result.Status)
                        {
                            if (result.Result.Items.Count <= 0)
                            {
                                break;
                            }
                            foreach (var item in result.Result.Items)
                            {
                                var deleteResult = await service.DeleteAsync(item.Id);
                            }
                        }
                        else
                        {
                            break;
                        }

                    }

                    foreach (var item in AppSettingsManager.StorageDic)
                    {
                        string areaName = item["AreaName"];
                        int shelfNum, layerNum, countsOfLayer;
                        int.TryParse(item["ShelfNum"], out shelfNum);
                        int.TryParse(item["LayerNum"], out layerNum);
                        int.TryParse(item["CountsOfLayer"], out countsOfLayer);

                        for (int i = 1; i <= shelfNum; i++)
                        {
                            for (int j = 1; j <= layerNum; j++)
                            {
                                for (int k = 1; k <= countsOfLayer; k++)
                                {
                                    string str = string.Format("{0}{1:00}{2:00}{3:00}", areaName, i, j, k);
                                    var result = await service.AddAsync(new Shared.Dtos.StorageStatusDto() { StorageName = str });
                                    if (!result.Status)
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

                AppSettingsManager.SaveInitEnable(false);
            }
            catch (Exception)
            {
            }
        }
    }
}

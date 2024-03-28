using StorageSystem.Service;
using StorageSystem.Shared.Dtos;
using System.Collections.ObjectModel;

namespace StorageSystem.ViewModels
{
    public class StorageBrowseViewModel : NavigationViewModel
    {
        private readonly IStorageStatusService service;

        public StorageBrowseViewModel(IStorageStatusService service, IContainerProvider containerProvider) : base(containerProvider)
        {
            this.service = service;
            StorageStatus = new ObservableCollection<StorageStatusDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetDatas();
            base.OnNavigatedTo(navigationContext);
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "上一页": GetDatas(--Index); break;
                case "下一页": GetDatas(++Index); break;
                case "跳转": GetDatas(Index); break;
                case "查询": GetDatas(Query); break;
            }
        }

        private async void GetDatas(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return;
            }

            var result = await service.GetGruopAsync(query);

            if (result.Status)
            {
                StorageStatus.Clear();
                foreach (var item in result.Result)
                {
                    storageStatus.Add(item);
                }
            }
        }
        private async void GetDatas(int index = 0)
        {
            if (index < 0)
            {
                Index = 0;
                return;
            }

            var result = await service.GetAllAsync(new Shared.Parameters.QueryParameter
            {
                PageIndex = index,
                PageSize = 25
            });

            if (result.Status)
            {
                if (result.Result.Items.Count > 0)
                {
                    StorageStatus.Clear();
                    foreach (var item in result.Result.Items)
                    {
                        storageStatus.Add(item);
                    }
                }
                else
                {
                    Index--;
                }
            }
        }

        private ObservableCollection<StorageStatusDto> storageStatus;

        public ObservableCollection<StorageStatusDto> StorageStatus
        {
            get { return storageStatus; }
            set { storageStatus = value; RaisePropertyChanged(); }
        }

        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; RaisePropertyChanged(); }
        }

        private string query;

        public string Query
        {
            get { return query; }
            set { query = value; RaisePropertyChanged(); }
        }

    }
}

using Microsoft.Win32;
using StorageSystem.Common;
using StorageSystem.Service;
using StorageSystem.Shared.Dtos;
using System.Collections.ObjectModel;

namespace StorageSystem.ViewModels
{
    public class StorageDefViewModel : NavigationViewModel
    {
        private readonly IStorageDetailService service;
        private readonly IContainerProvider containerProvider;
        private readonly IStorageStatusService statusService;

        public StorageDefViewModel(IStorageDetailService service, IContainerProvider containerProvider, IStorageStatusService statusService) : base(containerProvider)
        {
            this.service = service;
            this.containerProvider = containerProvider;
            this.statusService = statusService;
            StorageDetails = new ObservableCollection<StorageDetailDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "导入数据": ImportData(); break;
            }
        }

        private async void ImportData()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.xls,*.xlsx)|*.xls;*.xlsx;";
            if (dialog.ShowDialog().Value == true)
            {
                try
                {
                    foreach (var item in ExcelHelper.GetListFromFile(dialog.FileName))
                    {
                        var response = await statusService.UpdateAsync(new StorageStatusDto()
                        {
                            Status = 1,
                            StorageName = item.StorageName
                        });
                        if (response.Status)
                        {
                            var detailResult = await service.AddAsync(item);
                            if (detailResult.Status)
                            {
                                StorageDetails.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
            }
        }

        private ObservableCollection<StorageDetailDto> storageDetails;

        public ObservableCollection<StorageDetailDto> StorageDetails
        {
            get { return storageDetails; }
            set { storageDetails = value; RaisePropertyChanged(); }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; RaisePropertyChanged(); }
        }

    }
}

using Microsoft.Win32;
using StorageSystem.Common;
using StorageSystem.Service;
using StorageSystem.Shared.Dtos;
using System.Collections.ObjectModel;

namespace StorageSystem.ViewModels
{
    public class StorageInViewModel : NavigationViewModel
    {
        private readonly IStorageOutDetailService serviceOut;
        private readonly IStorageDetailService service;
        private readonly IStorageStatusService statusService;

        public StorageInViewModel(IStorageOutDetailService serviceOut, IStorageDetailService service, IContainerProvider containerProvider, IStorageStatusService statusService) : base(containerProvider)
        {
            this.serviceOut = serviceOut;
            this.service = service;
            this.statusService = statusService;
            StorageDetails = new ObservableCollection<StorageDetailDto>();
            StorageOutDetails = new ObservableCollection<StorageOutDetailDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        private async void Execute(string obj)
        {
            switch (obj)
            {
                case "入库查询": await InStorageSearch(); break;
                case "出库查询": await OutStorageSearch(); break;
                case "入库": await InStorage(StorageDetail); break;
                case "批量入库": await BatchStorage(InStorage); break;
                case "批量出库": await BatchStorage(OutStorage); break;
                case "出库": await OutStorage(StorageOutDetail); break;
            }
        }

        private async Task OutStorage(StorageDetailDto outDetail)
        {
            if (outDetail == null)
            {
                return;
            }

            StorageOutDetail = outDetail;
            StorageOutDetailDto storageOutDetailDto = new StorageOutDetailDto();
            storageOutDetailDto.StorageName = outDetail.StorageName;
            storageOutDetailDto.Infinish = outDetail.Infinish;
            storageOutDetailDto.WbsNo = outDetail.WbsNo;
            storageOutDetailDto.InstorageDate = outDetail.InstorageDate;
            storageOutDetailDto.MaterielDetail = outDetail.MaterielDetail;
            storageOutDetailDto.MaterielSN = outDetail.MaterielSN;
            storageOutDetailDto.OutStorageDate = DateTime.Now;
            storageOutDetailDto.OutFinish = true;

            var statusResult = await statusService.GetFirstOfDefaultAsync(outDetail.StorageName);
            if (statusResult.Status)
            {
                if (statusResult.Result.Status != 2)
                {
                    return;
                }
            }

            var response = await statusService.UpdateAsync(new StorageStatusDto()
            {
                Status = 0,
                StorageName = outDetail.StorageName
            });

            if (response.Status)
            {
                var searchResult = await service.GetFirstOfDefaultAsync(outDetail.MaterielSN);
                if (searchResult.Status)
                {
                    var deleteResult = await service.DeleteAsync(searchResult.Result.Id);
                    if (deleteResult.Status)
                    {
                        var addResult = await serviceOut.AddAsync(storageOutDetailDto);
                        if (addResult.Status)
                        {
                            StorageOutDetails.Add(storageOutDetailDto);
                        }
                    }
                }
            }
        }
        private async Task InStorageSearch()
        {
            StorageDetail = await QuerySingle(Sn);
        }

        private async Task OutStorageSearch()
        {
            StorageOutDetail = await QuerySingle(OutSn);
        }
        private async Task<StorageDetailDto> QuerySingle(string sn)
        {
            var result = await service.GetFirstOfDefaultAsync(sn);
            if (result.Status)
            {
                return result.Result;
            }
            return null;
        }

        private async Task InStorage(StorageDetailDto storageDetail)
        {
            if (storageDetail == null)
            {
                return;
            }

            StorageDetail = storageDetail;
            storageDetail.InstorageDate = DateTime.Now;
            storageDetail.Infinish = true;

            var statusResult = await statusService.GetFirstOfDefaultAsync(storageDetail.StorageName);
            if (statusResult.Status)
            {
                if (statusResult.Result.Status != 1)
                {
                    return;
                }
            }
            var result = await service.UpdateAsync(storageDetail);
            if (result.Status)
            {
                var response = await statusService.UpdateAsync(new StorageStatusDto()
                {
                    Status = 2,
                    StorageName = storageDetail.StorageName
                });
                if (response.Status)
                {
                    StorageDetails.Add(storageDetail);
                }
            }
        }

        private async Task BatchStorage(Func<StorageDetailDto, Task> action)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.xls,*.xlsx)|*.xls;*.xlsx;";
            if (dialog.ShowDialog().Value == true)
            {
                try
                {
                    foreach (var item in ExcelHelper.GetListFromFile(dialog.FileName))
                    {
                        var result = await service.GetFirstOfDefaultAsync(item.MaterielSN);
                        await action(result.Result);
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

        private ObservableCollection<StorageOutDetailDto> storageOutDetails;

        public ObservableCollection<StorageOutDetailDto> StorageOutDetails
        {
            get { return storageOutDetails; }
            set { storageOutDetails = value; RaisePropertyChanged(); }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; RaisePropertyChanged(); }
        }

        private string sn;

        public string Sn
        {
            get { return sn; }
            set { sn = value; RaisePropertyChanged(); }
        }

        private StorageDetailDto storageDetail;

        public StorageDetailDto StorageDetail
        {
            get { return storageDetail; }
            set { storageDetail = value; RaisePropertyChanged(); }
        }

        private string outSn;

        public string OutSn
        {
            get { return outSn; }
            set { outSn = value; RaisePropertyChanged(); }
        }

        private StorageDetailDto storageOutDetail;

        public StorageDetailDto StorageOutDetail
        {
            get { return storageOutDetail; }
            set { storageOutDetail = value; RaisePropertyChanged(); }
        }

    }
}

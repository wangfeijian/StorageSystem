namespace StorageSystem.Shared.Dtos
{
    public class StorageDetailDto : BaseDto
    {
        private string wbsNo;

        public string WbsNo
        {
            get { return wbsNo; }
            set { wbsNo = value; OnPropertyChanged(); }
        }

        private string storageName;

        public string StorageName
        {
            get { return storageName; }
            set { storageName = value; OnPropertyChanged(); }
        }

        private string materielDetail;

        public string MaterielDetail
        {
            get { return materielDetail; }
            set { materielDetail = value; OnPropertyChanged(); }
        }

        private string materielSN;

        public string MaterielSN
        {
            get { return materielSN; }
            set { materielSN = value; OnPropertyChanged(); }
        }

        private DateTime inStorageDate;

        public DateTime InstorageDate
        {
            get { return inStorageDate; }
            set { inStorageDate = value; OnPropertyChanged(); }
        }

        private bool inFinish;

        public bool Infinish
        {
            get { return inFinish; }
            set { inFinish = value; OnPropertyChanged(); }
        }

    }
}

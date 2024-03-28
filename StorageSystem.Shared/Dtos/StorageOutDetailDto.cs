namespace StorageSystem.Shared.Dtos
{
    public class StorageOutDetailDto : StorageDetailDto
    {
        private DateTime outStorageDate;

        public DateTime OutStorageDate
        {
            get { return outStorageDate; }
            set { outStorageDate = value; OnPropertyChanged(); }
        }

        private bool outFinish;

        public bool OutFinish
        {
            get { return outFinish; }
            set { outFinish = value; OnPropertyChanged(); }
        }

    }
}

namespace StorageSystem.Shared.Dtos
{
    public class StorageStatusDto : BaseDto
    {
        private string storageName;

        public string StorageName
        {
            get { return storageName; }
            set { storageName = value; OnPropertyChanged(); }
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }


    }
}

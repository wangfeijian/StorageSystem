namespace StorageSystem.Shared.Context
{
    public class StorageDetail
    {
        public int Id { get; set; }
        public string WbsNo { get; set; }
        public string StorageName { get; set; }
        public string MaterielDetail { get; set; }
        public string MaterielSN { get; set; }
        public DateTime InStorageDate { get; set; }
        public bool InFinish { get; set; }
    }
}

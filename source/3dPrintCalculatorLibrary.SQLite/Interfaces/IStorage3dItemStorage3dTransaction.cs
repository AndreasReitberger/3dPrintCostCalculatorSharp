namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    internal interface IStorage3dItemStorage3dTransaction
    {
        #region Properties
        public Guid StorageTransactionId { get; set; }

        public Guid StorageItemId { get; set; }
        #endregion
    }
}

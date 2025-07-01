namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    internal interface IStorage3dItemStorage3dLocation
    {
        #region Properties
        public Guid StorageLocationId { get; set; }

        public Guid StorageItemId { get; set; }
        #endregion
    }
}

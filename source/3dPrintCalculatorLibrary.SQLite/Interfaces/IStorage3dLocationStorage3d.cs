namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    public interface IStorage3dLocationStorage3d
    {
        #region Properties
        public Guid StorageLocationId { get; set; }

        public Guid StorageId { get; set; }
        #endregion
    }
}

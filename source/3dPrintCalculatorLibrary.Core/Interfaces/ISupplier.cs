namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface ISupplier
    {
        #region Properties 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DebitorNumber { get; set; }
        public bool IsActive { get; set; }
        public string Website { get; set; }
        public IList<IManufacturer> Manufacturers { get; set; }
        #endregion
    }
}

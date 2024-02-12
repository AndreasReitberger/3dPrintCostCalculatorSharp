namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IManufacturer
    {
        #region Properties 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DebitorNumber { get; set; }
        public bool IsActive { get; set; }
        public string Website { get; set; }
        public string Note { get; set; }
        public string CountryCode { get; set; }
        #endregion

    }
}

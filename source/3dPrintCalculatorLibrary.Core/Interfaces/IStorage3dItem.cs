namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IStorage3dItem
    {
        #region Properties
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public IMaterial3d Material { get; set; }
        public double Amount { get; set; }
        #endregion
    }
}

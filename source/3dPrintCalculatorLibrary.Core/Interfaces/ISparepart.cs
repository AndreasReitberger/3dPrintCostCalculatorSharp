namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface ISparepart
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Partnumber { get; set; }
        public double Costs { get; set; }
        #endregion
    }
}

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IMaintenance3d
    {
        #region Properties
        public Guid Id { get; set; }
        public IPrinter3d? Printer { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Duration { get; set; }
        public double AdditionalCosts { get; set; }
        public double Costs { get; }
        public IList<ISparepart> Spareparts { get; set; }
        #endregion
    }
}

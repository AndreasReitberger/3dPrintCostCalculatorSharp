#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IMaintenance3d
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid PrinterId { get; set; }
        public Printer3d? Printer { get; set; }
#else
        public IPrinter3d? Printer { get; set; }
#endif
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Duration { get; set; }
        public double AdditionalCosts { get; set; }
        public double Costs { get; }
#if SQL
        public List<Sparepart> Spareparts { get; set; }
#else
        public IList<ISparepart> Spareparts { get; set; }
#endif
        #endregion
    }
}

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IPrint3dInfo : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
#if SQL
        public Guid CalculationEnhancedId { get; set; }
        public Guid FileId { get; set; }
        public Guid PrinterId { get; set; }
        public File3dUsage? FileUsage { get; set; }
        public Printer3d? Printer { get; set; }
        public List<Item3dUsage> Items { get; set; }
        public List<Material3dUsage> Materials { get; set; }
#else
        public IFile3dUsage? FileUsage { get; set; }
        public IPrinter3d? Printer { get; set; }
        public IList<IItem3dUsage> Items { get; set; }
        public IList<IMaterial3dUsage> Materials { get; set; }
#endif
        #endregion
    }
}

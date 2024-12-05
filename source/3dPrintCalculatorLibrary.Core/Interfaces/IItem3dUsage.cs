#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IItem3dUsage : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid CalculationEnhancedId { get; set; }
        public Guid CalculationProfileId { get; set; }
        public Guid PrintInfoId { get; set; }
        public Guid ItemId { get; set; }
        public Item3d? Item { get; set; }
        public File3d? File { get; set; }
        public Guid FileId { get; set; }
#else
        public IItem3d? Item { get; set; }
        public IFile3d? File { get; set; }
#endif
        public double Quantity { get; set; }
        public bool LinkedToFile { get; set; }
    #endregion
    }
}

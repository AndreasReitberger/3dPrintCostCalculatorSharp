using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IStorage3dTransaction
    {
        #region Properties
        public Guid Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
#if SQL
        public Guid ItemId { get; set; }
        //public Storage3dItem? Item { get; set; }
#else
        public IStorage3dItem? Item { get; set; }
#endif
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public bool IsAddition { get; }
        #endregion
    }
}

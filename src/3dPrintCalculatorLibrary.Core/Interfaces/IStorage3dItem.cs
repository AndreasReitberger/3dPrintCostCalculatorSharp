#if SQL
using AndreasReitberger.Print3d.SQLite.StorageAdditions;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IStorage3dItem
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
#if SQL
        public Guid MaterialId { get; set; }
        public Material3d? Material { get; set; }
        public List<Storage3dTransaction> Transactions { get; set; }
#else
        public IMaterial3d? Material { get; set; }
        public IList<IStorage3dTransaction> Transactions { get; set; }
#endif
        public double Amount { get; }
        #endregion

        #region Methods
        public double GetAvailableAmount();
        #endregion
    }
}

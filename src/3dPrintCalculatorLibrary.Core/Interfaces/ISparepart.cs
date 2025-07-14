#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
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

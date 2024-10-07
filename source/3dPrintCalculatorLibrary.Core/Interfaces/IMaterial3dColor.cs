#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IMaterial3dColor : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid MaterialId { get; set; }
#endif
        public string Name { get; set; }
        public string HexColorCode { get; set; }
        #endregion
    }
}

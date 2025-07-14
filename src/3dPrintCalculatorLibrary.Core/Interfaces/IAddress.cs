#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IAddress
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid CustomerId { get; set; }
#endif
        public string Supplement { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        #endregion
    }
}

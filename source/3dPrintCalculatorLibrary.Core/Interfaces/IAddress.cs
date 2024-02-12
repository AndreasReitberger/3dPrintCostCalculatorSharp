using System;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IAddress
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Supplement { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        #endregion
    }
}

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface ICustomer3d
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid CalculationProfileId { get; set; }
        public Guid ContactPersonId { get; set; }
#endif
        public string CustomerId { get; set; }
        public bool IsCompany { get; set; }
        public string Salutation { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string VAT { get; set; }
#if SQL
        public ContactPerson? ContactPerson { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Email> EmailAddresses { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
#else
        public IPerson? ContactPerson { get; set; }
        public IList<IAddress> Addresses { get; set; }
        public IList<IEmail> EmailAddresses { get; set; }
        public IList<IPhoneNumber> PhoneNumbers { get; set; }
#endif
        public string Handler { get; set; }
        public string FullName { get; }
        public string MainAddress { get; }
        #endregion

        #region Methods
        public string GetAddress(int index = 0);
        public object Clone();

        #endregion
    }
}

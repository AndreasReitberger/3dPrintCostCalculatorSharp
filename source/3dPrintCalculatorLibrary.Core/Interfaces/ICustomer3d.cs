using System;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface ICustomer3d
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationProfileId { get; set; }
        public string CustomerId { get; set; }
        public bool IsCompany { get; set; }
        public string Salutation { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string VAT { get; set; }
        public IPerson ContactPerson { get; set; }
        public List<IAddress> Addresses { get; set; }
        public List<IEmail> EmailAddresses { get; set; }
        public List<IPhoneNumber> PhoneNumbers { get; set; }
        public string Handler { get; set; }
        public string FullName { get; }
        public string MainAddress { get; }
        #endregion

        #region Methods
        public string GetAddress(int index = 0);
        public void Clone();

        #endregion
    }
}

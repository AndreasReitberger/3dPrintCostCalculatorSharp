using System;

namespace AndreasReitberger.Print3d.Interfaces
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
        public Guid ContactPersonId { get; set; }
        //public List<IAddress> Addresses { get; set; }
        //public List<IEmail> EmailAddresses { get; set; } = new();
        //public List<PhoneNumber> PhoneNumbers { get; set; } = new();
        public string Handler { get; set; }
        public string FullName { get; }
        public string MainAddress { get; }
        #endregion

    }
}

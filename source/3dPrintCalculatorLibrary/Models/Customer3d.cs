using AndreasReitberger.Models.CustomerAdditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models
{
    public class Customer3d : ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string CustomerId
        { get; set; } = string.Empty;

        public bool IsCompany
        { get; set; } = false;
        public string Salutation 
        { get; set; } = string.Empty;

        public string Name
        { get; set; } = string.Empty;

        public string LastName
        { get; set; } = string.Empty;
        public string VAT
        { get; set; } = string.Empty;
        public ContactPerson ContactPerson
        { get; set; }

        public List<Address> Addresses { get; set; } = new List<Address>();

        public List<string> EmailAddresses { get; set; } = new List<string>();

        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public string Handler
        { get; set; } = string.Empty;

        public string FullName
        {
            get => IsCompany ? Name : string.Format("{0}, {1}", this.LastName, this.Name);
        }
        #endregion

        #region Constructor

        public Customer3d()
        {
        }

        #endregion

        #region overrides
        public override string ToString()
        {
            return IsCompany ? Name : string.Format("{0}, {1}", this.LastName, this.Name);
        }
        public override bool Equals(object obj)
        {
            var item = obj as Customer3d;
            if (item == null)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion

    }
}

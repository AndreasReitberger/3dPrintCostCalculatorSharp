using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.CustomerAdditions
{
    [Table("EmailAddresses")]
    public class Email
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Customer3d))]
        public Guid CustomerId { get; set; }
        public string EmailAddress { get; set; }
        #endregion

        #region Constructor
        public Email()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}

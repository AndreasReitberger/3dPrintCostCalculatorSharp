using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.CustomerAdditions
{
    [Table("PhoneNumbers")]
    public class PhoneNumber
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Customer3d))]
        public Guid CustomerId { get; set; }
        public string Phone { get; set; }
        #endregion

        #region Constructor
        public PhoneNumber()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}

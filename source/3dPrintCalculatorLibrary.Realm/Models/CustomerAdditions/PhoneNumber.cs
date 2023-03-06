using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.CustomerAdditions
{
    public partial class PhoneNumber : RealmObject, IPhoneNumber
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

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

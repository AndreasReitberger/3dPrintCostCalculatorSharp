using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.CustomerAdditions
{
    public partial class Email : RealmObject, IEmail
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

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

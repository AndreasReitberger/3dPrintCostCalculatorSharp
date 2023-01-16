using System;

namespace AndreasReitberger.Print3d.Interface
{
    public interface IPerson
    {
        #region Properties
        public Guid Id { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool ShowOnDocuments { get; set; }
        #endregion
    }
}

using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IEmail
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string EmailAddress { get; set; }
        #endregion
    }
}

namespace AndreasReitberger.Print3d.Core.Interfaces
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

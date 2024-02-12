namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IPhoneNumber
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Phone { get; set; }
        #endregion
    }
}

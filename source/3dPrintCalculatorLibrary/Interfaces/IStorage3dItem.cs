using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IStorage3dItem
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; }
        #endregion

        #region Methods
        public double GetAvailableAmount();
        #endregion
    }
}

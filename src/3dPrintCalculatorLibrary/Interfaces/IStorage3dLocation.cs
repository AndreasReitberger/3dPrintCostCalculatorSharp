using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IStorage3dLocation
    {
        #region Properties

        public Guid Id { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; }

        #endregion

        #region Methods

        //public IStorage3dItem CreateStockItem(IMaterial3d material, double amount, Unit unit);

        #endregion
    }
}

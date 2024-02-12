using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IStorage3dLocation
    {
        #region Properties

        public Guid Id { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public IList<IStorage3dItem> Items { get; set; }

        #endregion

        #region Methods

        public IStorage3dItem CreateStockItem(IMaterial3d material, double amount = 0, Unit unit = Unit.Kilogram);
        public void AddToStock(IStorage3dItem item, double amount, Unit unit);
        public void AddToStock(IMaterial3d material, double amount, Unit unit);
        public IStorage3dTransaction AddToStock(IMaterial3d material, double amount, Unit unit, Guid? calculationId = null);
        public bool TakeFromStock(IStorage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public bool TakeFromStock(IMaterial3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public IStorage3dTransaction TakeFromStock(IMaterial3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false);
        public bool UpdateStockAmount(IStorage3dItem item, double amount, Unit unit = Unit.Kilogram);
        #endregion
    }
}

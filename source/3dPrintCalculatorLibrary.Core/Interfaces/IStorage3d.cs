using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IStorage3d
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public IList<IStorage3dLocation> Locations { get; set; }
        #endregion

        #region Methods
        public IStorage3dItem CreateStockItem(IStorage3dLocation location, IMaterial3d material, double amount = 0, Unit unit = Unit.Kilogram);
        public void AddToStock(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit);
        public void AddToStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit);
        public IStorage3dTransaction? AddToStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, Guid? calculationId = null);
        public bool TakeFromStock(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public bool TakeFromStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public IStorage3dTransaction? TakeFromStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false);
        public bool UpdateStockAmount(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit = Unit.Kilogram);
        #endregion
    }
}

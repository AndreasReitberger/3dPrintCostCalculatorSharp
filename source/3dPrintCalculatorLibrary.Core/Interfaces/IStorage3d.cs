using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IStorage3d
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
#if SQL
        public List<Storage3dLocation> Locations { get; set; }
#else
        public IList<IStorage3dLocation> Locations { get; set; }
#endif
        #endregion

        #region Methods
#if SQL
        public Storage3dItem CreateStockItem(Storage3dLocation location, Material3d material, double amount = 0, Unit unit = Unit.Kilogram);
        public void AddToStock(Storage3dLocation location, Storage3dItem item, double amount, Unit unit);
        public void AddToStock(Storage3dLocation location, Material3d material, double amount, Unit unit);
        public Storage3dTransaction? AddToStock(Storage3dLocation location, Material3d material, double amount, Unit unit, Guid? calculationId = null);
        public bool TakeFromStock(Storage3dLocation location, Storage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public bool TakeFromStock(Storage3dLocation location, Material3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public Storage3dTransaction? TakeFromStock(Storage3dLocation location, Material3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false);
        public bool UpdateStockAmount(Storage3dLocation location, Storage3dItem item, double amount, Unit unit = Unit.Kilogram);
#else
        public IStorage3dItem CreateStockItem(IStorage3dLocation location, IMaterial3d material, double amount = 0, Unit unit = Unit.Kilogram);
        public void AddToStock(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit);
        public void AddToStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit);
        public IStorage3dTransaction? AddToStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, Guid? calculationId = null);
        public bool TakeFromStock(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public bool TakeFromStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public IStorage3dTransaction? TakeFromStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false);
        public bool UpdateStockAmount(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit = Unit.Kilogram);
#endif
        #endregion
    }
}

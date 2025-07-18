﻿using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IStorage3dLocation
    {
        #region Properties

        public Guid Id { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
#if SQL
        public List<Storage3dItem> Items { get; set; }
#else
        public IList<IStorage3dItem> Items { get; set; }
#endif

        #endregion

        #region Methods

#if SQL
        public Storage3dItem CreateStockItem(Material3d material, double amount = 0, Unit unit = Unit.Kilogram);
        public Storage3dTransaction? AddToStock(Storage3dItem item, double amount, Unit unit);
        public Storage3dTransaction? AddToStock(Material3d material, double amount, Unit unit);
        public Storage3dTransaction? AddToStock(Material3d material, double amount, Unit unit, Guid? calculationId = null);
        public Storage3dTransaction? TakeFromStock(Storage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public Storage3dTransaction? TakeFromStock(Material3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public Storage3dTransaction? TakeFromStock(Material3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false);
        public Storage3dTransaction? UpdateStockAmount(Storage3dItem item, double amount, Unit unit = Unit.Kilogram);
#else
        public IStorage3dItem CreateStockItem(IMaterial3d material, double amount = 0, Unit unit = Unit.Kilogram);
        public IStorage3dTransaction? AddToStock(IStorage3dItem item, double amount, Unit unit);
        public IStorage3dTransaction? AddToStock(IMaterial3d material, double amount, Unit unit);
        public IStorage3dTransaction? AddToStock(IMaterial3d material, double amount, Unit unit, Guid? calculationId = null);
        public IStorage3dTransaction? TakeFromStock(IStorage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public IStorage3dTransaction? TakeFromStock(IMaterial3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false);
        public IStorage3dTransaction? TakeFromStock(IMaterial3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false);
        public IStorage3dTransaction? UpdateStockAmount(IStorage3dItem item, double amount, Unit unit = Unit.Kilogram);
#endif
        #endregion
    }
}

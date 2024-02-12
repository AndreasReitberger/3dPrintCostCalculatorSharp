using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using AndreasReitberger.Print3d.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Storage3d : ObservableObject, IStorage3d
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        int capacity = 32;

        [ObservableProperty]
        IList<IStorage3dLocation> locations = [];

        #endregion

        #region Ctor
        public Storage3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public IStorage3dItem CreateStockItem(IStorage3dLocation location, IMaterial3d material, double amount = 0, Unit unit = Unit.Kilogram)
        {
            IStorage3dItem item = new Storage3dItem() { Material = material };
            if (amount != 0) UpdateStockAmount(location: location, item, amount, unit);
            location?.Items?.Add(item);
            return item;
        }

        public void AddToStock(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit)
        {
            if (item?.Material != null)
            {
                if (item?.Material.Unit == unit)
                {
                    item.Amount += amount;
                }
                else
                {
                    int target = UnitFactor.GetUnitFactor(item.Material.Unit);
                    int current = UnitFactor.GetUnitFactor(unit);

                    item.Amount += target > current ? amount / (target / current) : amount * (current / target);
                }
            }
        }
        public void AddToStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit)
        {
            IStorage3dItem item = location?.Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(location: location, item: item, amount: amount, unit: unit);
            else
                CreateStockItem(location: location, material, amount, unit);
        }

        public IStorage3dTransaction AddToStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, Guid? calculationId = null)
        {
            IStorage3dItem item = location?.Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(location: location, item: item, amount: amount, unit: unit);
            else
                item = CreateStockItem(location: location, material, amount, unit);
            return new Storage3dTransaction()
            {
                Amount = amount,
                Unit = unit,
                DateTime = DateTime.Now,
                Item = item,
            };
        }

        public bool TakeFromStock(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
        {
            if (item?.Material != null)
            {
                if (item.Amount >= amount)
                {
                    if (item?.Material.Unit == unit)
                    {
                        item.Amount -= amount;
                    }
                    else
                    {
                        int target = UnitFactor.GetUnitFactor(item.Material.Unit);
                        int current = UnitFactor.GetUnitFactor(unit);

                        item.Amount -= target > current ? amount / (target / current) : amount * (current / target);
                    }
                    return true;
                }
                else if (throwIfMaterialIsNotInStock)
                {
                    throw new ArgumentOutOfRangeException($"The amount of the material `{item?.Material}` is not sufficient for this transaction (In stock: {item?.Amount} / Requested: {amount}!");
                }
                else return false;
            }
            else if (throwIfMaterialIsNotInStock)
            {
                throw new ArgumentOutOfRangeException($"The material `{item?.Material}` is not available in the stock!");
            }
            else return false;
        }

        public bool TakeFromStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
        {
            IStorage3dItem item = location?.Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            return TakeFromStock(location: location, item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock);
        }

        public IStorage3dTransaction TakeFromStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false)
        {
            IStorage3dItem item = location?.Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (TakeFromStock(location: location, item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock))
            {
                return new Storage3dTransaction()
                {
                    Amount = -amount,
                    Unit = unit,
                    DateTime = DateTime.Now,
                    Item = item,
                };
            }
            else return null;
        }

        public bool UpdateStockAmount(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit = Unit.Kilogram)
        {
            if (amount > 0)
            {
                AddToStock(location: location, item: item, amount: amount, unit: unit);
                return true;
            }
            else if (amount < 0)
            {
                return TakeFromStock(location: location, item: item, amount: Math.Abs(amount), unit: unit);
            }
            return false;
        }
       
        #endregion
    }
}

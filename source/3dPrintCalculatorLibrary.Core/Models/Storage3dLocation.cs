using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using AndreasReitberger.Print3d.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Storage3dLocation : ObservableObject, IStorage3dLocation
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string location = string.Empty;

        [ObservableProperty]
        int capacity = 32;

        [ObservableProperty]
        IList<IStorage3dItem> items = [];
        #endregion

        #region Ctor
        public Storage3dLocation()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public IStorage3dItem CreateStockItem(IMaterial3d material, double amount = 0, Unit unit = Unit.Kilogram)
        {
            IStorage3dItem item = new Storage3dItem() { Material = material };
            if (amount != 0) UpdateStockAmount(item, amount, unit);
            Items.Add(item);
            return item;
        }

        public IStorage3dTransaction? AddToStock(IStorage3dItem item, double amount, Unit unit)
        {
            if (item.Material is not null)
            {
                IStorage3dTransaction transaction = new Storage3dTransaction()
                {
                    DateTime = DateTimeOffset.Now,
                    Unit = unit,
                };
                if (item.Material.Unit == unit)
                {
                    transaction.Amount += amount;
                }
                else
                {
                    int target = UnitFactor.GetUnitFactor(item.Material.Unit);
                    int current = UnitFactor.GetUnitFactor(unit);

                    transaction.Amount += target > current ? amount / (target / current) : amount * (current / target);
                }
                item.Transactions.Add(transaction);
                return transaction;
            }
            else return null;
        }
        public IStorage3dTransaction? AddToStock(IMaterial3d material, double amount, Unit unit)
        {
            IStorage3dItem? item = Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(item: item, amount: amount, unit: unit);
            else
                CreateStockItem(material, amount, unit);
            return item?.Transactions.LastOrDefault();
        }

        public IStorage3dTransaction? AddToStock(IMaterial3d material, double amount, Unit unit, Guid? calculationId = null)
        {
            IStorage3dItem? item = Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(item: item, amount: amount, unit: unit);
            else
                item = CreateStockItem(material, amount, unit);
            return item?.Transactions.LastOrDefault();
        }

        public IStorage3dTransaction? TakeFromStock(IStorage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
        {
            if (item.Material is not null)
            {
                IStorage3dTransaction transaction = new Storage3dTransaction()
                {
                    DateTime = DateTimeOffset.Now,
                    Unit = unit,
                };
                if (item.Material.Unit == unit)
                {
                    transaction.Amount -= amount;
                }
                else
                {
                    int target = UnitFactor.GetUnitFactor(item.Material.Unit);
                    int current = UnitFactor.GetUnitFactor(unit);

                    transaction.Amount -= target > current ? amount / (target / current) : amount * (current / target);
                }
                if (item.Amount + transaction.Amount >= 0)
                {
                    item.Transactions.Add(transaction);
                    return transaction;
                }
                else if (throwIfMaterialIsNotInStock)
                {
                    throw new ArgumentOutOfRangeException($"The amount of the material `{item?.Material}` is not sufficient for this transaction (In stock: {item?.Amount} / Requested: {amount}!");
                }
                else return null;
            }
            else if (throwIfMaterialIsNotInStock)
            {
                throw new ArgumentOutOfRangeException($"The material `{item?.Material}` is not available in the stock!");
            }
            else return null;
        }

        public IStorage3dTransaction? TakeFromStock(IMaterial3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
        {
            IStorage3dItem? item = Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item is null) return null;
            return TakeFromStock(item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock);
        }

        public IStorage3dTransaction? TakeFromStock(IMaterial3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false)
        {
            IStorage3dItem? item = Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item is null) return null;
            return TakeFromStock(item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock);
        }

        public IStorage3dTransaction? UpdateStockAmount(IStorage3dItem item, double amount, Unit unit = Unit.Kilogram)
        {
            if (amount > 0)
            {
                return AddToStock(item: item, amount: amount, unit: unit);
            }
            else if (amount < 0)
            {
                return TakeFromStock(item: item, amount: Math.Abs(amount), unit: unit);
            }
            return null;
        }
        #endregion
    }
}

using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.StorageAdditions;
using AndreasReitberger.Print3d.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Storages")]
    public partial class Storage3dOld : ObservableObject, IStorage3d
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        int capacity = 32;

        [ObservableProperty]
        [property: ManyToMany(typeof(Storage3dLocationStorage3d), CascadeOperations = CascadeOperation.All)]
        ObservableCollection<Storage3dLocation> locations = [];
        #endregion

        #region Ctor
        public Storage3dOld()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public Storage3dItem CreateStockItem(Storage3dLocation location, Material3d material, double amount = 0, Unit unit = Unit.Kilogram)
        {
            Storage3dItem item = new() { Material = material };
            if (amount != 0) UpdateStockAmount(location: location, item, amount, unit);
            location?.Items?.Add(item);
            return item;
        }

        public void AddToStock(Storage3dLocation location, Storage3dItem item, double amount, Unit unit)
        {
            if (item.Material is not null)
            {
                Storage3dTransaction transaction = new()
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
                item.Transactions?.Add(transaction);
            }
        }
        public void AddToStock(Storage3dLocation location, Material3d material, double amount, Unit unit)
        {
            Storage3dItem? item = location.Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(location: location, item: item, amount: amount, unit: unit);
            else
                CreateStockItem(location: location, material, amount, unit);
        }

        public Storage3dTransaction? AddToStock(Storage3dLocation location, Material3d material, double amount, Unit unit, Guid? calculationId = null)
        {
            Storage3dItem? item = location.Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(location: location, item: item, amount: amount, unit: unit);
            else
                item = CreateStockItem(location: location, material, amount, unit);
            return item?.Transactions.LastOrDefault();
        }

        public bool TakeFromStock(Storage3dLocation location, Storage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
        {
            if (item.Material is not null)
            {
                Storage3dTransaction transaction = new()
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
                    return true;
                }
                else if (throwIfMaterialIsNotInStock)
                {
                    throw new ArgumentOutOfRangeException($"The amount of the material `{item.Material}` is not sufficient for this transaction (In stock: {item.Amount} / Requested: {amount}!");
                }
                else return false;
            }
            else if (throwIfMaterialIsNotInStock)
            {
                throw new ArgumentOutOfRangeException($"The material `{item.Material}` is not available in the stock!");
            }
            else return false;
        }

        public bool TakeFromStock(Storage3dLocation location, Material3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
        {
            Storage3dItem? item = location.Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item is null) return false;
            return TakeFromStock(location: location, item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock);
        }

        public Storage3dTransaction? TakeFromStock(Storage3dLocation location, Material3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false)
        {
            Storage3dItem? item = location.Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item is not null && TakeFromStock(location: location, item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock))
            {
                return item?.Transactions.LastOrDefault();
            }
            else return null;
        }

        public bool UpdateStockAmount(Storage3dLocation location, Storage3dItem item, double amount, Unit unit = Unit.Kilogram)
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

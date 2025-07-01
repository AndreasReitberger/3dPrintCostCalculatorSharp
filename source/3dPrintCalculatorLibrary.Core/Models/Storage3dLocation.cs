using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
using AndreasReitberger.Print3d.SQLite.StorageAdditions;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Storage3dLocation)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Storage3dLocation : ObservableObject, IStorage3dLocation
    {
        #region Properties
#if SQL
        [property: PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Location { get; set; } = string.Empty;

        [ObservableProperty]
        public partial int Capacity { get; set; } = 32;

#if SQL

        [ObservableProperty]
        [ManyToMany(typeof(Storage3dItemStorage3dLocation), CascadeOperations = CascadeOperation.All)]
        public partial List<Storage3dItem> Items { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<IStorage3dItem> Items { get; set; } = [];
#endif
        #endregion

        #region Ctor
        public Storage3dLocation()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

#if SQL
        public Storage3dItem CreateStockItem(Material3d material, double amount = 0, Unit unit = Unit.Kilogram)
#else
        public IStorage3dItem CreateStockItem(IMaterial3d material, double amount = 0, Unit unit = Unit.Kilogram)
#endif
        {
#if SQL
            Storage3dItem item = new()
#else
            IStorage3dItem item = new Storage3dItem() 
#endif
            {
                Material = material
            };
            if (amount != 0) UpdateStockAmount(item, amount, unit);
            Items.Add(item);
            return item;
        }

#if SQL
        public Storage3dTransaction? AddToStock(Storage3dItem item, double amount, Unit unit)
#else
        public IStorage3dTransaction? AddToStock(IStorage3dItem item, double amount, Unit unit)
#endif
        {
            if (item.Material is not null)
            {
#if SQL
                Storage3dTransaction transaction = new()
#else
                IStorage3dTransaction transaction = new Storage3dTransaction()
#endif
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

#if SQL
        public Storage3dTransaction? AddToStock(Material3d material, double amount, Unit unit)
#else
        public IStorage3dTransaction? AddToStock(IMaterial3d material, double amount, Unit unit)
#endif
        {
#if SQL
            Storage3dItem? item =
#else
            IStorage3dItem? item =
#endif
                Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(item: item, amount: amount, unit: unit);
            else
                CreateStockItem(material, amount, unit);
            return item?.Transactions.LastOrDefault();
        }

#if SQL
        public Storage3dTransaction? AddToStock(Material3d material, double amount, Unit unit, Guid? calculationId = null)
#else
        public IStorage3dTransaction? AddToStock(IMaterial3d material, double amount, Unit unit, Guid? calculationId = null)
#endif
        {
#if SQL
            Storage3dItem? item =
#else
            IStorage3dItem? item =
#endif
                Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(item: item, amount: amount, unit: unit);
            else
                item = CreateStockItem(material, amount, unit);
            return item?.Transactions.LastOrDefault();
        }

#if SQL
        public Storage3dTransaction? TakeFromStock(Storage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
#else
        public IStorage3dTransaction? TakeFromStock(IStorage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
#endif
        {
            if (item.Material is not null)
            {
#if SQL
                Storage3dTransaction transaction = new()
#else
                IStorage3dTransaction transaction = new Storage3dTransaction()
#endif
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

#if SQL
        public Storage3dTransaction? TakeFromStock(Material3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
#else
        public IStorage3dTransaction? TakeFromStock(IMaterial3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
#endif
        {
#if SQL
            Storage3dItem? item =
#else
            IStorage3dItem? item = 
#endif
                Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item is null) return null;
            return TakeFromStock(item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock);
        }

#if SQL
        public Storage3dTransaction? TakeFromStock(Material3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false)
#else
        public IStorage3dTransaction? TakeFromStock(IMaterial3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false)
#endif
        {
#if SQL
            Storage3dItem? item =
#else
            IStorage3dItem? item =
#endif
                Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item is null) return null;
            return TakeFromStock(item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock);
        }

#if SQL
        public Storage3dTransaction? UpdateStockAmount(Storage3dItem item, double amount, Unit unit = Unit.Kilogram)
#else
        public IStorage3dTransaction? UpdateStockAmount(IStorage3dItem item, double amount, Unit unit = Unit.Kilogram)
#endif
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

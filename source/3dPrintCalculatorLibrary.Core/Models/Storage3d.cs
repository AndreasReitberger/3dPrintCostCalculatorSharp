using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
using AndreasReitberger.Print3d.SQLite.StorageAdditions;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Storage3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Storage3d : ObservableObject, IStorage3d
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial int Capacity { get; set; } = 32;

#if SQL
        [ObservableProperty]
        [ManyToMany(typeof(Storage3dLocationStorage3d), CascadeOperations = CascadeOperation.All)]
        public partial List<Storage3dLocation> Locations { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<IStorage3dLocation> Locations { get; set; } = [];
#endif
        #endregion

        #region Ctor
        public Storage3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods
#if SQL
        public Storage3dItem CreateStockItem(Storage3dLocation location, Material3d material, double amount = 0, Unit unit = Unit.Kilogram)
#else
        public IStorage3dItem CreateStockItem(IStorage3dLocation location, IMaterial3d material, double amount = 0, Unit unit = Unit.Kilogram)
#endif
        {
#if SQL
            Storage3dItem item = new() { Material = material };
#else
            IStorage3dItem item = new Storage3dItem() { Material = material };
#endif
            if (amount != 0) UpdateStockAmount(location: location, item, amount, unit);
            location?.Items?.Add(item);
            return item;
        }

#if SQL
        public void AddToStock(Storage3dLocation location, Storage3dItem item, double amount, Unit unit)
#else
        public void AddToStock(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit)
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
                item.Transactions?.Add(transaction);
            }
        }
#if SQL
        public void AddToStock(Storage3dLocation location, Material3d material, double amount, Unit unit)
#else
        public void AddToStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit)
#endif
        {
#if SQL
            Storage3dItem? item =
#else
            IStorage3dItem? item = 
#endif
                location.Items.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material is not null)
                AddToStock(location: location, item: item, amount: amount, unit: unit);
            else
                CreateStockItem(location: location, material, amount, unit);
        }

#if SQL
        public Storage3dTransaction? AddToStock(Storage3dLocation location, Material3d material, double amount, Unit unit, Guid? calculationId = null)
#else
        public IStorage3dTransaction? AddToStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, Guid? calculationId = null)
#endif
        {
#if SQL
            Storage3dItem? item =
#else
            IStorage3dItem? item =
#endif
                location.Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(location: location, item: item, amount: amount, unit: unit);
            else
                item = CreateStockItem(location: location, material, amount, unit);
            return item?.Transactions.LastOrDefault();
        }

#if SQL
        public bool TakeFromStock(Storage3dLocation location, Storage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
#else
        public bool TakeFromStock(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
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

#if SQL
        public bool TakeFromStock(Storage3dLocation location, Material3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
#else
        public bool TakeFromStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
#endif
        {
#if SQL
            Storage3dItem? item =
#else
            IStorage3dItem? item = 
#endif
                location.Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item is null) return false;
            return TakeFromStock(location: location, item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock);
        }

#if SQL
        public Storage3dTransaction? TakeFromStock(Storage3dLocation location, Material3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false)
#else
        public IStorage3dTransaction? TakeFromStock(IStorage3dLocation location, IMaterial3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false)
#endif
        {
#if SQL
            Storage3dItem? item =
#else
            IStorage3dItem? item = 
#endif
                location.Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item is not null && TakeFromStock(location: location, item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock))
            {
                return item?.Transactions.LastOrDefault();
            }
            else return null;
        }

#if SQL
        public bool UpdateStockAmount(Storage3dLocation location, Storage3dItem item, double amount, Unit unit = Unit.Kilogram)
#else
        public bool UpdateStockAmount(IStorage3dLocation location, IStorage3dItem item, double amount, Unit unit = Unit.Kilogram)
#endif
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

using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Utilities;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndreasReitberger.Print3d.Realm.StorageAdditions
{
    public partial class Storage3dLocation : RealmObject, IStorage3dLocation
    {
        #region Properties

        [PrimaryKey]
        public Guid Id { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; } = 32;

        public IList<Storage3dItem> Items { get; }
        #endregion

        #region Ctor
        public Storage3dLocation()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public Storage3dItem CreateStockItem(Material3d material, double amount = 0, Unit unit = Unit.Kilogram)
        {
            Storage3dItem item = new() { Material = material };
            if (amount != 0) UpdateStockAmount(item, amount, unit);
            Items?.Add(item);
            return item;
        }

        public void AddToStock(Storage3dItem item, double amount, Unit unit)
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
        public void AddToStock(Material3d material, double amount, Unit unit)
        {
            Storage3dItem item = Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(item: item, amount: amount, unit: unit);
            else
                CreateStockItem(material, amount, unit);
        }

        public Storage3dTransaction AddToStock(Material3d material, double amount, Unit unit, Guid? calculationId = null)
        {
            Storage3dItem item = Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (item?.Material != null)
                AddToStock(item: item, amount: amount, unit: unit);
            else
                item = CreateStockItem(material, amount, unit);
            return new()
            {
                Amount = amount,
                Unit = unit,
                CalculationId = calculationId,
                DateTime = DateTime.Now,
                Item = item,
            };
        }

        public bool TakeFromStock(Storage3dItem item, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
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

        public bool TakeFromStock(Material3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
        {
            Storage3dItem item = Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            return TakeFromStock(item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock);
        }

        public Storage3dTransaction TakeFromStock(Material3d material, double amount, Unit unit, Guid? calculationId = null, bool throwIfMaterialIsNotInStock = false)
        {
            Storage3dItem item = Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if (TakeFromStock(item: item, amount: amount, unit: unit, throwIfMaterialIsNotInStock: throwIfMaterialIsNotInStock))
            {
                return new()
                {
                    Amount = -amount,
                    Unit = unit,
                    CalculationId = calculationId,
                    DateTime = DateTime.Now,
                    Item = item,
                };
            }
            else return null;
        }

        public bool UpdateStockAmount(Storage3dItem item, double amount, Unit unit = Unit.Kilogram)
        {
            if (amount > 0)
            {
                AddToStock(item: item, amount: amount, unit: unit);
                return true;
            }
            else if (amount < 0)
            {
                return TakeFromStock(item: item, amount: Math.Abs(amount), unit: unit);
            }
            return false;
        }
        #endregion
    }
}

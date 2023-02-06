using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.StorageAdditions;
using AndreasReitberger.Print3d.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Storage3d : ObservableObject, IStorage3d
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        ObservableCollection<Storage3dItem> items = new();
        #endregion

        #region Ctor
        public Storage3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public void AddToStock(Material3d material, double amount, Unit unit)
        {
            Storage3dItem item = Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
            if(item?.Material != null)
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
            else
            {
                Items?.Add(new()
                {
                    Material = material,
                    Amount = amount,
                });
            }
        }

        public bool TakeFromStock(Material3d material, double amount, Unit unit, bool throwIfMaterialIsNotInStock = false)
        {
            Storage3dItem item = Items?.FirstOrDefault(curItem => curItem?.Material?.Id == material?.Id);
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
                    throw new ArgumentOutOfRangeException($"The amount of the material `{material}` is not sufficient for this transaction (In stock: {item.Amount} / Requested: {amount}!");
                }
                else return false;
            }
            else if (throwIfMaterialIsNotInStock)
            {
                throw new ArgumentOutOfRangeException($"The material `{material}` is not available in the stock!");
            }
            else return false;
        }

        #endregion
    }
}

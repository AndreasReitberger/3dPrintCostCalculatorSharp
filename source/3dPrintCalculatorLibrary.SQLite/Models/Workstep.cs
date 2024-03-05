using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using SQLite;
using System;
using SQLiteNetExtensions.Attributes;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Workstep)}s")]
    public partial class Workstep : ObservableObject, IWorkstep, ICloneable
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double price = 0;
        partial void OnPriceChanged(double value)
        {
            TotalCosts = CalcualteTotalCosts();
        }

        [ObservableProperty]
        [Obsolete("Use the WorkstepUsageParameter instead")]
        int quantity = 1;
        partial void OnQuantityChanged(int value)
        {
            TotalCosts = CalcualteTotalCosts();
        }

        [ObservableProperty]
        Guid categoryId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CategoryId), CascadeOperations = CascadeOperation.All)]
        WorkstepCategory category;

        [ObservableProperty]
        CalculationType calculationType;

        [ObservableProperty]
        [Obsolete("Use the WorkstepUsageParameter instead")]
        double duration = 0;
        partial void OnDurationChanged(double value)
        {
            TotalCosts = CalcualteTotalCosts();
        }

        [ObservableProperty]
        WorkstepType type;

        [ObservableProperty]
        [Obsolete("Use the WorkstepUsageParameter instead")]
        double totalCosts = 0;

        [ObservableProperty]
        string note = string.Empty;
        #endregion

        #region Constructors
        public Workstep() { }
        #endregion

        #region Private
        [Obsolete("Use the WorkstepUsageParameter instead")]
        double CalcualteTotalCosts()
        {
            try
            {
                if (Duration == 0)
                    return Price * Convert.ToDouble(Quantity);
                return Duration * Price * Convert.ToDouble(Quantity);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0} ({1}) - {2:C2}", Name, Type, Price);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Workstep item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
    }
}

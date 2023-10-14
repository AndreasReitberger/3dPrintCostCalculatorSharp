using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.WorkstepAdditions;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Workstep : RealmObject, IWorkstep, ICloneable
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        double price { get; set; } = 0;
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPriceChanged(value);
            }
        }
        void OnPriceChanged(double value)
        {
            TotalCosts = CalcualteTotalCosts();
        }

        int quantity { get; set; } = 1;

        [Obsolete("Use the WorkstepUsageParameter instead")]
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnQuantityChanged(value);
            }
        }
        void OnQuantityChanged(int value)
        {
            TotalCosts = CalcualteTotalCosts();
        }

        public Guid CategoryId { get; set; }

        public WorkstepCategory Category { get; set; }

        public int CalculationTypeId { get; set; }
        public CalculationType CalculationType
        {
            get => (CalculationType)CalculationTypeId;
            set { CalculationTypeId = (int)value; }
        }

        double duration { get; set; } = 0;

        [Obsolete("Use the WorkstepUsageParameter instead")]
        public double Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnDurationChanged(value);
            }
        }
        void OnDurationChanged(double value)
        {
            TotalCosts = CalcualteTotalCosts();
        }

        public int TypeId { get; set; }
        public WorkstepType Type
        {
            get => (WorkstepType)TypeId;
            set { TypeId = (int)value; }
        }


        [Obsolete("Use the WorkstepUsageParameter instead")]
        public double TotalCosts { get; set; } = 0;

        public string Note { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public Workstep()
        {
            Id = Guid.NewGuid();
        }
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

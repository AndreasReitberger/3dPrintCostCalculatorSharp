using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.WorkstepAdditions;
using Newtonsoft.Json;
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

        public double Price { get; set; } = 0;

        public Guid CategoryId { get; set; }

        public WorkstepCategory? Category { get; set; }

        public int CalculationTypeId { get; set; }
        public CalculationType CalculationType
        {
            get => (CalculationType)CalculationTypeId;
            set { CalculationTypeId = (int)value; }
        }

        public int TypeId { get; set; }
        public WorkstepType Type
        {
            get => (WorkstepType)TypeId;
            set { TypeId = (int)value; }
        }

        public string Note { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public Workstep()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Workstep item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        public object Clone() => MemberwiseClone();

        #endregion
    }
}

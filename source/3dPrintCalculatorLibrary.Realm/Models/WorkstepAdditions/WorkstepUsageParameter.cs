using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.WorkstepAdditions
{
    public partial class WorkstepUsageParameter : RealmObject, IWorkstepUsageParameter
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public WorkstepUsageParameterType ParameterType
        {
            get => (WorkstepUsageParameterType)ParameterTypeId;
            set { ParameterTypeId = (int)value; }
        }
        public int ParameterTypeId { get; set; } = (int)WorkstepUsageParameterType.Quantity;

        public double Value { get; set; } = 0;
        #endregion

        #region Constructors
        public WorkstepUsageParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not WorkstepUsageParameter item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        #endregion
    }
}

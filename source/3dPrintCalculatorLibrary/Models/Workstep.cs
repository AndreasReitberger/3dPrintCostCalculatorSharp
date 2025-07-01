using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.WorkstepAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Workstep : ObservableObject, IWorkstep, ICloneable
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Price { get; set; } = 0;

        [ObservableProperty]
        public partial Guid CategoryId { get; set; }

        [ObservableProperty]
        public partial WorkstepCategory? Category { get; set; }

        [ObservableProperty]
        public partial CalculationType CalculationType { get; set; }

        [ObservableProperty]
        public partial WorkstepType Type { get; set; }

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public Workstep() { }
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

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
        Guid id;

        [ObservableProperty]
        Guid calculationId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double price = 0;

        [ObservableProperty]
        Guid categoryId;

        [ObservableProperty]
        WorkstepCategory? category;

        [ObservableProperty]
        CalculationType calculationType;

        [ObservableProperty]
        WorkstepType type;

        [ObservableProperty]
        string note = string.Empty;
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

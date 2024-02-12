using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Workstep : ObservableObject, IWorkstep
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double price = 0;

        [ObservableProperty]
        IWorkstepCategory? category;

        [ObservableProperty]
        CalculationType calculationType;

        [ObservableProperty]
        WorkstepType type;

        [ObservableProperty]
        string note = string.Empty;
        #endregion

        #region Constructors
        public Workstep() 
        {   
            Id = Guid.NewGuid();
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
        public object Clone() => MemberwiseClone();
        
        #endregion
    }
}

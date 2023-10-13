using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.WorkstepAdditions
{
    public partial class WorkstepDuration : ObservableObject, IWorkstepDuration
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Workstep workstep;

        [ObservableProperty]
        Guid workstepId;

        [ObservableProperty]
        double duration = 0;
        #endregion

        #region Constructors
        public WorkstepDuration()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return Duration.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj is not WorkstepDuration item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}

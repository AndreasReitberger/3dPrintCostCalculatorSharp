using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    [Table("WorkstepDurations")]
    public partial class WorkstepDuration : ObservableObject, IWorkstepDuration
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
        public Guid calculationId;

        [ObservableProperty]
        [property: ManyToOne]
        public Calculation3d calculation;

        [ObservableProperty]
        public Guid workstepId;

        [ObservableProperty]
        public double duration = 0;
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

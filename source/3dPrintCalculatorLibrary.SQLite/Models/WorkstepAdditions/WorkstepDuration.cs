using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    [Table("WorkstepDurations")]
    public partial class WorkstepDuration : ObservableObject, IWorkstepDuration
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        Guid workstepId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
        Guid calculationId;

        /*
        [ObservableProperty]
        [property: ManyToOne]
        Calculation3d calculation;
        */

        [ObservableProperty]
        [property: ManyToOne(nameof(WorkstepId))]
        Workstep workstep;

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

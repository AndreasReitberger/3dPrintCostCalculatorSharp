using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.WorkstepAdditions
{
    public partial class WorkstepDuration : RealmObject, IWorkstepDuration
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Workstep Workstep { get; set; }

        public Guid WorkstepId { get; set; }

        public double Duration { get; set; } = 0;
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

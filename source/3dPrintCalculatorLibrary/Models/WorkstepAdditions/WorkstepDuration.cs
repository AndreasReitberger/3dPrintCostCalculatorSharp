using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models.WorkstepAdditions
{
    [Table("WorkstepDurations")]
    public class WorkstepDuration
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId
        { get; set; }

        [ManyToOne]
        public Calculation3d Calculation { get; set; }

        public Guid WorkstepId
        { get; set; }

        public double Duration
        { get; set; } = 0;
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

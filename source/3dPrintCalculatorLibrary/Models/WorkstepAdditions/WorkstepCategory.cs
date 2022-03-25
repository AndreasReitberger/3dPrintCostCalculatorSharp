using SQLite;
using System;

namespace AndreasReitberger.Models.WorkstepAdditions
{
    [Table("WorkstepCategories")]
    public class WorkstepCategory
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }// = Guid.NewGuid();

        public string Name
        { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public WorkstepCategory()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not WorkstepCategory item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}

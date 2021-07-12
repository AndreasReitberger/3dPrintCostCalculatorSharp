using System;

namespace AndreasReitberger.Models.WorkstepAdditions
{
    public class WorkstepCategory
    {
        #region Properties
        public Guid Id
        { get; set; } = Guid.NewGuid();

        public string Name
        { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public WorkstepCategory() { }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        /*
        public override bool Equals(object obj)
        {
            var item = obj as WorkstepCategory;
            if (item == null)
                return false;
            return this.Name.Equals(item.Name);
        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        */
        public override bool Equals(object obj)
        {
            var item = obj as WorkstepCategory;
            if (item == null)
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

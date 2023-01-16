using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;

namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    [Table("WorkstepCategories")]
    public partial class WorkstepCategory : ObservableObject, IWorkstepCategory
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        public string name = string.Empty;
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

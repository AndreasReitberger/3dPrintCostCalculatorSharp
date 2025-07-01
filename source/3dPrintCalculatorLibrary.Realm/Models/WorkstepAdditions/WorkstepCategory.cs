using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.WorkstepAdditions
{
    public partial class WorkstepCategory : RealmObject, IWorkstepCategory
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public WorkstepCategory()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not WorkstepCategory item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}

using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Workstep)}s")]
    public partial class Workstep : ObservableObject, IWorkstep, ICloneable
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double price = 0;

        [ObservableProperty]
        Guid categoryId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CategoryId), CascadeOperations = CascadeOperation.All)]
        WorkstepCategory? category;

        [ObservableProperty]
        CalculationType calculationType;

        [ObservableProperty]
        WorkstepType type;

        [ObservableProperty]
        string note = string.Empty;
        #endregion

        #region Constructors
        public Workstep() { }
        #endregion

        #region Overrides
        public override string ToString() => $"{Name} ({Type}) - {Price:C2}";
        
        public override bool Equals(object? obj)
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

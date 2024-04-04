using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(WorkstepUsage)}s")]
    public partial class WorkstepUsage : ObservableObject, IWorkstepUsage
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;

        [ObservableProperty]
        Guid workstepId;

        [ObservableProperty]
        [property: ManyToOne(nameof(WorkstepId), CascadeOperations = CascadeOperation.All)]
        Workstep workstep;
        partial void OnWorkstepChanged(Workstep value) => TotalCosts = GetTotalCosts();

        [ObservableProperty]
        Guid usageParameterId;

        [ObservableProperty]
        [property: ManyToOne(nameof(UsageParameterId), CascadeOperations = CascadeOperation.All)]
        WorkstepUsageParameter usageParameter;
        partial void OnUsageParameterChanged(WorkstepUsageParameter value) => TotalCosts = GetTotalCosts();

        [ObservableProperty]
        double totalCosts = 0;
        #endregion

        #region Constructors
        public WorkstepUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public double GetTotalCosts()
        {
            double cost;
            cost = (UsageParameter?.ParameterType) switch
            {
                Enums.WorkstepUsageParameterType.Duration => (Workstep?.Price * UsageParameter?.Value) ?? 0,
                _ => (Workstep?.Price * UsageParameter?.Value) ?? 0,
            };
            return cost;
        }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object obj)
        {
            if (obj is not WorkstepUsage item)
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

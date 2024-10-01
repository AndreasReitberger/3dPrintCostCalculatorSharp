using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(WorkstepUsage)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class WorkstepUsage : ObservableObject, IWorkstepUsage
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;
        
        [ObservableProperty]
        Guid workstepId;
        
        [ObservableProperty]
        [property: ManyToOne(nameof(WorkstepId), CascadeOperations = CascadeOperation.All)]
        Workstep? workstep;
        partial void OnWorkstepChanged(Workstep? value) => TotalCosts = GetTotalCosts();
        
        [ObservableProperty]
        Guid usageParameterId;

        [ObservableProperty]
        IWorkstepUsageParameter? usageParameter;

        [ObservableProperty]
        [property: ManyToOne(nameof(UsageParameterId), CascadeOperations = CascadeOperation.All)]
        WorkstepUsageParameter? usageParameter;
        partial void OnUsageParameterChanged(WorkstepUsageParameter? value) => TotalCosts = GetTotalCosts();
#else

        [ObservableProperty]
        IWorkstep? workstep;
        partial void OnWorkstepChanged(IWorkstep? value) => TotalCosts = GetTotalCosts();

        [ObservableProperty]
        IWorkstepUsageParameter? usageParameter;
        partial void OnUsageParameterChanged(IWorkstepUsageParameter? value) => TotalCosts = GetTotalCosts();
#endif

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
        public override bool Equals(object? obj)
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

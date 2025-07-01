using AndreasReitberger.Print3d.Core.Enums;
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
#if SQL
        [property: PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dEnhanced))]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dProfile))]
        public partial Guid CalculationProfileId { get; set; }

        [ObservableProperty]
        public partial Guid WorkstepId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(WorkstepId), CascadeOperations = CascadeOperation.All)]
        public partial Workstep? Workstep { get; set; }
        partial void OnWorkstepChanged(Workstep? value) => TotalCosts = GetTotalCosts();

        [ObservableProperty]
        public partial Guid UsageParameterId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(UsageParameterId), CascadeOperations = CascadeOperation.All)]
        public partial WorkstepUsageParameter? UsageParameter { get; set; }
        partial void OnUsageParameterChanged(WorkstepUsageParameter? value) => TotalCosts = GetTotalCosts();
#else

        [ObservableProperty]
        public partial IWorkstep? Workstep { get; set; }

        partial void OnWorkstepChanged(IWorkstep? value) => TotalCosts = GetTotalCosts();

        [ObservableProperty]
        public partial IWorkstepUsageParameter? UsageParameter { get; set; }

        partial void OnUsageParameterChanged(IWorkstepUsageParameter? value) => TotalCosts = GetTotalCosts();
#endif

        [ObservableProperty]
        public partial double TotalCosts { get; set; } = 0;

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
                WorkstepUsageParameterType.Duration => (Workstep?.Price * UsageParameter?.Value) ?? 0,
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
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}

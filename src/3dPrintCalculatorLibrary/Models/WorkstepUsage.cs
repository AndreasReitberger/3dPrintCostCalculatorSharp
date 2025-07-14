using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.WorkstepAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models
{
    public partial class WorkstepUsage : ObservableObject, IWorkstepUsage
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial Guid WorkstepId { get; set; }

        [ObservableProperty]
        public partial Workstep? Workstep { get; set; }

        [ObservableProperty]
        public partial Guid UsageParameterId { get; set; }

        [ObservableProperty]
        public partial WorkstepUsageParameter? UsageParameter { get; set; }

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
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}

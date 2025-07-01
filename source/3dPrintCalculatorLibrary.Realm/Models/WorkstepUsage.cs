using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.WorkstepAdditions;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class WorkstepUsage : RealmObject, IWorkstepUsage
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationId { get; set; }
        public Guid WorkstepId { get; set; }
        public Workstep? Workstep { get; set; }
        public WorkstepUsageParameter? UsageParameter { get; set; }
        public double TotalCosts { get; set; }

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

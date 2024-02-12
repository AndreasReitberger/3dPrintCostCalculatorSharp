﻿using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Core
{
    public partial class WorkstepUsage : ObservableObject, IWorkstepUsage
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        IWorkstep? workstep;

        [ObservableProperty]
        IWorkstepUsageParameter? usageParameter;

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

﻿using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    [Table($"{nameof(WorkstepUsageParameter)}s")]
    public partial class WorkstepUsageParameter : ObservableObject, IWorkstepUsageParameter
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        WorkstepUsageParameterType parameterType = WorkstepUsageParameterType.Quantity;

        [ObservableProperty]
        double value = 0;
        #endregion

        #region Constructors
        public WorkstepUsageParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object obj)
        {
            if (obj is not WorkstepUsageParameter item)
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

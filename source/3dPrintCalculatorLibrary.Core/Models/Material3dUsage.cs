using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Material3dUsage : ObservableObject, ICloneable, IMaterial3dUsage
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        IMaterial3d? material;

        [ObservableProperty]
        double percentage = 1;

        #endregion

        #region Constructor
        public Material3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();
        
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Item3dUsage item)
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

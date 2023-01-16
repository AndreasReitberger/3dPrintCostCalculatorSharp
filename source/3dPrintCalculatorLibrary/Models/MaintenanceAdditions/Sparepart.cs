using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.MaintenanceAdditions
{
    public partial class Sparepart : ObservableObject, ISparepart
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid maintenanceId;

        [ObservableProperty]
        public string name = string.Empty;

        [ObservableProperty]
        public string partnumber = string.Empty;

        [ObservableProperty]
        public double costs;
        #endregion

        #region Constructor
        public Sparepart() 
        { 
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Sparepart item)
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

using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.MaterialAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Material3d : ObservableObject, IMaterial3d, ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string SKU { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Unit Unit { get; set; } = Unit.Kilogram;

        [ObservableProperty]
        public partial double PackageSize { get; set; } = 1;

        [ObservableProperty]
        public partial double Density { get; set; } = 1;

        [ObservableProperty]
        public partial double FactorLToKg { get; set; } = 1;

        [ObservableProperty]
        public partial List<Material3dAttribute> Attributes { get; set; } = [];

        [ObservableProperty]
        public partial List<Material3dProcedureAttribute> ProcedureAttributes { get; set; } = [];

        [ObservableProperty]
        public partial List<Material3dColor> Colors { get; set; } = [];

        [ObservableProperty]
        public partial Material3dFamily MaterialFamily { get; set; } = Material3dFamily.Filament;

        [ObservableProperty]
        public partial Guid MaterialTypeId { get; set; }

        [ObservableProperty]
        public partial Material3dType? TypeOfMaterial { get; set; }

        [ObservableProperty]
        public partial Guid ManufacturerId { get; set; }

        [ObservableProperty]
        public partial Manufacturer? Manufacturer { get; set; }

        [ObservableProperty]
        public partial double UnitPrice { get; set; }

        [ObservableProperty]
        public partial double Tax { get; set; } = 0;

        [ObservableProperty]
        public partial bool PriceIncludesTax { get; set; } = true;

        [ObservableProperty]
        public partial string Uri { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ColorCode { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string SafetyDatasheet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string TechnicalDatasheet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Unit SpoolWeightUnit { get; set; } = Unit.Gram;

        [ObservableProperty]
        public partial double SpoolWeight { get; set; } = 200;

        [ObservableProperty]
        public partial byte[] Image { get; set; } = [];
        #endregion

        #region Constructor
        public Material3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Material3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        #endregion
    }
}

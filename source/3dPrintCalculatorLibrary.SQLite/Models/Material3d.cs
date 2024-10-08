﻿using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Materials")]
    public partial class Material3d : ObservableObject, IMaterial3d, ICloneable
    {
        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string sKU = string.Empty;

        [ObservableProperty]
        Unit unit = Unit.Kilogram;

        [ObservableProperty]
        double packageSize = 1;

        [ObservableProperty]
        double density = 1;

        [ObservableProperty]
        double factorLToKg = 1;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Material3dAttribute> attributes = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Material3dProcedureAttribute> procedureAttributes = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Material3dColor> colors = [];

        [ObservableProperty]
        Material3dFamily materialFamily = Material3dFamily.Filament;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid materialTypeId;

        [ObservableProperty]
        [property: ManyToOne(nameof(MaterialTypeId), CascadeOperations = CascadeOperation.All)]
        Material3dType? typeOfMaterial;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid manufacturerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ManufacturerId), CascadeOperations = CascadeOperation.All)]
        Manufacturer? manufacturer;

        [ObservableProperty]
        double unitPrice;

        [ObservableProperty]
        double tax = 0;

        [ObservableProperty]
        bool priceIncludesTax = true;

        [ObservableProperty]
        string uri = string.Empty;
        [ObservableProperty]
        string colorCode = string.Empty;

        [ObservableProperty]
        string note = string.Empty;

        [ObservableProperty]
        string safetyDatasheet = string.Empty;

        [ObservableProperty]
        string technicalDatasheet = string.Empty;

        [ObservableProperty]
        Unit spoolWeightUnit = Unit.Gram;

        [ObservableProperty]
        double spoolWeight = 200;

        [ObservableProperty]
        byte[] image = [];
        #endregion

        #region Constructor
        public Material3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Material3d item)
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

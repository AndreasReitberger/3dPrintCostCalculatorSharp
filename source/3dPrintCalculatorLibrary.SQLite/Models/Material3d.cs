using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Materials")]
    public partial class Material3d : ObservableObject, ICloneable, IMaterial3d
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
        public Guid calculationId;

        [ObservableProperty]
        public string name = string.Empty;

        [ObservableProperty]
        public string sKU = string.Empty;

        [ObservableProperty]
        public Unit unit = Unit.kg;

        [ObservableProperty]
        public double packageSize = 1;

        [ObservableProperty]
        public double density = 1;

        [ObservableProperty]
        public double factorLToKg = 1;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Material3dAttribute> attributes = new();

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Material3dProcedureAttribute> procedureAttributes = new();

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Material3dColor> colors = new();

        [ObservableProperty]
        public Material3dFamily materialFamily = Material3dFamily.Filament;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid materialTypeId;

        [ObservableProperty]
        [property: ManyToOne(nameof(MaterialTypeId))]
        public Material3dType typeOfMaterial;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid manufacturerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ManufacturerId))]
        public Manufacturer manufacturer;

        [ObservableProperty]
        public double unitPrice;

        [ObservableProperty]
        public double tax = 0;

        [ObservableProperty]
        public bool priceIncludesTax = true;

        [ObservableProperty]
        public string uri = string.Empty;
        [ObservableProperty]
        public string colorCode = string.Empty;

        [ObservableProperty]
        public string note = string.Empty;

        [ObservableProperty]
        public string safetyDatasheet = string.Empty;

        [ObservableProperty]
        public string technicalDatasheet = string.Empty;

        [ObservableProperty]
        public Unit spoolWeightUnit = Unit.g;

        [ObservableProperty]
        public double spoolWeight = 200;
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
        public override bool Equals(object obj)
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

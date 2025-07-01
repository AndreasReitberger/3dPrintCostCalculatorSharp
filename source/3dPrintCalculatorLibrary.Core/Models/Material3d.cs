using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Xml.Serialization;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Material3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Material3d : ObservableObject, IMaterial3d
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;
#endif

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

#if SQL
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
#else
        [ObservableProperty]
        IList<IMaterial3dAttribute> attributes = [];

        [ObservableProperty]
        IList<IMaterial3dProcedureAttribute> procedureAttributes = [];

        [ObservableProperty]
        IList<IMaterial3dColor> colors = [];

        [ObservableProperty]
        IMaterial3dType? typeOfMaterial;

        [ObservableProperty]
        IManufacturer? manufacturer;
#endif

        [ObservableProperty]
        Material3dFamily materialFamily = Material3dFamily.Filament;

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

        #region Clone
        public object Clone() => MemberwiseClone();
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

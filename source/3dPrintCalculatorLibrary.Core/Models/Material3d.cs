using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Material3d : ObservableObject, IMaterial3d
    {
        #region Properties
        [ObservableProperty]
        Guid id;

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
        IList<IMaterial3dAttribute> attributes = [];

        [ObservableProperty]
        IList<IMaterial3dProcedureAttribute> procedureAttributes = [];

        [ObservableProperty]
        IList<IMaterial3dColor> colors = [];

        [ObservableProperty]
        Material3dFamily materialFamily = Material3dFamily.Filament;

        [ObservableProperty]
        IMaterial3dType? typeOfMaterial;

        [ObservableProperty]
        IManufacturer? manufacturer;

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

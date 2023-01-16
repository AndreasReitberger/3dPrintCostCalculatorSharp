using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.FileAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models
{
    public partial class File3d : ObservableObject, IFile3d
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid calculationId;

        [ObservableProperty]
        [property: JsonIgnore]
        string name = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        object file;

        [ObservableProperty]
        [property: JsonIgnore]
        string fileName = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        string filePath = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        double volume = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid modelWeightId;

        [ObservableProperty]
        [property: JsonIgnore]
        [property: ManyToOne(nameof(ModelWeightId))]
        ModelWeight weight = new(-1, Enums.Unit.g);

        [ObservableProperty]
        [property: JsonIgnore]
        double printTime = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        int quantity = 1;

        [ObservableProperty]
        [property: JsonIgnore]
        bool multiplyPrintTimeWithQuantity = true;

        [ObservableProperty]
        [property: JsonIgnore]
        double printTimeQuantityFactor = 1;

        #endregion

        #region Constructor
        public File3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return this.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not File3d item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}

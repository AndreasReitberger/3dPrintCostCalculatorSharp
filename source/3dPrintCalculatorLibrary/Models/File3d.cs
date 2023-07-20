using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.FileAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models
{
    public partial class File3d : ObservableObject, IFile3d
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid calculationId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        object file;

        [ObservableProperty]
        string fileName = string.Empty;

        [ObservableProperty]
        string filePath = string.Empty;

        [ObservableProperty]
        double volume = 0;

        [ObservableProperty]
        Guid modelWeightId;

        [ObservableProperty]
        ModelWeight weight = new(-1, Enums.Unit.Gram);

        [ObservableProperty]
        double printTime = 0;

        [ObservableProperty]
        int quantity = 1;

        [ObservableProperty]
        bool multiplyPrintTimeWithQuantity = true;

        [ObservableProperty]
        double printTimeQuantityFactor = 1;

        [ObservableProperty]
        byte[] image = Array.Empty<byte>();
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
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not File3d item)
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

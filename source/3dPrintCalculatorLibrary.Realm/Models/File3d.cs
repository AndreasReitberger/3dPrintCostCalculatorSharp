using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.FileAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class File3d : ObservableObject, IFile3d
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid calculationId;

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
        public Guid modelWeightId;

        [ObservableProperty]
        ModelWeight weight = new(-1, Enums.Unit.Gramm);

        [ObservableProperty]
        double printTime = 0;

        [ObservableProperty]
        int quantity = 1;

        [ObservableProperty]
        bool multiplyPrintTimeWithQuantity = true;

        [ObservableProperty]
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

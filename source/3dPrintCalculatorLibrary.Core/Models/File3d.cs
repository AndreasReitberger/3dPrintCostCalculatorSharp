using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Newtonsoft.Json;
using System.IO;

namespace AndreasReitberger.Print3d.Core
{
    public partial class File3d : ObservableObject, IFile3d
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        object? file;

        [ObservableProperty]
        string fileName = string.Empty;

        [ObservableProperty]
        string filePath = string.Empty;
        partial void OnFilePathChanged(string value)
        {
            if (value is not null)
            {
                FileName = new FileInfo(value)?.Name ?? string.Empty;
                if (string.IsNullOrEmpty(Name)) Name = FileName;
            }
        }

        [ObservableProperty]
        double volume = 0;

        [ObservableProperty]
        IFile3dWeight weight = new File3dWeight(-1, Enums.Unit.Gram);

        [ObservableProperty]
        double printTime = 0;

        [ObservableProperty]
        int quantity = 1;

        [ObservableProperty]
        bool multiplyPrintTimeWithQuantity = true;

        [ObservableProperty]
        double printTimeQuantityFactor = 1;

        [ObservableProperty]
        byte[] image = [];
        #endregion

        #region Constructor
        public File3d()
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

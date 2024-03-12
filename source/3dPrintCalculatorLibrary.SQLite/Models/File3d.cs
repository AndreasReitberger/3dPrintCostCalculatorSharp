﻿using AndreasReitberger.Print3d.SQLite.FileAdditions;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Files")]
    public partial class File3d : ObservableObject, IFile3d
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
        string name = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore, Ignore]
        object file;

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
        Guid modelWeightId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ModelWeightId), CascadeOperations = CascadeOperation.All)]
        ModelWeight weight = new(-1, Enums.Unit.Gram);

        [ObservableProperty]
        double printTime = 0;

        [ObservableProperty]
        byte[] image = [];

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
        public override bool Equals(object? obj)
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

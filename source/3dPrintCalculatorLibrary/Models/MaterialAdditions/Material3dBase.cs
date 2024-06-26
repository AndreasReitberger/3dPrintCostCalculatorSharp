﻿using AndreasReitberger.Print3d.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    [Obsolete]
    public partial class Material3dBase : ObservableObject
    {
        #region Properties 
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Material3dTypes type;

        [ObservableProperty]
        string material = string.Empty;

        [ObservableProperty]
        string polymer = string.Empty;
        #endregion

        #region Constructors
        public Material3dBase()
        {
            Id = Guid.NewGuid();
        }
        public Material3dBase(Material3dTypes type, string material)
        {
            Id = Guid.NewGuid();
            Type = type;
            Material = material;
        }
        public Material3dBase(Material3dTypes type, string material, string polymer)
        {
            Id = Guid.NewGuid();
            Type = type;
            Material = material;
            Polymer = polymer;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return string.IsNullOrEmpty(Type.ToString()) ? Material : string.Format("{0} ({1})", Material, Type.ToString());
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Material3dBase item)
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

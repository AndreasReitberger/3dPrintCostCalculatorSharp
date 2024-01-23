﻿using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Print3dInfo : ObservableObject, IPrint3dInfo, ICloneable
    {

        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string? name;

        [ObservableProperty]
        Guid fileId;

        [ObservableProperty]
        File3d file;

        [ObservableProperty]
        Guid materialId;

        [ObservableProperty]
        Material3d material;

        [ObservableProperty]
        Guid printerId;

        [ObservableProperty]
        Printer3d printer;

        [ObservableProperty]
        List<Item3dUsage> items = new();
        #endregion

        #region Constructor

        public Print3dInfo()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Overrides
        public override string ToString() =>  JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object obj)
        {
            if (obj is not Printer3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        public object Clone() => MemberwiseClone();
        
        #endregion

    }
}

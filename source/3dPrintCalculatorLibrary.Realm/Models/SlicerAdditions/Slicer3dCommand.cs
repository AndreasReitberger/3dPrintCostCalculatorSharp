﻿using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Realms;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Realm.SlicerAdditions
{
    public partial class Slicer3dCommand : RealmObject, ISlicer3dCommand
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid SlicerId { get; set; }

        public Slicer3d Slicer { get; set; }

        public string Name { get; set; }

        public string Command { get; set; }

        public string OutputFilePatternString { get; set; }

        public bool AutoAddFilePath { get; set; }

        #endregion

        #region Constructor
        public Slicer3dCommand()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}

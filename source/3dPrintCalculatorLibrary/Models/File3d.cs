using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.FileAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Newtonsoft.Json;
using System.IO;

namespace AndreasReitberger.Print3d.Models
{
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
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonIgnore]
        public partial object? File { get; set; }

        [ObservableProperty]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string FilePath { get; set; } = string.Empty;

        partial void OnFilePathChanged(string value)
        {
            if (value is not null)
            {
                FileName = new FileInfo(value)?.Name ?? string.Empty;
                if (string.IsNullOrEmpty(Name)) Name = FileName;
            }
        }

        [ObservableProperty]
        public partial double Volume { get; set; } = 0;

        [ObservableProperty]
        public partial Guid ModelWeightId { get; set; }

        [ObservableProperty]
        public partial ModelWeight Weight { get; set; } = new(-1, Enums.Unit.Gram);

        [ObservableProperty]
        public partial double PrintTime { get; set; } = 0;

        [ObservableProperty]
        public partial byte[] Image { get; set; } = [];
        #endregion

        #region Constructor
        public File3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not File3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}

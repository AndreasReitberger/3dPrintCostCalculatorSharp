using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.FileAdditions;
using Realms;
using System;
using System.Diagnostics;
using System.IO;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class File3d : RealmObject, IFile3d
    {
        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Ignored]
        public object File { get; set; }

        public string FileName { get; set; } = string.Empty;

        string filePath = string.Empty;
        public string FilePath
        {
            get => filePath;
            set
            {
                filePath = value;
                OnFilePathChanged(value);
            }
        }
        void OnFilePathChanged(string value)
        {
            if (value is not null)
            {
                FileName = new FileInfo(value)?.Name ?? string.Empty;
                if (string.IsNullOrEmpty(Name)) Name = FileName;
            }
        }

        public double Volume { get; set; } = 0;

        public Guid ModelWeightId { get; set; }

        public ModelWeight Weight { get; set; } = new(-1, Enums.Unit.Gram);

        public double PrintTime { get; set; } = 0;

        public int Quantity { get; set; } = 1;

        public bool MultiplyPrintTimeWithQuantity { get; set; } = true;

        public double PrintTimeQuantityFactor { get; set; } = 1;

        public byte[] Image { get; set; } = Array.Empty<byte>();

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

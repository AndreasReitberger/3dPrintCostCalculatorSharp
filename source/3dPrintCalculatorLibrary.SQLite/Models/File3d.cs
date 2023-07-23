using AndreasReitberger.Print3d.SQLite.FileAdditions;
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
        [property: ForeignKey(typeof(Calculation3d))]
        Guid calculationId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore, Ignore]
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
        [property: ManyToOne(nameof(ModelWeightId))]
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

using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AndreasReitberger.Print3d.SQLite.PrinterAdditions
{
    [Table("BuildVolumes")]
    [Obsolete]
    public class BuildVolume : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properies
        [PrimaryKey]
        public Guid Id
        { get; set; }

        [JsonProperty(nameof(X))]
        double _x = 1;
        [JsonIgnore]
        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }

        [JsonProperty(nameof(Y))]
        double _y = 1;
        [JsonIgnore]
        public double Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }

        [JsonProperty(nameof(Z))]
        double _z = 1;
        [JsonIgnore]
        public double Z
        {
            get => _z;
            set
            {
                if (_z != value)
                {
                    _z = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }

        [JsonIgnore]
        public double Volume
        {
            get => Math.Round(X * Y * Z, 2);
        }
        #endregion

        #region Constructors
        public BuildVolume()
        {
            Id = Guid.NewGuid();
        }
        public BuildVolume(Guid id)
        {
            Id = id;
        }
        public BuildVolume(double x, double y, double z)
        {
            Id = Guid.NewGuid();
            X = x;
            Y = y;
            Z = z;
        }
        public BuildVolume(Guid id, double x, double y, double z)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
        }
        #endregion
    }
}

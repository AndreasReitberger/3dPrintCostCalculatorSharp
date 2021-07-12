using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AndreasReitberger.Models.PrinterAdditions
{
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
        double _x = 1;
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
        double _y = 1;
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
        double _z = 1;
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

        public double Volume
        {
            get => Math.Round(X * Y * Z, 2);
        }
        #endregion

        #region Constructors
        public BuildVolume() { }
        public BuildVolume(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        #endregion
    }
}

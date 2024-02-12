using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IPrinter3d
    {
        #region Properties
        public Guid Id { get; set; }
        public string Model { get; set; }
        public Printer3dType Type { get; set; }
        public IManufacturer Manufacturer { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public bool PriceIncludesTax { get; set; }
        public string Uri { get; set; }
        public IList<IPrinter3dAttribute> Attributes { get; set; }
        public Material3dFamily MaterialType { get; set; }
        public double PowerConsumption { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Height { get; set; }
        public IHourlyMachineRate HourlyMachineRate { get; set; }
        public IList<IMaintenance3d> Maintenances { get; set; }
        public IPrinter3dSlicerConfig SlicerConfig { get; set; }
        public byte[] Image { get; set; }
        public string Note { get; set; }
        public string Name { get; }
        public double Volume { get; }
        #endregion

        #region Metohds
        public double CalculateVolume();
        /*
        public List<IPrinter3dAttribute> Attributes { get; set; }
        public ObservableCollection<IMaintenance3d> Maintenances { get; set; }
        */
        #endregion
    }
}

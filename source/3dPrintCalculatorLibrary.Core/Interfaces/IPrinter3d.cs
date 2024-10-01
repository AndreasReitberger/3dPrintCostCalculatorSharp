
using AndreasReitberger.Print3d.Core.Enums;
#if SQL
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IPrinter3d : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Model { get; set; }
        public Printer3dType Type { get; set; }
#if SQL
        public Guid CalculationId { get; set; }
        public Guid CalculationProfileId { get; set; }
        public Guid ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public Guid SlicerId { get; set; }
        public List<Printer3dAttribute> Attributes { get; set; }
        public HourlyMachineRate? HourlyMachineRate { get; set; }
        public List<Maintenance3d> Maintenances { get; set; }
        public Printer3dSlicerConfig? SlicerConfig { get; set; }
#else
        public IManufacturer? Manufacturer { get; set; }
        public IList<IPrinter3dAttribute> Attributes { get; set; }
        public IHourlyMachineRate? HourlyMachineRate { get; set; }
        public IList<IMaintenance3d> Maintenances { get; set; }
        public IPrinter3dSlicerConfig? SlicerConfig { get; set; }
#endif
        public double Price { get; set; }
        public double Tax { get; set; }
        public bool PriceIncludesTax { get; set; }
        public string Uri { get; set; }
        public Material3dFamily MaterialType { get; set; }
        public double PowerConsumption { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Height { get; set; }
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

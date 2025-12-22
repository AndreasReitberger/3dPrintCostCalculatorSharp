#if SQL
using AndreasReitberger.Print3d.SQLite.Events;
using AndreasReitberger.Print3d.SQLite.Settings;

namespace AndreasReitberger.Print3d.SQLite.SourceGenerator
#else
using AndreasReitberger.Print3d.Core.Events;
namespace AndreasReitberger.Print3d.Core.SourceGenerator
#endif
{
#if SQL
    [JsonSerializable(typeof(CalculationChangedDatabaseEventArgs))]
    [JsonSerializable(typeof(Calculation3dEnhancedChangedDatabaseEventArgs))]
    [JsonSerializable(typeof(CustomersChangedDatabaseEventArgs))]
    [JsonSerializable(typeof(DatabaseEventArgs))]
    [JsonSerializable(typeof(DatabaseSettingsKeyValuePair))]
    [JsonSerializable(typeof(FilesChangedDatabaseEventArgs))]
    [JsonSerializable(typeof(HourlyMachineRatesChangedDatabaseEventArgs))]
    [JsonSerializable(typeof(MaterialsChangedDatabaseEventArgs))]
    [JsonSerializable(typeof(PrintersChangedDatabaseEventArgs))]
    [JsonSerializable(typeof(WorkstepsChangedDatabaseEventArgs))]
#endif
    [JsonSerializable(typeof(CalculatorEventArgs))]
    [JsonSerializable(typeof(MaterialChangedEventArgs))]
    [JsonSerializable(typeof(PrinterChangedEventArgs))]
    [JsonSerializable(typeof(Address))]
    [JsonSerializable(typeof(Calculation3d))]
    [JsonSerializable(typeof(Calculation3dEnhanced))]
    [JsonSerializable(typeof(Calculation3dProfile))]
    [JsonSerializable(typeof(CalculationAttribute))]
    [JsonSerializable(typeof(CalculationProcedureAttribute))]
    [JsonSerializable(typeof(CalculationProcedureParameter))]
    [JsonSerializable(typeof(CalculationProcedureParameterAddition))]
    [JsonSerializable(typeof(ContactPerson))]
    [JsonSerializable(typeof(CustomAddition))]
    [JsonSerializable(typeof(Customer3d))]
    [JsonSerializable(typeof(Email))]
    [JsonSerializable(typeof(File3d))]
    [JsonSerializable(typeof(File3dUsage))]
    [JsonSerializable(typeof(File3dWeight))]
    [JsonSerializable(typeof(Gcode))]
    [JsonSerializable(typeof(HourlyMachineRate))]
    [JsonSerializable(typeof(Item3d))]
    [JsonSerializable(typeof(Item3dUsage))]
    [JsonSerializable(typeof(Maintenance3d))]
    [JsonSerializable(typeof(Manufacturer))]
    [JsonSerializable(typeof(Material3d))]
    [JsonSerializable(typeof(Material3dAttribute))]
    [JsonSerializable(typeof(Material3dColor))]
    [JsonSerializable(typeof(Material3dProcedureAttribute))]
    [JsonSerializable(typeof(Material3dType))]
    [JsonSerializable(typeof(Material3dUsage))]
    [JsonSerializable(typeof(PhoneNumber))]
    [JsonSerializable(typeof(Print3dInfo))]
    [JsonSerializable(typeof(Printer3d))]
    [JsonSerializable(typeof(Printer3dAttribute))]
    [JsonSerializable(typeof(Printer3dSlicerConfig))]
    [JsonSerializable(typeof(PrintFileProcessingInfo))]
    [JsonSerializable(typeof(ProcedureAddition))]
    [JsonSerializable(typeof(ProcedureCalculationParameter))]
    [JsonSerializable(typeof(ProcedureSpecificAddition))]
    [JsonSerializable(typeof(Slicer3d))]
    [JsonSerializable(typeof(Slicer3dCommand))]
    [JsonSerializable(typeof(Sparepart))]
    [JsonSerializable(typeof(Storage3d))]
    [JsonSerializable(typeof(Storage3dItem))]
    [JsonSerializable(typeof(Storage3dLocation))]
    [JsonSerializable(typeof(Storage3dTransaction))]
    [JsonSerializable(typeof(Supplier))]
    [JsonSerializable(typeof(Workstep))]
    [JsonSerializable(typeof(WorkstepCategory))]
    [JsonSerializable(typeof(WorkstepUsage))]
    [JsonSerializable(typeof(WorkstepUsageParameter))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {
        /*
        public SourceGenerationContext(JsonSerializerOptions? options) : base(options)
        {

        }
        */
    }
}
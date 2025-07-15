## 3dPrintCostCalculatorSharp
A library to calculate 3d print costs, manager 3d printers & materials and many more!

[![.NET](https://github.com/AndreasReitberger/3dPrintCostCalculatorSharp/actions/workflows/dotnet-unittest.yml/badge.svg)]([https://github.com/AndreasReitberger/3dPrintCostCalculatorSharp/actions/workflows/dotnet-unittest.yml](https://github.com/AndreasReitberger/3dPrintCostCalculatorSharp/actions/workflows/dotnet-unittest.yml))

# Support me
If you want to support me, you can order over following affilate links (I'll get a small share from your purchase from the corresponding store).

- Prusa: http://www.prusa3d.com/#a_aid=AndreasReitberger *
- Jake3D: https://tidd.ly/3x9JOBp * 
- Amazon: https://amzn.to/2Z8PrDu *
- Coinbase: https://advanced.coinbase.com/join/KTKSEBP * (10€ in BTC for you if you open an account)
- TradeRepublic: https://refnocode.trade.re/wfnk80zm * (10€ in stocks for you if you open an account)

(*) Affiliate link
Thank you very much for supporting me!

# Nuget
Get the latest version from nuget.org<br>

| Package                             | Nuget  | Downloads |
| ----------------------------------- |:-----:| -------:|
| 3dPrintCalculatorLibrary | [![NuGet](https://img.shields.io/nuget/v/3dPrintCalculatorLibrary.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/3dPrintCalculatorLibrary) | [![NuGet](https://img.shields.io/nuget/dt/3dPrintCalculatorLibrary.svg)](https://www.nuget.org/packages/3dPrintCalculatorLibrary) |
|  3dPrintCalculatorLibrary.Core | [![NuGet](https://img.shields.io/nuget/v/3dPrintCalculatorLibrary.Core.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/3dPrintCalculatorLibrary.Core) | [![NuGet](https://img.shields.io/nuget/dt/3dPrintCalculatorLibrary.Core.svg)](https://www.nuget.org/packages/3dPrintCalculatorLibrary.Core) |
|  3dPrintCalculatorLibrary.SQLite | [![NuGet](https://img.shields.io/nuget/v/3dPrintCalculatorLibrary.SQLite.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/3dPrintCalculatorLibrary.SQLite) | [![NuGet](https://img.shields.io/nuget/dt/3dPrintCalculatorLibrary.SQLite.svg)](https://www.nuget.org/packages/3dPrintCalculatorLibrary.SQLite) |
|  3dPrintCalculatorLibrary.Realm | [![NuGet](https://img.shields.io/nuget/v/3dPrintCalculatorLibrary.Realm.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/3dPrintCalculatorLibrary.Realm) | [![NuGet](https://img.shields.io/nuget/dt/3dPrintCalculatorLibrary.Realm.svg)](https://www.nuget.org/packages/3dPrintCalculatorLibrary.Realm) |

# Usage
In order to perform a calculation, create a new 3d material and 3d printer.

```csharp
// Create a material
IMaterial3d material = new Material3d()
{
    Id = Guid.NewGuid(),
    Density = 1.24,
    Name = "Test Material",
    Unit = Unit.kg,
    PackageSize = 1,
    UnitPrice = 30,
    TypeOfMaterial = new Material3dType()
    {
        Id = Guid.NewGuid(),
        Material = "PETG",
        Polymer = "",
        Type = Material3dFamily.Filament,
    }
};
// Create a printer
IPrinter3d printer = new Printer3d()
{
    Id = Guid.NewGuid(),
    Manufacturer = new Manufacturer()
    {
        Id = Guid.NewGuid(),
        isActive = true,
        Name = "Prusa"
    },
    Model = "XL MK1",
    Price = 799,
    BuildVolume = new BuildVolume(25, 21, 21),
    MaterialType = Material3dFamily.Filament,
    Type = Printer3dType.FDM,
    PowerConsumption = 210,
};
```

Next, create some 3d files.
```csharp
  List<IFile3d> files = new() {
  new File3d()
  {
      Id = Guid.NewGuid(),
      PrintTime = 10.25,
      Volume = 12.36,
      Quantity = 3,
  },
  new File3d()
  {
      Id = Guid.NewGuid(),
      PrintTime = 10.25,
      Volume = 12.36,
      Quantity = 3,
      MultiplyPrintTimeWithQuantity = false
  },
}
```
Eventually create the Calculation3dEnhanced object and apply your information.
```csharp
ICalculation3dEnhanced _calculation = new Calculation3dEnhanced();
// Add data
_calculation.Files = files;
_calculation.Printers.Add(printer);
_calculation.Materials.Add(material);

// Add information
_calculation.FailRate = 25;
_calculation.EnergyCostsPerkWh = 0.30;
_calculation.ApplyenergyCost = true;
// Uses 75% of the max. power consumption set in the printer model (210 Watt)
_calculation.PowerLevel = 75;

_calculation.Calculate();
```

That's it ;)

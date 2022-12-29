using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Print3d;
using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Models;
using AndreasReitberger.Print3d.Models.CalculationAdditions;
using AndreasReitberger.Print3d.Models.MaterialAdditions;
using AndreasReitberger.Print3d.Models.PrinterAdditions;
using NUnit.Framework;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasReitberger.NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task DatabaseTestsAsync()
        {
            try
            {
                string databasePath = "testdatabase.db";
                DatabaseHandler.Instance = new DatabaseHandler(databasePath);
                if (DatabaseHandler.Instance.IsInitialized)
                {
                    // Clear all tables
                    await DatabaseHandler.Instance.TryClearAllTableAsync();
                    await DatabaseHandler.Instance.TryDropAllTableAsync();

                    // Recreate tables
                    await DatabaseHandler.Instance.RebuildAllTableAsync();

                    // Manufacturers
                    Manufacturer prusa = new Manufacturer()
                    {
                        Name = "Prusa",
                        DebitorNumber = "CZ000001",
                        Website = "https://shop.prusa3d.com/en/#a_aid=AndreasReitberger",
                        IsActive = true,
                    };
                    await DatabaseHandler.Instance.SetManufacturerWithChildrenAsync(prusa);
                    Manufacturer addedManufacturer = await DatabaseHandler.Instance.GetManufacturerWithChildrenAsync(prusa.Id);
                    Assert.IsNotNull(addedManufacturer);
                    Assert.IsTrue(
                        prusa.Name == addedManufacturer.Name &&
                        prusa.DebitorNumber == addedManufacturer.DebitorNumber &&
                        prusa.Website == addedManufacturer.Website
                        );
                    List<Manufacturer> manufacturers = await DatabaseHandler.Instance.GetManufacturersWithChildrenAsync();
                    Assert.IsTrue(manufacturers?.Count > 0);

                    List<Material3dType> materialTypes = new List<Material3dType>()
                    {
                        new Material3dType()
                        {
                            Material = "ABS",
                            Family = Material3dFamily.Filament,
                        },
                        new Material3dType()
                        {
                            Material = "ASA",
                            Family = Material3dFamily.Filament,
                        },
                        new Material3dType()
                        {
                            Material = "PLA",
                            Family = Material3dFamily.Filament,
                        },
                        new Material3dType()
                        {
                            Material = "PETG",
                            Family = Material3dFamily.Filament,
                        },
                        new Material3dType()
                        {
                            Material = "PET",
                            Family = Material3dFamily.Filament,
                        },
                    };
                    await DatabaseHandler.Instance.SetMaterialTypesWithChildrenAsync(materialTypes);
                    List<Material3dType> types = await DatabaseHandler.Instance.GetMaterialTypesWithChildrenAsync();
                    Assert.IsTrue(materialTypes.Count == types?.Count);

                    // Hourly Machine Rate
                    HourlyMachineRate mhr = new HourlyMachineRate()
                    {
                        MachineHours = 200,
                        PerYear = false,
                        EnergyCosts = 50,
                        LocationCosts = 20,
                        MaintenanceCosts = 120,
                    };
                    await DatabaseHandler.Instance.SetHourlyMachineRateWithChildrenAsync(mhr);
                    HourlyMachineRate hourlyMachineRate = await DatabaseHandler.Instance.GetHourlyMachineRateWithChildrenAsync(mhr.Id);
                    List<HourlyMachineRate> hourlyMachineRates = await DatabaseHandler.Instance.GetHourlyMachineRatesWithChildrenAsync();
                    Assert.IsTrue(hourlyMachineRates?.Count > 0);

                    // Printers
                    Printer3d prusaXL = new Printer3d()
                    {
                        Model = "XL",
                        Manufacturer = new Manufacturer()
                        {
                            Name = "Prusa",
                        },
                        Type = Printer3dType.FDM,
                        MaterialType = Material3dFamily.Filament,
                        Uri = "https://www.prusa3d.com/de/produkt/original-prusa-xl-3/#a_aid=AndreasReitberger",
                        Price = 2600,
                        PriceIncludesTax = true,
                        PowerConsumption = 300,
                        HourlyMachineRate = hourlyMachineRate,
                    };
                    await DatabaseHandler.Instance.SetPrinterWithChildrenAsync(prusaXL);
                    Printer3d printer = await DatabaseHandler.Instance.GetPrinterWithChildrenAsync(prusaXL.Id);
                    List<Printer3d> printers = await DatabaseHandler.Instance.GetPrintersWithChildrenAsync();
                    Assert.IsTrue(printers?.Count > 0);

                    // Materials
                    Material3d materialPETG = new Material3d()
                    {
                        Name = "Prusament PETG 1kg",
                        Density = 1.24,
                        Manufacturer = prusa,
                        MaterialFamily = Material3dFamily.Filament,
                        TypeOfMaterial = new Material3dType()
                        {
                            Material = "PETG",
                            Family = Material3dFamily.Filament,
                        },
                        UnitPrice = 30,
                        PriceIncludesTax = true,
                        PackageSize = 1,
                        Unit = Unit.kg,
                        Uri = "https://www.prusa3d.com/product/prusament-petg-anthracite-grey-1kg/#a_aid=AndreasReitberger"
                    };
                    await DatabaseHandler.Instance.SetMaterialWithChildrenAsync(materialPETG);
                    Material3d material = await DatabaseHandler.Instance.GetMaterialWithChildrenAsync(materialPETG.Id);
                    List<Material3d> materials = await DatabaseHandler.Instance.GetMaterialsWithChildrenAsync();
                    Assert.IsTrue(materials?.Count > 0);

                    Calculation3d calculation = new Calculation3d
                    {
                        Printer = printer,
                        Material = material,                       
                    };
                    calculation.Printers.Add(printer);
                    calculation.Materials.Add(material);
                    calculation.Files.Add(new File3d()
                    {
                        FileName = "TestFile",
                        PrintTime = 10.5,
                        Volume = 5.25,
                        Quantity = 3,
                    });
                    calculation.Calculate();

                    await DatabaseHandler.Instance.SetCalculationWithChildrenAsync(calculation);
                    Calculation3d calcFromDB = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    Assert.IsTrue(calculation.TotalCosts == calcFromDB.TotalCosts);

                    List<Calculation3d> calculations = await DatabaseHandler.Instance.GetCalculationsWithChildrenAsync();

                    Calculation3d calculation2 = new Calculation3d
                    {
                        Printer = printer,
                        Material = material,
                    };
                    calculation2.Printers.Add(printer);
                    calculation2.Materials.Add(material);
                    calculation2.Files.Add(new File3d()
                    {
                        FileName = "TestFile",
                        PrintTime = 25.5,
                        Volume = 10.25,
                        Quantity = 30,
                    });
                    calculation2.Calculate();

                    await DatabaseHandler.Instance.SetCalculationWithChildrenAsync(calculation2);
                    Calculation3d calcFromDB2 = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation2.Id);
                    Assert.IsTrue(calculation2.TotalCosts == calcFromDB2.TotalCosts);

                    calcFromDB = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    Assert.IsTrue(calculation.TotalCosts == calcFromDB.TotalCosts);

                    calculations = await DatabaseHandler.Instance.GetCalculationsWithChildrenAsync();

                    // Cleanup
                    await DatabaseHandler.Instance.DeleteCalculationAsync(calculation);
                    calculation = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    Assert.IsNull(calculation);

                    await DatabaseHandler.Instance.DeleteMaterialAsync(materialPETG);
                    material = await DatabaseHandler.Instance.GetMaterialWithChildrenAsync(materialPETG.Id);
                    Assert.IsNull(material);

                    await DatabaseHandler.Instance.DeletePrinterAsync(prusaXL);
                    printer = await DatabaseHandler.Instance.GetPrinterWithChildrenAsync(prusaXL.Id);
                    Assert.IsNull(printer);

                    await DatabaseHandler.Instance.DeleteManufacturerAsync(prusa);
                    addedManufacturer = await DatabaseHandler.Instance.GetManufacturerWithChildrenAsync(prusa.Id);
                    Assert.IsNull(addedManufacturer);
                }
                else
                {
                    Assert.Fail($"Database not initialized: {databasePath}");
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void DatabaseBuilderTest()
        {
            string databasePath = "testdatabase.db";
            using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                .WithDatabasePath(databasePath)
                .WithTable(typeof(Manufacturer))
                .WithTables(new List<Type> { typeof(Material3dType), typeof(Material3d) })
                .Build();

            List<TableMapping> mappings = handler.GetTableMappings();
            Assert.IsTrue(mappings?.Count > 0);
        }

        private Calculation3d _calculation;
        [Test]
        public void TestCalculation()
        {
            try
            {
                Material3d material = new Material3d()
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
                        Family = Material3dFamily.Filament,
                    }
                };
                Printer3d printer = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "XL MK1",
                    Price = 799,
                    BuildVolume = new BuildVolume(25, 21, 21),
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                File3d file = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };

                _calculation = new Calculation3d();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyenergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.Calculate();
                Assert.IsTrue(_calculation.IsCalculated);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }


        [Test]
        public void ExportCalculation()
        {
            try
            {
                Material3d material = new Material3d
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
                        Family = Material3dFamily.Filament,
                    }
                };
                Printer3d printer = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "XL MK1",
                    Price = 799,
                    BuildVolume = new BuildVolume(25, 21, 21),
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                File3d file = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };

                _calculation = new Calculation3d();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyenergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.Calculate();
#if NETFRAMEWORK
                Calculator3dExporter.Save(_calculation, @"mycalc.3dcx");
                Calculator3dExporter.Load(@"mycalc.3dcx", out Calculation3d calculation);
                Assert.IsTrue(calculation != null);
#endif
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void TestCalculationEdit()
        {
            try
            {
                Material3d material = new Material3d()
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
                        Family = Material3dFamily.Filament,
                    }
                };
                Printer3d printer = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "XL MK1",
                    Price = 799,
                    BuildVolume = new BuildVolume(25, 21, 21),
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                Printer3d printerSla = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "SL1 Speed",
                    Price = 1799,
                    BuildVolume = new BuildVolume(25, 21, 21),
                    MaterialType = Material3dFamily.Resin,
                    Type = Printer3dType.SLA,
                    PowerConsumption = 210,
                };
                File3d file = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };

                _calculation = new Calculation3d();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyenergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.ApplyProcedureSpecificAdditions = true;
                // Needed if the calculation is reloaded later
                SerializableDictionary<string, double> additionalInfo = new SerializableDictionary<string, double>
                {
                    { "replacementcosts", 20 },
                    { "wearfactor", 0.5 }
                };

                //List<CalculationProcedureParameter> paramters = new List<CalculationProcedureParameter>();
                List<CalculationProcedureParameter> parameters = new List<CalculationProcedureParameter>
            {
                new CalculationProcedureParameter()
                {
                    Type = ProcedureParameter.NozzleWearCosts,
                    Value = 2,
                    AdditionalInformation = additionalInfo,
                }
            };

                _calculation.ProcedureAttributes.Add(
                    new CalculationProcedureAttribute()
                    {
                        Attribute = ProcedureAttribute.NozzleWear,
                        Family = Material3dFamily.Filament,
                        Parameters = parameters,
                        Level = CalculationLevel.Printer,
                    }
                );

                _calculation.Calculate();

                CalculationAttribute wearCostAttribute = _calculation.OverallPrinterCosts.FirstOrDefault(item =>
                item.Type == CalculationAttributeType.ProcedureSpecificAddition &&
                item.Attribute == "NozzleWearCosts"
                );
                Assert.IsNotNull(wearCostAttribute);

                // Updat calculation
                _calculation.Printer = null;
                _calculation.Printers = new List<Printer3d>
                {
                    printerSla
                };

                _calculation.Calculate();
                wearCostAttribute = _calculation.OverallPrinterCosts.FirstOrDefault(item =>
                item.Type == CalculationAttributeType.ProcedureSpecificAddition &&
                item.Attribute == "NozzleWearCosts"
                );
                Assert.IsNull(wearCostAttribute);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void DatabaseSaveAndLoadTest()
        {
            try
            {
                var calc = GetTestCalculation();
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        Calculation3d GetTestCalculation()
        {
            Material3d material = new Material3d()
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
                    Family = Material3dFamily.Filament,
                }
            };
            Printer3d printer = new Printer3d()
            {
                Id = Guid.NewGuid(),
                Manufacturer = new Manufacturer()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    Name = "Prusa"
                },
                Model = "XL MK1",
                Price = 799,
                MaterialType = Material3dFamily.Filament,
                Type = Printer3dType.FDM,
                PowerConsumption = 210,
            };
            File3d file = new File3d()
            {
                Id = Guid.NewGuid(),
                PrintTime = 10.25,
                Volume = 12.36,
                Quantity = 3,
            };
            File3d file2 = new File3d()
            {
                Id = Guid.NewGuid(),
                PrintTime = 10.25,
                Volume = 12.36,
                Quantity = 3,
                MultiplyPrintTimeWithQuantity = false
            };

            _calculation = new Calculation3d();
            // Add data
            _calculation.Files.Add(file);
            _calculation.Files.Add(file2);
            _calculation.Printers.Add(printer);
            _calculation.Materials.Add(material);

            // Add information
            _calculation.FailRate = 25;
            _calculation.EnergyCostsPerkWh = 0.30;
            _calculation.ApplyenergyCost = true;
            // Uses 75% of the max. power consumption set in the printer model (210 Watt)
            _calculation.PowerLevel = 75;

            _calculation.Calculate();
            return _calculation;
        }
      
        public void MultiFileDifferTest()
        {
            try
            {
                Material3d material = new Material3d()
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
                        Family = Material3dFamily.Filament,
                    }
                };
                Material3d material2 = new Material3d()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.14,
                    Name = "Test Material #2",
                    Unit = Unit.kg,
                    PackageSize = 1,
                    UnitPrice = 25,
                    TypeOfMaterial = new Material3dType()
                    {
                        Id = Guid.NewGuid(),
                        Material = "PLA",
                        Polymer = "",
                        Family = Material3dFamily.Filament,
                    }
                };
                Printer3d printer = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "XL MK1",
                    Price = 799,
                    BuildVolume = new BuildVolume(25, 21, 21),
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                File3d file = new File3d()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MyFirst.gcode",
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MySecond.gcode",
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };
                File3d file3 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MyThird.gcode",
                    PrintTime = 2.25,
                    Volume = 3.36,
                    Quantity = 25,
                    MultiplyPrintTimeWithQuantity = true
                };

                _calculation = new Calculation3d();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Files.Add(file3);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);
                _calculation.Materials.Add(material2);

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyenergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.Calculate();

                double total = _calculation.GetTotalCosts();

                var materialCosts = PrintCalculator3d.GetMaterialCosts(_calculation);
                var machineCosts = PrintCalculator3d.GetMachineCosts(_calculation);

                _calculation.DifferFileCosts = true;
                _calculation.Calculate();

                double totalDiffer = _calculation.GetTotalCosts();

                Assert.IsTrue(_calculation.IsCalculated);
                Assert.IsTrue(total == totalDiffer);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
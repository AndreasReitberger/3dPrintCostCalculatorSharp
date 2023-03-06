using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Realm;
using AndreasReitberger.Print3d.Realm.CalculationAdditions;
using AndreasReitberger.Print3d.Realm.MaterialAdditions;
using AndreasReitberger.Print3d.Realm.StorageAdditions;
using NUnit.Framework;
using Realms;
using SQLite;

namespace AndreasReitberger.NUnitTest
{
    public class TestsRealm
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
                string databasePath = "testdatabase.realm";

                // https://www.mongodb.com/docs/realm/sdk/dotnet/quick-start/
                var config = new RealmConfiguration(databasePath);
                // Start with a clear database
                if (File.Exists(config.DatabasePath))
                {
                    File.Delete(config.DatabasePath);
                }
                using var realm = await Realm.GetInstanceAsync(config);
                if (!realm.IsClosed)
                {
                    realm.Write(() => realm.RemoveAll());
                    // Manufacturers
                    Manufacturer prusa = new()
                    {
                        Name = "Prusa",
                        DebitorNumber = "CZ000001",
                        Website = "https://shop.prusa3d.com/en/#a_aid=AndreasReitberger",
                        IsActive = true,
                    };
                    realm.Write(() => realm.Add(prusa));
                    Manufacturer addedManufacturer = realm.Find<Manufacturer>(prusa.Id);
                    Assert.IsNotNull(addedManufacturer);
                    Assert.IsTrue(
                        prusa.Name == addedManufacturer.Name &&
                        prusa.DebitorNumber == addedManufacturer.DebitorNumber &&
                        prusa.Website == addedManufacturer.Website
                        );

                    List<Manufacturer> manufacturers = realm.All<Manufacturer>().ToList();
                    Assert.IsTrue(manufacturers?.Count > 0);

                    List<Material3dType> materialTypes = new()
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

                    List<Material3dType> types = realm.All<Material3dType>().ToList();
                    realm.Write(() => realm.Add(materialTypes));
                    types = realm.All<Material3dType>().ToList();
                    Assert.IsTrue(materialTypes.Count == types?.Count);

                    // Hourly Machine Rate
                    HourlyMachineRate mhr = new()
                    {
                        MachineHours = 200,
                        PerYear = false,
                        EnergyCosts = 50,
                        LocationCosts = 20,
                        MaintenanceCosts = 120,
                    };
                    realm.Write(() => realm.Add(mhr));
                    HourlyMachineRate hourlyMachineRate = realm.Find<HourlyMachineRate>(mhr.Id);
                    List<HourlyMachineRate> hourlyMachineRates = realm.All<HourlyMachineRate>().ToList();
                    Assert.IsTrue(hourlyMachineRates?.Count > 0);

                    // Printers
                    Printer3d prusaXL = new()
                    {
                        Model = "XL",
                        Manufacturer = new()
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
                    realm.Write(() => realm.Add(prusaXL));
                    Printer3d printer = realm.Find<Printer3d>(prusaXL.Id);
                    List<Printer3d> printers = realm.All<Printer3d>().ToList();
                    Assert.IsTrue(printers?.Count > 0);

                    // Materials
                    Material3d materialPETG = new()
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
                        Unit = Unit.Kilogramm,
                        Uri = "https://www.prusa3d.com/product/prusament-petg-anthracite-grey-1kg/#a_aid=AndreasReitberger"
                    };
                    realm.Write(() => realm.Add(materialPETG));
                    Material3d material = realm.Find<Material3d>(materialPETG.Id);
                    List<Material3d> materials = realm.All<Material3d>().ToList();
                    Assert.IsTrue(materials?.Count > 0);

                    // Additional items
                    Item3d item = new()
                    {
                        Name = "70x4mm Screws",
                        SKU = "SC2565263189",
                        PackagePrice = 30,
                        PackageSize = 100,
                    };
                    Item3d item2 = new()
                    {
                        Name = "50x3mm Screws",
                        SKU = "SC23425435345",
                        PackagePrice = 35,
                        PackageSize = 200,
                    };

                    realm.Write(() => realm.Add(new List<Item3d>() { item, item2 }));
                    List<Item3d> items = realm.All<Item3d>().ToList();
                    Assert.IsTrue(items?.Count == 2);

                    List<Item3dUsage> usages = new(items.Select(curItem => new Item3dUsage() { Item = curItem, Quantity = 10 }));
                    realm.Write(() => realm.Add(usages));

                    Calculation3d calculation = new()
                    {
                        Printer = printer,
                        Material = material,
                        //AdditionalItems = usages,
                    };
                    usages.ForEach(usage => calculation.AdditionalItems.Add(usage));
                    calculation.Printers.Add(printer);
                    calculation.Materials.Add(material);
                    calculation.Files.Add(new File3d()
                    {
                        FileName = "TestFile",
                        PrintTime = 10.5,
                        Volume = 5.25,
                        Quantity = 3,
                    });
                    calculation.AdditionalItems.Add(new Item3dUsage()
                    {
                        Item = item,
                        LinkedToFile = true,
                        File = calculation.Files.First()
                    });

                    calculation.CalculateCosts();

                    realm.Write(() => realm.Add(calculation));
                    Calculation3d calcFromDB = realm.Find<Calculation3d>(calculation.Id);
                    realm.Write(() => calcFromDB.CalculateCosts());
                    Assert.IsTrue(calculation.TotalCosts == calcFromDB.TotalCosts);

                    List<Calculation3d> calculations = realm.All<Calculation3d>().ToList();

                    Calculation3d calculation2 = new()
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
                    calculation2.CalculateCosts();

                    realm.Write(() => realm.Add(calculation2));
                    Calculation3d calcFromDB2 = realm.Find<Calculation3d>(calculation2.Id);
                    Assert.IsTrue(calculation2.TotalCosts == calcFromDB2.TotalCosts);

                    calcFromDB = realm.Find<Calculation3d>(calculation.Id);
                    Assert.IsTrue(calculation.TotalCosts == calcFromDB.TotalCosts);

                    calculations = realm.All<Calculation3d>().ToList();

                    // Cleanup
                    realm.Write(() =>
                    {
                        realm.Remove(calculation);
                        //calculation = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                        //Assert.IsNull(calculation);

                        realm.Remove(materialPETG);
                        //material = await DatabaseHandler.Instance.GetMaterialWithChildrenAsync(materialPETG.Id);
                        //Assert.IsNull(material);

                        realm.Remove(prusaXL);
                        //printer = await DatabaseHandler.Instance.GetPrinterWithChildrenAsync(prusaXL.Id);
                        //Assert.IsNull(printer);

                        realm.Remove(prusa);
                        //addedManufacturer = await DatabaseHandler.Instance.GetManufacturerWithChildrenAsync(prusa.Id);
                        //Assert.IsNull(addedManufacturer);
                    });
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

        private Calculation3d _calculation;
        [Test]
        public void TestCalculation()
        {
            try
            {
                Material3d material = new()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.24,
                    Name = "Test Material",
                    Unit = Unit.Kilogramm,
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
                Printer3d printer = new()
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
                    Width = 25,
                    Height = 21,
                    Depth = 21,
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                File3d file = new()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };

                _calculation = new();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);
                _calculation.Rates.Add(new()
                {
                    Type = CalculationAttributeType.Tax,
                    IsPercentageValue = true,
                    Value = 19,
                });
                _calculation.Rates.Add(new()
                {
                    Type = CalculationAttributeType.Margin,
                    IsPercentageValue = true,
                    Value = 100,
                });

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.CalculateCosts();
                double totalCosts = _calculation.TotalCosts;
                Assert.IsTrue(_calculation.IsCalculated);

                List<double> costsCalc = new()
                {
                    _calculation.MachineCosts,
                    _calculation.MaterialCosts,
                    _calculation.CalculatedMargin,
                    _calculation.CalculatedTax,
                };
                double summedCalc = costsCalc.Sum();
                Assert.IsTrue(Math.Round(summedCalc, 2) == Math.Round(_calculation.TotalCosts, 2));


                Calculation3d _calculation2 = _calculation?.Clone() as Calculation3d;
                _calculation2.CalculateCosts();
                Assert.IsTrue(_calculation2.IsCalculated);
                Assert.IsTrue(totalCosts == _calculation2.TotalCosts);

                costsCalc = new()
                {
                    _calculation2.MachineCosts,
                    _calculation2.MaterialCosts,
                    _calculation2.CalculatedMargin,
                    _calculation2.CalculatedTax,
                };
                summedCalc = costsCalc.Sum();
                Assert.IsTrue(Math.Round(summedCalc, 2) == Math.Round(_calculation2.TotalCosts, 2));

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
                    Unit = Unit.Kilogramm,
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
                    Height = 25,
                    Width = 21,
                    Depth = 21,
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
                _calculation.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.CalculateCosts();
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
                    Unit = Unit.Kilogramm,
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
                _calculation.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.ApplyProcedureSpecificAdditions = true;
                // Needed if the calculation is reloaded later
                List<CalculationProcedureParameterAddition> additionalInfo = new()
                {
                    new CalculationProcedureParameterAddition("replacementcosts", 20),
                    new CalculationProcedureParameterAddition( "wearfactor", 0.5 ),
                };
                var nozzleWear = new CalculationProcedureParameter()
                {
                    Type = ProcedureParameter.NozzleWearCosts,
                    Value = 2,
                    //Additions = additionalInfo,
                };
                nozzleWear.AddRange(additionalInfo);
                List<CalculationProcedureParameter> parameters = new()
                {
                    nozzleWear
                };
                var attributes = new CalculationProcedureAttribute()
                {
                    Attribute = ProcedureAttribute.NozzleWear,
                    Family = Material3dFamily.Filament,
                    //Parameters = parameters,
                    Level = CalculationLevel.Printer,
                };
                attributes.AddRange(parameters);
                _calculation.ProcedureAttributes.Add(attributes);

                _calculation.CalculateCosts();

                CalculationAttribute wearCostAttribute = _calculation.OverallPrinterCosts.FirstOrDefault(item =>
                item.LinkedId == _calculation.Printer.Id &&
                item.Type == CalculationAttributeType.ProcedureSpecificAddition &&
                item.Attribute == "NozzleWearCosts"
                );
                Assert.IsNotNull(wearCostAttribute);

                // Updat calculation
                _calculation.Printer = null;
                _calculation.Printers.Add(printerSla);
                _calculation.Printer = _calculation.Printers[^1];

                _calculation.CalculateCosts();
                wearCostAttribute = _calculation.OverallPrinterCosts.FirstOrDefault(item =>
                item.LinkedId == _calculation.Printer.Id &&
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

        public void MultiFileDifferTest()
        {
            try
            {
                Material3d material = new Material3d()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.24,
                    Name = "Test Material",
                    Unit = Unit.Kilogramm,
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
                    Unit = Unit.Kilogramm,
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
                _calculation.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.CalculateCosts();

                double total = _calculation.GetTotalCosts();

                var materialCosts = PrintCalculator3d.GetMaterialCosts(_calculation);
                var machineCosts = PrintCalculator3d.GetMachineCosts(_calculation);

                _calculation.DifferFileCosts = true;
                _calculation.CalculateCosts();

                double totalDiffer = _calculation.GetTotalCosts();

                Assert.IsTrue(_calculation.IsCalculated);
                Assert.IsTrue(total == totalDiffer);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task StorageDatabaseTest()
        {
            try
            {
                string databasePath = "testdatabase.realm";
                // https://www.mongodb.com/docs/realm/sdk/dotnet/quick-start/
                var config = new RealmConfiguration(databasePath);
                // Start with a clear database
                if (File.Exists(config.DatabasePath))
                {
                    File.Delete(config.DatabasePath);
                }
                using var realm = await Realm.GetInstanceAsync(config);
                if (!realm.IsClosed)
                {
                    realm.Write(() => realm.RemoveAll());
                    double startAmount = 2.68;
                    Material3d material = new()
                    {
                        Name = "Test",
                        SKU = "Some material number",
                        PackageSize = 1,
                        Unit = Unit.Kilogramm,
                        UnitPrice = 29.99,
                        PriceIncludesTax = true,
                    };
                    realm.Write(() => realm.Add(material));

                    Storage3dItem item = new()
                    {
                        Material = material,
                        Amount = startAmount,
                    };
                    realm.Write(() => realm.Add(item));
                    Storage3d storage = new()
                    {
                        Name = "Main material storage",
                    };
                    storage.Items.Add(item);
                    realm.Write(() =>
                    {
                        realm.Add(storage);
                        storage.AddToStock(material, 750, Unit.Gramm);
                        var newItem = storage.Items.FirstOrDefault(curItem => curItem.Material.Id == material.Id);
                        // Check if the addition was successfully
                        Assert.IsTrue(newItem?.Amount == startAmount + 0.75);

                        // Just to check if the unit conversion is working
                        storage.TakeFromStock(material, 0.001, Unit.MetricTons, false);
                        newItem = storage.Items.FirstOrDefault(curItem => curItem.Material.Id == material.Id);
                        // Check if the addition was successfully
                        Assert.IsTrue(newItem?.Amount == startAmount + 0.75 - 1);
                    });
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
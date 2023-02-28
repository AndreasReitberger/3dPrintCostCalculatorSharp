using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite;
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.StorageAdditions;
using NUnit.Framework;
using SQLite;

namespace AndreasReitberger.NUnitTest
{
    public class TestsSqlite
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
                    Manufacturer prusa = new()
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
                    await DatabaseHandler.Instance.SetMaterialTypesWithChildrenAsync(materialTypes);
                    List<Material3dType> types = await DatabaseHandler.Instance.GetMaterialTypesWithChildrenAsync();
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
                    await DatabaseHandler.Instance.SetHourlyMachineRateWithChildrenAsync(mhr);
                    HourlyMachineRate hourlyMachineRate = await DatabaseHandler.Instance.GetHourlyMachineRateWithChildrenAsync(mhr.Id);
                    List<HourlyMachineRate> hourlyMachineRates = await DatabaseHandler.Instance.GetHourlyMachineRatesWithChildrenAsync();
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
                    await DatabaseHandler.Instance.SetPrinterWithChildrenAsync(prusaXL);
                    Printer3d printer = await DatabaseHandler.Instance.GetPrinterWithChildrenAsync(prusaXL.Id);
                    List<Printer3d> printers = await DatabaseHandler.Instance.GetPrintersWithChildrenAsync();
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
                    await DatabaseHandler.Instance.SetMaterialWithChildrenAsync(materialPETG);
                    Material3d material = await DatabaseHandler.Instance.GetMaterialWithChildrenAsync(materialPETG.Id);
                    List<Material3d> materials = await DatabaseHandler.Instance.GetMaterialsWithChildrenAsync();
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

                    await DatabaseHandler.Instance.SetItemsWithChildrenAsync(new() { item, item2 });
                    var items = await DatabaseHandler.Instance.GetItemsWithChildrenAsync();
                    Assert.IsTrue(items?.Count == 2);

                    List<Item3dUsage> usages = new(items.Select(curItem => new Item3dUsage() { Item = curItem, Quantity = 10 }));
                    await DatabaseHandler.Instance.SetItemUsagesWithChildrenAsync(usages);

                    Calculation3d calculation = new()
                    {
                        Printer = printer,
                        Material = material,
                        AdditionalItems = usages,
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
                    calculation.AdditionalItems.Add(new Item3dUsage()
                    {
                        Item = item,
                        LinkedToFile = true,
                        File = calculation.Files.First()
                    });

                    calculation.CalculateCosts();

                    await DatabaseHandler.Instance.SetCalculationWithChildrenAsync(calculation);
                    Calculation3d calcFromDB = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    Assert.IsTrue(calculation.TotalCosts == calcFromDB.TotalCosts);

                    List<Calculation3d> calculations = await DatabaseHandler.Instance.GetCalculationsWithChildrenAsync();

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

                    await DatabaseHandler.Instance.SetCalculationWithChildrenAsync(calculation2);
                    Calculation3d calcFromDB2 = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation2.Id);
                    Assert.IsTrue(calculation2.TotalCosts == calcFromDB2.TotalCosts);

                    calcFromDB = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    Assert.IsTrue(calculation.TotalCosts == calcFromDB.TotalCosts);

                    calculations = await DatabaseHandler.Instance.GetCalculationsWithChildrenAsync();

                    // Cleanup
                    await DatabaseHandler.Instance.DeleteCalculationAsync(calculation);
                    //calculation = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    //Assert.IsNull(calculation);

                    await DatabaseHandler.Instance.DeleteMaterialAsync(materialPETG);
                    //material = await DatabaseHandler.Instance.GetMaterialWithChildrenAsync(materialPETG.Id);
                    //Assert.IsNull(material);

                    await DatabaseHandler.Instance.DeletePrinterAsync(prusaXL);
                    //printer = await DatabaseHandler.Instance.GetPrinterWithChildrenAsync(prusaXL.Id);
                    //Assert.IsNull(printer);

                    await DatabaseHandler.Instance.DeleteManufacturerAsync(prusa);
                    //addedManufacturer = await DatabaseHandler.Instance.GetManufacturerWithChildrenAsync(prusa.Id);
                    //Assert.IsNull(addedManufacturer);
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

                _calculation.Calculate();
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

                //List<CalculationProcedureParameter> paramters = new List<CalculationProcedureParameter>();
                List<CalculationProcedureParameter> parameters = new List<CalculationProcedureParameter>
                {
                    new CalculationProcedureParameter()
                    {
                        Type = ProcedureParameter.NozzleWearCosts,
                        Value = 2,
                        Additions = additionalInfo,
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

                _calculation.CalculateCosts();

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

                _calculation.CalculateCosts();
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
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        Calculation3d GetTestCalculation()
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
                string databasePath = "testdatabase.db";
                DatabaseHandler.Instance = new DatabaseHandler(databasePath);
                if (DatabaseHandler.Instance.IsInitialized)
                {
                    // Clear all tables
                    await DatabaseHandler.Instance.TryClearAllTableAsync();
                    await DatabaseHandler.Instance.TryDropAllTableAsync();

                    // Recreate tables
                    await DatabaseHandler.Instance.RebuildAllTableAsync();

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
                    await DatabaseHandler.Instance.SetMaterialWithChildrenAsync(material);

                    Storage3dItem item = new()
                    {
                        Material = material,
                        Amount = startAmount,
                    };
                    await DatabaseHandler.Instance.SetStorageItemWithChildrenAsync(item);
                    Storage3d storage = new()
                    {
                        Name = "Main material storage",
                        Items = new() { item },
                    };
                    await DatabaseHandler.Instance.SetStorageWithChildrenAsync(storage);

                    storage.AddToStock(material, 750, Unit.Gramm);
                    var newItem = storage.Items.FirstOrDefault(curItem => curItem.Material == material);
                    // Check if the addition was successfully
                    Assert.IsTrue(newItem?.Amount == startAmount + 0.75);

                    // Just to check if the unit conversion is working
                    storage.TakeFromStock(material, 0.001, Unit.MetricTons, false);
                    newItem = storage.Items.FirstOrDefault(curItem => curItem.Material == material);
                    // Check if the addition was successfully
                    Assert.IsTrue(newItem?.Amount == startAmount + 0.75 - 1);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void TakeCalculationAmountFromStorageTest()
        {
            try
            {
                Calculation3d calculation = GetTestCalculation();
                Assert.IsNotNull(calculation);

                foreach (var material in calculation.Materials)
                {
                    Storage3dItem item = new()
                    {
                        Material = material,
                    };
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message, exc);
            }
        }
    }
}
using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite;
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.ProcedureAdditions;
using AndreasReitberger.Print3d.SQLite.StorageAdditions;
using NUnit.Framework;
using SQLite;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;

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
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
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
                        Unit = Unit.Kilogram,
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
                    List<Item3d>? items = await DatabaseHandler.Instance.GetItemsWithChildrenAsync();
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
                    Unit = Unit.Kilogram,
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

                Item3d item = new()
                {
                    Name = "Nuts M3",
                    PackageSize = 100,
                    PackagePrice = 9.99d,
                    Manufacturer = new()
                    {
                        Name = "Würth"
                    },
                    SKU = "2302423-1223"
                };
                Item3dUsage usage = new()
                {
                    Item = item,
                    Quantity = 30,
                    LinkedToFile = false,
                };

                _calculation = new();
                // Add data
                _calculation.AdditionalItems.Add(usage);
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
                    _calculation.ItemsCost,
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
                    _calculation2.ItemsCost,
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
                    Unit = Unit.Kilogram,
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
                    Unit = Unit.Kilogram,
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
        public async Task Item3dTests()
        {
            try
            {
                string databasePath = "testdatabase_items.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTables(new List<Type> {
                        typeof(Manufacturer),
                        typeof(Item3d),
                        typeof(Item3dUsage),
                    })
                    .Build();

                Manufacturer wuerth = new()
                {
                    Name = "Würth",
                    DebitorNumber = "DE26265126",
                    Website = "https://www.wuerth.de/",
                };
                await handler.SetManufacturerWithChildrenAsync(wuerth);

                Item3d item1 = new()
                {
                    Name = "Nuts M3",
                    PackageSize = 100,
                    PackagePrice = 9.99d,
                    Manufacturer = wuerth,
                    SKU = "2302423-1223"
                };
                Item3d item2 = new()
                {
                    Name = "Screws M3 20mm",
                    PackageSize = 50,
                    PackagePrice = 14.99d,
                    Manufacturer = wuerth,
                    SKU = "2302423-6413"
                };
                await handler.SetItemsWithChildrenAsync(new() { item1, item2 });

                var loadedItem1 = await handler.GetItemWithChildrenAsync(item1.Id);
                Assert.IsNotNull(loadedItem1);
                Assert.IsNotNull(loadedItem1?.Manufacturer);

                var loadedItems = await handler.GetItemsWithChildrenAsync();
                Assert.IsTrue(loadedItems?.Count == 2);


                Item3dUsage usage = new()
                {
                    Item = item1,
                    Quantity = 30,
                    LinkedToFile = false,
                };
                await handler.SetItemUsageWithChildrenAsync(usage);

                Item3dUsage loadedUsage = await handler.GetItemUsageWithChildrenAsync(usage.Id);
                Assert.IsNotNull(loadedUsage);
                Assert.IsNotNull(loadedUsage.Item);

                var manufacturerLoaded = loadedUsage?.Item?.Manufacturer;
                Assert.IsNotNull(manufacturerLoaded);

                var usages = await handler.GetItemUsagesWithChildrenAsync();
                Assert.IsTrue(usages?.Count == 1);

                await handler.DeleteItemUsageAsync(loadedUsage);

                usages = await handler.GetItemUsagesWithChildrenAsync();
                Assert.IsTrue(usages?.Count == 0);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task WorkstepTests()
        {
            try
            {
                string databasePath = "testdatabase_worksteps.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTables(new List<Type> {
                        typeof(Workstep),
                        typeof(WorkstepCategory),
                        typeof(WorkstepUsage),
                        typeof(WorkstepUsageParameter),
                    })
                    .Build();
                WorkstepCategory category = new()
                {
                    Name = "Construction"
                };
                await handler.SetWorkstepCategoryWithChildrenAsync(category);

                Workstep ws = new()
                {
                    Name = "3D CAD",
                    Category = category,
                    Price = 30,
                };
                await handler.SetWorkstepWithChildrenAsync(ws);

                WorkstepUsage usage = new()
                {
                    Workstep = ws,
                    UsageParameter = new()
                    {
                        ParameterType = WorkstepUsageParameterType.Duration,
                        Value = 1.5d
                    }
                };
                await handler.SetWorkstepUsageWithChildrenAsync(usage);


                WorkstepUsage loadedUsage = await handler.GetWorkstepUsageWithChildrenAsync(usage.Id);
                Assert.IsNotNull(loadedUsage);
                Assert.IsNotNull(loadedUsage?.UsageParameter);
                Assert.IsNotNull(loadedUsage?.Workstep);

                var usages = await handler.GetWorkstepUsagesWithChildrenAsync();
                Assert.IsTrue(usages?.Count == 1);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task DatabaseSaveAndLoadTest()
        {
            try
            {
                string databasePath = "testdatabase.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithDefaultTables()
                    .Build();

                await Task.Delay(250);

                Calculation3d calc = GetTestCalculation();
                calc.Material = calc.Materials.First();
                calc.Printer = calc.Printers.First();

                await calc.CalculateCostsAsync();

                await handler.SetItemUsagesWithChildrenAsync(calc.AdditionalItems);
                await handler.SetProcedureAdditionsWithChildrenAsync(calc.ProcedureAdditions.ToList());
                await handler.SetMaterialsWithChildrenAsync(calc.Materials);
                await handler.SetPrintersWithChildrenAsync(calc.Printers);
                await handler.SetCalculationWithChildrenAsync(calc);
                Calculation3d calc2 = await handler.GetCalculationWithChildrenAsync(calc.Id);

                await Task.Delay(250);
                Assert.IsNotNull(calc2);

                var item = await handler.GetItemWithChildrenAsync(calc.AdditionalItems.FirstOrDefault().ItemId);
                calc2.Material = calc.Materials.First();
                calc2.Printer = calc.Printers.First();

                await calc2.CalculateCostsAsync();
                await Task.Delay(250);
                if (calc2 is not null)
                {
                    Assert.IsTrue(calc.MachineCosts == calc2.MachineCosts, "Machine costs differ");
                    Assert.IsTrue(calc.MaterialCosts == calc2.MaterialCosts, "Material costs differ");
                    Assert.IsTrue(calc.CalculatedMargin == calc2.CalculatedMargin, "Margin differs");
                    Assert.IsTrue(calc.CalculatedTax == calc2.CalculatedTax, "Tax differs");
                    Assert.IsTrue(calc.EnergyCosts == calc2.EnergyCosts, "Energy costs differ");
                    Assert.IsTrue(calc.CustomAdditionCosts == calc2.CustomAdditionCosts, "Custom addition costs differ");
                    Assert.IsTrue(calc.ItemsCost == calc2.ItemsCost, "Item costs differ");
                }
                Assert.True(calc?.TotalCosts == calc2?.TotalCosts, "Total costs differ");
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
                Unit = Unit.Kilogram,
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
            Material3d material2 = new()
            {
                Id = Guid.NewGuid(),
                Density = 1.24,
                Name = "Test Material",
                Unit = Unit.Liters,
                PackageSize = 1,
                UnitPrice = 59,
                TypeOfMaterial = new Material3dType()
                {
                    Id = Guid.NewGuid(),
                    Material = "Tough",
                    Polymer = "",
                    Family = Material3dFamily.Resin,
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
            Printer3d printer2 = new()
            {
                Id = Guid.NewGuid(),
                Manufacturer = new Manufacturer()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    Name = "Prusa"
                },
                Model = "SL1",
                Price = 1299,
                MaterialType = Material3dFamily.Resin,
                Type = Printer3dType.SLA,
                PowerConsumption = 164,
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

            Item3d item = new()
            {
                Name = "Nuts M3",
                PackageSize = 100,
                PackagePrice = 9.99d,
                Manufacturer = new()
                {
                    Name = "Würth"
                },
                SKU = "2302423-1223"
            };
            Item3dUsage usage = new()
            {
                Item = item,
                Quantity = 30,
                LinkedToFile = false,
            };

            Print3dInfo info = new()
            {

            };

            _calculation = new Calculation3d()
            {
                Name = "My awesome calculation"
            };
            // Add data
            _calculation.AdditionalItems.Add(usage);
            _calculation.Files.Add(file);
            _calculation.Files.Add(file2);
            _calculation.Printers.Add(printer);
            _calculation.Printers.Add(printer2);
            _calculation.Materials.Add(material);
            _calculation.Materials.Add(material2);
            _calculation.ProcedureAdditions.Add(new ProcedureAddition()
            {
                Name = "Resin Tank Replacement",
                Description = "Take the costs for the resin tank replacement into account?",
                Enabled = true,
                Target = ProcedureAdditionTarget.Machine,
                TargetFamily = Material3dFamily.Resin,
                Parameters = new()
                {
                    new ProcedureCalculationParameter()
                    {
                        Name = "Tank replacement costs",
                        Type = ProcedureCalculationType.ReplacementCosts,
                        Price = 50,
                        WearFactor = 1000,
                        QuantityInPackage = 1,
                    }
                }
            });
            _calculation.ProcedureAdditions.Add(new ProcedureAddition()
            {
                Name = "Gloves",
                Description = "Take the costs for the gloves?",
                Enabled = true,
                Target = ProcedureAdditionTarget.General,
                TargetFamily = Material3dFamily.Resin,
                Parameters = new()
                {
                    new ProcedureCalculationParameter()
                    {
                        Name = "Gloves costs",
                        Type = ProcedureCalculationType.ConsumableGoods,
                        Price = 50,
                        AmountTakenForCalculation = 2,
                        QuantityInPackage = 100,
                    }
                }
            });

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
                Material3d material = new()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.24,
                    Name = "Test Material",
                    Unit = Unit.Kilogram,
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
                Material3d material2 = new()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.14,
                    Name = "Test Material #2",
                    Unit = Unit.Kilogram,
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
                    FileName = "MyFirst.gcode",
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MySecond.gcode",
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };
                File3d file3 = new()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MyThird.gcode",
                    PrintTime = 2.25,
                    Volume = 3.36,
                    Quantity = 25,
                    MultiplyPrintTimeWithQuantity = true
                };

                Item3d item = new()
                {
                    Name = "Nuts M3",
                    PackageSize = 100,
                    PackagePrice = 9.99d,
                    Manufacturer = new()
                    {
                        Name = "Würth"
                    },
                    SKU = "2302423-1223"
                };
                Item3dUsage usage = new()
                {
                    Item = item,
                    Quantity = 30,
                    LinkedToFile = false,
                };

                _calculation = new Calculation3d();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Files.Add(file3);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);
                _calculation.Materials.Add(material2);

                _calculation.AdditionalItems.Add(usage);

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
                string databasePath = "testdatabase_storage.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                DatabaseHandler.Instance = new DatabaseHandler(databasePath);
                if (DatabaseHandler.Instance.IsInitialized)
                {
                    // Clear all tables
                    await DatabaseHandler.Instance.TryClearAllTableAsync();
                    await DatabaseHandler.Instance.TryDropAllTableAsync();

                    // Recreate tables
                    await DatabaseHandler.Instance.RebuildAllTableAsync();
                    await Task.Delay(250);

                    double startAmount = 2.68;
                    Material3d material = new()
                    {
                        Name = "Test",
                        SKU = "Some material number",
                        PackageSize = 1,
                        Unit = Unit.Kilogram,
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

                    storage.AddToStock(material, 750, Unit.Gram);
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


        [Test]
        public void ProcedureSpecificAdditionsTest()
        {
            try
            {
                // Hardware replacement costs
                ProcedureAddition resinTank = new()
                {
                    Name = "Resin Tank Replacement",
                    Description = "Take the costs for the resin tank replacement into account?",
                    Enabled = true,
                    Target = ProcedureAdditionTarget.Machine,
                    TargetFamily = Material3dFamily.Resin,
                    Parameters = new()
                    {
                        new ProcedureCalculationParameter()
                        {
                            Name = "Tank replacement costs",
                            Type = ProcedureCalculationType.ReplacementCosts,
                            Price = 50,
                            WearFactor = 1,
                            QuantityInPackage = 1,
                        }
                    }
                };
                double resinWearCosts = resinTank.CalculateCosts();
                Assert.IsTrue(resinWearCosts == 0.5d);
                // Consumable goods (like filters and gloves)
                ProcedureAddition gloves = new()
                {
                    Name = "Gloves",
                    Description = "Take the costs for the gloves?",
                    Enabled = true,
                    Target = ProcedureAdditionTarget.General,
                    TargetFamily = Material3dFamily.Resin,
                    Parameters = new()
                    {
                        new ProcedureCalculationParameter()
                        {
                            Name = "Gloves costs",
                            Type = ProcedureCalculationType.ConsumableGoods,
                            Price = 50,
                            AmountTakenForCalculation = 2,
                            QuantityInPackage = 100,
                        }
                    }
                };
                double glovesCosts = gloves.CalculateCosts();
                Assert.IsTrue(glovesCosts == 1d);

                var calculation = GetTestCalculation();
                calculation.Procedure = Material3dFamily.Resin;
                calculation.ApplyProcedureSpecificAdditions = true;
                calculation.CalculateCosts();

                Assert.NotNull(calculation.OverallPrinterCosts?.FirstOrDefault(cost => cost.Attribute == "Resin Tank Replacement"));
                Assert.NotNull(calculation.Costs?.FirstOrDefault(cost => cost.Attribute == "Gloves"));
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task PrintInfoCalculationTestAsync()
        {
            try
            {
                string databasePath = "testdatabase_info.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                DatabaseHandler.Instance = new DatabaseHandler(databasePath);
                if (DatabaseHandler.Instance.IsInitialized)
                {
                    // Clear all tables
                    await DatabaseHandler.Instance.TryClearAllTableAsync();
                    await DatabaseHandler.Instance.TryDropAllTableAsync();

                    // Recreate tables
                    await DatabaseHandler.Instance.RebuildAllTableAsync();
                    await Task.Delay(250);

                    Material3d material = new()
                    {
                        Name = "Test",
                        SKU = "Some material number",
                        PackageSize = 1,
                        Unit = Unit.Kilogram,
                        UnitPrice = 29.99,
                        PriceIncludesTax = true,
                    };
                    await DatabaseHandler.Instance.SetMaterialWithChildrenAsync(material);

                    File3d file = new()
                    {
                        Name = "My cool file",
                        Volume = 251.54,
                        PrintTime = 2.34,
                        Quantity = 1,
                    };
                    await DatabaseHandler.Instance.SetFileWithChildrenAsync(file);

                    Print3dInfo info = new()
                    {
                        File = file,
                        Material = material,
                    };
                    await DatabaseHandler.Instance.SetPrintInfoWithChildrenAsync(info);

                    Calculation3d calc = new()
                    {

                    };
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
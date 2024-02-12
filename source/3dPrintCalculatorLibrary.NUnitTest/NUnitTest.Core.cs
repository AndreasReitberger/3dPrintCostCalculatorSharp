using AndreasReitberger.Print3d.Core;
using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using NUnit.Framework;

namespace AndreasReitberger.NUnitTest
{
    public class TestsCore
    {
        ICalculation3dEnhanced? calculation;

        [SetUp]
        public void Setup()
        {
            calculation = new Calculation3dEnhanced()
            {
                Name = "Test",
                Customer = new Customer3d()
                {
                    Name = "Max",
                    LastName = "Mustermann",
                    IsCompany = false,
                    EmailAddresses =
                    [
                        new Email()
                        {
                            EmailAddress = "max.mustermann@example.com"
                        },
                    ],
                    Addresses = [
                        new Address()
                        {
                            City = "Muserstadt",
                            Zip = "93426",
                            CountryCode = "de",
                            Street = "Musterstraße 4",
                        }
                    ],
                },
                PrintInfos = [
                    // FDM job
                    new Print3dInfo()
                    {
                        Name = "My awesome print job",
                        File = new File3d()
                        {
                            FileName = "my.gcode",
                            PrintTime = 5.263,
                            Volume = 35.54,
                            Quantity = 5,
                        },
                        Materials = [
                            new Material3dUsage()
                            {
                                Material = new Material3d()
                                {
                                    Name = "Prusament PETG 1kg JetBlack",
                                    Density = 1.24,
                                    ColorCode = "#000000",
                                    MaterialFamily = Material3dFamily.Filament,
                                    TypeOfMaterial = new Material3dType() { Family = Material3dFamily.Filament, Material = "PETG" },
                                    PackageSize = 1,
                                    Unit = Unit.Kilogram,
                                    UnitPrice = 30,
                                    Manufacturer = new Manufacturer()
                                    {
                                        Name = "Prusa",
                                    }
                                },
                                Percentage = 0.5,
                            },
                            new Material3dUsage()
                            {
                                Material = new Material3d()
                                {
                                    Name = "Prusament PLA 1kg JetBlack",
                                    Density = 1.24,
                                    ColorCode = "#000000",
                                    MaterialFamily = Material3dFamily.Filament,
                                    TypeOfMaterial = new Material3dType() { Family = Material3dFamily.Filament, Material = "PLA" },
                                    PackageSize = 1,
                                    Unit = Unit.Kilogram,
                                    UnitPrice = 25,
                                    Manufacturer = new Manufacturer()
                                    {
                                        Name = "Prusa",
                                    }
                                },
                                Percentage = 0.5,
                            }
                        ],
                        Printer = new Printer3d()
                        {
                            Type = Printer3dType.FDM,
                            Model = "i3 MK3S",
                            PowerConsumption = 210,
                            Price = 899,

                        },
                        Items = [
                            new Item3dUsage()
                            {
                                  Item = new Item3d()
                                  {
                                      Name = "M3x16 Screws",
                                      PackagePrice = 20,
                                      PackageSize = 100,
                                  }, 
                                  Quantity = 10,
                            },
                        ]
                    },
                    // Resing job
                    new Print3dInfo()
                    {
                        Name = "My first resin print job",
                        File = new File3d()
                        {
                            FileName = "Batman.dlp",
                            PrintTime = 2.65,
                            Volume = 65.546,
                            Quantity = 2,
                        },
                        Materials = [
                            new Material3dUsage()
                            {
                                Material = new Material3d()
                                {
                                    Name = "Anycubic Tough Resin",
                                    Density = 1.11,
                                    ColorCode = "#000000",
                                    MaterialFamily = Material3dFamily.Resin,
                                    TypeOfMaterial = new Material3dType() { Family = Material3dFamily.Resin, Material = "TOUGH" },
                                    PackageSize = 1,
                                    Unit = Unit.Liters,
                                    UnitPrice = 65,
                                    Manufacturer = new Manufacturer()
                                    {
                                        Name = "Anycubic",
                                    }
                                },
                                Percentage = 1,
                            }
                        ],
                        Printer = new Printer3d()
                        {
                            Type = Printer3dType.LENS,
                            Model = "Photon X",
                            PowerConsumption = 160,
                            Price = 599,

                        }
                    }
                ]
            };
        }

        

        [Test]
        public void Item3dTests()
        {
            try
            {
                IManufacturer wuerth = new Manufacturer()
                {
                    Name = "Würth",
                    DebitorNumber = "DE26265126",
                    Website = "https://www.wuerth.de/",
                };

                IItem3d item1 = new Item3d()
                {
                    Name = "Nuts M3",
                    PackageSize = 100,
                    PackagePrice = 9.99d,
                    Manufacturer = wuerth,
                    SKU = "2302423-1223"
                };
                IItem3d item2 = new Item3d()
                {
                    Name = "Screws M3 20mm",
                    PackageSize = 50,
                    PackagePrice = 14.99d,
                    Manufacturer = wuerth,
                    SKU = "2302423-6413"
                };

                Assert.That(item1 is not null);
                Assert.That(item2 is not null);
                Assert.That(item1?.Manufacturer is not null);
                Assert.That(item2?.Manufacturer is not null);

                IItem3dUsage usage = new Item3dUsage()
                {
                    Item = item1,
                    Quantity = 30,
                    LinkedToFile = false,
                };
                Assert.That(usage is not null);
                Assert.That(usage.Item is not null);

                IManufacturer? manufacturerLoaded = usage?.Item?.Manufacturer;
                Assert.That(manufacturerLoaded is not null);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void WorkstepTests()
        {
            try
            {
                IWorkstepCategory category = new WorkstepCategory()
                {
                    Name = "Construction"
                };
                IWorkstep ws = new Workstep()
                {
                    Name = "3D CAD",
                    Category = category,
                    Price = 30,
                };
                WorkstepUsage usage = new()
                {
                    Workstep = ws,
                    UsageParameter = new WorkstepUsageParameter()
                    {
                        ParameterType = WorkstepUsageParameterType.Duration,
                        Value = 1.5d
                    }
                };

                Assert.That(usage is not null);
                Assert.That(usage?.UsageParameter is not null);
                Assert.That(usage?.Workstep is not null);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void StorageTest()
        {
            try
            {
                double startAmount = 2.68;
                IMaterial3d material = new Material3d()
                {
                    Name = "Test",
                    SKU = "Some material number",
                    PackageSize = 1,
                    Unit = Unit.Kilogram,
                    UnitPrice = 29.99,
                    PriceIncludesTax = true,
                };
                IStorage3dItem item = new Storage3dItem()
                {
                    Material = material,
                    Amount = startAmount,
                };

                IStorage3dLocation location = new Storage3dLocation()
                {
                    Location = "LR-01-01-01",
                    Capacity = 32,
                };
                location.Items.Add(item);
                IStorage3d storage = new Storage3d()
                {
                    Name = "Main material storage",
                };
                storage.Locations.Add(location);

                location.AddToStock(material, 750, Unit.Gram);
                var newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                // Check if the addition was successfully
                Assert.That(newItem?.Amount == startAmount + 0.75);

                // Just to check if the unit conversion is working
                location.TakeFromStock(material, 0.001, Unit.MetricTons, false);
                newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                // Check if the addition was successfully
                Assert.That(newItem?.Amount == startAmount + 0.75 - 1);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void ProcedureSpecificAdditionsTest()
        {
            try
            {
                // Hardware replacement costs
                IProcedureAddition resinTank = new ProcedureAddition()
                {
                    Name = "Resin Tank Replacement",
                    Description = "Take the costs for the resin tank replacement into account?",
                    Enabled = true,
                    Target = ProcedureAdditionTarget.Machine,
                    TargetFamily = Material3dFamily.Resin,
                    Parameters =
                    [
                        new ProcedureCalculationParameter()
                        {
                            Name = "Tank replacement costs",
                            Type = ProcedureCalculationType.ReplacementCosts,
                            Price = 50,
                            WearFactor = 1,
                            QuantityInPackage = 1,
                        }
                    ]
                };
                double resinWearCosts = resinTank.CalculateCosts();
                Assert.That(resinWearCosts == 0.5d);
                // Consumable goods (like filters and gloves)
                IProcedureAddition gloves = new ProcedureAddition()
                {
                    Name = "Gloves",
                    Description = "Take the costs for the gloves?",
                    Enabled = true,
                    Target = ProcedureAdditionTarget.General,
                    TargetFamily = Material3dFamily.Resin,
                    Parameters =
                    [
                        new ProcedureCalculationParameter()
                        {
                            Name = "Gloves costs",
                            Type = ProcedureCalculationType.ConsumableGoods,
                            Price = 50,
                            AmountTakenForCalculation = 2,
                            QuantityInPackage = 100,
                        }
                    ]
                };
                double glovesCosts = gloves.CalculateCosts();
                Assert.That(glovesCosts == 1d);

                calculation.Procedure = Material3dFamily.Resin;
                calculation.ProcedureAdditions.Add(resinTank);
                calculation.ProcedureAdditions.Add(gloves);
                calculation.ApplyProcedureSpecificAdditions = true;
                calculation.CalculateCosts();

                // Todo: Check why the procedure specific stuff is not added
                Assert.That(calculation.OverallPrinterCosts?.FirstOrDefault(cost => cost.Attribute == "Resin Tank Replacement") is not null);
                Assert.That(calculation.Costs?.FirstOrDefault(cost => cost.Attribute == "Gloves") is not null);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void PrintInfoCalculationTest()
        {
            try
            {
                if (true)
                {
                    IManufacturer manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    };

                    IMaterial3d material = new Material3d()
                    {
                        Name = "Test",
                        SKU = "Some material number",
                        Manufacturer = manufacturer,
                        PackageSize = 1,
                        Unit = Unit.Kilogram,
                        UnitPrice = 29.99,
                        PriceIncludesTax = true,
                    };

                    IMaterial3d material2 = new Material3d()
                    {
                        Name = "Test 2",
                        SKU = "Some other material number",
                        Manufacturer = manufacturer,
                        PackageSize = 1,
                        Unit = Unit.Kilogram,
                        UnitPrice = 59.99,
                        PriceIncludesTax = true,
                    };

                    IHourlyMachineRate hmr = new HourlyMachineRate()
                    {
                        ReplacementCosts = 799,
                        MachineHours = 160,
                        PerYear = false,
                        EnergyCosts = 20,
                    };

                    IPrinter3d printer = new Printer3d()
                    {
                        Id = Guid.NewGuid(),
                        Manufacturer = manufacturer,
                        Model = "XL MK1",
                        Price = 799,
                        MaterialType = Material3dFamily.Filament,
                        Type = Printer3dType.FDM,
                        PowerConsumption = 210,
                        HourlyMachineRate = hmr,
                    };

                    IPrinter3d printer2 = new Printer3d()
                    {
                        Id = Guid.NewGuid(),
                        Manufacturer = manufacturer,
                        Model = "XL - Dual Head",
                        Price = 2499,
                        MaterialType = Material3dFamily.Filament,
                        Type = Printer3dType.FDM,
                        PowerConsumption = 400,
                        HourlyMachineRate = hmr,
                    };

                    IFile3d file = new File3d()
                    {
                        Name = "My cool file",
                        Volume = 251.54,
                        PrintTime = 2.34,
                        Quantity = 1,
                    };
                    IFile3d file2 = new File3d()
                    {
                        Name = "Another cool file",
                        Volume = 23.64,
                        PrintTime = 0.55,
                        Quantity = 5,
                    };

                    IManufacturer wuerth = new Manufacturer()
                    {
                        Name = "Würth",
                        DebitorNumber = "DE26265126",
                        Website = "https://www.wuerth.de/",
                    };

                    IItem3d item = new Item3d()
                    {
                        Name = "Nuts M3",
                        PackageSize = 100,
                        PackagePrice = 9.99d,
                        Manufacturer = wuerth,
                        SKU = "2302423-1223"
                    };
                    IItem3dUsage usage = new Item3dUsage()
                    {
                        Item = item,
                        Quantity = 5,
                    };

                    IPrint3dInfo info = new Print3dInfo()
                    {
                        File = file,
                        Materials = [new Material3dUsage() { Material = material, Percentage = 1 }],
                        Printer = printer,
                        Items = [usage],
                    };
                    IPrint3dInfo info2 = new Print3dInfo()
                    {
                        File = file2,
                        // Multi-Material for one file
                        Materials = [new Material3dUsage() { Material = material, Percentage = 0.5 }, new Material3dUsage() { Material = material2, Percentage = 0.5 }],
                        Printer = printer2,
                    };

                    ICalculation3dEnhanced calc = new Calculation3dEnhanced()
                    {
                        Name = "Test Calculation",
                        PrintInfos = [info, info2],
                    };

                    calc.CalculateCosts();
                    double total = calc.GetTotalCosts();
                    Assert.That(total > 0);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
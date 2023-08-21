using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.CustomerAdditions;
using AndreasReitberger.Print3d.SQLite.Events;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite.StorageAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using SQLiteNetExtensionsAsync.Extensions;

namespace AndreasReitberger.Print3d.SQLite
{
    public partial class DatabaseHandler
    {
        #region Methods

        #region Public

        #region Manufacturers
        public async Task<List<Manufacturer>> GetManufacturersWithChildrenAsync()
        {
            return await DatabaseAsync
                .GetAllWithChildrenAsync<Manufacturer>(recursive: true)
                ;
        }

        public async Task<Manufacturer> GetManufacturerWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Manufacturer>(id, recursive: true)
                ;
        }

        public async Task SetManufacturerWithChildrenAsync(Manufacturer manufacturer)
        {
            await DatabaseAsync
                .InsertOrReplaceWithChildrenAsync(manufacturer, recursive: true)
                ;
            //OnDatabaseOpertions?.Invoke(typeof(Manufacturer));
        }

        public async Task SetManufacturersWithChildrenAsync(List<Manufacturer> manufacturers, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(manufacturers);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(manufacturers);
        }

        public async Task<int> DeleteManufacturerAsync(Manufacturer manufacturer)
        {
            return await DatabaseAsync.DeleteAsync<Manufacturer>(manufacturer?.Id);
        }

        #endregion

        #region Suppliers
        public async Task<List<Supplier>> GetSuppliersWithChildrenAsync()
        {
            return await DatabaseAsync?
                .GetAllWithChildrenAsync<Supplier>(recursive: true)
                ;
        }

        public async Task<Supplier> GetSupplierWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<Supplier>(id, recursive: true)
                ;
        }

        public async Task SetSupplierWithChildrenAsync(Supplier supplier)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(supplier, recursive: true)
                ;
        }

        public async Task SetSuppliersWithChildrenAsync(List<Supplier> suppliers, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(suppliers);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(suppliers);
        }

        public async Task<int> DeleteSupplierAsync(Supplier supplier)
        {
            return await DatabaseAsync?.DeleteAsync<Supplier>(supplier?.Id);
        }

        #endregion

        #region MaterialTypes
        public async Task<List<Material3dType>> GetMaterialTypesWithChildrenAsync()
        {
            return await DatabaseAsync?
                .GetAllWithChildrenAsync<Material3dType>(recursive: true);
        }

        public List<Material3dType> GetMaterialTypes()
        {
            return Database?.Table<Material3dType>().ToList();
        }

        public async Task<Material3dType> GetMaterialTypeWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<Material3dType>(id, recursive: true)
                ;
        }

        public Material3dType GetMaterialType(Guid id)
        {
            return Database?.Table<Material3dType>()
                            .Where(i => i.Id == id)
                            .FirstOrDefault();
        }

        public async Task SetMaterialTypeWithChildrenAsync(Material3dType materialType)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(materialType, recursive: true)
                ;
        }

        public int? SetMaterialType(Material3dType materialType)
        {
            Material3dType item = GetMaterialType(materialType.Id);
            return item != null ?
                Database?.Update(materialType) :
                Database?.Insert(materialType);
        }

        public async Task SetMaterialTypesWithChildrenAsync(List<Material3dType> materialTypes, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(materialTypes);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(materialTypes);
        }

        public async Task<int> DeleteMaterialTypeAsync(Material3dType materialType)
        {
            return await DatabaseAsync?.DeleteAsync<Material3dType>(materialType?.Id);
        }

        public int? DeleteMaterialType(Material3dType materialType)
        {
            return Database?.Delete<Material3dType>(materialType?.Id);
        }

        #endregion

        #region Materials
        public async Task<List<Material3d>> GetMaterialsWithChildrenAsync()
        {
            // To trigger event
            Materials = await DatabaseAsync
                .GetAllWithChildrenAsync<Material3d>(recursive: true)
                ;
            return Materials;
        }

        public async Task<Material3d> GetMaterialWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Material3d>(id, recursive: true)
                ;
        }

        public async Task SetMaterialWithChildrenAsync(Material3d material)
        {
            await DatabaseAsync
                .InsertOrReplaceWithChildrenAsync(material, recursive: true)
                ;
        }

        public async Task SetMaterialsWithChildrenAsync(List<Material3d> materials, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(materials);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(materials);
        }

        public async Task<int> DeleteMaterialAsync(Material3d material)
        {
            return await DatabaseAsync.DeleteAsync<Material3d>(material?.Id);
        }

        #endregion

        #region Printers
        public async Task<List<Printer3d>> GetPrintersWithChildrenAsync()
        {
            Printers = await DatabaseAsync
                .GetAllWithChildrenAsync<Printer3d>(recursive: true)
                ;
            return Printers;
        }

        public async Task<Printer3d> GetPrinterWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Printer3d>(id, recursive: true)
                ;
        }

        public async Task SetPrintersWithChildrenAsync(List<Printer3d> printers, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(printers);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(printers);
        }

        public async Task SetPrinterWithChildrenAsync(Printer3d printer)
        {
            if (printer?.SlicerConfig != null)
            {
                await SetSlicerConfigWithChildrenAsync(printer.SlicerConfig).ConfigureAwait(false);
            }
            await DatabaseAsync
                .InsertOrReplaceWithChildrenAsync(printer, recursive: true)
                ;
        }

        public async Task<int> DeletePrinterAsync(Printer3d printer)
        {
            return await DatabaseAsync.DeleteAsync<Printer3d>(printer?.Id);
        }

        #endregion

        #region Maintenance
        public async Task<List<Maintenance3d>> GetMaintenancesWithChildrenAsync()
        {
            return await DatabaseAsync?
                .GetAllWithChildrenAsync<Maintenance3d>(recursive: true)
                ;
        }

        public async Task<Maintenance3d> GetMaintenanceWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<Maintenance3d>(id, recursive: true)
                ;
        }

        public async Task SetMaintenanceWithChildrenAsync(Maintenance3d maintenance)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(maintenance, recursive: true)
                ;
        }

        public async Task SetMaintenancesWithChildrenAsync(List<Maintenance3d> maintenances, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(maintenances);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(maintenances);
        }

        public async Task<int> DeleteMaintenanceAsync(Maintenance3d maintenance)
        {
            return await DatabaseAsync?.DeleteAsync<Maintenance3d>(maintenance.Id);
        }

        public async Task<int[]> DeleteMaintenancesAsync(List<Maintenance3d> maintenances)
        {
            Stack<int> results = new();
            for (int i = 0; i < maintenances?.Count; i++)
            {
                results.Push(await DatabaseAsync?.DeleteAsync<Maintenance3d>(maintenances[i]?.Id));
            }
            return results.ToArray();
        }

        #endregion

        #region Customers
        public async Task<List<Customer3d>> GetCustomersWithChildrenAsync()
        {
            Customers = await DatabaseAsync?
                .GetAllWithChildrenAsync<Customer3d>(recursive: true)
                ;
            return Customers;
        }

        public async Task<Customer3d> GetCustomerWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<Customer3d>(id, recursive: true)
                ;
        }

        // https://stackoverflow.com/questions/35975235/one-to-many-relationship-in-sqlite-xamarin
        public async Task SetCustomerWithChildrenAsync(Customer3d customer)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(customer, recursive: true)
                ;
        }

        public async Task SetCustomersWithChildrenAsync(List<Customer3d> customers, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(customers);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(customers);
        }

        public async Task<int> DeleteCustomerAsync(Customer3d customer)
        {
            return await DatabaseAsync?
                .DeleteAsync<Customer3d>(customer.Id);
        }

        public async Task<int[]> DeleteCustomersAsync(List<Customer3d> customers)
        {
            Stack<int> results = new();
            for (int i = 0; i < customers?.Count; i++)
            {
                results.Push(await DatabaseAsync?.DeleteAsync<Customer3d>(customers[i]?.Id));
            }
            return results.ToArray();
        }

        #endregion

        #region Files
        public async Task<List<File3d>> GetFilesWithChildrenAsync()
        {
            Files = await DatabaseAsync
                .GetAllWithChildrenAsync<File3d>(recursive: true)
                ;
            return Files;
        }

        public async Task<File3d> GetFileWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<File3d>(id, recursive: true)
                ;
        }

        // https://stackoverflow.com/questions/35975235/one-to-many-relationship-in-sqlite-xamarin
        public async Task SetFileWithChildrenAsync(File3d file)
        {
            await DatabaseAsync
                .InsertOrReplaceWithChildrenAsync(file, recursive: true)
                ;
        }

        public async Task SetFilesWithChildrenAsync(List<File3d> files, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(files);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(files);
        }

        public async Task<int> DeleteFileAsync(File3d file)
        {
            return await DatabaseAsync
                .DeleteAsync<File3d>(file.Id);
        }

        public async Task<int[]> DeleteFilesAsync(List<File3d> files)
        {
            Stack<int> results = new();
            for (int i = 0; i < files?.Count; i++)
            {
                results.Push(await DatabaseAsync.DeleteAsync<File3d>(files[i]?.Id));
            }
            return results.ToArray();
        }

        #endregion

        #region Addresses
        public async Task<List<Address>> GetAddressesWithChildrenAsync()
        {
            return await DatabaseAsync?
                .GetAllWithChildrenAsync<Address>(recursive: true)
                ;
        }

        public async Task<Address> GetAddressWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<Address>(id, recursive: true)
                ;
        }

        public async Task SetAddressWithChildrenAsync(Address address)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(address, recursive: true)
                ;
        }

        public async Task SetAddressesWithChildrenAsync(List<Address> addresses, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(addresses);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(addresses);
        }

        public async Task<int> DeleteAddressAsync(Address address)
        {
            return await DatabaseAsync?.DeleteAsync<Address>(address?.Id);
        }

        public async Task<int[]> DeleteAddressesAsync(List<Address> addresses)
        {
            Stack<int> results = new();
            for (int i = 0; i < addresses?.Count; i++)
            {
                int rowId = await DatabaseAsync?.DeleteAsync<Address>(addresses[i]?.Id);
                results.Push(rowId);
            }
            return results.ToArray();
        }

        public async Task<int> DeleteAllAddressesAsync()
        {
            return await DatabaseAsync?.DeleteAllAsync<Address>(); ;
        }

        #endregion

        #region Calculations
        public async Task<List<Calculation3d>> GetCalculationsWithChildrenAsync()
        {
            List<Calculation3d> calculations = await DatabaseAsync?
                .GetAllWithChildrenAsync<Calculation3d>(recursive: true)
                ;
            // Workaround, because the foreign keys from the printer / materials are not loaded somehow...
            /**/
            for (int i = 0; i < calculations?.Count; i++)
            {
                /**/
                Calculation3d calculation = calculations[i];
                for (int j = 0; j < calculation.Printers?.Count; j++)
                {
                    Printer3d printer = calculation.Printers[j];
                    calculation.Printers[j] = await GetPrinterWithChildrenAsync(printer.Id);
                }
                calculations[i].Printer =
                    calculations[i].Printers.FirstOrDefault(item => item.Id == calculations[i].Printer?.Id) ??
                    calculations[i].Printers.FirstOrDefault();

                for (int j = 0; j < calculation.Materials?.Count; j++)
                {
                    Material3d material = calculation.Materials[j];
                    calculation.Materials[j] = await GetMaterialWithChildrenAsync(material.Id);
                }
                calculations[i].Material =
                    calculations[i].Materials.FirstOrDefault(item => item.Id == calculations[i].Material?.Id) ??
                    calculations[i].Materials.FirstOrDefault();

                calculations[i]?.CalculateCosts();
            }
            return calculations;
        }

        public async Task<Calculation3d> GetCalculationWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<Calculation3d>(id, recursive: true)
                ;
        }

        public async Task RerfreshCalculationsAsync()
        {
            Calculations = await GetCalculationsWithChildrenAsync();
            OnCalculationsChangedEvent(new CalculationChangedDatabaseEventArgs()
            {
                Calculations = Calculations,
            });
        }

        public async Task SetCalculationWithChildrenAsync(Calculation3d calculation, bool updateList = true)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(calculation, recursive: true)
                ;
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
        }

        public async Task SetCalculationsWithChildrenAsync(List<Calculation3d> calculations, bool replaceExisting = true, bool updateList = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(calculations);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(calculations);
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
        }

        public async Task<int> DeleteCalculationAsync(Calculation3d calculation, bool updateList = true)
        {
            return await DatabaseAsync?.DeleteAsync<Calculation3d>(calculation?.Id);
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
        }

        public async Task<int[]> DeleteCalculationsAsync(List<Calculation3d> calculations, bool updateList = true)
        {
            Stack<int> results = new();
            for (int i = 0; i < calculations?.Count; i++)
            {
                int rowId = await DatabaseAsync?.DeleteAsync<Calculation3d>(calculations[i]?.Id);
                results.Push(rowId);
            }
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
            return results.ToArray();
        }

        public async Task<int> DeleteAllCalculationsAsync(bool updateList = true)
        {
            return await DatabaseAsync?.DeleteAllAsync<Calculation3d>();
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
        }

        #endregion

        #region CalculationProfiles
        public async Task<List<Calculation3dProfile>> GetCalculationProfilesWithChildrenAsync()
        {
            return await DatabaseAsync?
                .GetAllWithChildrenAsync<Calculation3dProfile>(recursive: true)
                ;
        }

        public async Task<Calculation3dProfile> GetCalculationProfileWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<Calculation3dProfile>(id, recursive: true)
                ;
        }

        public async Task SetCalculationProfileWithChildrenAsync(Calculation3dProfile profile)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(profile, recursive: true)
                ;
        }

        public async Task SetCalculationProfilesWithChildrenAsync(List<Calculation3dProfile> profiles, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(profiles);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(profiles);
        }

        public async Task<int> DeleteCalculationProfileAsync(Calculation3dProfile profile)
        {
            return await DatabaseAsync?
                .DeleteAsync<Calculation3dProfile>(profile?.Id)
                ;
        }

        public async Task<int[]> DeleteCalculationProfilesAsync(List<Calculation3dProfile> profiles)
        {
            Stack<int> results = new();
            for (int i = 0; i < profiles?.Count; i++)
            {
                int rowId = await DatabaseAsync?.DeleteAsync<Calculation3dProfile>(profiles[i]?.Id);
                results.Push(rowId);
            }
            return results.ToArray();
        }

        public async Task<int> DeleteAllCalculationProfilesAsync()
        {
            return await DatabaseAsync?
                .DeleteAllAsync<Calculation3dProfile>()
                ;
        }

        #endregion

        #region Worksteps
        public async Task<List<Workstep>> GetWorkstepsWithChildrenAsync()
        {
            Worksteps = await DatabaseAsync?
                .GetAllWithChildrenAsync<Workstep>(recursive: true)
                ;
            return Worksteps;
        }

        public async Task<Workstep> GetWorkstepWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<Workstep>(id, recursive: true)
                ;
        }

        public async Task SetWorkstepWithChildrenAsync(Workstep workstep)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(workstep, recursive: true)
                ;
        }

        public async Task SetMWorkstepsWithChildrenAsync(List<Workstep> worksteps, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(worksteps);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(worksteps);
        }

        public async Task<int> DeleteWorkstepAsync(Workstep workstep)
        {
            return await DatabaseAsync?.DeleteAsync<Workstep>(workstep?.Id);
        }

        #endregion

        #region Workstep Categories
        public async Task<List<WorkstepCategory>> GetWorkstepCategoriesWithChildrenAsync()
        {
            return await DatabaseAsync?
                .GetAllWithChildrenAsync<WorkstepCategory>(recursive: true);
        }

        public async Task<WorkstepCategory> GetWorkstepCategoryWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<WorkstepCategory>(id, recursive: true)
                ;
        }
        public async Task SetWorkstepCategoryWithChildrenAsync(WorkstepCategory workstepCategory)
        {
            await DatabaseAsync?
                .InsertOrReplaceWithChildrenAsync(workstepCategory, recursive: true)
                ;
        }

        public async Task SetWorkstepCategoriesWithChildrenAsync(List<WorkstepCategory> categories, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(categories);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(categories);
        }

        public async Task<int> DeleteWorkstepCategoryAsync(WorkstepCategory workstepCategory)
        {
            return await DatabaseAsync?.DeleteAsync<WorkstepCategory>(workstepCategory?.Id);
        }

        #endregion

        #region Hourly Machine Rates

        public async Task<List<HourlyMachineRate>> GetHourlyMachineRatesWithChildrenAsync()
        {
            HourlyMachineRates = await DatabaseAsync?
                .GetAllWithChildrenAsync<HourlyMachineRate>(recursive: true);
            return HourlyMachineRates;
        }

        public async Task<HourlyMachineRate> GetHourlyMachineRateWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync?
                .GetWithChildrenAsync<HourlyMachineRate>(id, recursive: true);
        }

        public async Task SetHourlyMachineRateWithChildrenAsync(HourlyMachineRate hourlyMachineRate, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceWithChildrenAsync(hourlyMachineRate, recursive: true);
            else
                await DatabaseAsync?.InsertWithChildrenAsync(hourlyMachineRate);
        }

        public async Task SetHourlyMachineRatesWithChildrenAsync(List<HourlyMachineRate> hourlyMachineRates, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync?.InsertOrReplaceAllWithChildrenAsync(hourlyMachineRates);
            else
                await DatabaseAsync?.InsertAllWithChildrenAsync(hourlyMachineRates);
        }

        public async Task<int> DeleteHourlyMachineRateAsync(HourlyMachineRate hourlyMachineRate)
        {
            return await DatabaseAsync?.DeleteAsync<HourlyMachineRate>(hourlyMachineRate?.Id);
        }

        #endregion

        #region Custom Addition

        public async Task<List<CustomAddition>> GetCustomAdditionsWithChildrenAsync()
        {
            return await DatabaseAsync
                .GetAllWithChildrenAsync<CustomAddition>(recursive: true)
                ;
        }

        public async Task<CustomAddition> GetCustomAdditionWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<CustomAddition>(id, recursive: true)
                ;
        }

        public async Task SetCustomAdditionWithChildrenAsync(CustomAddition customAddition)
        {
            await DatabaseAsync
                .InsertOrReplaceWithChildrenAsync(customAddition, recursive: true);
        }

        public async Task SetCustomAdditionsWithChildrenAsync(List<CustomAddition> items, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(items);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(items);
        }

        public async Task<int> DeleteCustomAdditionAsync(CustomAddition customAddition)
        {
            return await DatabaseAsync.DeleteAsync<CustomAddition>(customAddition?.Id);
        }

        #endregion

        #region Items
        public async Task<List<Item3d>> GetItemsWithChildrenAsync()
        {
            return await DatabaseAsync
                .GetAllWithChildrenAsync<Item3d>(recursive: true)
                ;
        }

        public async Task<Item3d> GetItemWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Item3d>(id, recursive: true)
                ;
        }

        public async Task SetItemWithChildrenAsync(Item3d item)
        {
            await DatabaseAsync.InsertOrReplaceWithChildrenAsync(item, recursive: true);
        }

        public async Task SetItemsWithChildrenAsync(List<Item3d> items, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(items);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(items);
        }

        public async Task<int> DeleteItemAsync(Item3d item)
        {
            return await DatabaseAsync.DeleteAsync<CustomAddition>(item?.Id);
        }

        #endregion

        #region Item Usages
        public async Task<List<Item3dUsage>> GetItemUsagesWithChildrenAsync()
        {
            return await DatabaseAsync
                .GetAllWithChildrenAsync<Item3dUsage>(recursive: true)
                ;
        }

        public async Task<Item3dUsage> GetItemUsageWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Item3dUsage>(id, recursive: true)
                ;
        }

        public async Task SetItemUsageWithChildrenAsync(Item3dUsage item)
        {
            await DatabaseAsync.InsertOrReplaceWithChildrenAsync(item, recursive: true);
        }

        public async Task SetItemUsagesWithChildrenAsync(List<Item3dUsage> items, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(items);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(items);
        }

        public async Task<int> DeleteItemUsageAsync(Item3dUsage itemUsage)
        {
            return await DatabaseAsync.DeleteAsync<Item3dUsage>(itemUsage?.Id);
        }

        #endregion

        #region Storage Items
        public async Task<List<Storage3dItem>> GetStorageItemsWithChildrenAsync()
        {
            return await DatabaseAsync
                .GetAllWithChildrenAsync<Storage3dItem>(recursive: true)
                ;
        }

        public async Task<Storage3dItem> GetStorageItemWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Storage3dItem>(id, recursive: true)
                ;
        }

        public async Task SetStorageItemWithChildrenAsync(Storage3dItem item)
        {
            await DatabaseAsync.InsertOrReplaceWithChildrenAsync(item, recursive: true);
        }

        public async Task SetStorageItemsWithChildrenAsync(List<Storage3dItem> items, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(items);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(items);
        }

        public async Task<int> DeleteStorageItemAsync(Storage3dItem item)
        {
            return await DatabaseAsync.DeleteAsync<Storage3dItem>(item?.Id);
        }

        #endregion

        #region Storage Transaction
        public async Task<List<Storage3dTransaction>> GetStorageTransactionsWithChildrenAsync()
        {
            return await DatabaseAsync
                .GetAllWithChildrenAsync<Storage3dTransaction>(recursive: true)
                ;
        }

        public async Task<Storage3dTransaction> GetStorageTransactionWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Storage3dTransaction>(id, recursive: true)
                ;
        }

        public async Task SetStorageTransactionWithChildrenAsync(Storage3dTransaction transaction)
        {
            await DatabaseAsync.InsertOrReplaceWithChildrenAsync(transaction, recursive: true);
        }

        public async Task SetStorageTransactionsWithChildrenAsync(List<Storage3dTransaction> transactions, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(transactions);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(transactions);
        }

        public async Task<int> DeleteStorageTransactionAsync(Storage3dTransaction transaction)
        {
            return await DatabaseAsync.DeleteAsync<Storage3dTransaction>(transaction?.Id);
        }

        #endregion

        #region Storage
        public async Task<List<Storage3d>> GetStoragesWithChildrenAsync()
        {
            return await DatabaseAsync
                .GetAllWithChildrenAsync<Storage3d>(recursive: true)
                ;
        }

        public async Task<Storage3d> GetStorageWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Storage3d>(id, recursive: true)
                ;
        }

        public async Task SetStorageWithChildrenAsync(Storage3d storage)
        {
            await DatabaseAsync.InsertOrReplaceWithChildrenAsync(storage, recursive: true);
        }

        public async Task SetStorageWithChildrenAsync(List<Storage3d> storages, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(storages);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(storages);
        }

        public async Task<int> DeleteStorageAsync(Storage3d storage)
        {
            return await DatabaseAsync.DeleteAsync<Storage3d>(storage?.Id);
        }

        #endregion

        #region SlicerConfigs

        public async Task<List<Printer3dSlicerConfig>> GetSlicerConfigWithChildrenAsync()
        {
            return await DatabaseAsync
                .GetAllWithChildrenAsync<Printer3dSlicerConfig>(recursive: true)
                ;
        }

        public async Task<Printer3dSlicerConfig> GetSlicerConfigWithChildrenAsync(Guid id)
        {
            return await DatabaseAsync
                .GetWithChildrenAsync<Printer3dSlicerConfig>(id, recursive: true)
                ;
        }

        public async Task SetSlicerConfigWithChildrenAsync(Printer3dSlicerConfig config)
        {
            await DatabaseAsync.InsertOrReplaceWithChildrenAsync(config, recursive: true);
        }

        public async Task SetSlicerConfigsWithChildrenAsync(List<Printer3dSlicerConfig> configs, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(configs);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(configs);
        }

        public async Task<int> DeleteSlicerConfigAsync(Printer3dSlicerConfig configs)
        {
            return await DatabaseAsync.DeleteAsync<Printer3dSlicerConfig>(configs?.Id);
        }

        #endregion

        #endregion

        #endregion
    }
}

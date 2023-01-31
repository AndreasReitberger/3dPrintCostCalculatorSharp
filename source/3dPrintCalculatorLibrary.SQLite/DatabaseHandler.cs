using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.CustomerAdditions;
using AndreasReitberger.Print3d.SQLite.Events;
using AndreasReitberger.Print3d.SQLite.FileAdditions;
using AndreasReitberger.Print3d.SQLite.Interfaces;
using AndreasReitberger.Print3d.SQLite.MaintenanceAdditions;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite.Settings;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.Diagnostics;

namespace AndreasReitberger.Print3d.SQLite
{
    public partial class DatabaseHandler : ObservableObject, IDatabaseHandler
    {
        #region Instance
        static DatabaseHandler? _instance;
        static readonly object Lock = new();
        public static DatabaseHandler Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseHandler();
                    }
                }
                return _instance;
            }

            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }

        }
        #endregion

        #region Properties
        [ObservableProperty]
        bool isInitialized = false;

        [ObservableProperty]
        string databasePath = "";

        [ObservableProperty]
        SQLiteConnection _database;

        [ObservableProperty]
        SQLiteAsyncConnection databaseAsync;

        [ObservableProperty]
        List<Action> delegates = new();

        [ObservableProperty]
        List<Type> tables = new();
        #endregion

        #region Collections
        [ObservableProperty]
        List<Calculation3d> calculations = new();

        [ObservableProperty]
        List<Printer3d> printers = new();
        partial void OnPrintersChanged(List<Printer3d> value)
        {
            OnPrintersChangedEvent(new PrintersChangedDatabaseEventArgs()
            {
                Printers = value,
            });
        }

        [ObservableProperty]
        List<Material3d> materials = new();
        partial void OnMaterialsChanged(List<Material3d> value)
        {
            OnMaterialsChangedEvent(new MaterialsChangedDatabaseEventArgs()
            {
                Materials = value,
            });
        }

        [ObservableProperty]
        List<Customer3d> customers = new();
        partial void OnCustomersChanged(List<Customer3d> value)
        {
            OnCustomersChangedEvent(new CustomersChangedDatabaseEventArgs()
            {
                Customers = value,
            });
        }

        [ObservableProperty]
        List<Workstep> worksteps = new();
        partial void OnWorkstepsChanged(List<Workstep> value)
        {
            OnWorkstepsChangedEvent(new WorkstepsChangedDatabaseEventArgs()
            {
                Worksteps = value,
            });
        }

        [ObservableProperty]
        List<HourlyMachineRate> hourlyMachineRates = new();
        partial void OnHourlyMachineRatesChanged(List<HourlyMachineRate> value)
        {
            OnHourlyMachineRatesChangedEvent(new HourlyMachineRatesChangedDatabaseEventArgs()
            {
                HourlyMachineRates = value,
            });
        }
        #endregion

        #region Constructor
        public DatabaseHandler() { }
        public DatabaseHandler(string databasePath, bool updateInstance = true)
        {
            DatabaseAsync = new SQLiteAsyncConnection(databasePath);
            Database = new SQLiteConnection(databasePath);
            InitTables();
            IsInitialized = true;
            if (updateInstance) Instance = this;
        }
        #endregion

        #region Methods

        #region Private

        #endregion

        #region Public

        #region Init
        public void InitTables()
        {
            Database?.CreateTable<Manufacturer>();
            Database?.CreateTable<Supplier>();
            Database?.CreateTable<Printer3d>();
            Database?.CreateTable<Printer3dAttribute>();
            Database?.CreateTable<Printer3dSlicerConfig>();
            Database?.CreateTable<Maintenance3d>();
            Database?.CreateTable<Sparepart>();
            Database?.CreateTable<Material3dColor>();
            Database?.CreateTable<Material3dType>();
            Database?.CreateTable<Material3dAttribute>();
            Database?.CreateTable<Material3dProcedureAttribute>();
            Database?.CreateTable<Material3d>();
            Database?.CreateTable<WorkstepCategory>();
            Database?.CreateTable<Workstep>();
            Database?.CreateTable<HourlyMachineRate>();
            Database?.CreateTable<Customer3d>();
            Database?.CreateTable<CustomAddition>();
            Database?.CreateTable<CalculationAttribute>();
            Database?.CreateTable<CalculationProcedureAttribute>();
            Database?.CreateTable<CalculationProcedureParameter>();
            Database?.CreateTable<CalculationProcedureParameterAddition>();
            Database?.CreateTable<Calculation3d>();
            Database?.CreateTable<File3d>();
            Database?.CreateTable<ModelWeight>();
            Database?.CreateTable<Address>();
            Database?.CreateTable<Email>();
            Database?.CreateTable<PhoneNumber>();
            Database?.CreateTable<ContactPerson>();
            Database?.CreateTable<Calculation3dProfile>();
            Database?.CreateTable<Printer3dCalculation>();
            Database?.CreateTable<Material3dCalculation>();
            Database?.CreateTable<WorkstepCalculation>();
            Database?.CreateTable<CustomAdditionCalculation>();
            Database?.CreateTable<WorkstepDuration>();
            Database?.CreateTable<Item3d>();
            Database?.CreateTable<Item3dUsage>();

            Database?.CreateTable<DatabaseSettingsKeyValuePair>();
        }

        public async Task InitTablesAsync()
        {
            await DatabaseAsync.CreateTableAsync<Manufacturer>();
            await DatabaseAsync.CreateTableAsync<Supplier>();
            await DatabaseAsync.CreateTableAsync<Printer3d>();
            await DatabaseAsync.CreateTableAsync<Printer3dAttribute>();
            await DatabaseAsync.CreateTableAsync<Printer3dSlicerConfig>();
            await DatabaseAsync.CreateTableAsync<Maintenance3d>();
            await DatabaseAsync.CreateTableAsync<Sparepart>();
            await DatabaseAsync.CreateTableAsync<Material3dColor>();
            await DatabaseAsync.CreateTableAsync<Material3dType>();
            await DatabaseAsync.CreateTableAsync<Material3dAttribute>();
            await DatabaseAsync.CreateTableAsync<Material3dProcedureAttribute>();
            await DatabaseAsync.CreateTableAsync<Material3d>();
            await DatabaseAsync.CreateTableAsync<WorkstepCategory>();
            await DatabaseAsync.CreateTableAsync<Workstep>();
            await DatabaseAsync.CreateTableAsync<HourlyMachineRate>();
            await DatabaseAsync.CreateTableAsync<Customer3d>();
            await DatabaseAsync.CreateTableAsync<CustomAddition>();
            await DatabaseAsync.CreateTableAsync<CalculationAttribute>();
            await DatabaseAsync.CreateTableAsync<CalculationProcedureAttribute>();
            await DatabaseAsync.CreateTableAsync<CalculationProcedureParameter>();
            await DatabaseAsync.CreateTableAsync<CalculationProcedureParameterAddition>();
            await DatabaseAsync.CreateTableAsync<Calculation3d>();
            await DatabaseAsync.CreateTableAsync<File3d>();
            await DatabaseAsync.CreateTableAsync<ModelWeight>();
            await DatabaseAsync.CreateTableAsync<Address>();
            await DatabaseAsync.CreateTableAsync<Email>();
            await DatabaseAsync.CreateTableAsync<PhoneNumber>();
            await DatabaseAsync.CreateTableAsync<ContactPerson>();
            await DatabaseAsync.CreateTableAsync<Calculation3dProfile>();
            await DatabaseAsync.CreateTableAsync<Printer3dCalculation>();
            await DatabaseAsync.CreateTableAsync<Material3dCalculation>();
            await DatabaseAsync.CreateTableAsync<WorkstepCalculation>();
            await DatabaseAsync.CreateTableAsync<CustomAdditionCalculation>();
            await DatabaseAsync.CreateTableAsync<WorkstepDuration>();
            await DatabaseAsync.CreateTableAsync<Item3d>();
            await DatabaseAsync.CreateTableAsync<Item3dUsage>();

            await DatabaseAsync.CreateTableAsync<DatabaseSettingsKeyValuePair>();
        }

        public void CreateTable(Type table)
        {
            Database?.CreateTable(table);
        }

        public void CreateTables(List<Type> tables)
        {
            Database?.CreateTables(CreateFlags.None, tables?.ToArray());
        }

        #endregion

        #region Delegates
        public async Task UpdateAllDelegatesAsync()
        {
            //var actions = Delegates.Select(task => new Task(task));
            List<Task> tasks = new(Delegates.Select(task => new Task(task)));
            await Task.WhenAll(tasks);
        }
        #endregion

        #region Database
        public void InitDatabase(string databasePath)
        {
            DatabaseAsync = new SQLiteAsyncConnection(databasePath);
            Database = new SQLiteConnection(databasePath);
            InitTables();
            IsInitialized = true;
            Instance = this;
        }

        public async Task InitDatabaseAsync(string databasePath)
        {
            DatabaseAsync = new SQLiteAsyncConnection(databasePath);
            Database = new SQLiteConnection(databasePath);
            await InitTablesAsync();
            IsInitialized = true;
            Instance = this;
        }

        public async Task CloseDatabaseAsync()
        {
            Database?.Close();
            await DatabaseAsync.CloseAsync();
        }

        public List<TableMapping> GetTableMappings(string databasePath = "")
        {
            if (DatabaseAsync == null && !string.IsNullOrWhiteSpace(databasePath))
            {
                InitDatabase(databasePath);
            }
            return DatabaseAsync.TableMappings.ToList();
        }

        public async Task RebuildAllTableAsync()
        {
            await InitTablesAsync();
        }

        public async Task DropAllTableAsync()
        {
            //List<Task> tasks = new();
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                await DatabaseAsync.DropTableAsync(mapping);
                //tasks.Add(Database?.DeleteAllAsync(mapping));
            }
            //await Task.WhenAll(tasks);
        }

        public async Task TryDropAllTableAsync()
        {
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                try
                {
                    await DatabaseAsync.DropTableAsync(mapping);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public async Task<bool> TryDropTableAsync(TableMapping mapping)
        {
            try
            {
                int result = await DatabaseAsync.DropTableAsync(mapping);
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task ClearAllTableAsync()
        {
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                await DatabaseAsync.DeleteAllAsync(mapping);
            }
        }

        public async Task ClearTableAsync(TableMapping mapping)
        {
            await DatabaseAsync.DeleteAllAsync(mapping);
        }

        public async Task TryClearAllTableAsync()
        {
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                try
                {
                    await DatabaseAsync.DeleteAllAsync(mapping);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public async Task BackupDatabaseAsync(string targetFolder, string databaseName)
        {
            await DatabaseAsync.BackupAsync(targetFolder, databaseName);
        }

        public void BackupDatabase(string targetFolder, string databaseName)
        {
            Database?.Backup(targetFolder, databaseName);
        }

        public void Close()
        {
            Database?.Close();
            DatabaseAsync?.CloseAsync();
        }

        public void Dispose()
        {
            Close();
        }
        #endregion

        #region Static

        public static async Task<Tuple<T, TimeSpan?>> StopWatchFunctionAsync<T>(Func<T> action, bool inNewTask = false)
        {
            Stopwatch timer = new();
            timer.Start();
            T result;
            if (inNewTask)
            {
                result = await Task.Run(() =>
                {
                    return action();
                });
            }
            else
            {
                result = action();
            }
            timer.Stop();
            //var t = new Tuple<T, TimeSpan?>(result, timer?.Elapsed);
            return new Tuple<T, TimeSpan?>(result, timer?.Elapsed);
        }

        public static T StopWatchFunction<T>(Func<T> action, out TimeSpan? duration)
        {
            Stopwatch timer = new();
            timer.Start();
            T result = action();

            timer.Stop();
            duration = timer?.Elapsed;

            return result;
        }

        #endregion

        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #endregion

        #endregion
    }
}
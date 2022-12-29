using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Print3d.Interface;
using AndreasReitberger.Print3d.Models;
using AndreasReitberger.Print3d.Models.CalculationAdditions;
using AndreasReitberger.Print3d.Models.CustomerAdditions;
using AndreasReitberger.Print3d.Models.Events;
using AndreasReitberger.Print3d.Models.FileAdditions;
using AndreasReitberger.Print3d.Models.MaintenanceAdditions;
using AndreasReitberger.Print3d.Models.MaterialAdditions;
using AndreasReitberger.Print3d.Models.PrinterAdditions;
using AndreasReitberger.Print3d.Models.Settings;
using AndreasReitberger.Print3d.Models.WorkstepAdditions;
using SQLite;
using System.Diagnostics;

namespace AndreasReitberger.Print3d
{
    public partial class DatabaseHandler : BaseModel, IDatabaseHandler
    {
        #region Instance
        static DatabaseHandler _instance = null;
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
        bool _isInitialized = false;
        public bool IsInitialized
        {
            get => _isInitialized;
            private set
            {
                if (_isInitialized == value) return;
                _isInitialized = value;
                OnPropertyChanged();
            }
        }

        string _databasePath = "";
        public string DatabasePath
        {
            get => _databasePath;
            private set
            {
                if (_databasePath == value) return;
                _databasePath = value;
                OnPropertyChanged();
            }
        }

        SQLiteConnection _database;
        public SQLiteConnection Database
        {
            get => _database;
            private set
            {
                if (_database == value) return;
                _database = value;
                OnPropertyChanged();
            }
        }

        SQLiteAsyncConnection _databaseAsync;
        public SQLiteAsyncConnection DatabaseAsync
        {
            get => _databaseAsync;
            private set
            {
                if (_databaseAsync == value) return;
                _databaseAsync = value;
                OnPropertyChanged();
            }
        }

        List<Action> _delegates = new();
        public List<Action> Delegates
        {
            get => _delegates;
            private set
            {
                if (_delegates == value) return;
                _delegates = value;
                OnPropertyChanged();
            }
        }

        List<Type> _tables = new();
        public List<Type> Tables
        {
            get => _tables;
            private set
            {
                if (_tables == value) return;
                _tables = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Collections
        List<Calculation3d> _calculations = new();
        public List<Calculation3d> Calculations
        {
            get => _calculations;
            private set
            {
                if (_calculations == value) return;
                //if (_calculations?.SequenceEqual(value) ?? false) return;
                _calculations = value;
                OnPropertyChanged();
            }
        }

        List<Printer3d> _printers = new();
        public List<Printer3d> Printers
        {
            get => _printers;
            private set
            {
                //if (_printers == value) return;
                if (_printers?.SequenceEqual(value) ?? false) return;
                _printers = value;
                OnPropertyChanged();
                OnPrintersChanged(new PrintersChangedDatabaseEventArgs()
                {
                    Printers = value,
                });
            }
        }

        List<Material3d> _materials = new();
        public List<Material3d> Materials
        {
            get => _materials;
            private set
            {
                if (_materials?.SequenceEqual(value) ?? false) return;
                _materials = value;
                OnPropertyChanged();
                OnMaterialsChanged(new MaterialsChangedDatabaseEventArgs()
                {
                    Materials = value,
                });
            }
        }

        List<Customer3d> _customers = new();
        public List<Customer3d> Customers
        {
            get => _customers;
            private set
            {
                if (_customers?.SequenceEqual(value) ?? false) return;
                _customers = value;
                OnPropertyChanged();
                OnCustomersChanged(new CustomersChangedDatabaseEventArgs()
                {
                    Customers = value,
                });
            }
        }

        List<Workstep> _worksteps = new();
        public List<Workstep> Worksteps
        {
            get => _worksteps;
            private set
            {
                if (_worksteps?.SequenceEqual(value) ?? false) return;
                _worksteps = value;
                OnPropertyChanged();
                OnWorkstepsChanged(new WorkstepsChangedDatabaseEventArgs()
                {
                    Worksteps = value,
                });
            }
        }

        List<HourlyMachineRate> _hourlyMachineRates = new();
        public List<HourlyMachineRate> HourlyMachineRates
        {
            get => _hourlyMachineRates;
            private set
            {
                if (_hourlyMachineRates?.SequenceEqual(value) ?? false) return;
                _hourlyMachineRates = value;
                OnPropertyChanged();
                OnHourlyMachineRatesChanged(new HourlyMachineRatesChangedDatabaseEventArgs()
                {
                    HourlyMachineRates = value,
                });
            }
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
            Database?.CreateTable<BuildVolume>();
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

            Database?.CreateTable<DatabaseSettingsKeyValuePair>();
        }

        public async Task InitTablesAsync()
        {
            await DatabaseAsync?.CreateTableAsync<Manufacturer>();
            await DatabaseAsync?.CreateTableAsync<Supplier>();
            await DatabaseAsync?.CreateTableAsync<BuildVolume>();
            await DatabaseAsync?.CreateTableAsync<Printer3d>();
            await DatabaseAsync?.CreateTableAsync<Printer3dAttribute>();
            await DatabaseAsync?.CreateTableAsync<Printer3dSlicerConfig>();
            await DatabaseAsync?.CreateTableAsync<Maintenance3d>();
            await DatabaseAsync?.CreateTableAsync<Sparepart>();
            await DatabaseAsync?.CreateTableAsync<Material3dColor>();
            await DatabaseAsync?.CreateTableAsync<Material3dType>();
            await DatabaseAsync?.CreateTableAsync<Material3dAttribute>();
            await DatabaseAsync?.CreateTableAsync<Material3dProcedureAttribute>();
            await DatabaseAsync?.CreateTableAsync<Material3d>();
            await DatabaseAsync?.CreateTableAsync<WorkstepCategory>();
            await DatabaseAsync?.CreateTableAsync<Workstep>();
            await DatabaseAsync?.CreateTableAsync<HourlyMachineRate>();
            await DatabaseAsync?.CreateTableAsync<Customer3d>();
            await DatabaseAsync?.CreateTableAsync<CustomAddition>();
            await DatabaseAsync?.CreateTableAsync<CalculationAttribute>();
            await DatabaseAsync?.CreateTableAsync<CalculationProcedureAttribute>();
            await DatabaseAsync?.CreateTableAsync<CalculationProcedureParameter>();
            await DatabaseAsync?.CreateTableAsync<CalculationProcedureParameterAddition>();
            await DatabaseAsync?.CreateTableAsync<Calculation3d>();
            await DatabaseAsync?.CreateTableAsync<File3d>();
            await DatabaseAsync?.CreateTableAsync<ModelWeight>();
            await DatabaseAsync?.CreateTableAsync<Address>();
            await DatabaseAsync?.CreateTableAsync<Email>();
            await DatabaseAsync?.CreateTableAsync<PhoneNumber>();
            await DatabaseAsync?.CreateTableAsync<ContactPerson>();
            await DatabaseAsync?.CreateTableAsync<Calculation3dProfile>();

            await DatabaseAsync?.CreateTableAsync<DatabaseSettingsKeyValuePair>();
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
            await DatabaseAsync?.CloseAsync();
        }

        public List<TableMapping> GetTableMappings(string databasePath = "")
        {
            if (DatabaseAsync == null && !string.IsNullOrWhiteSpace(databasePath))
            {
                InitDatabase(databasePath);
            }
            return DatabaseAsync?.TableMappings.ToList();
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
                await DatabaseAsync?.DropTableAsync(mapping);
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
                    await DatabaseAsync?.DropTableAsync(mapping);
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
                int result = await DatabaseAsync?.DropTableAsync(mapping);
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
                await DatabaseAsync?.DeleteAllAsync(mapping);
            }
        }

        public async Task ClearTableAsync(TableMapping mapping)
        {
            await DatabaseAsync?.DeleteAllAsync(mapping);
        }

        public async Task TryClearAllTableAsync()
        {
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                try
                {
                    await DatabaseAsync?.DeleteAllAsync(mapping);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public async Task BackupDatabaseAsync(string targetFolder, string databaseName)
        {
            await DatabaseAsync?.BackupAsync(targetFolder, databaseName);
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

        #endregion

        #endregion
    }
}
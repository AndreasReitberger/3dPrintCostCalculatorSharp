using AndreasReitberger.Print3d.SQLite.Events;

namespace AndreasReitberger.Print3d.SQLite
{
    public partial class DatabaseHandler
    {
        #region Events
        public event EventHandler<ErrorEventArgs> ErrorEvent;
        protected virtual void OnErrorEvent(ErrorEventArgs e)
        {
            ErrorEvent?.Invoke(this, e);
        }

        public event EventHandler<DatabaseEventArgs> DataChanged;
        protected virtual void OnDataChangedEvent(DatabaseEventArgs e)
        {
            DataChanged?.Invoke(this, e);
        }

        public event EventHandler<DatabaseEventArgs> QueryFinished;
        protected virtual void OnQueryFinishedEvent(DatabaseEventArgs e)
        {
            QueryFinished?.Invoke(this, e);
        }

        public event EventHandler<CalculationChangedDatabaseEventArgs> CalculationsChanged;
        protected virtual void OnCalculationsChangedEvent(CalculationChangedDatabaseEventArgs e)
        {
            CalculationsChanged?.Invoke(this, e);
        }

        public event EventHandler<Calculation3dEnhancedChangedDatabaseEventArgs> EnhancedCalculationsChanged;
        protected virtual void OnCalculationsChangedEvent(Calculation3dEnhancedChangedDatabaseEventArgs e)
        {
            EnhancedCalculationsChanged?.Invoke(this, e);
        }

        public event EventHandler<PrintersChangedDatabaseEventArgs> PrintersChanged;
        protected virtual void OnPrintersChangedEvent(PrintersChangedDatabaseEventArgs e)
        {
            PrintersChanged?.Invoke(this, e);
        }

        public event EventHandler<MaterialsChangedDatabaseEventArgs> MaterialsChanged;
        protected virtual void OnMaterialsChangedEvent(MaterialsChangedDatabaseEventArgs e)
        {
            MaterialsChanged?.Invoke(this, e);
        }

        public event EventHandler<CustomersChangedDatabaseEventArgs> CustomersChanged;
        protected virtual void OnCustomersChangedEvent(CustomersChangedDatabaseEventArgs e)
        {
            CustomersChanged?.Invoke(this, e);
        }

        public event EventHandler<WorkstepsChangedDatabaseEventArgs> WorkstepsChanged;
        protected virtual void OnWorkstepsChangedEvent(WorkstepsChangedDatabaseEventArgs e)
        {
            WorkstepsChanged?.Invoke(this, e);
        }

        public event EventHandler<FilesChangedDatabaseEventArgs> FilesChanged;
        protected virtual void OnFilesChangedEvent(FilesChangedDatabaseEventArgs e)
        {
            FilesChanged?.Invoke(this, e);
        }

        public event EventHandler<HourlyMachineRatesChangedDatabaseEventArgs> HourlyMachineRatesChanged;
        protected virtual void OnHourlyMachineRatesChangedEvent(HourlyMachineRatesChangedDatabaseEventArgs e)
        {
            HourlyMachineRatesChanged?.Invoke(this, e);
        }

        #endregion
    }
}

using AndreasReitberger.Models.Events;

namespace AndreasReitberger
{
    public partial class DatabaseHandler
    {
        #region Events
        public event EventHandler<DatabaseEventArgs> DataChanged;
        protected virtual void OnDataChanged(DatabaseEventArgs e)
        {
            DataChanged?.Invoke(this, e);
        }

        public event EventHandler<DatabaseEventArgs> QueryFinished;
        protected virtual void OnQueryFinished(DatabaseEventArgs e)
        {
            QueryFinished?.Invoke(this, e);
        }

        public event EventHandler<CalculationChangedDatabaseEventArgs> CalculationsChanged;
        protected virtual void OnCalculationsChanged(CalculationChangedDatabaseEventArgs e)
        {
            CalculationsChanged?.Invoke(this, e);
        }

        public event EventHandler<PrintersChangedDatabaseEventArgs> PrintersChanged;
        protected virtual void OnPrintersChanged(PrintersChangedDatabaseEventArgs e)
        {
            PrintersChanged?.Invoke(this, e);
        }

        public event EventHandler<MaterialsChangedDatabaseEventArgs> MaterialsChanged;
        protected virtual void OnMaterialsChanged(MaterialsChangedDatabaseEventArgs e)
        {
            MaterialsChanged?.Invoke(this, e);
        }

        public event EventHandler<CustomersChangedDatabaseEventArgs> CustomersChanged;
        protected virtual void OnCustomersChanged(CustomersChangedDatabaseEventArgs e)
        {
            CustomersChanged?.Invoke(this, e);
        }

        public event EventHandler<WorkstepsChangedDatabaseEventArgs> WorkstepsChanged;
        protected virtual void OnWorkstepsChanged(WorkstepsChangedDatabaseEventArgs e)
        {
            WorkstepsChanged?.Invoke(this, e);
        }

        public event EventHandler<HourlyMachineRatesChangedDatabaseEventArgs> HourlyMachineRatesChanged;
        protected virtual void OnHourlyMachineRatesChanged(HourlyMachineRatesChangedDatabaseEventArgs e)
        {
            HourlyMachineRatesChanged?.Invoke(this, e);
        }

        #endregion
    }
}

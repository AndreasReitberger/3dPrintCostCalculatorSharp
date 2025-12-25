namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class CustomersChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Customer3d> Customers { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.CustomersChangedDatabaseEventArgs);
        #endregion
    }
}

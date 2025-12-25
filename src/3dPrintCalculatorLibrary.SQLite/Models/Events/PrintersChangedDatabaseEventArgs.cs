namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class PrintersChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Printer3d> Printers { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.PrintersChangedDatabaseEventArgs);
        #endregion
    }
}

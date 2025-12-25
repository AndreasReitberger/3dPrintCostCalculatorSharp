namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class FilesChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<File3d> Files { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.FilesChangedDatabaseEventArgs);
        #endregion
    }
}

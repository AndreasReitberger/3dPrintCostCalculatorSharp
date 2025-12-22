namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class DatabaseEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; } = string.Empty;
        public TimeSpan? Duration { get; set; } = TimeSpan.FromSeconds(0);
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.DatabaseEventArgs);
        #endregion
    }
}

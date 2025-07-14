namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    public interface IDatabaseSettingsKeyValuePair
    {
        public string Key { get; set; }
        public string JsonValue { get; set; }
        public string ValueType { get; set; }
    }
}

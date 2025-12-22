using AndreasReitberger.Print3d.SQLite.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace AndreasReitberger.Print3d.SQLite.Settings
{
    [Table("settings")]
    public partial class DatabaseSettingsKeyValuePair : ObservableObject, IDatabaseSettingsKeyValuePair
    {
        #region Properties
        [ObservableProperty]
        [PrimaryKey]
        public partial string Key { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string JsonValue { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ValueType { get; set; } = string.Empty;

        [ObservableProperty]
        [Ignore]
        public partial Type? Type { get; set; }
        #endregion

        #region Constructor
        public DatabaseSettingsKeyValuePair() { }
        public DatabaseSettingsKeyValuePair(string key, string jsonValue, Type type)
        {
            Key = key;
            JsonValue = jsonValue;
            Type = type;
            ValueType = type.ToString();
        }
        #endregion

        #region Override
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.DatabaseSettingsKeyValuePair);
        #endregion
    }
}

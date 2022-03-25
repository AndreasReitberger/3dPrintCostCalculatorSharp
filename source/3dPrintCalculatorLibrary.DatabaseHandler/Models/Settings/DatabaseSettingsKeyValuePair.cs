using Newtonsoft.Json;
using SQLite;

namespace AndreasReitberger.Models.Settings
{
    [Table("settings")]
    public partial class DatabaseSettingsKeyValuePair
    {
        #region Properties
        [PrimaryKey]
        public string Key { get; set; }
        public string JsonValue { get; set; }
        public string ValueType { get; set; }
        #endregion

        #region Constructor
        public DatabaseSettingsKeyValuePair() { }
        public DatabaseSettingsKeyValuePair(string key, string value)
        {
            Key = key;
            JsonValue = value;
            ValueType = value?.GetType().Name;
        }
        public DatabaseSettingsKeyValuePair(string key, object value)
        {
            Key = key;
            JsonValue = JsonConvert.SerializeObject(value);
            ValueType = value?.GetType().Name;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

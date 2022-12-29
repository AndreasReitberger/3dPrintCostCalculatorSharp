﻿using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Models.Events
{
    public class PrintersChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Printer3d> Printers { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

﻿using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Models.Events
{
    public class PrinterChangedEventArgs : CalculatorEventArgs
    {
        #region Properties
        public Printer3d? Printer { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

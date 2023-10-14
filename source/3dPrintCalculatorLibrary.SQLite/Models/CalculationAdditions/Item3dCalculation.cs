using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    public class Item3dCalculation
    {
        [ForeignKey(typeof(Item3d))]
        public Guid ItemId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}

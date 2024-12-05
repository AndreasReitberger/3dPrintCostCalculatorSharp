namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    public class WorkstepUsageCalculation3dEnhanced
    {
        [ForeignKey(typeof(WorkstepUsage))]
        public Guid WorkstepUsageId { get; set; }

        [ForeignKey(typeof(Calculation3dEnhanced))]
        public Guid CalculationId { get; set; }
    }
}

namespace AndreasReitberger.Print3d.Core.Interfaces
{ 
    public interface ICalculatorEventArgs
    {
        public string? Message { get; set; }
        public Guid CalculationId { get; set; }
    }
}

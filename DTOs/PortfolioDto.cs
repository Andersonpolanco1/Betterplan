namespace BetterplanAPI.DTOs
{
    public class PortfolioDto
    {
        public double Maxrangeyear { get; set; }
        public double Minrangeyear { get; set; }
        public string Uuid { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Id { get; set; }
        public int Financialentityid { get; set; }
        public int Risklevelid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Isdefault { get; set; }
        public string? Profitability { get; set; }
        public int? Investmentstrategyid { get; set; }
        public string? Version { get; set; }
        public string? Extraprofitabilitycurrencycode { get; set; }
        public double Estimatedprofitability { get; set; }
        public double Bpcomission { get; set; }
    }
}

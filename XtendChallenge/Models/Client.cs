using XtendChallenge.Services.Formatters;

namespace XtendChallenge.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public FormatterType FormatterType { get; set; }
        public decimal BalanceThreshold { get; set; }
    }
}
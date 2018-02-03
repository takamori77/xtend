using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            var client = obj as Client;
            return client != null &&
                   Id == client.Id &&
                   Name == client.Name &&
                   Abbreviation == client.Abbreviation &&
                   FormatterType == client.FormatterType &&
                   BalanceThreshold == client.BalanceThreshold;
        }

        public override int GetHashCode()
        {
            var hashCode = -1806908350;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Abbreviation);
            hashCode = hashCode * -1521134295 + FormatterType.GetHashCode();
            hashCode = hashCode * -1521134295 + BalanceThreshold.GetHashCode();
            return hashCode;
        }
    }
}
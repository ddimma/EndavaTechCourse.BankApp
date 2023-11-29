namespace EndavaTechCourse.BankApp.Shared
{
    public class Converter
    {
        private readonly Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>
        {
            { "USD", 18m },
            { "EUR", 20m },
            { "MDL", 1.0m },
            { "GBR", 22m },
            { "RUR", 0.18m },
        };

        private readonly Dictionary<string, decimal> commisionRates = new Dictionary<string, decimal>
        {
            { "Basic", 0.15m },
            { "Silver", 0.1m },
            { "Gold", 0.05m },
            { "PLatinum", 0 },
        };

        public decimal ConvertCurrency(decimal amount, string sourceCurrencyCode, string targetCurrencyCode)
        {
            decimal amountMDL = amount * exchangeRates[sourceCurrencyCode];
            return amountMDL / exchangeRates[targetCurrencyCode];
        }

        public decimal ApplyCommision(decimal amount, string sourceWalletType)
        {
            return amount * commisionRates[sourceWalletType];
        }
    }
}


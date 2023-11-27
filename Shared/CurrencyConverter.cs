namespace EndavaTechCourse.BankApp.Shared
{
    public class CurrencyConverter
    {
        private readonly Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>
        {
            { "USD", 18m },
            { "EUR", 20m },
            { "MDL", 1.0m },
            { "GBR", 22m },
            { "RUR", 0.18m },
        };

        public decimal ConvertCurrency(decimal amount, string sourceCurrencyCode, string targetCurrencyCode)
        {
            decimal amountMDL = amount * exchangeRates[sourceCurrencyCode];
            return amountMDL / exchangeRates[targetCurrencyCode];
        }
    }
}


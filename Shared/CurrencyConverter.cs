namespace EndavaTechCourse.BankApp.Shared
{
    public class CurrencyConverter
    {
        private readonly Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>
        {
            { "USD", 1.0m },
            { "EUR", 0.85m }, // Example exchange rate for EUR (1 USD = 0.85 EUR)
            { "MDL", 18m },
        };

        public decimal ConvertCurrency(decimal amount, string sourceCurrencyCode, string targetCurrencyCode)
        {
            decimal exchangeRate = exchangeRates[targetCurrencyCode] / exchangeRates[sourceCurrencyCode];
            return amount * exchangeRate;
        }
    }
}


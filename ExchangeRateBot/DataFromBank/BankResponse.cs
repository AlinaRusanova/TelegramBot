using Newtonsoft.Json;

namespace ExchangeRateBot.DataFromBank
{
    public class BankResponse
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetExchangeRate(ExchangeData exchangeData)
        {

            if (exchangeData == null || exchangeData.Currency == null)
            { throw new ArgumentNullException(); }

            string url = $"https://api.privatbank.ua/p24api/exchange_rates?json&date={exchangeData.Date}";

            var dataFromBank = await DataFromTheBankResult<PBResponse>(url);

            var exchangeRate = dataFromBank.ExchangeRate.ToList().FirstOrDefault(x => x.Currency == exchangeData.Currency)?.PurchaseRateNB;

            return exchangeRate;
        }

        private static async Task<T> DataFromTheBankResult<T>(string url)
        {

            HttpResponseMessage response = await client.GetAsync(url);

            string responseBody = await response.Content.ReadAsStringAsync();

            var bankResponse = JsonConvert.DeserializeObject<T>(responseBody);

            return bankResponse;
        }

    }
}

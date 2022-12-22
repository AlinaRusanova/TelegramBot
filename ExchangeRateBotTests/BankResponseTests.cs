using ExchangeRateBot;
using ExchangeRateBot.DataFromBank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRateBotTests
{
    [TestClass]
    public class BankResponseTests
    {
        private static readonly ExchangeData _exchangeData = new() { Currency = "EUR", Date = DateOnly.FromDateTime(DateTime.Parse("07.11.2022"))};

        [TestMethod]        
        public async Task GetExchangeRate_Valid()
        {
           string exchangeRate = await BankResponse.GetExchangeRate(_exchangeData);
            var expectedResult = "35.9817000";

           Assert.AreEqual(expectedResult, exchangeRate, "Input and output strings aren`t equal");
        }

        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        [TestMethod]
        public async Task GetExchangeRate_Exception()
        {
            string exchangeRate = await BankResponse.GetExchangeRate(null);
        }
    }
}
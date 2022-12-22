
namespace ExchangeRateBot.Additional
{
    public class Messages
    {
        public const string Welcome = "Please enter /start to know exchange rate";
        public const string Result = "The exchange rate for {0} = {1} UAH per 1 {2}.";
        public const string ChoosedCurrency = "And for what currency?";
        public const string NotCorrectCurrency = "Sorry, we don't have an exchange rate for {0}. Please, try another one";
        public const string ChoosedDate = "What date do you wanna know the exchange rate? You can choose a date from offered or type your own in the format 'DD.MM.YYYY'.";
        public const string NotCorrectDate = "Sorry, we don't have an exchange rate for {0}. Please, try another date no later than {1}";
        public const string ApiRequestException = "Telegram API Error: \n[{0}]\n {1} ";

    }

}

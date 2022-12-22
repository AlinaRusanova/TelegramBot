using Telegram.Bot.Types.ReplyMarkups;

namespace ExchangeRateBot.Additional
{
    public class Keyboards
    {
        public static ReplyKeyboardMarkup KeyboardForCurrency = new ReplyKeyboardMarkup("")
        {
            Keyboard = new[] {
                                          new[]
                                            {
                                            new KeyboardButton(CurrencyEnum.EUR.ToString()),
                                            new KeyboardButton(CurrencyEnum.USD.ToString())
                                            },
                                         new[]
                                            {
                                            new KeyboardButton(CurrencyEnum.PLN.ToString()),
                                            new KeyboardButton(CurrencyEnum.GBP.ToString())
                                            }
                                            }
        };


        public static ReplyKeyboardMarkup KeyboardForDate = new ReplyKeyboardMarkup("")
        {
            Keyboard = new[] {
                new[]
                                {
                    new KeyboardButton($"{DateOnly.FromDateTime(DateTime.Today)}"),
                    new KeyboardButton($"{DateOnly.FromDateTime(DateTime.Today.AddDays(-1))}")
                },
                new[]
                                {
                    new KeyboardButton($"{DateOnly.FromDateTime(DateTime.Today.AddMonths(-1))}"),
                    new KeyboardButton($"{DateOnly.FromDateTime(DateTime.Today.AddYears(-1))}")
                } }
        };


    }
}

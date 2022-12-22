using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using ExchangeRateBot.Additional;
using ExchangeRateBot.DataFromBank;
using Telegram.Bot.Types.ReplyMarkups;
using Serilog;

namespace ExchangeRateBot
{
    public class Handlers
    {
        private static readonly ExchangeData _exchangeData = new ();
        private static int _offSet = 0;

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
        {
            if (botClient == null || exception == null)
            { throw new ArgumentNullException(); }

            var errorMessage = exception switch
            { 
                ApiRequestException apiRequestException => String.Format(Messages.ApiRequestException,apiRequestException.ErrorCode,apiRequestException.Message),
                _=>exception.ToString()
            };

            Log.Information(errorMessage);
            await Task.CompletedTask;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
        {
            Log.Information(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type == UpdateType.Message)
            {
                
                var message = update.Message;


                if (update.Message.Type != MessageType.Text)
                {
                    return;
                }


                if (message.Text.ToLower() == "/start")
                {
                        await botClient.SendTextMessageAsync(message.Chat.Id, Messages.ChoosedDate,  replyMarkup: Keyboards.KeyboardForDate);
                        _offSet = 1;
                        return;
                }

                if (_offSet == 1)
                {
                    await GetDate(botClient, message);

                    return;
                }


                else if (_offSet == 2)
                {

                    await GetCurrency(botClient, message);
                    return;

                }

                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, Messages.Welcome);
                }
            }
        }


        private static async Task GetDate(ITelegramBotClient botClient, Message message)
        {
            while (!DateOnly.TryParse(message.Text, out DateOnly tryDate) || tryDate > DateOnly.FromDateTime(DateTime.Today))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, String.Format(Messages.NotCorrectDate, message.Text, DateOnly.FromDateTime(DateTime.Today)), ParseMode.Markdown, replyMarkup: Keyboards.KeyboardForDate);
                return;
            }

            var date = DateOnly.Parse(message.Text);
            _exchangeData.Date = date;
            _offSet = 2;

            await botClient.SendTextMessageAsync(message.Chat.Id, Messages.ChoosedCurrency, ParseMode.Markdown, replyMarkup: Keyboards.KeyboardForCurrency);
        }


        private static async Task GetCurrency(ITelegramBotClient botClient, Message message)
        {
            while (!Enum.GetNames(typeof(CurrencyEnum)).Contains(message.Text.ToUpper()))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, String.Format(Messages.NotCorrectCurrency, message.Text), ParseMode.Markdown, replyMarkup: Keyboards.KeyboardForCurrency);
                return;
            }
          
            _offSet = 0;
            _exchangeData.Currency = message.Text.ToUpper();

            var exchangeRate = await BankResponse.GetExchangeRate(_exchangeData);

            if (exchangeRate == null)
            {
                await botClient.SendTextMessageAsync(message.Chat, String.Format(Messages.NotCorrectCurrency, message.Text));
            }

            else { await botClient.SendTextMessageAsync(message.Chat, String.Format(Messages.Result, _exchangeData.Date, exchangeRate, _exchangeData.Currency), replyMarkup: new ReplyKeyboardRemove()); }

            await botClient.SendTextMessageAsync(message.Chat, Messages.Welcome);
        }

    }
}

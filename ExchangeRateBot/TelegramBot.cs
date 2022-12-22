using Serilog;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace ExchangeRateBot
{
    public class TelegramBot
    {

        private static readonly TelegramBotClient botClient = new TelegramBotClient("5732104770:AAFArcmEhxUHmmkW7I-BjbtheqPsyQ1h1lU");
        private static CancellationTokenSource token = new();

        
        public static void Start()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(@"LogFile.log").CreateLogger();

            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };

            botClient.StartReceiving(Handlers.HandleUpdateAsync,
                                    Handlers.HandleErrorAsync,
                                    receiverOptions,
                                    token.Token);

            Console.ReadLine();
        }

    }
}
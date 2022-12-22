using ExchangeRateBot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace ExchangeRateBotTests
{
    [TestClass]
    public class HandlersTests
    {
        private readonly Mock<ITelegramBotClient> _telegramBotClientMock = new Mock<ITelegramBotClient>(); 
        private static CancellationTokenSource token = new();

        [TestMethod]
        public async Task HandleUpdateAsync_TestForStart()
        {
            //arange

            var update = new Update
            {
                Message = new Message
                {
                    Text = "/start",
                    Date = DateTime.Now,
                    Chat = new Chat()
                }
            };

            _telegramBotClientMock.Setup(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), default)).Verifiable();
          
            //act

            await Handlers.HandleUpdateAsync(_telegramBotClientMock.Object, update, token.Token);

            //assert

            _telegramBotClientMock.Verify(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), default), Times.AtLeast(1));

        }


        [TestMethod]
        public async Task HandleUpdateAsync_TestForDate()
        {
            //arange

            var update = new Update
            {
                Message = new Message
                {
                    Text = "09.11.2022",
                    Date = DateTime.Now,
                    Chat = new Chat()
                }
            };

            _telegramBotClientMock.Setup(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), default)).Verifiable();

            //act

            await Handlers.HandleUpdateAsync(_telegramBotClientMock.Object, update, token.Token);

            //assert

            _telegramBotClientMock.Verify(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), default), Times.AtLeast(1));

        }

        [TestMethod]
        public async Task HandleUpdateAsync_TestForCurrency()
        {
            //arange

            var update = new Update
            {
                Message = new Message
                {
                    Text = "EUR",
                    Date = DateTime.Now,
                    Chat = new Chat()
                }
            };

            _telegramBotClientMock.Setup(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), default)).Verifiable();

            //act

            await Handlers.HandleUpdateAsync(_telegramBotClientMock.Object, update, token.Token);

            //assert

            _telegramBotClientMock.Verify(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), default), Times.AtLeast(1));

        }


        [TestMethod]
        public async Task HandleUpdateAsync_TestForError()
        {
            //arange

            var update = new Update
            {
                Message = new Message
                {
                    Text = string.Empty,
                    Date = DateTime.Now,
                    Chat = new Chat()
                }
            };

            _telegramBotClientMock.Setup(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), default)).Verifiable();

            //act

            await Handlers.HandleUpdateAsync(_telegramBotClientMock.Object, update, token.Token);

            //assert

            _telegramBotClientMock.Verify(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), default), Times.AtLeast(1));

        }

        [ExpectedException(typeof(NullReferenceException), "Exception was not thrown")]
        [TestMethod]
        public async Task HandleUpdateAsync_Exception()
        {
            await Handlers.HandleUpdateAsync(_telegramBotClientMock.Object, null, token.Token);

        }

        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        [TestMethod]
        public async Task HandleUErrorAsync_Exception()
        {
            await Handlers.HandleErrorAsync(_telegramBotClientMock.Object, null, token.Token);

        }

        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        [TestMethod]
        public async Task HandleUErrorAsync_Exception2()
        {
            var exception = new Exception();
            await Handlers.HandleErrorAsync(null, exception, token.Token);

        }

        [TestMethod]
        public async Task HandleUErrorAsync()
        {
            var exception = new Exception();
            await Handlers.HandleErrorAsync(_telegramBotClientMock.Object, exception, token.Token);

        }
    }
}

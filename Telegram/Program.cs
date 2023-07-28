using System.Runtime.CompilerServices;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

var bot = new TelegramBotClient("6398676430:AAF31gyPffl3RicCzBVGlc9Ixs9lvkKoo-w");
var mainKeyboard = MakeBaseKeyBoards();

var offset = 0;
while (true)
{
    var messages = await bot.GetUpdatesAsync(offset);

    foreach (var message in messages)
    {
        if (message == null)
            continue;

        offset = message.Id + 1;
        var text = message.Message.Text;
        var chatId = message.Message.Chat.Id;
        var from = message.Message.From;

        if (text.Contains("/start"))
        {
            await StartBot(bot, mainKeyboard, message, chatId, from);
        }
        else if (text.Contains("تماس با من"))
        {
            await ContactUs(bot, message, chatId);

        }
        else if (text.Contains("تلگرام"))
        {
            FileStream stream = System.IO.File.Open("E:\\C#\\Telegram\\Telegram\\Files\\ContactUs\\telegram.jpg", FileMode.Open);
            await bot.SendPhotoAsync(chatId, new InputOnlineFile(stream), "23323232");
        }
        else if (text.Contains("بازگشت به منوی اصلی"))
        {
            await bot.SendTextMessageAsync(chatId, "بازگشت به منوی اصلی", null, null, true, false, false, null, false, mainKeyboard);
        }



    }
}

static async Task ContactUs(TelegramBotClient bot, Update message, long chatId)
{
    var contactrow1 = new KeyboardButton[]
    {
                new KeyboardButton("موبایل"+"\U00000031"+"\U000020E3"),
                new KeyboardButton("تلگرام"+"\U00000032"+"\U000020E3"),
                new KeyboardButton("اینستا"+"\U00000033"+"\U000020E3")
    };
    var contactrow2 = new KeyboardButton[]
    {
                new KeyboardButton("بازگشت به منوی اصلی"+"\U00002B05")
    };
    var contactuskeyboards = new ReplyKeyboardMarkup(contactrow1);
    contactuskeyboards.Keyboard = new KeyboardButton[][] { contactrow1, contactrow2 };
    contactuskeyboards.ResizeKeyboard = true;
    await bot.SendTextMessageAsync(chatId, "کجا باهام می خوای حرف بزنی؟؟؟؟", null, null, true, false, false, message.Message.MessageId, false, contactuskeyboards);
}

static async Task StartBot(TelegramBotClient bot, ReplyKeyboardMarkup mainKeyboard, Update message, long chatId, User? from)
{
    var sb = new StringBuilder();
    sb.AppendLine(string.Format("{0} عزیز", from.Username));
    sb.AppendLine("به ربات تلگرامی من خوش آمدید" + "\U0001F618");
    await bot.SendTextMessageAsync(chatId, sb.ToString(), null, null, true, false, false, message.Message.MessageId, false, mainKeyboard);
}

static ReplyKeyboardMarkup MakeBaseKeyBoards()
{
    var row1 = new KeyboardButton[] {
    new KeyboardButton("درباره من" + "\U0001F609"),
    new KeyboardButton("پیشنهاد به من" + "\U00002600")
    };
    var row2 = new KeyboardButton[]
    {
    new KeyboardButton("تماس با من" + "\U0000260E")
    };
    var mainKeyboard = new ReplyKeyboardMarkup(row1);
    mainKeyboard.Keyboard = new KeyboardButton[][] { row1, row2 };
    mainKeyboard.ResizeKeyboard = true;
    return mainKeyboard;
}
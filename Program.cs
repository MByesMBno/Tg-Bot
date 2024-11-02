using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace fffirst
{
    internal class Program
    {
        private static DateTime date = DateTime.Today;
        private static string token = "7908513787:AAGdjAiOW4yVToJBirUllC1lD_78pn4wq3g";
        static Dictionary<int, string> info1_odd = new Dictionary<int, string>() { [3] = "Мод.Сис(Лек.) У503", [4] = "Мехатрон.Комп.(Лек.) У503" };
        static Dictionary<int, string> info1_even = new Dictionary<int, string>() { [4] = "Мехатрон.Комп.(Лек.) У503" };
        static Dictionary<int, string> info2_odd = new Dictionary<int, string>() { [3] = "Мех.Комп.(Прак.) У403"};
        static Dictionary<int, string> info2_even = new Dictionary<int, string>() { [3] = "Мех.Комп.(Прак.) У403", [4] = "Основы Имидж.(Лек.) К613", [5] = "Основы Имидж.(Прак.) К605" };
        static Dictionary<int, string> info3 = new Dictionary<int, string>() { [1] = "Комп.Сети(Прак.) У406", [2] = "Англ.Язык(Прак.) У504", [3] = "Мод.Сис.(Прак.) У406", [4] = "CDIO(Прак.) У105" };
        static Dictionary<int, string> info5_odd = new Dictionary<int, string>() { [2] = "Комп.Сети(Лек.) У503", [4] = "ЛСУ(Лек.) У503", [5] = "ЛСУ(Прак.) У501", [6] = "Метрология(Лек.) У105", [7] = "Метрология(Лек.) У106" };
        static Dictionary<int, string> info5_even = new Dictionary<int, string>() { [2]="Комп.Сети(Лек.) У503", [4] = "ЛСУ(Лек.) У503", [5] = "ЛСУ(Прак.) У501", [6] = "Метрология(Лек.) У105"};
        static Dictionary<int, string> info6_odd = new Dictionary<int, string>() { [2] = "БД(Лек.) У903", [3] = "Прог.Моб.Ус-в(Лек.) У903", [4] = "Прог.Моб.Ус-в(Прак.) У105", [5] = "БД(Прак.) У903" };
        static Dictionary<int, string> info6_even = new Dictionary<int, string>() { [5] = "БД(Прак.) У903" };
        static string[] pairs = new string[] {"1 пара: 08:30-9:50", "2 пара: 10:00-11:20",  
            "3 пара: 11:30-12:50", "4 пара: 13:20-14:40", 
            "5 пара: 14:50-16:10", "6 пара: 16:20-17:40", 
            "7 пара: 18:00-19:20", "8 пара: 19:30-20:50"};
        static void Main(string[] args)
        {
            var client = new TelegramBotClient(token);
            
            client.StartReceiving(updateHandler,pollingErrorHandler);
            
            Console.ReadLine();           
        }
        static Boolean GetWeekType(DateTime date)
        {
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            int weekNumber = calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNumber % 2 == 0 ? true:false;
        }
       
        async private static Task updateHandler(ITelegramBotClient bot, Update update, CancellationToken token)
        {
            var mes=update.Message;
            if (mes != null) {
                Dictionary<int, string> info;
                if (mes.Text.ToLower().Contains("/pairs")) {
                    await bot.SendTextMessageAsync(mes.Chat.Id, "Выгружаю...");
                    foreach (string item in pairs)
                    {
                        await bot.SendTextMessageAsync(mes.Chat.Id, item);
                    }
                }
                    if (mes.Text.ToLower().Contains("/schedule")) {
                    await bot.SendTextMessageAsync(mes.Chat.Id, "Секундочку ща подумаю...");
                    if (date.DayOfWeek.ToString() == "Monday") {
                        if (GetWeekType(date))
                        {
                            info = info1_even;
                        }
                        else {
                            info = info1_odd;
                        }
                        foreach (var i in info)
                        {
                            await bot.SendTextMessageAsync(mes.Chat.Id, $"{i.Key} . {i.Value}");
                        }
                        return;
                    }

                    if (date.DayOfWeek.ToString() == "Tuesday")
                    {
                        if (GetWeekType(date))
                        {
                            info = info2_even;
                        }
                        else
                        {
                            info = info2_odd;
                        }
                        foreach (var i in info)
                        {
                            await bot.SendTextMessageAsync(mes.Chat.Id, $"{i.Key} . {i.Value}");
                        }
                        return;
                    }

                    if (date.DayOfWeek.ToString() == "Wednesday")
                    {
                        
                        foreach (var i in info3)
                        {
                            await bot.SendTextMessageAsync(mes.Chat.Id, $"{i.Key} . {i.Value}");
                        }
                        return;
                    }
                    
                    if (date.DayOfWeek.ToString() == "Friday")
                    {
                        if (GetWeekType(date))
                        {
                            info = info5_even;
                        }
                        else
                        {
                            info = info5_odd;
                        }
                        foreach (var i in info)
                        {
                            await bot.SendTextMessageAsync(mes.Chat.Id, $"{i.Key} . {i.Value}");
                        }
                        return;
                    }

                    if (date.DayOfWeek.ToString() == "Saturday")
                    {
                        if (GetWeekType(date))
                        {
                            info = info6_even;
                        }
                        else
                        {
                            info = info6_odd;
                        }
                        foreach (var i in info)
                        {
                            await bot.SendTextMessageAsync(mes.Chat.Id, $"{i.Key} . {i.Value}");
                        }
                        return;
                    }

                    if (date.DayOfWeek.ToString() == "Sunday" || date.DayOfWeek.ToString() == "Thursday")
                    {
                            await bot.SendTextMessageAsync(mes.Chat.Id, "Чиназес");
                        return;
                    }

                }
            }
        }
        async private static Task pollingErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        
    }
}

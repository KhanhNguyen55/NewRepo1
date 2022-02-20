using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace DebionTradePlatform.Services
{
    public class MessageService
    {
        
        public void SendMessage(string text)
        {
            //string destID = "220223021";
            try
            {
                var bot = new Telegram.Bot.TelegramBotClient("5113653895:AAHoUjiF2lHbrnWampugdIfqw1pO-QCq4Sk");
                bot.SendTextMessageAsync("-1001740429824", text);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void SendMessageToTelegram(string content)
        {
            string html = string.Empty;
            string url = @"https://api.telegram.org/bot5113653895:AAHoUjiF2lHbrnWampugdIfqw1pO-QCq4Sk/sendMessage?chat_id=-1001740429824&text=" + content;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
        }
    }
}

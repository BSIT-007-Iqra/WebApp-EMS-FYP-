using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication1.Utils
{
    public class NotificationFunction
    {
        public static void SendMessage(string notification)
        {
            string serverKey = "AAAATQRMe5M:APA91bEtqTAchDBJOO5bsx-l7QHKZD6Uzz6zJ3koCl1KouQgLoY5QOCusMTh9Fed_sjzUdj5hJ7oyTJpXwbct6Zpao1sRb-ZzXHWxEWnE8bb7PN6fLLET4gknW7s4Rnn5Kq3wJBypSlX";
            var notificationInputDto = new
            {
                to = "cL7skDI9PAfmBsN-Ao2cZI:APA91bFu_rinOfAESjcKVzmEyq7Pn1q1V9VxKEeeQOzDlIObSzMG1j-FFotxGPcBdmCDFIwhkxDJ9sEtXowuXW6nht7oaspeIh4Z1rubUm0YurJVOLoD9k9LoUhZgLjbE9v0k-5wcTKF",
                notification = new
                {
                    body = notification,
                    title = "Readrix",
                    icon = "",
                    type = ""
                },
                data = new
                {
                    key1 = "value1",
                    key2 = "value2"
                }
            };
            try
            {
                var result = "";
                var webAddr = "https://fcm.googleapis.com/fcm/send";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(JsonConvert.SerializeObject(notificationInputDto));
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
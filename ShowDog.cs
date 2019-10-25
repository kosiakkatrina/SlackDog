using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SlackDogApp
{
    public class ShowDog
    {
        public static HttpListener httpListener = new HttpListener();

        public void Run()
        {
            httpListener.Prefixes.Add("http://localhost:5000/");
            httpListener.Start();
            while (true)
            {
                HttpListenerContext context = httpListener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string message = "{\n\"blocks\": [\n{\n\"type\": \"section\",\n\"text\": {\n\"type\": \"mrkdwn\",\n\"text\": \"*Look at all those chickens*\"\n}\n},\n{\n\"type\": \"image\",\n\"title\": {\n\"type\": \"plain_text\",\n\"text\": \"Pug\",\n\"emoji\": true\n},\n\"image_url\": \"https://www.petmd.com/sites/default/files/Acute-Dog-Diarrhea-47066074.jpg\",\n\"alt_text\": \"Acute-Dog-Diarrhea\"\n}\n]\n}";
                
                var body = new StreamReader(context.Request.InputStream).ReadToEnd();
                var bodyString = HttpUtility.UrlDecode(body);

                //Dictionary<string, string> dictionary = bodyString.Keys.Cast<string>()
                  //  .ToDictionary(k => k, k => bodyString[k]);

               // Console.WriteLine(dictionary.Values);
                
                
                byte[] responseArray = Encoding.UTF8.GetBytes(message);
                response.AddHeader("Content-type", "application/json");
                response.OutputStream.Write(responseArray, 0, responseArray.Length);
                response.KeepAlive = false;
                response.Close();
            }
            Console.Write("Hello everyone!");
        }
    }
}
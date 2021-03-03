using System;
using System.Linq;
using Riposte;
using Riposte.Sim;

namespace ShopTitansCheat.Utils
{
    class Log
    {
        private static readonly Lazy<Log> Lazy = new Lazy<Log>(() => new Log());
        public static Log Instance => Lazy.Value;

        Log()
        {

        }

        internal void PrintMessageInGame(string message, OverlayMessageControl.MessageType type)
        {
            Game.UI.overlayMessage.PushMessage(message, type);
        }

        public void Critical(object value)
        {
            Color(ConsoleColor.Magenta);
            Console.WriteLine("[!] " + value.ToString());
            Color();
        }

        public void Info(object value)
        {
            Color(ConsoleColor.Cyan);
            Console.WriteLine("[+] " + value.ToString());
        }

        public bool QueryYesNo(string question)
        {
            var input = QueryString(question);
            if (input.ToLower().StartsWith("y")) return true;
            else return false;
        }

        public string QueryString(string question)
        {
            Color(ConsoleColor.Yellow);
            Console.Write("[?] " + question);
            Color();
            return Console.ReadLine();
        }

        public void Info(object value, ConsoleColor color)
        {
            Color(color);
            Console.WriteLine("[+] " + value.ToString());
        }

        public void Error(object value)
        {
            Color(ConsoleColor.Red);
            Console.WriteLine("[-] " + value.ToString());
            Color();
        }

        public void Fatal(object value)
        {
            Error(value);
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void Color(ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
        }
    }
}

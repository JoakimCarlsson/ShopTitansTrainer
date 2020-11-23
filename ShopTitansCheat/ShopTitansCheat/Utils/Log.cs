using System;
using Riposte;

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

        internal void PrintConsoleMessage(string message, ConsoleColor backgroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{DateTime.Now:T} {message}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}

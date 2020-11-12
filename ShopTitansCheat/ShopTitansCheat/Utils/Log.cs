using System;
using Riposte;

namespace ShopTitansCheat.Utils
{
    class Log
    {
        internal static void PrintMessageInGame(string message, OverlayMessageControl.MessageType type)
        {
            Game.UI.overlayMessage.PushMessage(message, type);
        }

        internal static void PrintConsoleMessage(string message, ConsoleColor backgroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{DateTime.Now:T} {message}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}

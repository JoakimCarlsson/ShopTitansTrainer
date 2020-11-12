using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ShopTitansCheat.Utils
{
    public static class AllocConsoleHandler
    {
        [DllImport("Kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("msvcrt.dll")]
        public static extern int system(string cmd);

        public static void Open()
        {
            AllocConsole();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

            //If we want too see errors from unity;
            //Application.logMessageReceivedThreaded += (condition, stackTrace, type) => Console.WriteLine(condition + " " + stackTrace);
        }

        public static void ClearAllocConsole()
        {
            system("CLS");
        }
    }
}
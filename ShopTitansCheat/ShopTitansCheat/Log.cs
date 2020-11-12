using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;

namespace ShopTitansCheat
{
    class Log
    {
        internal static void PrintMessage(string message, OverlayMessageControl.MessageType type)
        {
            Game.UI.overlayMessage.PushMessage(message, type);
        }
    }
}

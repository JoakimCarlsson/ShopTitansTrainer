using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;

namespace ShopTitansCheat.Utils
{
    class GameHelpers
    {
        internal static bool StartCraft(string itemName)
        {
            if (dg.a0(Game.User.aof(), itemName).ar())
            {
                Game.SimManager.SendUserAction("CraftItem", new Dictionary<string, object>
                {
                    {
                        "item",
                        itemName
                    }

                });
                Log.Instance.PrintMessageInGame(string.Format(Game.Texts.GetText("craft_started"), Game.Texts.GetText(itemName)), OverlayMessageControl.MessageType.Neutral);
                return true;
            }
            return false;
        }
    }
}

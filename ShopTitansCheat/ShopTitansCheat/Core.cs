using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;

namespace ShopTitansCheat
{
    internal class Core
    {
        public static List<string> GetAllItems()
        {
            List<string> strs = new List<string>();
            foreach (GClass281 items in Game.User.observableDictionary_2.Values)
            {
                strs.Add(items.string_0);
            }

            return strs;
        }

        public static bool StartCraft(string itemName)
        {
            foreach (GClass281 gb in Game.User.observableDictionary_2.Values)
            {
                if (gb.string_0 == itemName)
                {
                    Game.Data.method_257(itemName);
                    Game.SimManager.SendUserAction("CraftItem", new Dictionary<string, object>
                    {
                        {
                            "item",
                            gb.string_0
                        }
                    });
                    Game.UI.overlayMessage.PushMessage(string.Format(Game.Texts.GetText("craft_started"),
                        Game.Texts.GetText(gb.string_0)));
                    Game.User.action_0();
                    return true;
                }
            }

            return false;
        }

        public static List<Equipment> PeekCraft(string craftName)
        {
            List<Equipment> equips = new List<Equipment>();
            foreach (var item in Game.User.observableDictionary_16.Values.Reverse())
            {
                if (item.string_0 != craftName)
                    continue;

                equips.Add(new Equipment(Game.Texts.GetText(item.string_0), (ItemQuality)item.int_0, item.bool_0));
            }

            return equips;
        }
    }
}

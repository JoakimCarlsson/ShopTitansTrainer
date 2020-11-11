using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using Riposte.Sim;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;

namespace ShopTitansCheat
{
    internal class Core
    {
        public static List<Equipment> GetAllItems()
        {
            List<Equipment> strs = new List<Equipment>();

            if (Game.PlayState != null)
            {
                foreach (GClass281 item in Game.User.observableDictionary_2.Values)
                {
                    strs.Add(new Equipment
                    {
                        ShortName = item.string_0,
                        FullName = Game.Texts.GetText(item.method_0()),
                    });
                }
            }

            return strs;
        }

        public static bool StartCraft(string itemName)
        {
            foreach (GClass281 items in Game.User.observableDictionary_2.Values)
            {
                if (items.string_0 == itemName)
                {
                    ItemData itemData = Game.Data.method_257(items.string_0);
                    GClass166.GClass350 test = GClass166.smethod_0(Game.SimManager.CurrentContext, itemData.Uid);
                    if (test.imethod_0() && !test.method_0())
                    {
                        Game.SimManager.SendUserAction("CraftItem", new Dictionary<string, object>
                        {
                        {
                            "item",
                            items.string_0
                        }});
                        Game.UI.overlayMessage.PushMessage(string.Format(Game.Texts.GetText("craft_started"),
                            Game.Texts.GetText(items.string_0)));
                        Game.User.action_0();
                        return true;
                    }

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
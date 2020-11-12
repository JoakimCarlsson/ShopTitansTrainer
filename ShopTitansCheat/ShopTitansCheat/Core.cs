using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using Riposte.Sim;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

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
                    //TODO MAKE SURE THIS WORKS.

                    if (GClass166.smethod_0(Game.User.vmethod_0(), items.string_0).imethod_0())
                    {
                        Game.SimManager.SendUserAction("CraftItem", new Dictionary<string, object>
                        {
                            {
                                "item",
                                items.string_0
                            }

                        });
                        Log.PrintMessage(string.Format(Game.Texts.GetText("craft_started"), Game.Texts.GetText(items.string_0)), OverlayMessageControl.MessageType.Neutral);
                        Game.User.action_0();
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool StoreFinishedItem(string craftName)
        {
            foreach (var item in Game.User.observableDictionary_16.Values.Reverse())
            {
                if (item.string_0 != craftName)
                    continue;
                if (true)
                {
                    Console.WriteLine(item.long_0);
                    Game.SimManager.SendUserAction("CraftStore", new Dictionary<string, object>
                    {
                        {
                            "craftId",
                            item.long_0
                        }
                    });
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

                equips.Add(new Equipment(Game.Texts.GetText(item.string_0), (ItemQuality) item.int_0, item.bool_0));
            }

            return equips;
        }

        public static IEnumerator Wait(int seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
    }
}
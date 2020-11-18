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

    internal class Core : MonoBehaviour
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
            using (IEnumerator<GClass281> enumerator = Game.User.observableDictionary_2.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    GClass281 current = enumerator.Current;
                    if (current != null && current.string_0 == itemName)
                    {
                        if (GClass166.smethod_0(Game.User.vmethod_0(), current.string_0).imethod_0())
                        {
                            Game.SimManager.SendUserAction("CraftItem", new Dictionary<string, object>
                        {
                            {
                                "item",
                                current.string_0
                            }

                        });
                            Log.PrintMessageInGame(string.Format(Game.Texts.GetText("craft_started"), Game.Texts.GetText(current.string_0)), OverlayMessageControl.MessageType.Neutral);
                            Game.User.action_0();
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public static List<Equipment> PeekCraft(string craftName)
        {
            List<Equipment> equips = new List<Equipment>();

            using (var enumerator = Game.User.observableDictionary_16.Values.Reverse().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    if (current.string_0 != craftName)
                        continue;

                    equips.Add(new Equipment(Game.Texts.GetText(current.string_0), (ItemQuality)current.int_0, current.bool_0));
                }
            }

            return equips;
        }

        public static void CollectGarbage()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
            Log.PrintConsoleMessage("Garbage collected", ConsoleColor.Cyan);
        }

        public static void Test()
        {
            Console.WriteLine($"GameObject: {Game.Instance.gameObject.activeSelf}");
            //Console.WriteLine($"SomethingElsehg: {}");
            //Game.Instance.GOCache.gameObject.SetActive(false);
            //Console.WriteLine(Game.Instance.GOCache.gameObject.activeSelf);
            //Destroy(Game.Instance.GOCache.gameObject);
            //Game.Instance.BaseHairStyleCache.gameObject.SetActive(false);
            //Game.Instance.FacialHairCache.gameObject.SetActive(false);
            //Game.Instance.GOChestCache.gameObject.SetActive(false);
            //Game.Instance.GOFurnitureCache.gameObject.SetActive(false);
            //Game.Instance.GOPetCache.gameObject.SetActive(false);
            //Game.Instance.GOItemCache.gameObject.SetActive(false);
            //Game.Instance.GOBossCache.gameObject.SetActive(false);
        }
    }
}
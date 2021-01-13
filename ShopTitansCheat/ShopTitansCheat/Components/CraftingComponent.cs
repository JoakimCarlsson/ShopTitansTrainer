using System;
using System.Collections.Generic;
using System.Linq;
using Riposte;
using Riposte.Sim;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;

namespace ShopTitansCheat.Components
{
    class CraftingComponent
    {
        private int _i = 1;

        internal void Craft()
        {
            foreach (Equipment item in Settings.Crafting.CraftingEquipmentsList)
            {
                if (item.Done)
                    continue;

                if (!StartCraft(item.ShortName))
                {
                    Log.Instance.PrintConsoleMessage($"Not enough for {item.FullName}, continuing", ConsoleColor.Red);
                    return;
                }

                Log.Instance.PrintConsoleMessage($"Sucesfully crafted {item.FullName}, {item.ItemQuality}", ConsoleColor.Green);
                item.Done = true;
                item.FullName = $"{item.FullName}, {item.Done}";

                if (Settings.Crafting.CraftingEquipmentsList.All(i => i.Done))
                {
                    Log.Instance.PrintConsoleMessage($"Crafted everything in list. \tRestarting.", ConsoleColor.Green);

                    foreach (Equipment equipment in Settings.Crafting.CraftingEquipmentsList)
                    {
                        equipment.FullName = equipment.FullName.Replace(", True", "");
                        equipment.Done = false;
                    }

                    return;
                }
            }
        }

        internal bool GlitchCraft()
        {
            foreach (Equipment item in Settings.Crafting.CraftingEquipmentsList)
            {
                if (item.Done)
                    continue;

                if (!StartCraft(item.ShortName))
                {
                    Log.Instance.PrintConsoleMessage($"Not enough resources for {item.FullName}, moving onto next item.", ConsoleColor.Red);
                    continue;
                }

                Equipment equipment = PeekCraft(item.ShortName)[0];

                if (equipment.ItemQuality >= item.ItemQuality)
                {
                    Log.Instance.PrintConsoleMessage($"{equipment}, Tries: {_i}", ConsoleColor.Green);

                    _i = 1;
                    item.Done = true;
                    item.FullName = $"{item.FullName}, {item.Done}";
                    return true;
                }

                Log.Instance.PrintConsoleMessage($"{equipment}, Tries: {_i++}", ConsoleColor.Yellow);
                Game.Instance.Restart();
                return false;
            }

            if (Settings.Crafting.CraftingEquipmentsList.All(i => i.Done))
            {
                Log.Instance.PrintConsoleMessage("We are done\n Stopping.", ConsoleColor.Green);
                return true;
            }

            return false;
        }

        internal bool CraftRandomStuffOverValue(int value)
        {
            List<gm> tmpList = Game.User.zs.Values.ToList();
            tmpList.Shuffle();
            foreach (gm blueprint in tmpList)
            {
                ItemData itemData = Game.Data.ep(blueprint.b0);
                string fullName = Game.Texts.GetText(blueprint.ai7());

                if (Settings.Crafting.CraftBookmarked)
                {
                    if (!blueprint.ck)
                        continue;
                }

                if (!Settings.Crafting.IncludeElements || !Settings.Crafting.IncludeRune)
                    if (itemData.Type == ItemData.ItemType.Tag || itemData.Type == ItemData.ItemType.Rune)
                        continue;
        
                if (itemData.Value < value)
                    continue;
        
                if (StartCraft(blueprint.b0))
                {
                    Log.Instance.PrintConsoleMessage($"started crafting: {blueprint.b0}", ConsoleColor.Green);
                    return true;
                }
        
                Log.Instance.PrintConsoleMessage($"not enough materials too craft: {fullName}", ConsoleColor.Red);
                
                return false;
            }
            return false;
        }

        private bool StartCraft(string itemName)
        {
            if (dg.a0(Game.User.aoe(), itemName).ar())
            {
                Game.SimManager.SendUserAction("CraftItem", new Dictionary<string, object>
                {
                    {
                        "item",
                        itemName
                    }

                });
                Log.Instance.PrintMessageInGame(string.Format(Game.Texts.GetText("craft_started"), Game.Texts.GetText(itemName)), OverlayMessageControl.MessageType.Neutral);
                //Game.User.action_0();
                return true;
            }
            return false;
        }

        private List<Equipment> PeekCraft(string craftName)
        {
            List<Equipment> equips = new List<Equipment>();

            using (IEnumerator<g6> enumerator = Game.User.z8.Values.Reverse().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    if (current.hm != craftName)
                        continue;

                    equips.Add(new Equipment(Game.Texts.GetText(current.hm), (ItemQuality)current.ht, current.hs));
                }
            }
            return equips;
        }
    }
}
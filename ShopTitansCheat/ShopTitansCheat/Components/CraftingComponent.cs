using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Riposte;
using Riposte.Sim;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

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
                    Log.Instance.PrintConsoleMessage($"Not enough resources for {item.FullName}", ConsoleColor.Red);
                    return true;
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
            List<GClass281> tmpList = Game.User.observableDictionary_2.Values.ToList();
            tmpList.Shuffle();
            foreach (GClass281 blueprint in tmpList)
            {
                ItemData itemData = Game.Data.method_257(blueprint.string_0);
                string fullName = Game.Texts.GetText(blueprint.method_0());

                if (!Settings.Crafting.IncludeElements || !Settings.Crafting.IncludeRune)
                    if (itemData.Type == ItemData.ItemType.Tag || itemData.Type == ItemData.ItemType.Rune)
                        continue;

                if (itemData.Value < value)
                    continue;

                if (StartCraft(blueprint.string_0))
                {
                    Log.Instance.PrintConsoleMessage($"started crafting: {blueprint.string_0}", ConsoleColor.Green);
                    return true;
                }

                Log.Instance.PrintConsoleMessage($"not enough materials too craft: {fullName}", ConsoleColor.Red);

                return false;
            }
            return false;
        }

        private bool StartCraft(string itemName)
        {
            if (GClass166.smethod_0(Game.User.vmethod_0(), itemName).imethod_0())
            {
                Game.SimManager.SendUserAction("CraftItem", new Dictionary<string, object>
                {
                    {
                        "item",
                        itemName
                    }

                });
                Log.Instance.PrintMessageInGame(string.Format(Game.Texts.GetText("craft_started"), Game.Texts.GetText(itemName)), OverlayMessageControl.MessageType.Neutral);
                Game.User.action_0();
                return true;
            }
            return false;
        }

        private List<Equipment> PeekCraft(string craftName)
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
    }
}
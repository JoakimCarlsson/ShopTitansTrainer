using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Riposte;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat.Components
{
    class CraftingComponent : MonoBehaviour
    {
        internal bool Crafting;
        internal bool RegularCrafting;
        internal List<Equipment> Items = new List<Equipment>();

        internal List<ItemQuality> ItemQualities = new List<ItemQuality>
        {
            ItemQuality.Uncommon,
            ItemQuality.Flawless,
            ItemQuality.Epic,
            ItemQuality.Legendary
        };

        private int _i = 1;
        private void Update()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            if (Crafting)
            {
                if (!RegularCrafting)
                    GlitchCraft();
            }
        }


        internal void StartRegularCraft(float i)
        {
            InvokeRepeating(nameof(Craft), 0, i);
        }

        internal void StopRegularCraft()
        {
            CancelInvoke(nameof(Craft));
        }

        private void Craft()
        {
            foreach (Equipment item in Items)
            {
                if (item.Done)
                    continue;

                if (!Core.StartCraft(item.ShortName))
                {
                    Log.PrintConsoleMessage($"Not enough for {item.FullName}, continuing", ConsoleColor.Red);
                    return;
                }

                Log.PrintConsoleMessage($"Sucesfully crafted {item.FullName}, {item.ItemQuality}", ConsoleColor.Green);
                item.Done = true;
                item.FullName = $"{item.FullName}, {item.Done}";

                if (Items.All(i => i.Done))
                {
                    Log.PrintConsoleMessage($"Crafted everything in list. \tRestarting.", ConsoleColor.Green);

                    foreach (Equipment equipment in Items)
                    {
                        equipment.FullName = equipment.FullName.Replace(", True", "");
                        equipment.Done = false;
                    }

                    return;
                }
            }
        }

        private void GlitchCraft()
        {
            foreach (Equipment item in Items)
            {
                if (item.Done)
                    continue;

                if (!Core.StartCraft(item.ShortName))
                {
                    Log.PrintConsoleMessage($"Not enough resources for {item.FullName}\tWaiting 20 seconds", ConsoleColor.Red);
                    StartCoroutine(WaitThenStart(20));
                    Crafting = false;
                    return;
                }
                Equipment equipment = Core.PeekCraft(item.ShortName)[0];

                if (equipment.ItemQuality >= item.ItemQuality)
                {
                    Log.PrintConsoleMessage($"{equipment}, Tries: {_i}", ConsoleColor.Green);

                    _i = 1;
                    item.Done = true;
                    item.FullName = $"{item.FullName}, {item.Done}";
                    Crafting = false;

                    StartCoroutine(WaitThenStart(20));

                    if (Items.All(i => i.Done))
                    {
                        Log.PrintConsoleMessage("We are done\n Stopping.", ConsoleColor.Green);
                        Crafting = false;
                    }

                    return;
                }
                Log.PrintConsoleMessage($"{equipment}, Tries: {_i++}", ConsoleColor.Yellow);
                Resources.UnloadUnusedAssets();
                GC.Collect();
                Game.Instance.Restart();
                return;
            }
        }

        private IEnumerator WaitThenStart(int seconds)
        {
            Log.PrintConsoleMessage($"We waited {seconds} seconds.", ConsoleColor.Blue);
            yield return new WaitForSeconds(seconds);
            Crafting = true;
        }
    }
}
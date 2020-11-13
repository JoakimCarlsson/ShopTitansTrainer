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

        private void Start()
        {
            //InvokeRepeating(nameof(Test), 0, 1);
        }

        private void Test()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        internal void StartRegularCraft(float repeatRate)
        {
            InvokeRepeating(nameof(Craft), 0, repeatRate);
        }

        internal void StopRegularCraft()
        {
            CancelInvoke(nameof(Craft));
        }

        internal void StartGlitchCraft(float repeatRate)
        {
            Crafting = true;
            InvokeRepeating(nameof(GlitchCraft), 0, repeatRate);
        }

        internal void StopGlitchCraft()
        {
            CancelInvoke(nameof(GlitchCraft));
        }

        private void Craft()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

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
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState" || !Crafting)
                return;

            foreach (Equipment item in Items)
            {
                if (item.Done)
                    continue;

                if (!Core.StartCraft(item.ShortName))
                {
                    Log.PrintMessageInGame("Not enough resources, please do something about that retard.", OverlayMessageControl.MessageType.Error);
                    Log.PrintConsoleMessage("Not enough resources, please do something about that retard.", ConsoleColor.Red);
                    Crafting = false;
                    Log.PrintConsoleMessage("Waiting 20 secounds then trying again.", ConsoleColor.Red);
                    StartCoroutine(WaitThenStart(20));
                    return;
                }
                Equipment equipment = Core.PeekCraft(item.ShortName)[0];

                if (equipment.ItemQuality >= item.ItemQuality)
                {
                    Log.PrintConsoleMessage($"{equipment}, Tries: {_i}", ConsoleColor.Green);

                    _i = 0;
                    item.Done = true;
                    item.FullName = $"{item.FullName}, {item.Done}";
                    Crafting = false;
                    StartCoroutine(WaitThenStart(20));

                    if (Items.All(i => i.Done))
                    {
                        Log.PrintConsoleMessage("We are done\n Stopping.", ConsoleColor.Green);
                        Crafting = false;
                        CancelInvoke(nameof(GlitchCraft));
                    }

                    return;
                }
                Resources.UnloadUnusedAssets();
                GC.Collect();
                Log.PrintConsoleMessage($"{equipment}, Tries: {_i++}", ConsoleColor.Yellow);
                Game.Instance.Restart();
                return;
            }
        }

        private IEnumerator WaitThenStart(int seconds)
        {
            Log.PrintConsoleMessage($"Waiting {seconds} seconds.", ConsoleColor.Green);
            yield return new WaitForSeconds(seconds);
            Crafting = true;
        }
    }
}
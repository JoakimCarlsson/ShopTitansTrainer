using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Riposte;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat.Components
{
    class CraftingComponent : MonoBehaviour
    {
        internal bool Crafting = false;
        internal List<Equipment> Items = new List<Equipment>();
        internal ItemQuality itemQuality;

        internal List<ItemQuality> itemQualities = new List<ItemQuality>
        {
            ItemQuality.Uncommon,
            ItemQuality.Flawless,
            ItemQuality.Epic,
            ItemQuality.Legendary
        };

        private int _i = 1;


        private void Update()
        {
            if (Crafting)
            {
                DoCraft();
            }
        }

        private void DoCraft()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
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
                    Log.PrintConsoleMessage("Stopping.", ConsoleColor.Red);
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

                    StartCoroutine(Wait(20));

                    if (Items.All(i => i.Done))
                    {
                        Log.PrintConsoleMessage("We are done\n Stopping.", ConsoleColor.Green);
                        if (equipment.FullName.Contains("True"))
                            equipment.FullName.Replace(" True", "");
                        Crafting = false;
                    }

                    return;
                }
                else
                {
                    Log.PrintConsoleMessage($"{equipment}, Tries: {_i++}", ConsoleColor.Yellow);
                    Game.Instance.Restart();
                    return;
                }
            }

        }

        private IEnumerator Wait(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            Log.PrintConsoleMessage($"We waited {seconds} seconds.", ConsoleColor.Green);
            Crafting = true;
        }
    }
}
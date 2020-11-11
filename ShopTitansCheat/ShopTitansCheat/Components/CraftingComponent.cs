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

        private int _i = 0;
        private string _craftItem = "flood";


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
                    Game.UI.overlayMessage.PushMessage($"Not enough resources, please do something about that retard.");
                    Crafting = false;
                    return;
                }
                Equipment equipment = Core.PeekCraft(item.ShortName)[0];

                Console.WriteLine($"{equipment} tries: {_i++}");

                if (equipment.ItemQuality >= item.ItemQuality)
                {
                    _i = 0;
                    item.Done = true;
                    item.FullName = $"{item.FullName}, {item.Done}";
                    Crafting = false;
                    Game.UI.overlayMessage.PushMessage($"crafted: {equipment}");


                    StartCoroutine(Wait(20));

                    if (Items.All(i => i.Done))
                    {
                        Console.WriteLine("We are done\n Stopping.");
                        Crafting = false;
                    }

                    return;
                }
                else
                {
                    Game.Instance.Restart();
                    return;
                }
            }

        }

        private IEnumerator Wait(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            Console.WriteLine($"We waited {seconds} seconds.");
            Crafting = true;
        }
    }
}
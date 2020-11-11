using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Riposte;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat.Components
{
    class CraftingComponent : MonoBehaviour
    {
        internal static bool Crafting = false;
        internal static List<Equipment> Items = new List<Equipment>();
        internal static ItemQuality itemQuality;

        internal static List<ItemQuality> itemQualities = new List<ItemQuality>
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
            foreach (Equipment item in Items)
            {
                if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                    return;

                if (item.Done)
                    continue;
                else
                    item.FullName = item.FullName;

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
                    StartCoroutine(Wait());

                    return;
                }
                else
                {
                    Game.Instance.Restart();
                    return;
                }
            }

        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(20);
            Console.WriteLine("WE WAITED FOR 20 SECONDS");
            Crafting = true;
        }
    }
}
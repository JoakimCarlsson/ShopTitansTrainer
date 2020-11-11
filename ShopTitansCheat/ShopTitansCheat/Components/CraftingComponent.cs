using System;
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
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            if (!Core.StartCraft(Items[0].ShortName))
            {
                Game.UI.overlayMessage.PushMessage($"Not enough resources, please do something about that retard.");
                Crafting = false;
                return;
            }
            Equipment equipment = Core.PeekCraft(Items[0].ShortName)[0];

            Console.WriteLine($"{equipment.ToString()} tries: {_i++}");

            if (equipment.ItemQuality >= ItemQuality.Flawless)
            {
                Console.WriteLine("WE ARE IN HERE FOR SOME REASON");
                Crafting = false;
                Game.UI.overlayMessage.PushMessage($"crafted: {equipment}");
            }
            else
            {
                Game.Instance.Restart();
            }
        }
    }
}
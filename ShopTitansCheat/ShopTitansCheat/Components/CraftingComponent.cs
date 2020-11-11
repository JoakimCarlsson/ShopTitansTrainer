using System;
using System.Collections.Generic;
using Riposte;
using ShopTitansCheat.Data;
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
            Console.WriteLine(Game.PlayState.CurrentViewState);

            if (Crafting)
            {
                DoCraft();
            }
        }

        private void DoCraft()
        {
            if (Game.PlayState == null)
                return;

            if (Core.StartCraft(Items[0].ShortName))
            {
                Equipment equipment = Core.PeekCraft(Items[0].ShortName)[0];

                Game.UI.overlayMessage.PushMessage($"{equipment} {_i}");

                if (equipment.ItemQuality >= itemQuality)
                {
                    Crafting = false;
                }
                else
                {
                    _i++;
                    Game.Instance.Restart();
                }
            }
        }
    }
}
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

            if (Core.StartCraft(Items[0].Name))
            {
                Equipment equipment = Core.PeekCraft(Items[0].Name)[0];

                Game.UI.overlayMessage.PushMessage(
                    $"{equipment.Name}, {equipment.ItemQuality}, {equipment.Double}, {_i}");

                if (equipment.ItemQuality >= ItemQuality.Flawless)
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
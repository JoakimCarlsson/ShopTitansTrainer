using System;
using System.Collections.Generic;
using Riposte;
using ShopTitansCheat.Data;
using UnityEngine;

namespace ShopTitansCheat.Components
{
    class CraftingComponent : MonoBehaviour
    {
        internal bool _doCraft = false;
        internal static List<string> Items = new List<string>();

        private int _i = 0;
        private string _craftItem = "flood";


        private void Update()
        {
            if (_doCraft)
            {
                DoCraft();
            }
        }

        private void DoCraft()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            if (Core.StartCraft(_craftItem))
            {
                Equipment equipment = Core.PeekCraft(_craftItem)[0];

                Game.UI.overlayMessage.PushMessage(
                    $"{equipment.Name}, {equipment.ItemQuality}, {equipment.Double}, {_i}");

                if (equipment.ItemQuality >= ItemQuality.Legendary)
                {
                    _doCraft = false;
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
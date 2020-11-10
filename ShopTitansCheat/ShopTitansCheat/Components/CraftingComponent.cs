using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Riposte;
using ShopTitansCheat.Data;
using UnityEngine;

namespace ShopTitansCheat.Components
{
    class CraftingComponent : MonoBehaviour
    {
        public static bool _doCraft = false;
        private List<string> items = new List<string>();

        private int _i = 0;
        private string _craftItem = "flood";
        internal void Test()
        {
            items = Core.GetAllItems();
            foreach (string item in items)
            {
                Console.WriteLine(item);
            }
        }

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

                Game.UI.overlayMessage.PushMessage($"{equipment.Name}, {equipment.ItemQuality}, {equipment.Double}, {_i}");

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    class AutoSellMenu : MonoBehaviour
    {
        private Rect _autoSellWindow;
        private bool _autoSellVisualVisible;

        private void Start()
        {
            _autoSellWindow = new Rect(790f, 250f, 250f, 150f);
        }

        private void OnGUI()
        {
            if (_autoSellVisualVisible)
            {
                _autoSellWindow = GUILayout.Window(100, _autoSellWindow, RenderUi, "Random Options.");
            }
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 100:
                    AutoSellMainMenu();
                    break;
            }
            GUI.DragWindow();
        }
        private void AutoSellMainMenu()
        {
            GUILayout.Label("Auto Sell");
            Settings.AutoSell.AutoSellToNpc = GUILayout.Toggle(Settings.AutoSell.AutoSellToNpc, "Auto Sell");
            Settings.AutoSell.SmallTalk = GUILayout.Toggle(Settings.AutoSell.SmallTalk, "Small Talk");
            Settings.AutoSell.Refuse = GUILayout.Toggle(Settings.AutoSell.Refuse, "Refuse Items");
            Settings.AutoSell.SurchargeDiscount = GUILayout.Toggle(Settings.AutoSell.SurchargeDiscount, "Surcharge Or Discount");
            Settings.AutoSell.Suggest = GUILayout.Toggle(Settings.AutoSell.Suggest, "Suggest");
            Settings.AutoSell.BuyFromNpc = GUILayout.Toggle(Settings.AutoSell.BuyFromNpc, "Buy From NPC");

            //GUILayout.Label($"Surcharge Over {(int)Settings.AutoSell.SurchargeAmount}");
            //Settings.AutoSell.SurchargeAmount = (int)GUILayout.HorizontalSlider(Settings.AutoSell.SurchargeAmount, 0, 1000000);

            //GUILayout.Label($"Discount  Under {(int)Settings.AutoSell.DiscountAmount}");
            //Settings.AutoSell.DiscountAmount = (int)GUILayout.HorizontalSlider(Settings.AutoSell.DiscountAmount, 0, 1000000);
        }

        public void Show()
        {
            _autoSellVisualVisible = !_autoSellVisualVisible;
        }
    }
}

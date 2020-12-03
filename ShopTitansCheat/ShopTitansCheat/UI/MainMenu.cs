using System;
using System.Collections.Generic;
using Riposte;
using ShopTitansCheat.Components;
using ShopTitansCheat.UI;
using UnityEngine;

namespace ShopTitansCheat
{
    public class Menu : MonoBehaviour
    {
        private bool _visible = true;

        private CraftingMenu _craftingMenu;
        private SkillMenu _skillMenu;

        private Rect _mainWindow;
        //Options
        private Rect _optionsListWindow;
        //Auto sell
        private Rect _autoSellWindow;

        //options
        private bool _optionsListWindowVisualVisible;
        //autosell
        private bool _autoSellVisualVisible;

        private readonly string _watermark = "Shop Titans Bot 0.24b";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);
            //options
            _optionsListWindow = new Rect(1050f, 250f, 250f, 150f);
            //auto sell
            _autoSellWindow = new Rect(790f, 250f, 250f, 150f);
            //skills.

            _skillMenu = Game.Instance.gameObject.AddComponent<SkillMenu>();
            _craftingMenu = Game.Instance.gameObject.AddComponent<CraftingMenu>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;

            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;
        }

        private void OnGUI()
        {
            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, _watermark);

            if (_optionsListWindowVisualVisible)
            {
                _optionsListWindow = GUILayout.Window(5, _optionsListWindow, RenderUi, "Random Options.");
            }

            if (_autoSellVisualVisible)
            {
                _autoSellWindow = GUILayout.Window(6, _autoSellWindow, RenderUi, "Random Options.");
            }
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 0:
                    MainMenu();
                    break;

                case 5:
                    RandomOptionsMenu();
                    break;

                case 6:
                    AutoSellMenu();
                    break;
            }

            GUI.DragWindow();
        }

        private void AutoSellMenu()
        {
            GUILayout.Label("Auto Sell");
            Settings.AutoSell.AutoSellToNpc = GUILayout.Toggle(Settings.AutoSell.AutoSellToNpc, "Auto Sell");
            Settings.AutoSell.SmallTalk = GUILayout.Toggle(Settings.AutoSell.SmallTalk, "Small Talk");
            Settings.AutoSell.Refuse = GUILayout.Toggle(Settings.AutoSell.Refuse, "Refuse Items");
            Settings.AutoSell.SurchargeDiscount = GUILayout.Toggle(Settings.AutoSell.SurchargeDiscount, "Surcharge Or Discount");
            Settings.AutoSell.Suggest = GUILayout.Toggle(Settings.AutoSell.Suggest, "Suggest");
            Settings.AutoSell.BuyFromNpc = GUILayout.Toggle(Settings.AutoSell.BuyFromNpc, "Buy From NPC");

            GUILayout.Label($"Surcharge Over {(int)Settings.AutoSell.SurchargeAmount}");
            Settings.AutoSell.SurchargeAmount = (int)GUILayout.HorizontalSlider(Settings.AutoSell.SurchargeAmount, 0, 1000000);

            GUILayout.Label($"Discount  Under {(int)Settings.AutoSell.DiscountAmount}");
            Settings.AutoSell.DiscountAmount = (int)GUILayout.HorizontalSlider(Settings.AutoSell.DiscountAmount, 0, 1000000);
        }

        private void RandomOptionsMenu()
        {
            GUILayout.Label("Random Options");
            Settings.Misc.AutoFinishCraft = GUILayout.Toggle(Settings.Misc.AutoFinishCraft, "Finish Crafts");

            Settings.Misc.UseEnergy = GUILayout.Toggle(Settings.Misc.UseEnergy, $"Use Energy Over {(int)Settings.Misc.UseEnergyAmount}");

            if (Settings.Misc.UseEnergy)
                if (Game.User != null)
                    Settings.Misc.UseEnergyAmount = GUILayout.HorizontalSlider(Settings.Misc.UseEnergyAmount, 0, Game.User.method_45());

            Settings.Misc.RemoveWindowPopup = GUILayout.Toggle(Settings.Misc.RemoveWindowPopup, "Remove Window pop up.");
        }



        private void MainMenu()
        {
            if (GUILayout.Button("Crafting Component"))
            {
                _craftingMenu.Show();
            }

            if (GUILayout.Button("Skill Component"))
            {
                _skillMenu.Show();
            }

            if (GUILayout.Button("Quest Component"))
            {

            }

            if (GUILayout.Button("Enchantment Component"))
            {

            }

            if (GUILayout.Button("Chest Component"))
            {

            }

            if (GUILayout.Button("Auto Component"))
            {
                _autoSellVisualVisible = !_autoSellVisualVisible;
            }

            if (GUILayout.Button("Options"))
            {
                _optionsListWindowVisualVisible = !_optionsListWindowVisualVisible;
            }

            GUI.color = Color.red;
            if (GUILayout.Button("Click me"))
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
            }
            GUI.color = Color.white;
        }
    }
}
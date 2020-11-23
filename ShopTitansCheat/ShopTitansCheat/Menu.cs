using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Riposte;
using Riposte.Sim;
using ShopTitansCheat.Components;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;
using UnityEngine.Profiling;

namespace ShopTitansCheat
{
    public class Menu : MonoBehaviour
    {
        private bool _visible = true;

        private Rect _mainWindow;
        private Rect _craftingWindow;
        private Rect _craftingListWindow;
        private Rect _myCraftingListWindow;
        private Rect _qualityListWindow;
        private Rect _optionsListWindow;
        private Rect _autoSellWindow;

        private bool _autoSellVisualVisible;
        private bool _optionsListWindowVisualVisible;
        private bool _craftingVisualVisible;
        private bool _craftingListVisualVisible;
        private bool _myCraftingListVisualVisible;
        private bool _qualityListVisualVisible;

        private string _searchText = "";

        private readonly string _watermark = "Shop Titans Bot 0.21";
        private List<Equipment> _bluePrints = new List<Equipment>();

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);
            _craftingWindow = new Rect(270f, 60f, 250f, 150f);
            _craftingListWindow = new Rect(530f, 60f, 250f, 150f);
            _myCraftingListWindow = new Rect(790f, 60f, 250f, 150f);
            _qualityListWindow = new Rect(1050f, 60f, 250f, 150f);
            _optionsListWindow = new Rect(1050f, 250f, 250f, 150f);
            _autoSellWindow = new Rect(790f, 250f, 250f, 150f);
        }

        private void Update()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;

            if (_bluePrints.Count == 0)
                _bluePrints = GetAllItems();
        }

        private void OnGUI()
        {
            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, _watermark);

            if (_craftingVisualVisible)
            {
                _craftingWindow = GUILayout.Window(1, _craftingWindow, RenderUi, "Crafting Window");
                _craftingListWindow = GUILayout.Window(2, _craftingListWindow, RenderUi, "Crafting Window");
                _myCraftingListWindow = GUILayout.Window(3, _myCraftingListWindow, RenderUi, "Crafting Window");
                _qualityListWindow = GUILayout.Window(4, _qualityListWindow, RenderUi, "Quality Window");
            }

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

                case 1:
                    CraftingMenu();
                    break;

                case 2:
                    CraftingListMenu();
                    break;

                case 3:
                    ItemsToCraftMenu();
                    break;

                case 4:
                    SelectQualityMenu();
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
                    Settings.Misc.UseEnergyAmount = GUILayout.HorizontalSlider(Settings.Misc.UseEnergyAmount, 0, Game.User.method_39());

            Settings.Misc.RemoveWindowPopup = GUILayout.Toggle(Settings.Misc.RemoveWindowPopup, "Remove Window pop up.");
        }

        private void SelectQualityMenu()
        {
            GUILayout.Label("Select Quality");
            foreach (ItemQuality itemQuality in Settings.Crafting.ItemQualities)
            {
                if (GUILayout.Button(itemQuality.ToString()))
                {
                    Settings.Crafting.CraftingEquipmentsList.Last().ItemQuality = itemQuality;
                }
            }
        }

        private void ItemsToCraftMenu()
        {
            GUILayout.Label("Items To Craft");
            foreach (Equipment item in Settings.Crafting.CraftingEquipmentsList)
            {
                if (GUILayout.Button($"{item.FullName}, {item.ItemQuality}"))
                {
                    Settings.Crafting.CraftingEquipmentsList.Remove(item);
                }
            }
        }

        private void CraftingListMenu()
        {
            GUILayout.Label("Crafting List");
            _searchText = GUILayout.TextField(_searchText, 15, "textfield");

            foreach (Equipment item in _bluePrints)
            {
                if (!string.IsNullOrEmpty(_searchText))
                    if (!item.FullName.Contains(_searchText))
                        continue;

                if (GUILayout.Button(item.FullName))
                {
                    Settings.Crafting.CraftingEquipmentsList.Add(new Equipment
                    {
                        FullName = item.FullName,
                        ShortName = item.ShortName,
                        Done = false,
                    });
                }
            }
        }

        private void CraftingMenu()
        {
            GUILayout.Label("Crafting");



            GUILayout.Label("Item List 1");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save configuration"))
                Config.SaveCraftingList("equip");

            if (GUILayout.Button("Load configuration"))
                Config.LoadCraftingList("equip");
            GUILayout.EndHorizontal();

            GUILayout.Label("Item List 2");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save configuration"))
                Config.SaveCraftingList("regular");

            if (GUILayout.Button("Load configuration"))
                Config.LoadCraftingList("regular");

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("Toggle me if you want normal crafting");
            Settings.Crafting.RegularCrafting = GUILayout.Toggle(Settings.Crafting.RegularCrafting, "Regular craft.");

            GUILayout.Label("Will craft random items.");
            Settings.Crafting.CraftRandomStuff = GUILayout.Toggle(Settings.Crafting.CraftRandomStuff, $"Craft Random Stuff Over Value {Settings.Crafting.CraftRandomStuffValue}");
            if (Settings.Crafting.CraftRandomStuff)
            {
                Settings.Crafting.CraftRandomStuffValue = (int)GUILayout.HorizontalSlider(Settings.Crafting.CraftRandomStuffValue, 0, 1000000);
                Settings.Crafting.IncludeElements = GUILayout.Toggle(Settings.Crafting.IncludeElements, "Include Elements");
                Settings.Crafting.IncludeRune = GUILayout.Toggle(Settings.Crafting.IncludeRune, "Include Runes");
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Start"))
            {
                if (Settings.Crafting.CraftingEquipmentsList.Count == 0)
                {
                    Log.PrintMessageInGame($"Please add items..", OverlayMessageControl.MessageType.Error);
                }
                else
                {
                    Settings.Crafting.ThisIsATempBool = true;
                    Settings.Crafting.DoCrafting = true;
                }
            }

            if (GUILayout.Button("Stop"))
            {
                Settings.Crafting.DoCrafting = false;
                Settings.Crafting.ThisIsATempBool = false;

                foreach (Equipment equipment in Settings.Crafting.CraftingEquipmentsList)
                {
                    equipment.FullName = equipment.FullName.Replace(", True", "");
                    equipment.Done = false;
                }
            }
            GUILayout.EndHorizontal();
        }

        private void MainMenu()
        {
            if (GUILayout.Button("Crafting"))
            {
                _craftingVisualVisible = !_craftingVisualVisible;
                _myCraftingListVisualVisible = !_myCraftingListVisualVisible;
                _craftingListVisualVisible = !_craftingListVisualVisible;
                _qualityListVisualVisible = !_qualityListVisualVisible;
            }

            if (GUILayout.Button("Options"))
            {
                _optionsListWindowVisualVisible = !_optionsListWindowVisualVisible;
            }

            if (GUILayout.Button("Auto Sell Options"))
            {
                _autoSellVisualVisible = !_autoSellVisualVisible;
            }

            if (GUILayout.Button("Lower Performance."))
            {
                Application.targetFrameRate = 10;
                Application.backgroundLoadingPriority = ThreadPriority.High;
            }

            if (GUILayout.Button("Higher Performance."))
            {
                Application.targetFrameRate = 60;
                Application.backgroundLoadingPriority = ThreadPriority.High;
            }
        }

        public static List<Equipment> GetAllItems()
        {
            List<Equipment> strs = new List<Equipment>();
            if (Game.PlayState != null)
            {
                foreach (GClass281 item in Game.User.observableDictionary_2.Values)
                {
                    strs.Add(new Equipment
                    {
                        ShortName = item.string_0,
                        FullName = Game.Texts.GetText(item.method_0()),
                    });
                }
            }

            return strs;
        }
    }
}
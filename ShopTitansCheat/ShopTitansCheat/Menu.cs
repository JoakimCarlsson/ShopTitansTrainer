using System;
using System.Collections.Generic;
using Riposte;
using ShopTitansCheat.Components;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat
{
    public class Menu : MonoBehaviour
    {
        private bool _visible = true;

        private Rect _mainWindow;
        //crafting
        private Rect _craftingWindow;
        private Rect _craftingListWindow;
        private Rect _myCraftingListWindow;
        private Rect _qualityListWindow;
        //Options
        private Rect _optionsListWindow;
        //Auto sell
        private Rect _autoSellWindow;
        //Skills
        private Rect _skillWindow;
        private Rect _heroListWindow;
        private Rect _heroSkillsWindow;
        private Rect _xpItemsListWindow;


        //crafting
        private bool _craftingVisualVisible;
        private bool _craftingListVisualVisible;
        private bool _myCraftingListVisualVisible;
        private bool _qualityListVisualVisible;
        //options
        private bool _optionsListWindowVisualVisible;
        //autosell
        private bool _autoSellVisualVisible;
        //Skills
        private bool _skillVisualVisible;
        private bool _heroListVisualVisible;
        private bool _heroSkillVisualVisible;
        private bool _xpItemsListVisualVisible;

        private string _searchText = "";
        private string _priceText = "0";

        private readonly string _watermark = "Shop Titans Bot 0.24b";
        private List<Equipment> _bluePrints = new List<Equipment>();

        private List<string> _heroList = new List<string>();
        private List<string> _heroSkillsList = new List<string>();
        private List<string> _xpItemsList = new List<string>();

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);
            //crafting
            _craftingWindow = new Rect(270f, 60f, 250f, 150f);
            _craftingListWindow = new Rect(530f, 60f, 250f, 150f);
            _myCraftingListWindow = new Rect(790f, 60f, 250f, 150f);
            _qualityListWindow = new Rect(1050f, 60f, 250f, 150f);
            //options
            _optionsListWindow = new Rect(1050f, 250f, 250f, 150f);
            //auto sell
            _autoSellWindow = new Rect(790f, 250f, 250f, 150f);
            //skills.
            _xpItemsListWindow = new Rect(1050f, 60f, 250f, 150f);
            _heroSkillsWindow = new Rect(790f, 60f, 250f, 150f);
            _heroListWindow = new Rect(530f, 60f, 250f, 150f);
            _skillWindow = new Rect(270f, 60f, 250f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;

            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            if (_bluePrints.Count == 0)
                _bluePrints = GetAllItems();

            if (_heroList.Count == 0)
                _heroList = SkillComponent.GetHeros();

            if (_heroSkillsList.Count == 0)
                _heroSkillsList = SkillComponent.GetSkills();

            if (_xpItemsList.Count == 0)
                _xpItemsList = SkillComponent.FindItems("XP Drink");
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

            if (_skillVisualVisible)
            {
                _skillWindow = GUILayout.Window(7, _skillWindow, RenderUi, "Skill Window");
                _heroListWindow = GUILayout.Window(8, _heroListWindow, RenderUi, "Hero List Window");
                _heroSkillsWindow = GUILayout.Window(9, _heroSkillsWindow, RenderUi, "Hero Skill Window");
                _xpItemsListWindow = GUILayout.Window(10, _xpItemsListWindow, RenderUi, "Xp Items Window");
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

                case 7:
                    SkillMenu();
                    break;

                case 8:
                    HeroMenuList();
                    break;

                case 9:
                    SkillsMenuList();
                    break;

                case 10:
                    XpItemsMenuList();
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

        private void SkillMenu()
        {
            GUILayout.Label("Main skills menu");

        }

        private void HeroMenuList()
        {
            GUILayout.Label("Heros");
            foreach (string s in _heroList)
            {
                GUILayout.Button(s);
            }
        }

        private void SkillsMenuList()
        {
            GUILayout.Label("Skill");

            foreach (string s in _heroSkillsList)
            {
                GUILayout.Toggle(false, s);
            }
        }

        private void XpItemsMenuList()
        {
            GUILayout.Label("XP Items.");
            foreach (string s in _xpItemsList)
            {
                GUILayout.Toggle(false, s);
            }
        }

        private void CraftingMenu()
        {
            GUILayout.Label("Crafting");
            GUILayout.Label("Item List 1");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save configuration"))
                Config.Instance.SaveCraftingList("equip");

            if (GUILayout.Button("Load configuration"))
                Config.Instance.LoadCraftingList("equip");
            GUILayout.EndHorizontal();

            GUILayout.Label("Item List 2");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save configuration"))
                Config.Instance.SaveCraftingList("regular");

            if (GUILayout.Button("Load configuration"))
                Config.Instance.LoadCraftingList("regular");

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("Toggle me if you want normal crafting");
            Settings.Crafting.RegularCrafting = GUILayout.Toggle(Settings.Crafting.RegularCrafting, "Regular craft.");

            GUILayout.Label("Will craft random items.");
            Settings.Crafting.CraftRandomStuff = GUILayout.Toggle(Settings.Crafting.CraftRandomStuff, $"Craft Random Stuff Over Value {Settings.Crafting.CraftRandomStuffValue}");
            if (Settings.Crafting.CraftRandomStuff)
            {
                //TODO IMPLEMENT TEXTBOX
                _priceText = GUILayout.TextField(_priceText, 15, "textfield");
                Settings.Crafting.CraftRandomStuffValue = int.Parse(_priceText);
                Settings.Crafting.IncludeElements = GUILayout.Toggle(Settings.Crafting.IncludeElements, "Include Elements");
                Settings.Crafting.IncludeRune = GUILayout.Toggle(Settings.Crafting.IncludeRune, "Include Runes");
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Start"))
            {
                if (Settings.Crafting.CraftingEquipmentsList.Count == 0)
                {
                    Log.Instance.PrintMessageInGame($"Please add items..", OverlayMessageControl.MessageType.Error);
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
            if (GUILayout.Button("Crafting Component"))
            {
                _craftingVisualVisible = !_craftingVisualVisible;
                _myCraftingListVisualVisible = !_myCraftingListVisualVisible;
                _craftingListVisualVisible = !_craftingListVisualVisible;
                _qualityListVisualVisible = !_qualityListVisualVisible;
            }

            if (GUILayout.Button("Skill Component"))
            {
                _skillVisualVisible = !_skillVisualVisible;
                _heroListVisualVisible = !_heroListVisualVisible;
                _heroSkillVisualVisible = !_heroSkillVisualVisible;
                _xpItemsListVisualVisible = !_xpItemsListVisualVisible;

                Console.WriteLine("----------Items---------");

                foreach (GClass338 item in Game.User.observableDictionary_1.Values)
                {
                    if (Game.Texts.GetText(item.string_0 + "_name").Contains("XP Drink"))
                    {
                        Console.WriteLine(Game.Texts.GetText(item.string_0 + "_name"));
                    }
                }

                //Console.WriteLine("----------Skills---------");
                //foreach (KeyValuePair<string, object> textsDatum in Game.DataLoader.TextsData)
                //{
                //    string key = textsDatum.Key;
                //    if (key.StartsWith("skill_") && key.EndsWith("_name"))
                //    {
                //        Console.WriteLine(textsDatum.Value as string);
                //    }
                //}
                //Console.WriteLine("----------Heros---------");
                //foreach (GClass282 hero in Game.User.observableDictionary_4.Values)
                //{
                //    Console.WriteLine(hero.Name);
                //}
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

        public List<Equipment> GetAllItems()
        {
            List<Equipment> strs = new List<Equipment>();
            if (Game.PlayState != null)
            {
                foreach (GClass287 item in Game.User.observableDictionary_2.Values)
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
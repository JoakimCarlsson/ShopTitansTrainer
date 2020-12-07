using System.Collections.Generic;
using System.Linq;
using Riposte;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    class CraftingMenu : MonoBehaviour
    {
        private Rect _craftingWindow;
        private Rect _craftingListWindow;
        private Rect _myCraftingListWindow;
        private Rect _qualityListWindow;

        private bool _craftingVisualVisible;

        private string _priceText = "0";
        private string _searchText = "";

        private List<Equipment> _bluePrints = new List<Equipment>();

        private void Start()
        {
            _craftingWindow = new Rect(270f, 60f, 250f, 150f);
            _craftingListWindow = new Rect(530f, 60f, 250f, 150f);
            _myCraftingListWindow = new Rect(790f, 60f, 250f, 150f);
            _qualityListWindow = new Rect(1050f, 60f, 250f, 150f);
        }

        private void Update()
        {
            if (_bluePrints.Count == 0)
                _bluePrints = GetAllItems();
        }

        private void OnGUI()
        {
            if (_craftingVisualVisible)
            {
                _craftingWindow = GUILayout.Window(1, _craftingWindow, RenderUi, "Crafting Window");
                _craftingListWindow = GUILayout.Window(2, _craftingListWindow, RenderUi, "Crafting Window");
                _myCraftingListWindow = GUILayout.Window(3, _myCraftingListWindow, RenderUi, "Crafting Window");
                _qualityListWindow = GUILayout.Window(4, _qualityListWindow, RenderUi, "Quality Window");
            }
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 1:
                    MainCraftingMenu();
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
            }
            GUI.DragWindow();

        }
        private void MainCraftingMenu()
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

                _priceText = GUILayout.TextField(_priceText, 15, "textfield");
                int.TryParse(_priceText, out int number);
                Settings.Crafting.CraftRandomStuffValue = number;
                Settings.Crafting.CraftBookmarked = GUILayout.Toggle(Settings.Crafting.CraftBookmarked, "Only Bookmarked");
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


        private void SelectQualityMenu()
        {
            GUILayout.Label("Select Quality");
            foreach (ItemQuality itemQuality in Settings.Crafting.ItemQualities)
            {
                if (GUILayout.Button(itemQuality.ToString()))
                {
                    Enumerable.Last(Settings.Crafting.CraftingEquipmentsList).ItemQuality = itemQuality;
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

        private List<Equipment> GetAllItems()
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

        public void Show()
        {
            _craftingVisualVisible = !_craftingVisualVisible;
        }
    }
}

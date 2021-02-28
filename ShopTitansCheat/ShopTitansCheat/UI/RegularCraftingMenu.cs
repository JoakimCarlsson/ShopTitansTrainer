using System.Collections.Generic;
using System.Linq;
using Riposte;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    class RegularCraftingMenu : MonoBehaviour
    {
        private Rect _craftingWindow;
        private Rect _craftingListWindow;
        private Rect _myCraftingListWindow;

        private bool _craftingVisualVisible;

        private string _priceText = "0";
        private string _searchText = "";

        private List<Equipment> _bluePrints = new List<Equipment>();

        private void Start()
        {
            _craftingWindow = new Rect(270f, 60f, 250f, 150f);
            _craftingListWindow = new Rect(530f, 60f, 250f, 150f);
            _myCraftingListWindow = new Rect(790f, 60f, 250f, 150f);
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
                _craftingListWindow = GUILayout.Window(2, _craftingListWindow, RenderUi, "Blueprint List Window");
                _myCraftingListWindow = GUILayout.Window(3, _myCraftingListWindow, RenderUi, "Crafting List Window");
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
            }
            GUI.DragWindow();

        }
        private void MainCraftingMenu()
        {
            GUILayout.Label("TestRegularCrafting");
            GUILayout.Label("Item List 1");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save configuration"))
                Config.Instance.SaveCraftingList("equip");

            if (GUILayout.Button("Load configuration"))
                Config.Instance.LoadCraftingList("equip");
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            Settings.RegularCrafting.CraftRandomItems = GUILayout.Toggle(Settings.RegularCrafting.CraftRandomItems, $"Craft Random Stuff Over Value {Settings.RegularCrafting.CraftRandomStuffValue}");
            if (Settings.RegularCrafting.CraftRandomItems)
            {
                _priceText = GUILayout.TextField(_priceText, 15, "textfield");
                int.TryParse(_priceText, out int number);
                Settings.RegularCrafting.CraftRandomStuffValue = number;
                Settings.RegularCrafting.CraftBookmarked = GUILayout.Toggle(Settings.RegularCrafting.CraftBookmarked, "Only Bookmarked");
                Settings.RegularCrafting.IncludeElements = GUILayout.Toggle(Settings.RegularCrafting.IncludeElements, "Include Elements");
                Settings.RegularCrafting.IncludeRune = GUILayout.Toggle(Settings.RegularCrafting.IncludeRune, "Include Runes");
            }

            GUILayout.Space(20);

            Settings.RegularCrafting.CraftBookmarked = GUILayout.Toggle(Settings.RegularCrafting.CraftBookmarked, $"Craft Bookmarked Items");

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Start"))
            {
                if (Settings.RegularCrafting.CraftingEquipmentsList.Count == 0)
                {
                    Log.Instance.PrintMessageInGame($"Please add items..", OverlayMessageControl.MessageType.Error);
                }
                else
                {
                    Settings.RegularCrafting.DoCrafting = true;
                    Settings.RegularCrafting.ThisIsATempBool = true;
                    Settings.RegularCrafting.DoCrafting = true;
                }
            }

            if (GUILayout.Button("Stop"))
            {
                Settings.RegularCrafting.DoCrafting = false;
                Settings.RegularCrafting.ThisIsATempBool = false;
                Settings.RegularCrafting.DoCrafting = false;

                foreach (Equipment equipment in Settings.RegularCrafting.CraftingEquipmentsList)
                {
                    equipment.FullName = equipment.FullName.Replace(", True", "");
                    equipment.Done = false;
                }
            }
            GUILayout.EndHorizontal();
        }

        private void ItemsToCraftMenu()
        {
            GUILayout.Label("Items To Craft");
            foreach (Equipment item in Settings.RegularCrafting.CraftingEquipmentsList)
            {
                if (GUILayout.Button($"{item.FullName}, {item.ItemQuality}"))
                {
                    Settings.RegularCrafting.CraftingEquipmentsList.Remove(item);
                }
            }
        }

        private void CraftingListMenu()
        {
            GUILayout.Label("Regular Crafting List");
            _searchText = GUILayout.TextField(_searchText, 15, "textfield");

            foreach (Equipment item in _bluePrints)
            {
                if (!string.IsNullOrEmpty(_searchText))
                    if (!item.FullName.Contains(_searchText))
                        continue;

                if (GUILayout.Button(item.FullName))
                {
                    Settings.RegularCrafting.CraftingEquipmentsList.Add(new Equipment
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
                foreach (var item in Game.User.zt.Values)
                {
                    strs.Add(new Equipment
                    {
                        ShortName = item.b0,
                        FullName = Game.Texts.GetText(item.ai8()),
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

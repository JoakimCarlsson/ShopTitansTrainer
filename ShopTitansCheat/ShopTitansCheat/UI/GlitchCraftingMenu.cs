using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    class GlitchCraftingMenu : MonoBehaviour
    {
        private Rect _craftingWindow;
        private Rect _craftingListWindow;
        private Rect _myCraftingListWindow;
        private Rect _qualityListWindow;

        private bool _craftingVisualVisible;

        private string _searchText = "";

        private List<Equipment> _bluePrints = new List<Equipment>();

        private void Start()
        {
            _craftingWindow = new Rect(270f, 60f, 250f, 150f);
            _craftingListWindow = new Rect(530f, 60f, 250f, 150f);
            _myCraftingListWindow = new Rect(790f, 60f, 250f, 150f);
            _qualityListWindow = new Rect(1050f, 60f, 250f, 150f);
        }

        private void OnGUI()
        {
            if (_craftingVisualVisible)
            {
                _craftingWindow = GUILayout.Window(1, _craftingWindow, RenderUi, "TestRegularCrafting Window");
                _craftingListWindow = GUILayout.Window(2, _craftingListWindow, RenderUi, "TestRegularCrafting Window");
                _myCraftingListWindow = GUILayout.Window(3, _myCraftingListWindow, RenderUi, "TestRegularCrafting Window");
                _qualityListWindow = GUILayout.Window(4, _qualityListWindow, RenderUi, "Quality Window");
            }
        }

        private void Update()
        {
            if (_bluePrints.Count == 0)
                _bluePrints = GetAllItems();
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
            GUILayout.Label("TestRegularCrafting");
            GUILayout.Label("Item List 1");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save configuration"))
                Config.Instance.SaveCraftingList("Glitch Equip");

            if (GUILayout.Button("Load configuration"))
                Config.Instance.LoadCraftingList("Glitch Equip", true);

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Start"))
            {
                if (Settings.GlitchCrafting.CraftingEquipmentsList.Count == 0)
                {
                    Log.Instance.PrintMessageInGame($"Please add items..", OverlayMessageControl.MessageType.Error);
                }
                else
                {
                    Settings.GlitchCrafting.ShouldCraft = true;
                    Settings.GlitchCrafting.DoCrafting = true;
                }
            }

            if (GUILayout.Button("Stop"))
            {
                Settings.GlitchCrafting.DoCrafting = false;
                Settings.GlitchCrafting.ShouldCraft = false;

                foreach (Equipment equipment in Settings.GlitchCrafting.CraftingEquipmentsList)
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
            foreach (ItemQuality itemQuality in Settings.GlitchCrafting.ItemQualities)
            {
                if (GUILayout.Button(itemQuality.ToString()))
                {
                    Enumerable.Last(Settings.GlitchCrafting.CraftingEquipmentsList).ItemQuality = itemQuality;
                }
            }
        }

        private void ItemsToCraftMenu()
        {
            GUILayout.Label("Items To Craft");
            foreach (Equipment item in Settings.GlitchCrafting.CraftingEquipmentsList)
            {
                if (GUILayout.Button($"{item.FullName}, {item.ItemQuality}"))
                {
                    Settings.GlitchCrafting.CraftingEquipmentsList.Remove(item);
                }
            }
        }

        private void CraftingListMenu()
        {
            GUILayout.Label("TestRegularCrafting List");
            _searchText = GUILayout.TextField(_searchText, 15, "textfield");

            foreach (Equipment item in _bluePrints)
            {
                if (!string.IsNullOrEmpty(_searchText))
                    if (!item.FullName.Contains(_searchText))
                        continue;

                if (GUILayout.Button(item.FullName))
                {
                    Settings.GlitchCrafting.CraftingEquipmentsList.Add(new Equipment
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

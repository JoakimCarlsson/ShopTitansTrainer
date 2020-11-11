using System;
using System.Collections.Generic;
using Riposte;
using ShopTitansCheat.Components;
using ShopTitansCheat.Data;
using UnityEngine;

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

        private bool _craftingVisualVisible;
        private bool _craftingListVisualVisible;
        private bool _myCraftingListVisualVisible;
        private bool _qualityListVisualVisible;

        private CraftingComponent _craftingComponent;
        private MiscComponent _miscComponent;

        private readonly string _watermark = "Hello, this is a test";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);
            _craftingWindow = new Rect(270f, 60f, 250f, 150f);
            _craftingListWindow = new Rect(530f, 60f, 250f, 150f);
            _myCraftingListWindow = new Rect(790f, 60f, 250f, 150f);
            _qualityListWindow = new Rect(1050f, 60f, 250f, 150f);
            _craftingComponent = Game.Instance.gameObject.AddComponent<CraftingComponent>();
            _miscComponent = Game.Instance.gameObject.AddComponent<MiscComponent>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;
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
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 0:
                    MainMenu();
                    break;

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

        private void SelectQualityMenu()
        {
            GUILayout.Label("Select Quality");
            foreach (ItemQuality itemQuality in _craftingComponent.itemQualities)
            {
                if (GUILayout.Button(itemQuality.ToString()))
                {
                    _craftingComponent.Items.Last().ItemQuality = itemQuality;
                }
            }
        }

        private void ItemsToCraftMenu()
        {
            GUILayout.Label("Items To Craft");
            foreach (Equipment item in _craftingComponent.Items)
            {
                if (GUILayout.Button($"{item.FullName}, {item.ItemQuality}"))
                {
                    _craftingComponent.Items.Remove(item);
                }
            }
        }

        private void CraftingListMenu()
        {
            GUILayout.Label("Crafting List");

            foreach (Equipment item in Core.GetAllItems())
            {
                if (GUILayout.Button(item.FullName))
                {
                    _craftingComponent.Items.Add(new Equipment
                    {
                        FullName = item.FullName,
                        ShortName = item.ShortName,
                        Done = false,
                    });
                }
            }
        }

        private void MainCraftingMenu()
        {
            GUILayout.Label("Test Crafting");

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Start"))
            {
                if (_craftingComponent.Items.Count == 0)
                {
                    Game.UI.overlayMessage.PushMessage($"Please add items..");
                }
                else
                {
                    _craftingComponent.Crafting = true;
                }
            }

            if (GUILayout.Button("Stop"))
            {
                _craftingComponent.Crafting = false;

                foreach (Equipment equipment in _craftingComponent.Items)
                {
                    equipment.Done = false;
                }
            }

            GUILayout.EndHorizontal();
        }

        private void MainMenu()
        {
            if (GUILayout.Button("Glitch Crafting"))
            {
                _craftingVisualVisible = !_craftingVisualVisible;
                _myCraftingListVisualVisible = !_myCraftingListVisualVisible;
                _craftingListVisualVisible = !_craftingListVisualVisible;
                _qualityListVisualVisible = !_qualityListVisualVisible;
            }

            if (GUILayout.Button("Regular Bot"))
            {

            }

            if (GUILayout.Button("Options"))
            {

            }
        }
    }
}
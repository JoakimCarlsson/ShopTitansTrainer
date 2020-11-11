using System;
using System.Collections.Generic;
using Riposte;
using ShopTitansCheat.Components;
using ShopTitansCheat.Data;
using UnityEngine;

namespace ShopTitansCheat
{
    public class Main : MonoBehaviour
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

        private readonly string _watermark = "Hello, this is a test";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);
            _craftingWindow = new Rect(270f, 60f, 250f, 150f);
            _craftingListWindow = new Rect(530f, 60f, 250f, 150f);
            _myCraftingListWindow = new Rect(790f, 60f, 250f, 150f);
            _qualityListWindow = new Rect(1050f, 60f, 250f, 150f);
            Game.Instance.gameObject.AddComponent<CraftingComponent>();

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
                    if (GUILayout.Button("Crafting"))
                    {
                        _craftingVisualVisible = !_craftingVisualVisible;
                        _myCraftingListVisualVisible = !_myCraftingListVisualVisible;
                        _craftingListVisualVisible = !_craftingListVisualVisible;
                        _qualityListVisualVisible = !_qualityListVisualVisible;
                    }
                    break;

                case 1:
                    GUILayout.Label("Test Crafting");

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Start"))
                    {
                         CraftingComponent.Crafting = true;
                    }

                    if (GUILayout.Button("Stop"))
                    {
                        CraftingComponent.Crafting = false;
                    }

                    GUILayout.EndHorizontal();
                    break;

                case 2:
                    GUILayout.Label("Crafting List");
                    foreach (Equipment item in Core.GetAllItems())
                    {
                        if (GUILayout.Button(item.FullName))
                        {
                            CraftingComponent.Items.Add(item);
                        }
                    }

                    break;
                case 3:
                    GUILayout.Label("Items To Craft");
                    foreach (Equipment item in CraftingComponent.Items)
                    {
                        if (GUILayout.Button($"{item.FullName}, {CraftingComponent.itemQuality}"))
                        {
                            CraftingComponent.Items.Remove(item);
                        }
                    }
                    break;

                case 4:
                    GUILayout.Label("Select Quality");
                    foreach (ItemQuality itemQuality in CraftingComponent.itemQualities)
                    {
                        if (GUILayout.Button(itemQuality.ToString()))
                        {
                            CraftingComponent.itemQuality = itemQuality;
                        }
                    }

                    break;
            }

            GUI.DragWindow();
        }
    }
}
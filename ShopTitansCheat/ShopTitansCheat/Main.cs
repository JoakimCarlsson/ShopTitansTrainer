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

        private bool _craftingVisualVisible;
        private bool _craftingListVisualVisible;
        private bool _myCraftingListVisualVisible;

        private readonly string _watermark = "Hello, this is a test";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);
            _craftingWindow = new Rect(270f, 60f, 250f, 150f);
            _craftingListWindow = new Rect(530f, 60f, 250f, 150f);
            _myCraftingListWindow = new Rect(790f, 60f, 250f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;
        }

        private void OnGUI()
        {
            if (_visible)
                _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, _watermark);

            if (_craftingVisualVisible)
            {
                _craftingWindow = GUILayout.Window(1, _craftingWindow, RenderUi, "Crafting Window");
                _craftingListWindow = GUILayout.Window(2, _craftingListWindow, RenderUi, "Crafting Window");
                _myCraftingListWindow = GUILayout.Window(3, _myCraftingListWindow, RenderUi, "Crafting Window");
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
                    }
                    break;

                case 1:
                    GUILayout.Label("Test Crafting");

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Start"))
                    {
                         Game.Instance.gameObject.AddComponent<CraftingComponent>();
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
                    GUILayout.BeginScrollView(Vector2.down);
                    foreach (Equipment item in Core.GetAllItems())
                    {
                        if (GUILayout.Button(item.Name))
                        {
                            CraftingComponent.Items.Add(item);
                        }
                    }
                    GUILayout.EndScrollView();


                    break;
                case 3:
                    GUILayout.Label("Items To Craft");
                    foreach (Equipment item in CraftingComponent.Items)
                    {
                        GUILayout.Button(item.Name);
                    }
                        
                    break;
            }

            GUI.DragWindow();
        }
    }
}
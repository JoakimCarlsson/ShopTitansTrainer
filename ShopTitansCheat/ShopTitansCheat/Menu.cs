using System;
using System.Collections.Generic;
using System.Reflection;
using Riposte;
using ShopTitansCheat.Components;
using ShopTitansCheat.Data;
using UnityEngine;
using ShopTitansCheat.Utils;

namespace ShopTitansCheat
{
    public class Menu : MonoBehaviour
    {
        private bool _visible = true;
        
        private Rect _mainWindow;
        private readonly string _watermark = "Hello, this is a test";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;

//            Console.WriteLine(Game.PlayState.CurrentViewState);

        }

        private void OnGUI()
        {
            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, _watermark);

        }
        
        private void RenderUi(int id)
        {
            switch (id)
            {
                case 0:
                    if (GUILayout.Button("Start"))
                    {
                        Game.Instance.gameObject.AddComponent<CraftingComponent>();
                        CraftingComponent._doCraft = true;
                    }

                    if (GUILayout.Button("Stop"))
                    {
                        CraftingComponent._doCraft = false;
                    }
                    break;
            }
            GUI.DragWindow();
        }

        private void Test()
        {
            
        }

    }
}
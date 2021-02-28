using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Riposte;
using Riposte.Sim;
using ShopTitansCheat.Components;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    public class Menu : MonoBehaviour
    {
        private bool _visible = true;
        private RegularCraftingMenu _regularCraftingMenu;
        private GlitchCraftingMenu _glitchCraftingMenu;
        private AutoSellMenu _autoSellMenu;
        private MiscItemsMenu _miscItemsMenu;

        private Rect _mainWindow;

        private readonly string _watermark = "Shop Titans Bot 0.30b";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);

            _miscItemsMenu = Game.Instance.gameObject.AddComponent<MiscItemsMenu>();
            _regularCraftingMenu = Game.Instance.gameObject.AddComponent<RegularCraftingMenu>();
            _glitchCraftingMenu = Game.Instance.gameObject.AddComponent<GlitchCraftingMenu>();
            _autoSellMenu = Game.Instance.gameObject.AddComponent<AutoSellMenu>();
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
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 0:
                    MainMenu();
                    break;
            }

            GUI.DragWindow();
        }

        private void MainMenu()
        {
            if (GUILayout.Button("Regular Crafting Component"))
                _regularCraftingMenu.Show();

            if (GUILayout.Button("Glitch Crafting Component"))
                _glitchCraftingMenu.Show();

            if (GUILayout.Button("Auto Sell Component"))
                _autoSellMenu.Show();

            if (GUILayout.Button("Options"))
                _miscItemsMenu.Show();
        }

        private void Test() //aec
        {
            var barValue = Game.User.GetMemberValue("aec");
            Console.WriteLine(barValue);
            Game.User.SetMemberValue("aec", true);
            barValue = Game.User.GetMemberValue("aec");
            Console.WriteLine(barValue);
        }
    }
}
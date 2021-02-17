using System;
using System.Collections.Generic;
using System.Linq;
using Riposte;
using Riposte.Sim;
using ShopTitansCheat.Components;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    public class Menu : MonoBehaviour
    {
        private bool _visible = true;
        private CraftingMenu _craftingMenu;
        private SkillMenu _skillMenu;
        private AutoSellMenu _autoSellMenu;
        private MiscItemsMenu _miscItemsMenu;
        private QuestMenu _questMenu;

        private Rect _mainWindow;

        private readonly string _watermark = "Shop Titans Bot 0.24b";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);

            _miscItemsMenu = Game.Instance.gameObject.AddComponent<MiscItemsMenu>();
            _skillMenu = Game.Instance.gameObject.AddComponent<SkillMenu>();
            _craftingMenu = Game.Instance.gameObject.AddComponent<CraftingMenu>();
            _autoSellMenu = Game.Instance.gameObject.AddComponent<AutoSellMenu>();
            _questMenu = Game.Instance.gameObject.AddComponent<QuestMenu>();
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
            if (GUILayout.Button("Crafting Component"))
                _craftingMenu.Show();

            //if (GUILayout.Button("Skill Component"))
            //{
            //    //_skillMenu.Show();
            //}

            //if (GUILayout.Button("Quest Component"))
            //{
            //    //_questMenu.Show();
            //}

            //if (GUILayout.Button("Enchantment Component"))
            //{

            //}

            if (GUILayout.Button("Auto Sell Component"))
                _autoSellMenu.Show();

            if (GUILayout.Button("Options"))
                _miscItemsMenu.Show();
        }
    }
}
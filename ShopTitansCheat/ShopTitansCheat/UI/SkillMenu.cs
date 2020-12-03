using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTitansCheat.Components;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    class SkillMenu : MonoBehaviour
    {
        private Rect _skillWindow;
        private Rect _heroListWindow;
        private Rect _heroSkillsWindow;
        private Rect _xpItemsListWindow;

        private bool _skillVisualVisible;
        private bool _heroListVisualVisible;
        private bool _heroSkillVisualVisible;
        private bool _xpItemsListVisualVisible;

        private List<string> _heroList = new List<string>();
        private List<string> _heroSkillsList = new List<string>();
        private List<string> _xpItemsList = new List<string>();


        private void Start()
        {
            _xpItemsListWindow = new Rect(530f, 60f, 250f, 150f); 
            _heroSkillsWindow = new Rect(790f, 60f, 250f, 150f);
            _heroListWindow = new Rect(1050f, 60f, 250f, 150f);
            _skillWindow = new Rect(270f, 60f, 250f, 150f);
        }

        private void Update()
        {

            if (_heroList.Count == 0)
                _heroList = SkillComponent.GetHeros();

            if (_heroSkillsList.Count == 0)
                _heroSkillsList = SkillComponent.GetSkills();

            if (_xpItemsList.Count == 0)
                _xpItemsList = SkillComponent.FindItems("XP Drink");
        }

        private void OnGUI()
        {
            if (_skillVisualVisible)
            {
                _skillWindow = GUILayout.Window(1, _skillWindow, RenderUi, "Skill Window");
                _heroListWindow = GUILayout.Window(2, _heroListWindow, RenderUi, "Hero List Window");
                _heroSkillsWindow = GUILayout.Window(3, _heroSkillsWindow, RenderUi, "Hero Skill Window");
                _xpItemsListWindow = GUILayout.Window(4, _xpItemsListWindow, RenderUi, "Xp Items Window");
            }
        }

        public void Show()
        {
            _skillVisualVisible = !_skillVisualVisible;
            _heroListVisualVisible = !_heroListVisualVisible;
            _heroSkillVisualVisible = !_heroSkillVisualVisible;
            _xpItemsListVisualVisible = !_xpItemsListVisualVisible;
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 1:
                    SkillMainMenu();
                    break;

                case 2:
                    HeroMenuList();
                    break;

                case 3:
                    SkillsMenuList();
                    break;

                case 4:
                    XpItemsMenuList();
                    break;
            }

            GUI.DragWindow();
        }

        private void SkillMainMenu()
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
    }
}

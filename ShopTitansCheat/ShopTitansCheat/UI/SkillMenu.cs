using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using Riposte.Sim;
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

        private List<GClass282> _heroList = new List<GClass282>();
        private List<string> _heroSkillsList = new List<string>();
        private List<GClass338> _xpItemsList = new List<GClass338>();

        private string _searchText = "";
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
            try
            {
                GUILayout.Label("Main skills menu");
                GUILayout.Label(Settings.Skill.SelectedHero != null ? $"Selected Hero: {Settings.Skill.SelectedHero.Name}" : "Selected Hero: None.");

                GUILayout.Label("Selected Skills: ");
                foreach (string skill in Settings.Skill.Skills)
                {
                    if (GUILayout.Button(skill))
                        Settings.Skill.Skills.Remove(skill);
                }
                GUILayout.Space(10);

                GUILayout.Label("Selected XP Items");
                foreach (GClass338 item in Settings.Skill.XpItems)
                {
                    if (GUILayout.Button(Game.Texts.GetText(item.string_0 + "_name")))
                        Settings.Skill.XpItems.Remove(item);
                }

                GUILayout.Space(10);
                if (GUILayout.Button("Start"))
                {
                    
                }

                if (GUILayout.Button("Stop"))
                {
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void HeroMenuList()
        {
            GUILayout.Label("Heros");
            foreach (GClass282 hero in _heroList)
            {
                if (GUILayout.Button(hero.Name))
                {
                    Settings.Skill.SelectedHero = hero;
                }
            }
        }

        private void SkillsMenuList()
        {
            GUILayout.Label("Skill");
            _searchText = GUILayout.TextField(_searchText, 15, "textfield");

            foreach (string skill in _heroSkillsList)
            {
                if (!string.IsNullOrEmpty(_searchText))
                    if (!skill.Contains(_searchText))
                        continue;

                if (GUILayout.Button(skill))
                {
                    if (Settings.Skill.Skills.Contains(skill))
                        continue;
                    
                    Settings.Skill.Skills.Add(skill);
                }
            }
        }

        private void XpItemsMenuList()
        {
            GUILayout.Label("XP Items.");
            foreach (GClass338 item in _xpItemsList)
            {
                string itemName = Game.Texts.GetText(item.string_0 + "_name");
                if (GUILayout.Button(itemName))
                {
                    if (Settings.Skill.XpItems.Contains(item))
                        continue;
                    
                    Settings.Skill.XpItems.Add(item);
                }
            }
        }
    }
}

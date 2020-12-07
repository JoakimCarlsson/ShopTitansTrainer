using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    class QuestMenu : MonoBehaviour
    {
        private Rect _questMainWindow;
        private Rect _questQueWindow;
        private Rect _questsListWindow;
        private Rect _questsDifficultyWindow;

        private bool _questVisualVisible;

        private void Start()
        {
            _questMainWindow = new Rect(270f, 60f, 250f, 150f);
            _questQueWindow = new Rect(530f, 60f, 250f, 150f);
            _questsListWindow = new Rect(790f, 60f, 250f, 150f);
            _questsDifficultyWindow = new Rect(1050f, 60f, 250f, 150f);
        }

        private void OnGUI()
        {
            if (_questVisualVisible)
            {
                _questMainWindow = GUILayout.Window(200, _questMainWindow, RenderUi, "Quest Options.");
                _questQueWindow = GUILayout.Window(201, _questQueWindow, RenderUi, "Quest Que.");
                _questsListWindow = GUILayout.Window(202, _questsListWindow, RenderUi, "Quests List.");
                _questsDifficultyWindow = GUILayout.Window(203, _questsDifficultyWindow, RenderUi, "Quest Difficulty");
            }
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 200:
                    QuestMainMenu();
                    break;
                case 201:
                    QuestQueWindow();
                    break;
                case 202:
                    QuestListWindow();
                    break;
                case 203:
                    questsDifficultyWindow();
                    break;
            }
            GUI.DragWindow();
        }

        private void questsDifficultyWindow()
        {
            GUILayout.Label("Quests Difficulty");
        }

        private void QuestListWindow()
        {
            GUILayout.Label("Quests List");
        }

        private void QuestQueWindow()
        {
            GUILayout.Label("Que Window");
        }

        private void QuestMainMenu()
        {
            GUILayout.Label("Quest test");
        }

        public void Show()
        {
            _questVisualVisible = !_questVisualVisible;
        }
    }
}

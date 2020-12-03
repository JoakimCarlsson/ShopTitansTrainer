using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using UnityEngine;

namespace ShopTitansCheat.UI
{
    class MiscItemsMenu : MonoBehaviour
    {
        private Rect _optionsListWindow;
        private bool _optionsListWindowVisualVisible;

        private void Start()
        {
            _optionsListWindow = new Rect(1050f, 250f, 250f, 150f);
        }

        private void OnGUI()
        {
            if (_optionsListWindowVisualVisible)
            {
                _optionsListWindow = GUILayout.Window(1, _optionsListWindow, RenderUi, "Random Options.");
            }
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 1:
                    RandomOptionsMenu();
                    break;
            }

            GUI.DragWindow();
        }

        private void RandomOptionsMenu()
        {
            GUILayout.Label("Random Options");
            Settings.Misc.AutoFinishCraft = GUILayout.Toggle(Settings.Misc.AutoFinishCraft, "Finish Crafts");

            Settings.Misc.UseEnergy = GUILayout.Toggle(Settings.Misc.UseEnergy, $"Use Energy Over {(int)Settings.Misc.UseEnergyAmount}");

            if (Settings.Misc.UseEnergy)
                if (Game.User != null)
                    Settings.Misc.UseEnergyAmount = GUILayout.HorizontalSlider(Settings.Misc.UseEnergyAmount, 0, Game.User.method_45());

            Settings.Misc.RemoveWindowPopup = GUILayout.Toggle(Settings.Misc.RemoveWindowPopup, "Remove Window pop up.");
        }

        public void Show()
        {
            _optionsListWindowVisualVisible = !_optionsListWindowVisualVisible;
        }
    }
}

using System;
using System.Collections;
using Riposte;
using ShopTitansCheat.Components;
using ShopTitansCheat.UI;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat
{
    class Main : MonoBehaviour
    {
        private int _frame;
        private Menu _menu;
        private CraftingComponent _craftingComponent;
        private AutoSellComponent _autoSellComponent;
        private MiscComponent _miscComponent;

        public static AlertPopupControl Alert(string title, string body, Action callback = null)
        {
            if (callback == null)
            {
                callback = delegate
                {
                };
            }
            return AlertPopupControl.ShowButton(title, body, "I'm gay", callback);
        }
        private void Start()
        {
            _menu = Game.Instance.gameObject.AddComponent<Menu>();
            Game.Scheduler.Register(Scheduler.Priority.BeginFrame, Update);
            _craftingComponent = new CraftingComponent();
            _autoSellComponent = new AutoSellComponent();
            _miscComponent = new MiscComponent();
        }

        private void Update()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            _frame++;
            if (Settings.Crafting.ThisIsATempBool)
            {
                if (_frame % 66 == 0)
                {
                    if (Settings.Crafting.DoCrafting && !Settings.Crafting.CraftRandomStuff)
                    {
                        Log.Instance.PrintConsoleMessage("Trying To Craft", ConsoleColor.Cyan);
                        DoCrafting();
                    }

                    if (Settings.Crafting.DoCrafting && Settings.Crafting.CraftRandomStuff)
                    {
                        Log.Instance.PrintConsoleMessage("Trying To Craft RandomStuff", ConsoleColor.Cyan);
                        CraftRandomStuff();
                    }
                }
            }

            if (Settings.Misc.AutoFinishCraft)
                if (_frame % 77 == 0)
                {
                    Log.Instance.PrintConsoleMessage("Trying Store Craft", ConsoleColor.DarkBlue);
                    StoreFinished();
                }

            if (Settings.AutoSell.AutoSellToNpc)
                if (_frame % 111 == 0)
                {
                    Log.Instance.PrintConsoleMessage("Trying Auto Sell", ConsoleColor.DarkCyan);
                    AutoSell();
                }

            if (Settings.Misc.RemoveWindowPopup)
                Game.UI.RemoveAllWindows(WindowsManager.MenuLayer.Popup);
        }

        private void CraftRandomStuff()
        {
            _craftingComponent.CraftRandomStuffOverValue(Settings.Crafting.CraftRandomStuffValue);
            Log.Instance.PrintConsoleMessage("We are waiting 2 seconds.", ConsoleColor.Blue);
            Settings.Crafting.DoCrafting = false;
            StartCoroutine(WaitThenStart(2));
        }

        private void AutoSell()
        {
            _autoSellComponent.AutoSell();
        }

        private void StoreFinished()
        {
            _miscComponent.FinishCraft();
        }

        private void DoCrafting()
        {
            if (Settings.Crafting.RegularCrafting)
            {
                _craftingComponent.Craft();
                Settings.Crafting.DoCrafting = false;
                StartCoroutine(WaitThenStart(20));
            }
            else
            {
                if (_craftingComponent.GlitchCraft())
                {
                    Settings.Crafting.DoCrafting = false;
                    Game.Instance.GarbageCollect();
                    Log.Instance.PrintConsoleMessage("We are waiting 20 seconds.", ConsoleColor.Blue);
                    StartCoroutine(WaitThenStart(20));
                }
                else
                {
                    Settings.Crafting.DoCrafting = false;
                    StartCoroutine(WaitThenStart(0.1f));
                }
            }
        }

        private IEnumerator WaitThenStart(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Settings.Crafting.DoCrafting = true;
        }
    }
}

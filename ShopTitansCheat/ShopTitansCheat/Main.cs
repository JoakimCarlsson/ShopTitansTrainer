using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using ShopTitansCheat.Components;
using ShopTitansCheat.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace ShopTitansCheat
{
    class Main : MonoBehaviour
    {
        private int _frame;
        private Menu _menu;
        private CraftingComponent _craftingComponent;
        private AutoSellComponent _autoSellComponent;
        private MiscComponent _miscComponent;

        private void Start()
        {
            _menu = Game.Instance.gameObject.AddComponent<Menu>();
            Game.Scheduler.Register(Scheduler.Priority.BeginFrame, Update);

            Console.WriteLine("IF YOU READ THIS U ARE GAY");

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
                        Log.PrintConsoleMessage("Trying To Craft", ConsoleColor.Cyan);
                        DoCrafting();
                    }

                    if (Settings.Crafting.DoCrafting && Settings.Crafting.CraftRandomStuff)
                    {
                        Log.PrintConsoleMessage("Trying To Craft RandomStuff", ConsoleColor.Cyan);
                        CraftRandomStuff();
                    }
                }
            }

            if (Settings.Misc.AutoFinishCraft && !Settings.Crafting.DoCrafting)
                if (_frame % 111 == 0)
                {
                    Log.PrintConsoleMessage("Trying Store Craft", ConsoleColor.DarkBlue);

                    StoreFinished();

                }

            if (Settings.AutoSell.AutoSellToNpc && !Settings.Crafting.DoCrafting)
                if (_frame % 77 == 0)
                {
                    Log.PrintConsoleMessage("Trying Auto Sell", ConsoleColor.DarkCyan);
                    AutoSell();
                }

            if (Settings.Misc.RemoveWindowPopup)
                Game.UI.RemoveAllWindows(WindowsManager.MenuLayer.Popup);
        }

        private void CraftRandomStuff()
        {
            _craftingComponent.CraftRandomStuffOverValue(Settings.Crafting.CraftRandomStuffValue);
                Log.PrintConsoleMessage("We are waiting 5 seconds.", ConsoleColor.Blue);
                Settings.Crafting.DoCrafting = false;
                StartCoroutine(WaitThenStart(5));
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
                    Core.CollectGarbage();
                    Log.PrintConsoleMessage("We are waiting 20 seconds.", ConsoleColor.Blue);
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

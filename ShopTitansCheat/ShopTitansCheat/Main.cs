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
        private RegularCraftingComponent _regularCraftingComponent;
        private GlitchCraftingComponent _glitchCraftingComponent;
        private AutoSellComponent _autoSellComponent;
        private MiscComponent _miscComponent;

        private void Start()
        {
            _menu = Game.Instance.gameObject.AddComponent<Menu>();
            Game.Scheduler.Register(Scheduler.Priority.BeginFrame, Update);
            _regularCraftingComponent = new RegularCraftingComponent();
            _glitchCraftingComponent = new GlitchCraftingComponent();
            _autoSellComponent = new AutoSellComponent();
            _miscComponent = new MiscComponent();
        }

        private void Update()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;
            _frame++;

            if (_frame % 66 == 0)
            {
                if (Settings.RegularCrafting.DoCrafting || Settings.GlitchCrafting.DoCrafting)
                    DoCrafting();
            }

            if (Settings.Misc.AutoFinishCraft)
                if (_frame % 77 == 0)
                {
                    Log.Instance.Info("Trying Store Craft");
                    StoreCrafts();
                }

            if (Settings.AutoSell.AutoSellToNpc)
                if (_frame % 111 == 0)
                {
                    Log.Instance.Info("Trying Auto Sell");
                    AutoSell();
                }

            if (Settings.Misc.RemoveWindowPopup)
                Game.UI.RemoveAllWindows(WindowsManager.MenuLayer.Popup);
        }

        private void AutoSell()
        {
            _autoSellComponent.AutoSell();
        }

        private void StoreCrafts()
        {
            _miscComponent.FinishCraft();
        }

        private void DoCrafting()
        {
            if (Settings.RegularCrafting.ThisIsATempBool)
            {
                if (Settings.RegularCrafting.DoCrafting)
                {
                    _regularCraftingComponent.Craft();
                    StartCoroutine(WaitThenStart(20));
                }
            }

            if (Settings.GlitchCrafting.ShouldCraft)
            {
                if (Settings.GlitchCrafting.DoCrafting)
                {
                    if (_glitchCraftingComponent.GlitchCraft())
                    {
                        Settings.GlitchCrafting.DoCrafting = false;
                        Log.Instance.Info("We are waiting 20 seconds.", ConsoleColor.Blue);
                        StartCoroutine(WaitThenStart(20, true));
                    }
                    else
                    {
                        Settings.RegularCrafting.DoCrafting = false;
                        StartCoroutine(WaitThenStart(0.1f, true));
                    }
                }
            }

        }

        private IEnumerator WaitThenStart(float seconds, bool glitchCraft = false)
        {
            if (glitchCraft)
            {
                yield return new WaitForSeconds(seconds);
                Settings.GlitchCrafting.DoCrafting = true;
            }
            else
            {
                yield return new WaitForSeconds(seconds);
                Settings.RegularCrafting.DoCrafting = true;
            }
        }
    }
}

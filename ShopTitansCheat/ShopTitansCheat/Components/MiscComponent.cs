using System;
using System.Collections.Generic;
using Riposte;
using ShopTitansCheat.Utils;
using UnityEngine;

namespace ShopTitansCheat.Components
{
    class MiscComponent
    {
        internal void FinishCraft()
        {
            foreach (var craft in Game.User.z9.Values.ToList())
            {
                if (Settings.Misc.UseEnergy && Game.User.ajo() > Settings.Misc.UseEnergyAmount)
                {
                    SpeedCraft();
                }
                else if (dh.a0(craft).ar())
                {
                    Log.Instance.PrintConsoleMessage($"{craft.hm} stored.", ConsoleColor.Green);

                    Game.SimManager.SendUserAction("CraftStore", new Dictionary<string, object>
                    {
                        {
                            "craftId",
                            craft.hl
                        }
                    });
                }
            }
        }

        private void SpeedCraft()
        {
            foreach (var craft in Game.User.z9.Values.ToList())
            {
                if (!craft.cr())
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>
                    {
                        {
                            "type",
                            1
                        },
                        {
                            "id",
                            craft.hl
                        }
                    };
                    if (fj.a0(Game.SimManager.CurrentContext, dictionary, null).ar())
                    {
                        Game.SimManager.SendUserAction("SpeedUpTimer", dictionary);
                    }
                    else
                    {
                        Game.UI.overlayMessage.PushMessage(Game.Texts.GetText("not_enough_energy", null),
                            OverlayMessageControl.MessageType.Error);
                    }
                }
                else
                {
                    Game.SimManager.SendUserAction("CraftStore", new Dictionary<string, object>
                    {
                        {
                            "craftId",
                            craft.hl
                        }
                    });
                }
            }
        }
    }
}

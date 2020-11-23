using System;
using System.Collections.Generic;
using Riposte;
using ShopTitansCheat.Utils;

namespace ShopTitansCheat.Components
{
    class MiscComponent
    {
        internal void FinishCraft()
        {
            foreach (GClass301 gclass3 in Game.User.observableDictionary_16.Values.ToList())
            {
                if (Settings.Misc.UseEnergy && Game.User.method_38() > Settings.Misc.UseEnergyAmount)
                {
                    SpeedCraft();
                }
                else if (GClass167.smethod_0(gclass3).imethod_0())
                {
                    Log.Instance.PrintConsoleMessage($"{gclass3.string_0} stored.", ConsoleColor.Green);

                    Game.SimManager.SendUserAction("CraftStore", new Dictionary<string, object>
                    {
                        {
                            "craftId",
                            gclass3.long_0
                        }
                    });
                }
            }
        }

        private void SpeedCraft()
        {
            foreach (GClass301 craft in Game.User.observableDictionary_16.Values.ToList())
            {

                if (!craft.imethod_3())
                {
                    craft.imethod_6();
                    Dictionary<string, object> dictionary = new Dictionary<string, object>
                {
                    {
                        "type",
                        1
                    },
                    {
                        "id",
                        craft.long_0
                    }
                };
                    if (GClass238.smethod_0(Game.SimManager.CurrentContext, dictionary, null).imethod_0())
                    {
                        Game.SimManager.SendUserAction("SpeedUpTimer", dictionary);
                    }
                    else
                    {
                        Log.Instance.PrintMessageInGame(Game.Texts.GetText("not_enough_energy", null), OverlayMessageControl.MessageType.Error);
                    }
                }
                else
                {
                    Game.SimManager.SendUserAction("CraftStore", new Dictionary<string, object>
                    {
                        {
                            "craftId",
                            craft.long_0
                        }
                    });
                }
            }

        }
    }
}

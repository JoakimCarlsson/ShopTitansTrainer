using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using Riposte.Sim;
using UnityEngine;

namespace ShopTitansCheat.Components
{
    class MiscComponent : MonoBehaviour
    {
        internal bool AutoFinishCraft;
        internal bool AutoSellToNpc;
        internal bool RemoveWindowPopup;

        private void Update()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            if (AutoFinishCraft)
                FinishCraft();

            if (AutoSellToNpc)
                AutoSell();

            if (RemoveWindowPopup)
                Game.UI.RemoveAllWindows(WindowsManager.MenuLayer.Popup);
        }

        private void AutoSell()
        {
            foreach (GClass339 gclass10 in Game.User.observableDictionary_7.Values)
            {
                GClass548 gclass11 = Game.SimManager.CurrentContext.method_23().method_19(gclass10.imethod_0()) as GClass548;
                if (gclass10.method_3() > 0L && gclass11.method_58() == "VAIAsk")
                {
                    Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                        {
                            {
                                "id",
                                gclass10.imethod_0()
                            },
                            {
                                "type",
                                1
                            }
                        });
                    Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                        {
                            {
                                "id",
                                gclass10.imethod_0()
                            },
                            {
                                "type",
                                4
                            }
                        });
                    if (Game.User.method_38() < Game.User.method_39() / 2)
                    {
                        Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                            {
                                {
                                    "id",
                                    gclass10.imethod_0()
                                },
                                {
                                    "type",
                                    7
                                }
                            });
                    }
                    else if (Game.User.method_38() == Game.User.method_39())
                    {
                        Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                            {
                                {
                                    "id",
                                    gclass10.imethod_0()
                                },
                                {
                                    "type",
                                    6
                                }
                            });
                    }
                    Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                        {
                            {
                                "id",
                                gclass10.imethod_0()
                            },
                            {
                                "type",
                                9
                            }
                        });
                    if (gclass11.method_90() == GClass255.InteractionType.Initialize)
                    {
                        Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                            {
                                {
                                    "id",
                                    gclass10.imethod_0()
                                },
                                {
                                    "type",
                                    10
                                }
                            });
                    }
                    if (gclass11.method_90() == GClass255.InteractionType.Initialize)
                    {
                        foreach (Item item in Game.User.observableDictionary_0.Values)
                        {
                            if (gclass10.gclass276_0.method_8().method_25(item.Data.Type))
                            {
                                Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                                    {
                                        {
                                            "id",
                                            gclass10.imethod_0()
                                        },
                                        {
                                            "type",
                                            8
                                        },
                                        {
                                            "itemId",
                                            item.long_0
                                        }
                                    });
                            }
                        }
                    }
                    if (gclass11.method_90() == GClass255.InteractionType.Initialize)
                    {
                        Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                            {
                                {
                                    "id",
                                    gclass10.imethod_0()
                                },
                                {
                                    "type",
                                    9
                                }
                            });
                    }
                    if (gclass11.method_90() == GClass255.InteractionType.Initialize)
                    {
                        Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                            {
                                {
                                    "id",
                                    gclass10.imethod_0()
                                },
                                {
                                    "type",
                                    2
                                }
                            });
                    }
                }
            }
        }

        private void FinishCraft()
        {
            foreach (GClass301 gclass3 in Game.User.observableDictionary_16.Values.ToList(false))
            {
                if (GClass167.smethod_0(gclass3).imethod_0())
                {
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
    }
}

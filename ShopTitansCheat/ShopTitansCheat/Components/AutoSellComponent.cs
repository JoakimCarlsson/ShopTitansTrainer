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
    class AutoSellComponent : MonoBehaviour
    {
        internal bool AutoSellToNpc;

        private void Update()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            if (AutoSellToNpc)
                AutoSell();
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
    }
}

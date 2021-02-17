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
    class AutoSellComponent
    {
        internal void AutoSell()
        {
            foreach (ig visitor in Game.User.z0.Values)
            {
                n8 n8 = Game.SimManager.CurrentContext.an().c(visitor.cw()) as n8;
                if (visitor.ajb() > 0L && n8.bi() == "VAIAsk")
                {
                    StartConversationWithNpc(visitor);

                    if (Settings.AutoSell.SmallTalk)
                        SmallTalkWithNpc(visitor);

                    if (Settings.AutoSell.SurchargeDiscount)
                        SurchargeOrDiscount(visitor);

                    if (Settings.AutoSell.BuyFromNpc)
                        BuyNpcItem(n8, visitor);

                    if (Settings.AutoSell.Suggest)
                        SuggestItem(n8, visitor);

                    SellItem(n8, visitor);

                    if (Settings.AutoSell.Refuse)
                        RefuseItem(n8, visitor);
                }
            }
        }

        private void SurchargeOrDiscount(ig gclass10)
        {
            if (Game.User.ajo() < Game.User.ajp() / 2)
            {
                Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                {
                    {
                        "id",
                        gclass10.cw()
                    },
                    {
                        "type",
                        7
                    }
                });
            }
            else if (Game.User.ajn() == Game.User.ajp())
            {
                Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                {
                    {
                        "id",
                        gclass10.cw()
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
                    gclass10.cw()
                },
                {
                    "type",
                    9
                }
            });
        }

        private void RefuseItem(n8 gclass11, ig gclass10)
        {
            if (gclass11.bt() == f2.InteractionType.Initialize)
            {
                Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                {
                    {
                        "id",
                        gclass10.cw()
                    },
                    {
                        "type",
                        2
                    }
                });
            }
        }

        private void SellItem(n8 gclass11, ig gclass10)
        {
            if (gclass11.bt() == f2.InteractionType.Initialize)
            {
                Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                {
                    {
                        "id",
                        gclass10.cw()
                    },
                    {
                        "type",
                        9
                    }
                });
            }
        }

        private void SuggestItem(n8 gclass11, ig gclass10)
        {
            if (gclass11.bt() == f2.InteractionType.Initialize)
            {
                foreach (Item item in Game.User.zr.Values)
                {
                    if (gclass10.agm.ajb().ep(item.Data.Type))
                    {
                        Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                        {
                            {
                                "id",
                                gclass10.cw()
                            },
                            {
                                "type",
                                8
                            },
                            {
                                "itemId",
                                item.m8
                            }
                        });
                    }
                }
            }
        }

        private void BuyNpcItem(n8 gclass11, ig gclass10)
        {
            if (gclass11.bt() == f2.InteractionType.Initialize)
            {
                Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
                {
                    {
                        "id",
                        gclass10.cw()
                    },
                    {
                        "type",
                        10
                    }
                });
            }
        }

        private void SmallTalkWithNpc(ig gclass10)
        {
            Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
            {
                {
                    "id",
                    gclass10.cw()
                },
                {
                    "type",
                    4
                }
            });
        }

        private void StartConversationWithNpc(ig gclass10)
        {
            Game.SimManager.SendUserAction("VisitorInteract", new Dictionary<string, object>
            {
                {
                    "id",
                    gclass10.cw()
                },
                {
                    "type",
                    1
                }
            });
        }
    }
}

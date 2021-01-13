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
            foreach (ie visitor in Game.User.zz.Values)
            {
                n7 n2 = Game.SimManager.CurrentContext.an().c(visitor.cw()) as n7;
                if (visitor.aja() > 0L && n2.bi() == "VAIAsk")
                {
                    StartConversationWithNpc(visitor);

                    if (Settings.AutoSell.SmallTalk)
                        SmallTalkWithNpc(visitor);

                    if (Settings.AutoSell.SurchargeDiscount)
                        SurchargeOrDiscount(visitor);

                    if (Settings.AutoSell.BuyFromNpc)
                        BuyNpcItem(n2, visitor);

                    if (Settings.AutoSell.Suggest)
                        SuggestItem(n2, visitor);

                    SellItem(n2, visitor);

                    if (Settings.AutoSell.Refuse)
                        RefuseItem(n2, visitor);
                }
            }
        }

        private void SurchargeOrDiscount(ie gclass10)
        {
            if (Game.User.ajn() < Game.User.ajo() / 2)
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
            else if (Game.User.ajn() == Game.User.ajo())
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

        private void RefuseItem(n7 gclass11, ie gclass10)
        {
            if (gclass11.bt() == f1.InteractionType.Initialize)
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

        private void SellItem(n7 gclass11, ie gclass10)
        {
            if (gclass11.bt() == f1.InteractionType.Initialize)
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

        private void SuggestItem(n7 gclass11, ie gclass10)
        {
            if (gclass11.bt() == f1.InteractionType.Initialize)
            {
                foreach (Item item in Game.User.zq.Values)
                {
                    if (gclass10.agl.aja().ep(item.Data.Type))
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

        private void BuyNpcItem(n7 gclass11, ie gclass10)
        {
            if (gclass11.bt() == f1.InteractionType.Initialize)
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

        private void SmallTalkWithNpc(ie gclass10)
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

        private void StartConversationWithNpc(ie gclass10)
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

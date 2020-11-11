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
        internal bool RemoveWindowPopup;

        private void Update()
        {
            if (Game.PlayState == null || Game.PlayState.CurrentViewState != "ShopState")
                return;

            if (AutoFinishCraft)
                FinishCraft();

            if (RemoveWindowPopup)
                Game.UI.RemoveAllWindows(WindowsManager.MenuLayer.Popup);
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

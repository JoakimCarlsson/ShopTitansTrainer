using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using UnityEngine;

namespace ShopTitansCheat.Components
{
    class MiscComponent : MonoBehaviour
    {
        internal bool AutoFinishCraft;
        internal bool AutoSellToNpc;
        private void Update()
        {
            if (AutoFinishCraft)
                FinishCraft();

            if (AutoSellToNpc)
                AutoSell();
        }

        private void AutoSell()
        {

        }

        private void FinishCraft()
        {

        }
    }
}

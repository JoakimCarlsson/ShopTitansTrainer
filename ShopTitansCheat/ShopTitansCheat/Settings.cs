﻿using System.Collections.Generic;
using System.Threading;
using ShopTitansCheat.Data;

namespace ShopTitansCheat
{
    class Settings
    {
        internal class RegularCrafting
        {
            internal static bool ThisIsATempBool = false;
            internal static bool DoCrafting = false;
            internal static bool CraftRandomItems = false;
            internal static int CraftRandomStuffValue = 0;
            internal static bool CraftBookmarked = false;
            internal static bool IncludeRune = false;
            internal static bool IncludeElements = false;
            internal static List<Equipment> CraftingEquipmentsList = new List<Equipment>();
        }

        internal class GlitchCrafting
        {
            internal static bool ShouldCraft = false;
            internal static bool DoCrafting = false;

            internal static List<ItemQuality> ItemQualities = new List<ItemQuality>
            {
                ItemQuality.Uncommon,
                ItemQuality.Flawless,
                ItemQuality.Epic,
                ItemQuality.Legendary
            };

            internal static List<Equipment> CraftingEquipmentsList = new List<Equipment>();
        }

        internal class Misc
        {
            internal static bool AutoFinishCraft = false;
            internal static bool RemoveWindowPopup = false;
            internal static bool UseEnergy = false;
            internal static float UseEnergyAmount = 0;
        }

        internal class AutoSell
        {
            internal static bool AutoSellToNpc = false;
            internal static bool SmallTalk = false;
            internal static bool Refuse = false;
            internal static bool SurchargeDiscount = false;
            internal static bool Suggest = false;
            internal static bool BuyFromNpc = false;
            internal static long SurchargeAmount = 0;
            internal static long DiscountAmount = 0;
        }

    }
}

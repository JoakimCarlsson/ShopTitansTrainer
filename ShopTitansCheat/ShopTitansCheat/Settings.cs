using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTitansCheat.Data;

namespace ShopTitansCheat
{
    class Settings
    {
        internal class Crafting
        {
            internal static bool ThisIsATempBool = false;
            internal static bool DoCrafting = false;
            internal static bool RegularCrafting = false;

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
            internal static bool CraftRandomStuff = false;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;
using Riposte.Sim;
using ShopTitansCheat.Data;
using ShopTitansCheat.Utils;

namespace ShopTitansCheat.Components
{
    class GlitchCraftingComponent
    {
        private int _i = 1;

        internal bool GlitchCraft()
        {
            foreach (Equipment item in Settings.GlitchCrafting.CraftingEquipmentsList)
            {
                if (item.Done)
                    continue;

                if (!GameHelpers.StartCraft(item.ShortName))
                {
                    Log.Instance.Error($"Not enough resources for {item.FullName}, moving onto next item.");
                    continue;
                }

                Equipment equipment = PeekCraft(item.ShortName)[0];

                if (equipment.ItemQuality >= item.ItemQuality)
                {
                    Log.Instance.Info($"{equipment}, Tries: {_i}", ConsoleColor.Green);

                    _i = 1;
                    item.Done = true;
                    item.FullName = $"{item.FullName}, {item.Done}";
                    return true;
                }

                Log.Instance.Info($"{equipment}, Tries: {_i++}", ConsoleColor.Yellow);
                Game.Instance.Restart();
                return false;
            }

            if (Settings.GlitchCrafting.CraftingEquipmentsList.All(i => i.Done))
            {
                Log.Instance.Info("We are done\n Stopping.", ConsoleColor.Green);
                return true;
            }

            return false;
        }



        private List<Equipment> PeekCraft(string craftName)
        {
            List<Equipment> equips = new List<Equipment>();

            using (var enumerator = Game.User.z9.Values.Reverse().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    if (current.hm != craftName)
                        continue;

                    equips.Add(new Equipment(Game.Texts.GetText(current.hm), (ItemQuality)current.ht, current.hs));
                }
            }
            return equips;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riposte;

namespace ShopTitansCheat.Components
{
    class SkillComponent
    {
        //private List<string> _heros = GetHeros();
        //private List<string> _skills = GetSkills("test");
        //private List<string> _xpDrinks = FindItems("XP Drink");

        public static List<string> FindItems(string name)
        {
            List<string> tmpList = new List<string>();
            foreach (GClass338 item in Game.User.observableDictionary_1.Values)
            {
                if (Game.Texts.GetText(item.string_0 + "_name").Contains(name))
                {
                    tmpList.Add(Game.Texts.GetText(item.string_0 + "_name"));
                }
            }
            return tmpList;
        }

        public static List<string> GetSkills()
        {
            List<string> tmpList = new List<string>();
            foreach (KeyValuePair<string, object> textsDatum in Game.DataLoader.TextsData)
            {
                string key = textsDatum.Key;
                if (key.StartsWith("skill_") && key.EndsWith("_name"))
                {
                    tmpList.Add(textsDatum.Value as string);
                }
            }
            return tmpList;
        }

        public static List<string> GetHeros()
        {
            List<string> tmpList = new List<string>();
            foreach (GClass282 hero in Game.User.observableDictionary_4.Values)
            {
                tmpList.Add(hero.Name);
            }
            return tmpList;
        }
    }
}

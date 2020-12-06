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

        public void DoStuff()
        {

        }

        public static List<GClass338> FindItems(string name)
        {
            List<GClass338> tmpList = new List<GClass338>();
            foreach (GClass338 item in Game.User.observableDictionary_1.Values)
            {
                if (Game.Texts.GetText(item.string_0 + "_name").Contains(name))
                {
                    tmpList.Add(item);
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

        public static List<GClass282> GetHeros()
        {
            List<GClass282> tmpList = new List<GClass282>();
            foreach (GClass282 hero in Game.User.observableDictionary_4.Values)
            {
                tmpList.Add(hero);
            }
            return tmpList;
        }

        //public static List<string> HeroSkills(GClass282 hero)
        //{
        //    List<string> list = new List<string>();
        //    foreach (var z in Game.User.observableDictionary_4.Values)
        //    {
        //        if (!z.Name.Equals(hero.Name))
        //        {
        //            continue;
        //        }
        //        foreach (string item in z.Skills)
        //        {
        //            list.Add(Game.Texts.GetText($"skill_{item}_name"));
        //        }
        //    }
        //    return list;
        //}
    }
}

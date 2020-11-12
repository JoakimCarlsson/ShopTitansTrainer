using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Riposte;
using ShopTitansCheat.Components;
using ShopTitansCheat.Data;

namespace ShopTitansCheat.Utils
{
    class Config
    {
        internal static void SaveCraftingList(CraftingComponent craftingComponent, string fileName)
        {
            if (craftingComponent.Items.Count == 0)
            {
                Log.PrintMessageInGame("No Items Too Save !", OverlayMessageControl.MessageType.Error);
            }
            else
            {
                File.WriteAllText($"{fileName}.json", JsonConvert.SerializeObject(craftingComponent.Items));
                Log.PrintMessageInGame("Saved Sucesfully!", OverlayMessageControl.MessageType.Neutral);
            }
        }

        public static void LoadCraftingList(CraftingComponent craftingComponent, string fileName)
        {
            string text;

            using (StreamReader streamReader = new StreamReader($"{fileName}.json"))
            {
                text = streamReader.ReadToEnd();
            }
            var deserializeObject = JsonConvert.DeserializeObject<List<Equipment>>(text);
            craftingComponent.Items = deserializeObject;
        }
    }
}

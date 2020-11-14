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
        internal static void SaveCraftingList(string fileName)
        {
            if (Settings.Crafting.CraftingEquipmentsList.Count == 0)
            {
                Log.PrintMessageInGame("No Items Too Save !", OverlayMessageControl.MessageType.Error);
            }
            else
            {
                File.WriteAllText($"{fileName}.json", JsonConvert.SerializeObject(Settings.Crafting.CraftingEquipmentsList));
                Log.PrintMessageInGame("Saved Sucesfully!", OverlayMessageControl.MessageType.Neutral);
            }
        }

        public static void LoadCraftingList(string fileName)
        {
            string text;

            using (StreamReader streamReader = new StreamReader($"{fileName}.json"))
            {
                text = streamReader.ReadToEnd();
            }
            var deserializeObject = JsonConvert.DeserializeObject<List<Equipment>>(text);
            Settings.Crafting.CraftingEquipmentsList = deserializeObject;
        }
    }
}

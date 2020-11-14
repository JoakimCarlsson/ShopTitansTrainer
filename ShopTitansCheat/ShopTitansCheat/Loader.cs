using System;
using Riposte;
using UnityEngine;
using ShopTitansCheat.Utils;
using Debug = Riposte.Debug;


namespace ShopTitansCheat
{
    public class Loader : MonoBehaviour
    {
        public static void Load()
        {
            AllocConsoleHandler.Open();
            Game.Instance.gameObject.AddComponent<Main>();
        }
    }
}

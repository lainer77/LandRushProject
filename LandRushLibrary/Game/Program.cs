using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LandRushLibrary.Factory;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using LandRushLibrary.Utilities;
using Newtonsoft.Json;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Sword sword = ItemFactory.Instance.Create<Sword>(ItemID.OLD_SWORD);
            Console.WriteLine( sword.ItemId );
        }
    }
}

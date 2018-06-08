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
            Console.WriteLine(Player.Instance.AttackPower);
            Console.WriteLine(Player.Instance.Name);
            Console.WriteLine(Player.Instance.Level);
        }
    }
}

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
            List<int> list = new List<int>();

            list.Add(1);
            list.Add(10);
            list.Add(9);


            Console.WriteLine(list.Sum());

        }
    }
}

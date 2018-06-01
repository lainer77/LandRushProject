using System;
using System.IO;
using LandRushLibrary.Factory;
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
//            UnitSerializer.Instance.Serialize();
            var slime = MonsterFactory.Instance.Create(MonsterID.Slime);
            Console.WriteLine(slime.Name);
            Console.WriteLine(slime.AttackPower);
            return;
            var json = File.ReadAllText("c:\\p.json");
            Player p = JsonConvert.DeserializeObject<Player>(json);
            Console.WriteLine(p.Level);

//            Player p = new Player();
//            p.Level = 3;
//
//            string json = JsonConvert.SerializeObject(p);
//            File.WriteAllText("c:\\p.json", json);
        }
    }
}

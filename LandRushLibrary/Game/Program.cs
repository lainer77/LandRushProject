using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using LandRushLibrary.Factory;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using LandRushLibrary.Upgrade;
using LandRushLibrary.Utilities;
using LandRushLibrary.Drop;
using Newtonsoft.Json;

namespace Game
{
    class Program
    {
        static private int[] _dropPro = {0, 0, 0, 0, 0, 0};
        static int _dropTry = 10000;
        static int _upTry = 10000;
        static int  _upSuceess = 0;


        static void Main(string[] args)
        {
            //foo();
            goo();
        }


        static void foo()
        {
            Monster orcLod = MonsterFactory.Instance.Create(MonsterID.OrcLord);
            

            for (int i = 0; i < _dropTry; i++)
            {
                Monster orc = MonsterFactory.Instance.Create(MonsterID.Orc);
                Monster lord = MonsterFactory.Instance.Create(MonsterID.OrcLord);

                lord.ItemDropped += OnItemDropped;
                orc.ItemDropped += OnItemDropped;

                //orcLod.Attack(orc);
                //orcLod.Attack(orc);

                orcLod.Attack(lord);
                orcLod.Attack(lord);
                orcLod.Attack(lord);
                orcLod.Attack(lord);
                orcLod.Attack(lord);
                orcLod.Attack(lord);

            }

            Console.WriteLine("================================================================");

            foreach (var x in _dropPro)
            {
                Console.WriteLine( $"{(double)x / _dropTry * 100:N02}%");
            }
        }


        static void OnItemDropped(object sender, Monster.ItemDroppedEventArgs e)
        {

            foreach (var item in e.DropItems)
            {
                //_dropPro[item.Amount]++;

                Console.Write($"{item.Name}:{item.Amount} / ");

                if (item.ItemId == ItemID.Iron)
                    _dropPro[item.Amount]++;
            }

            Console.WriteLine();

        }


        static void goo()
        {
            Inventory inven = Inventory.Instance;
            Upgrader upgrader = Upgrader.Instance;

            upgrader.UpgradeTried += OnUpTried;

            for (int i = 0; i < _upTry; i++)
            {
                Sword sword = ItemFactory.Instance.Create<Sword>(ItemID.KnightSword);



                UpgradeCost cost = upgrader.GetUpgradeCost(sword);

                foreach (var item in cost.RequireIngredients)
                {
                    GameItem temp = ItemFactory.Instance.Create(item.Key);
                    temp.Amount = item.Value;

                    inven.AddGameItem(temp);
                }

                upgrader.Upgrade<Sword>(ref sword);
            }

            Console.WriteLine("================================================================");

            Console.WriteLine($"{(double)_upSuceess / _upTry * 100:N02}%");

        }

        public static void OnUpTried(object sender, Upgrader.UpgradeTriedEventArgs e)
        {

            if (e.Success)
            {
                Console.WriteLine("성공");
                _upSuceess++;
            }
            else
            {
                Console.WriteLine("실패");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LandRushLibrary.Factory;
using LandRushLibrary.ItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using LandRushLibrary.Utilities;
using Newtonsoft.Json;

namespace Game
{
    class Program
    {
        static private int[] _probability = { 0, 0, 0,0,0,0 };
        static int _try = 100000;

        static void Main(string[] args)
        {
            Player player = Player.Instance;

            PlayerEquipmentManager equipmentManager = PlayerEquipmentManager.Instance;

            equipmentManager.SetEquipmentToSlot(1, ItemFactory.Instance.Create<Sword>(ItemID.OLD_SWORD));
            equipmentManager.SetEquipmentToSlot(2, ItemFactory.Instance.Create<Bow>(ItemID.OLD_BOW));
            equipmentManager.SetEquipmentToSlot(3, ItemFactory.Instance.Create<Shield>(ItemID.OLD_SHIELD));
            equipmentManager.SetEquipmentToSlot(4, ItemFactory.Instance.Create<Quiver>(ItemID.OLD_QUIVER));

            equipmentManager.EquipCurrentPair();

            Monster orc;
            Monster orcLord;

            for (int i = 0; i < _try; i++)
            {
                orc = MonsterFactory.Instance.Create(MonsterID.ORC);
                orc.ItemDropped += OnItemDropped;

                player.Attack(orc, ((Sword)player.RightItem).AttackPower);
                player.Attack(orc, ((Sword)player.RightItem).AttackPower);

                //orcLord = MonsterFactory.Instance.Create(MonsterID.ORC_LORD);
                //orcLord.ItemDropped += OnItemDropped;

                //for (int j = 0; j < 12; j++)
                //{
                //    player.Attack(orcLord, ((Sword)player.RightItem).AttackPower);
                //}
            }

            Console.WriteLine("==================================================================");

            foreach (var item in _probability)
            {
                Console.WriteLine( $"{(double)item / _try * 100:N2}%");
            }

        }

        static void OnItemDropped(object sender, ItemDroppedEventArgs e)
        {


            foreach (var item in e.DropItems)
            {
                Console.Write($"{item.ItemId}:{ item.Amount} / ");

                if(item.ItemId == ItemID.POTION)
                {
                    _probability[item.Amount]++;
                }
            }

            Console.WriteLine();
        }
    }
}

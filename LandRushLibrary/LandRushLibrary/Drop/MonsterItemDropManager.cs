using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LandRushLibrary.Drop
{
    internal class MonsterItemDropManager
    {
        private static MonsterItemDropManager _instance;
        public static MonsterItemDropManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MonsterItemDropManager();

                return _instance;
            }
        }

        private MonsterItemDropManager()
        {
            _dropRates = new Dictionary<MonsterGrade, List<DropList>>();

            List<DropList> normalDropList = new List<DropList>();
            DropList dropList = new DropList();


            dropList = new DropList(ItemID.ARROW, 0.2f, 2);
            normalDropList.Add(dropList);
            dropList = new DropList(ItemID.ARROW, 0.3f, 3);
            normalDropList.Add(dropList);
            dropList = new DropList(ItemID.ARROW, 0.2f, 4);
            normalDropList.Add(dropList);
            dropList = new DropList(ItemID.ARROW, 0.1f, 5);
            normalDropList.Add(dropList);


            dropList = new DropList(ItemID.POTION, 0.6f, 1);
            normalDropList.Add(dropList);

            dropList = new DropList(ItemID.WOOD, 0.5f, 1);
            normalDropList.Add(dropList);
            dropList = new DropList(ItemID.WOOD, 0.3f, 2);
            normalDropList.Add(dropList);
            dropList = new DropList(ItemID.WOOD, 0.2f, 3);
            normalDropList.Add(dropList);

            _dropRates.Add(MonsterGrade.NORMAL, normalDropList);

            ////////////////////////////////////////////////

            List<DropList> bossDropList = new List<DropList>();

            dropList = new DropList(ItemID.ARROW, 0.1f, 2);
            bossDropList.Add(dropList);
            dropList = new DropList(ItemID.ARROW, 0.2f, 3);
            bossDropList.Add(dropList);
            dropList = new DropList(ItemID.ARROW, 0.3f, 4);
            bossDropList.Add(dropList);
            dropList = new DropList(ItemID.ARROW, 0.2f, 5);
            bossDropList.Add(dropList);

            dropList = new DropList(ItemID.POTION, 0.6f, 1);
            bossDropList.Add(dropList);

            dropList = new DropList(ItemID.IRON, 1.0f, 1);
            bossDropList.Add(dropList);

            _dropRates.Add(MonsterGrade.BOSS, bossDropList);

            _random = new Random((int)DateTime.Now.Ticks);
        }

        private Dictionary<MonsterGrade, List<DropList>> _dropRates;
        private Random _random;

        public List<DroppedItems> DropItem(MonsterGrade deadMonsterGrade)
        {
            List<DropList> dropList = _dropRates[deadMonsterGrade];
            List<DroppedItems> finalDropList = new List<DroppedItems>();

            var divList = dropList.GroupBy(x => x.ItemId);

            int random = _random.Next(100);

            foreach (var list in divList)
            {
                int rateSum = 0;

                foreach (var item in list)
                {
                    rateSum += (int)(item.Rate * 100);

                    if (rateSum >= random)
                    {
                        finalDropList.Add(new DroppedItems(item.ItemId, item.Amount));
                        break;
                    }
                }

            }

            return finalDropList;
        }

    }
}


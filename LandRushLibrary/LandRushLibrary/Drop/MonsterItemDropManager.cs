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
            DropList dropList = new DropList();


            List<DropList> normalDropList = new List<DropList>();

            dropList = new DropList(itemId: ItemID.ARROW, rate: 0.2f, amount: 2);
            normalDropList.Add(item: dropList);
            dropList = new DropList(itemId: ItemID.ARROW, rate: 0.3f, amount: 3);
            normalDropList.Add(item: dropList);
            dropList = new DropList(itemId: ItemID.ARROW, rate: 0.2f, amount: 4);
            normalDropList.Add(item: dropList);
            dropList = new DropList(itemId: ItemID.ARROW, rate: 0.1f, amount: 5);
            normalDropList.Add(item: dropList);

            dropList = new DropList(itemId: ItemID.POTION, rate: 0.6f, amount: 1);
            normalDropList.Add(item: dropList);

            dropList = new DropList(itemId: ItemID.WOOD, rate: 0.5f, amount: 1);
            normalDropList.Add(item: dropList);
            dropList = new DropList(itemId: ItemID.WOOD, rate: 0.3f, amount: 2);
            normalDropList.Add(item: dropList);
            dropList = new DropList(itemId: ItemID.WOOD, rate: 0.2f, amount: 3);
            normalDropList.Add(item: dropList);

            _dropRates.Add(key: MonsterGrade.NORMAL, value: normalDropList);

            ////////////////////////////////////////////////

            List<DropList> bossDropList = new List<DropList>();

            dropList = new DropList(itemId: ItemID.ARROW, rate: 0.1f, amount: 2);
            bossDropList.Add(item: dropList);
            dropList = new DropList(itemId: ItemID.ARROW, rate: 0.2f, amount: 3);
            bossDropList.Add(item: dropList);
            dropList = new DropList(itemId: ItemID.ARROW, rate: 0.3f, amount: 4);
            bossDropList.Add(item: dropList);
            dropList = new DropList(itemId: ItemID.ARROW, rate: 0.2f, amount: 5);
            bossDropList.Add(item: dropList);

            dropList = new DropList(itemId: ItemID.POTION, rate: 0.6f, amount: 1);
            bossDropList.Add(item: dropList);

            dropList = new DropList(itemId: ItemID.IRON, rate: 1.0f, amount: 1);
            bossDropList.Add(item: dropList);

            _dropRates.Add(key: MonsterGrade.BOSS, value: bossDropList);

            _random = new Random(Seed: (int)DateTime.Now.Ticks);
        }

        private Dictionary<MonsterGrade, List<DropList>> _dropRates;
        private Random _random;

        /// <summary>
        /// TODO: rateSum 이라는 게 무엇? ramdom이 낮을 수록 얻는 아이템이 많다는 뜻???
        /// </summary>
        /// <param name="deadMonsterGrade"></param>
        /// <returns></returns>
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


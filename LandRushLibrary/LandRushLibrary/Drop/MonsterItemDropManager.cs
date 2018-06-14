using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using System.Collections.Generic;
using System.Linq;
using System;
using LandRushLibrary.Factory;
using LandRushLibrary.Interfaces;
using LandRushLibrary.Items;

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
            _dropRates = new Dictionary<MonsterGrade, List<DropRateInfo>>();
            DropRateInfo dropRateInfo = new DropRateInfo();


            List<DropRateInfo> normalDropList = new List<DropRateInfo>();

            dropRateInfo = new DropRateInfo(ItemID.Arrow, rate: 0.2f, amount: 2);
            normalDropList.Add(item: dropRateInfo);
            dropRateInfo = new DropRateInfo(itemId: ItemID.Arrow, rate: 0.3f, amount: 3);
            normalDropList.Add(item: dropRateInfo);
            dropRateInfo = new DropRateInfo(itemId: ItemID.Arrow, rate: 0.2f, amount: 4);
            normalDropList.Add(item: dropRateInfo);
            dropRateInfo = new DropRateInfo(itemId: ItemID.Arrow, rate: 0.1f, amount: 5);
            normalDropList.Add(item: dropRateInfo);

            dropRateInfo = new DropRateInfo(itemId: ItemID.HpPotion, rate: 0.6f, amount: 1);
            normalDropList.Add(item: dropRateInfo);

            dropRateInfo = new DropRateInfo(itemId: ItemID.Wood, rate: 0.5f, amount: 1);
            normalDropList.Add(item: dropRateInfo);
            dropRateInfo = new DropRateInfo(itemId: ItemID.Wood, rate: 0.3f, amount: 2);
            normalDropList.Add(item: dropRateInfo);
            dropRateInfo = new DropRateInfo(itemId: ItemID.Wood, rate: 0.2f, amount: 3);
            normalDropList.Add(item: dropRateInfo);

            _dropRates.Add(key: MonsterGrade.Normal, value: normalDropList);

            ////////////////////////////////////////////////

            List<DropRateInfo> bossDropList = new List<DropRateInfo>();

            dropRateInfo = new DropRateInfo(itemId: ItemID.Arrow, rate: 0.1f, amount: 2);
            bossDropList.Add(item: dropRateInfo);
            dropRateInfo = new DropRateInfo(itemId: ItemID.Arrow, rate: 0.2f, amount: 3);
            bossDropList.Add(item: dropRateInfo);
            dropRateInfo = new DropRateInfo(itemId: ItemID.Arrow, rate: 0.3f, amount: 4);
            bossDropList.Add(item: dropRateInfo);
            dropRateInfo = new DropRateInfo(itemId: ItemID.Arrow, rate: 0.2f, amount: 5);
            bossDropList.Add(item: dropRateInfo);

            dropRateInfo = new DropRateInfo(itemId: ItemID.HpPotion, rate: 0.6f, amount: 1);
            bossDropList.Add(item: dropRateInfo);

            dropRateInfo = new DropRateInfo(itemId: ItemID.Iron, rate: 1.0f, amount: 1);
            bossDropList.Add(item: dropRateInfo);

            _dropRates.Add(key: MonsterGrade.Boss, value: bossDropList);

            _random = new Random(Seed: (int)DateTime.Now.Ticks);
        }

        private Dictionary<MonsterGrade, List<DropRateInfo>> _dropRates;
        private Random _random;

        /// <summary>
        /// TODO: rateSum 이라는 게 무엇? ramdom이 낮을 수록 얻는 아이템이 많다는 뜻???
        /// </summary>
        /// <param name="deadMonsterGrade"></param>
        /// <returns></returns>
        public List<GameItem> DropItem(MonsterGrade deadMonsterGrade)
        {
            List<DropRateInfo> dropRateInfos = _dropRates[deadMonsterGrade];
            List<GameItem> dropItems = new List<GameItem>();

            var gradeRates = dropRateInfos.GroupBy(x => x.DropItem.ItemId);

            int random = _random.Next(100);

            foreach (var rate in gradeRates)
            {
                int rateSum = 0;

                foreach (var x in rate)
                {
                    rateSum += (int)(x.Rate * 100);

                    if (rateSum >= random)
                    {
                        GameItem dropItem = ItemFactory.Instance.Create(x.DropItem.ItemId);
                        dropItem.Amount = x.DropItem.Amount;
                        dropItems.Add(dropItem);

                        break;
                    }
                }

            }

            return dropItems;
        }

        private class DropRateInfo
        {

            public DropRateInfo()
            {

            }

            public GameItem DropItem { get; private set; }
            public float Rate { get; private set; }

            public DropRateInfo(ItemID itemId, float rate, int amount = 1)
            {
                DropItem = ItemFactory.Instance.Create(itemId);

                if (amount != 1)
                {
                    DropItem.Amount = amount;
                }

                Rate = rate;
            }

        }

    }
}


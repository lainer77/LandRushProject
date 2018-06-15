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
            _dropInfos = new Dictionary<MonsterGrade, List<DropRateInfo>>();
            _dropList = new List<DropRateInfo>();

            InitDropInfos();

            _random = new Random((int)DateTime.Now.Ticks);
        }

        private Dictionary<MonsterGrade, List<DropRateInfo>> _dropInfos;
        private List<DropRateInfo> _dropList;
        private Random _random;

        private void InitDropInfos()
        {

            AddDropInfo(ItemID.Arrow, 0.2f, 2);
            AddDropInfo(ItemID.Arrow, 0.3f, 3);
            AddDropInfo(ItemID.Arrow, 0.2f, 4);
            AddDropInfo(ItemID.Arrow, 0.1f, 5);

            AddDropInfo(ItemID.HpPotion, 0.6f, 1);

            AddDropInfo(ItemID.Wood, 0.5f, 1);
            AddDropInfo(ItemID.Wood, 0.3f, 2);
            AddDropInfo(ItemID.Wood, 0.2f, 3);

            _dropInfos.Add(key: MonsterGrade.Normal, value: _dropList);

            ////////////////////////////////////////////////

            _dropInfos.Clear();

            AddDropInfo(ItemID.Arrow, 0.1f, 2);
            AddDropInfo(ItemID.Arrow, 0.2f, 3);
            AddDropInfo(ItemID.Arrow, 0.3f, 4);
            AddDropInfo(ItemID.Arrow, 0.2f, 5);

            AddDropInfo(ItemID.HpPotion, 0.6f, 1);

            AddDropInfo(ItemID.Iron, 1.0f, 1);

            _dropInfos.Add(MonsterGrade.Boss, _dropList);
        }

        private void AddDropInfo(ItemID itemId, float rate, int amount)
        {
            DropRateInfo dropRateInfo = new DropRateInfo(itemId, rate, amount);

            _dropList.Add(dropRateInfo);
        }

        /// <summary>
        /// TODO: rateSum 이라는 게 무엇? ramdom이 낮을 수록 얻는 아이템이 많다는 뜻???
        /// </summary>
        /// <param name="deadMonsterGrade"></param>
        /// <returns></returns>
        public List<GameItem> DropItem(MonsterGrade deadMonsterGrade)
        {
            List<DropRateInfo> dropRateInfos = _dropInfos[deadMonsterGrade];
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


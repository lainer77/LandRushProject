using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LandRushLibrary.Drop
{
    public class MonsterItemDropManager
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
            DropList rate = new DropList();


            rate = new DropList(ItemID.ARROW, 0.2f, 2);
            normalDropList.Add(rate);
            rate = new DropList(ItemID.ARROW, 0.3f, 3);
            normalDropList.Add(rate);
            rate = new DropList(ItemID.ARROW, 0.2f, 4);
            normalDropList.Add(rate);
            rate = new DropList(ItemID.ARROW, 0.1f, 5);
            normalDropList.Add(rate);


            rate = new DropList(ItemID.POTION, 0.6f, 1);
            normalDropList.Add(rate);

            rate = new DropList(ItemID.WOOD, 0.5f, 1);
            normalDropList.Add(rate);
            rate = new DropList(ItemID.WOOD, 0.3f, 2);
            normalDropList.Add(rate);
            rate = new DropList(ItemID.WOOD, 0.2f, 3);
            normalDropList.Add(rate);

            _dropRates.Add(MonsterGrade.NORMAL, normalDropList);

            ////////////////////////////////////////////////

            List<DropList> bossDropList = new List<DropList>();

            rate = new DropList(ItemID.ARROW, 0.1f, 2);
            bossDropList.Add(rate);
            rate = new DropList(ItemID.ARROW, 0.2f, 3);
            bossDropList.Add(rate);
            rate = new DropList(ItemID.ARROW, 0.3f, 4);
            bossDropList.Add(rate);
            rate = new DropList(ItemID.ARROW, 0.2f, 5);
            bossDropList.Add(rate);

            rate = new DropList(ItemID.POTION, 0.6f, 1);
            bossDropList.Add(rate);

            rate = new DropList(ItemID.IRON, 1.0f, 1);
            bossDropList.Add(rate);

            _dropRates.Add(MonsterGrade.BOSS, bossDropList);

            _random = new Random((int)DateTime.Now.Ticks);
        }

        private Dictionary<MonsterGrade, List<DropList>> _dropRates;
        private Random _random;

        public void DropItem(Monster deadMonster)
        {
            List<DropList> dropList = _dropRates[deadMonster.MonsterGrade];
            List<DropInfo> finalDropList = new List<DropInfo>();

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
                        finalDropList.Add(new DropInfo(item.ItemId, item.Amount));
                        break;
                    }
                }

            }

            OnItemDropped(new ItemDroppedEventArgs(finalDropList));
        }

        #region ItemDropped event things for C# 3.0
        public event EventHandler<ItemDroppedEventArgs> ItemDropped;

        protected virtual void OnItemDropped(ItemDroppedEventArgs e)
        {
            if (ItemDropped != null)
                ItemDropped(this, e);
        }

        private ItemDroppedEventArgs OnItemDropped(List<DropInfo> dropInfos)
        {
            ItemDroppedEventArgs args = new ItemDroppedEventArgs(dropInfos);
            OnItemDropped(args);

            return args;
        }

        private ItemDroppedEventArgs OnItemDroppedForOut()
        {
            ItemDroppedEventArgs args = new ItemDroppedEventArgs();
            OnItemDropped(args);

            return args;
        }

        public class ItemDroppedEventArgs : EventArgs
        {
            public List<DropInfo> DropInfos { get; set; }

            public ItemDroppedEventArgs()
            {
            }

            public ItemDroppedEventArgs(List<DropInfo> dropInfos)
            {
                DropInfos = dropInfos;
            }
        }
        #endregion

    }
}


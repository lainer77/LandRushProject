using System.Collections.Generic;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using LandRushLibrary.Utilities;

namespace LandRushLibrary.Factory
{
    public class MonsterFactory
    {
        #region Singleton
        private static MonsterFactory _instace;

        public static MonsterFactory Instance
        {
            get
            {
                if(_instace == null)
                    _instace = new MonsterFactory();

                return _instace;
            }
        }

        private MonsterFactory()
        {
            _monsters = MonsterSerializer.Instance.Deseriailize();
        }

        private readonly Dictionary<MonsterID, Monster> _monsters;
        #endregion

        public Monster Create(MonsterID monsterId)
        {
            Monster monster = _monsters[monsterId].Clone();

            return monster;
        }

    }
}

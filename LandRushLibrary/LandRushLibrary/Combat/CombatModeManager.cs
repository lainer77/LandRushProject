
using System;

namespace LandRushLibrary.Combat
{
    public class CombatModeManager
    {
        private static CombatModeManager _instance;

        public static CombatModeManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new CombatModeManager();

                return _instance;
            }
        }

        public bool IsCombatMode { get; private set; }

        public void SetCombatMode(bool combatMode)
        {
            IsCombatMode = combatMode;

            OnCombatModeChnage(new CombatModeChnageEventArgs(combatMode));
        }


        #region CombatModeChnage event things for C# 3.0
        public event EventHandler<CombatModeChnageEventArgs> CombatModeChnage;

        protected virtual void OnCombatModeChnage(CombatModeChnageEventArgs e)
        {
            if (CombatModeChnage != null)
                CombatModeChnage(this, e);
        }

        private CombatModeChnageEventArgs OnCombatModeChnage(bool isCombatMode)
        {
            CombatModeChnageEventArgs args = new CombatModeChnageEventArgs(isCombatMode);
            OnCombatModeChnage(args);

            return args;
        }

        private CombatModeChnageEventArgs OnCombatModeChnageForOut()
        {
            CombatModeChnageEventArgs args = new CombatModeChnageEventArgs();
            OnCombatModeChnage(args);

            return args;
        }


        #endregion
    }

    public class CombatModeChnageEventArgs : EventArgs
    {
        public bool IsCombatMode { get; set; }

        public CombatModeChnageEventArgs()
        {
        }

        public CombatModeChnageEventArgs(bool isCombatMode)
        {
            IsCombatMode = isCombatMode;
        }
    }
}

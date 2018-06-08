
using System;

namespace LandRushLibrary.Map
{
    public class StageMap
    {
        public StageMap(Room startRoom)
        {
            CurrentRoom = startRoom;
        }

        public Room CurrentRoom { get; private set; }

        public void MoveRoo(RoomDistance distance)
        {

            try
            {
                switch (distance)
                {
                    case RoomDistance.Left:
                        CurrentRoom = CurrentRoom.LeftRoom;
                        break;
                    case RoomDistance.Right:
                        CurrentRoom = CurrentRoom.RightRoom;
                        break;
                    case RoomDistance.Previous:
                        CurrentRoom = CurrentRoom.PrevRoom;
                        break;
                    case RoomDistance.Next:
                        CurrentRoom = CurrentRoom.NextRoom;
                        break;
                }
            }
            catch(NullReferenceException e)
            {
                OnNotExistRoom(new NotExistRoomEventArgs(distance));
                return;
            }

            OnChangedCurrentRoom(new ChangedCurrentRoomEventArgs(CurrentRoom, distance));
        }

        #region ChangedCurrentRoom event things for C# 3.0
        public event EventHandler<ChangedCurrentRoomEventArgs> ChangedCurrentRoom;

        protected virtual void OnChangedCurrentRoom(ChangedCurrentRoomEventArgs e)
        {
            if (ChangedCurrentRoom != null)
                ChangedCurrentRoom(this, e);
        }

        private ChangedCurrentRoomEventArgs OnChangedCurrentRoom(Room room, RoomDistance distance)
        {
            ChangedCurrentRoomEventArgs args = new ChangedCurrentRoomEventArgs(room, distance);
            OnChangedCurrentRoom(args);

            return args;
        }

        private ChangedCurrentRoomEventArgs OnChangedCurrentRoomForOut()
        {
            ChangedCurrentRoomEventArgs args = new ChangedCurrentRoomEventArgs();
            OnChangedCurrentRoom(args);

            return args;
        }

        public class ChangedCurrentRoomEventArgs : EventArgs
        {
            public Room Room { get; set; }
            public RoomDistance Distance { get; set; }

            public ChangedCurrentRoomEventArgs()
            {
            }

            public ChangedCurrentRoomEventArgs(Room room, RoomDistance distance)
            {
                Room = room;
                Distance = distance;
            }
        }
        #endregion

        #region NotExistRoom event things for C# 3.0
        public event EventHandler<NotExistRoomEventArgs> NotExistRoom;

        protected virtual void OnNotExistRoom(NotExistRoomEventArgs e)
        {
            if (NotExistRoom != null)
                NotExistRoom(this, e);
        }

        private NotExistRoomEventArgs OnNotExistRoom(RoomDistance distance)
        {
            NotExistRoomEventArgs args = new NotExistRoomEventArgs(distance);
            OnNotExistRoom(args);

            return args;
        }

        private NotExistRoomEventArgs OnNotExistRoomForOut()
        {
            NotExistRoomEventArgs args = new NotExistRoomEventArgs();
            OnNotExistRoom(args);

            return args;
        }

        public class NotExistRoomEventArgs : EventArgs
        {
            public RoomDistance Distance { get; set; }

            public NotExistRoomEventArgs()
            {
            }

            public NotExistRoomEventArgs(RoomDistance distance)
            {
                Distance = distance;
            }
        }
        #endregion


    }

    public enum RoomDistance
    {
        Left,
        Right,
        Previous,
        Next
    }
}


namespace LandRushLibrary.Map
{
    public class Room
    {
        public int RoomNumber { get; set; }

        public Room LeftRoom { get; private set; }
        public Room RightRoom { get; private set; }
        public Room PrevRoom { get; private set; }
        public Room NextRoom { get; private set; }

        public void SetLeftRoom(Room leftRoom)
        {
            LeftRoom = leftRoom;
            leftRoom.RightRoom = this;
        }


        public void SetRightRoom(Room rightRoom)
        {
            RightRoom = rightRoom;
            rightRoom.LeftRoom = this;
        }

        public void SetPreRoom(Room prevRoom)
        {
            PrevRoom = prevRoom;
            prevRoom.NextRoom = this;
        }

        public void SetNextRoom(Room nextRoom)
        {
            NextRoom = nextRoom;
            nextRoom.PrevRoom = this;
        }
    }
}

namespace UncleTayHouse.Models
{
    public class GameItem
    {
        public int id = 0;
        public int location = 0;
        public int objId = 0;
        public string name = "";
        public string desc = "";

        public bool IsCarrying()
        {
            if (location == Constants.CARRYING)
            {
                return true;
            }
            return false;
        }
        public bool IsHidden()
        {
            if (location == Constants.HIDDEN)
            {
                return true;
            }
            return false;
        }
        public bool IsTied()
        {
            if (location == Constants.TIED)
            {
                return true;
            }
            return false;
        }
        public bool TieItem()
        {
            location = Constants.TIED;
            return true;
        }
        public bool TakeItem()
        {
            location = Constants.CARRYING;
            return true;
        }
        public bool LeaveItem(int position)
        {
            location = position;
            return true;
        }
        public bool HideItem()
        {
            location = Constants.HIDDEN;
            return true;
        }

    }
}
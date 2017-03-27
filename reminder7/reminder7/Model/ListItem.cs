using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reminder7.Model
{
    class ListItem
    {
        public string DisplayTitle { get; internal set; }
        public string DisplayDateAndTime { get; internal set; }

        public Boolean isEndItem = false;

        public DateTime getDateTime()
        {
            return DateTime.Parse(DisplayDateAndTime);
        }

        public void setIsEndItem(Boolean isEnd)
        {
            isEndItem = isEnd;
        }

        public Boolean getIsEndItem()
        {
            return isEndItem;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reminder7.Model
{
    public interface Notifications
    {
        void pushNotification(String text);
        void clearNotification();
    }
}

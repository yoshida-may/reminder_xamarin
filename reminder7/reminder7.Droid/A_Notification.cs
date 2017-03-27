using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using reminder7.Model;

namespace reminder7.Droid
{
    public class A_Notification : Notifications
    {
        public void pushNotification(String text)
        {
            // 通知エリアに表示
            var intent = new Android.Content.Intent(Android.App.Application.Context, typeof(MainActivity));
            var pendingIntntent = PendingIntent.GetActivity(
                Android.App.Application.Context, 0, intent, 0);

            var n = new Notification.Builder(Android.App.Application.Context)
                .SetContentTitle("予定の時刻になりました")
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentText(text)

                .SetOngoing(true)
                .SetContentIntent(pendingIntntent)

                .SetVibrate(new long[] { 0, 200, 100, 200, 100, 200 })
                .SetTicker(text)
                .Build();

            var nManager = (NotificationManager)Android.App.Application.Context.GetSystemService(
                Context.NotificationService);
            nManager.Notify(0, n);

            Intent alertIntent = new Intent(Android.App.Application.Context, typeof(AlertDialogActivity));
            alertIntent.PutExtra("message", text);
            alertIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(alertIntent);
        }

        public void clearNotification()
        {
            var nManager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            nManager.Cancel(0);
        }
    }
}
using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using reminder7.Model;
using Android.Content;
using Xamarin.Forms;
using reminder7.Droid;

[assembly: Dependency(typeof(A_Notification))]

namespace reminder7.Droid
{
    [Activity(Label = "reminder7", Icon = "@drawable/icon", 
        Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode =LaunchMode.SingleInstance)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();

            // Notificationを削除
            var n = DependencyService.Get<Notifications>();
            n.clearNotification();
        }
    }
}


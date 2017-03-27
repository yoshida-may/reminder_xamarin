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

namespace reminder7.Droid
{
    [Activity(Label = "AlertDialogActivity", Theme = "@android:style/Theme.Translucent.NoTitleBar")]
    public class AlertDialogActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Intent intent = this.Intent;
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle("予定の時刻になりました");
            alertDialogBuilder.SetMessage(intent.GetStringExtra("message"));
            alertDialogBuilder.SetPositiveButton("OK", OnClickedOk);
            alertDialogBuilder.SetCancelable(false);
            AlertDialog alertDialog = alertDialogBuilder.Create();
            alertDialog.Show();

            // Create your application here
        }

        private void OnClickedOk(object sender, DialogClickEventArgs e)
        {
            // Finishしない・・・。
            // https://forums.xamarin.com/discussion/15804/close-activity-after-alert-dialog　と同じっぽい。
            Finish();
        }
    }
}
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
using KojosAvailability.Droid.Helpers;

namespace KojosAvailability.Droid.Receivers
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted, Intent.ActionReboot })]
    public class OnBoot : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            NotificationHelper.StartAvailabilityNotification(context);
        }
    }
}
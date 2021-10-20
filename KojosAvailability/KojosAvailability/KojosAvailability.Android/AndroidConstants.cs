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

namespace KojosAvailability.Droid
{
    class AndroidConstants
    {
        public const string channelId = "firestatus";
        public const string channelName = "Fire Status";
        public const string channelDescription = "Fire Status 15 minute update channel.";
        public const int pendingIntentId = 0;

        public const string TitleKey = "title";
        public const string MessageKey = "message";

    }
}
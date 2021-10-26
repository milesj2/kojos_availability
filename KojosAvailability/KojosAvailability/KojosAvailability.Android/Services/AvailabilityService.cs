using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Kojos.GartanClient.CommonModels;
using KojosAvailability.Helpers;

namespace KojosAvailability.Droid.Services
{
    [Service(Name = "uk.kojos.availability.OnCallNotificationJob",
        Permission = "android.permission.BIND_JOB_SERVICE")]
    public class AvailabilityService : JobService
    {
        NotificationManager manager;

        bool channelInitialized = false;
        int messageId = -1;

        static bool _refreshing;

        public override bool OnStartJob(JobParameters jobParams)
        {
            Task.Run(async () =>
            {
                await DoWork();
                JobFinished(jobParams, false);
            });

            // Return true because of the asynchronous work
            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            throw new NotImplementedException();
        }

        async Task<bool> DoWork()
        {
            if (_refreshing) return true;

            _refreshing = true;

            if (!channelInitialized) CreateNotificationChannel();

            string message;
            string icoIdentifier;

            OnCallStatusModel onCallStatus = null;

            if (!await WatchGuardHelper.AuthenticateWatchGuard())
            {
                _refreshing = false;
                return false;
            }

            try
            {
                if (!await GartanHelper.InitialiseGartan())
                {
                    _refreshing = false;
                    return false;
                }
                onCallStatus = await GartanHelper.GetOnCallStatus();
            }
            catch (Exception e)
            {
            }

            if (onCallStatus == null)
            {
                _refreshing = false;
                return false;
            }
            string onCall = onCallStatus.OnCall ? "On" : "Off";
            string onTheRun = onCallStatus.OnTheRun ? "On" : "Off";

            message = $"{onCall} call | {onTheRun} the run";
            icoIdentifier = $"{onCall.ToLower()}_call_{onTheRun.ToLower()}_run";

            CreateNotification("Status", message, icoIdentifier);


            _refreshing = false;


            return true;
        }

        void CreateNotification(string title, string message, string icoIdentifier)
        {
            messageId++;

            Intent intent = new Intent(Application.Context, typeof(MainActivity));
            intent.PutExtra(AndroidConstants.TitleKey, title);
            intent.PutExtra(AndroidConstants.MessageKey, message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(Application.Context, AndroidConstants.pendingIntentId, intent, PendingIntentFlags.OneShot);

            var ico = Resources.GetIdentifier(icoIdentifier, "drawable", Application.Context.PackageName);
            var sml_ico = Resources.GetIdentifier("sml_ico_" + icoIdentifier, "drawable", Application.Context.PackageName);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context, AndroidConstants.channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetLargeIcon(BitmapFactory.DecodeResource(Application.Context.Resources, ico))
                .SetSmallIcon(sml_ico)
                .SetDefaults(NotificationCompat.DefaultLights | NotificationCompat.DefaultSound)
                .SetVibrate(new long[] { 0L })
                .SetSound(null)
                .SetOngoing(true);

            var notification = builder.Build();

            manager.Notify(messageId, notification);

            //return messageId;

            return;
        }

        void CreateNotificationChannel()
        {
            manager = (NotificationManager)Application.Context.GetSystemService(Application.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(AndroidConstants.channelName);
                var channel = new NotificationChannel(AndroidConstants.channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = AndroidConstants.channelDescription
                };
                channel.EnableVibration(false);
                channel.SetSound(null, null);
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
    }
}
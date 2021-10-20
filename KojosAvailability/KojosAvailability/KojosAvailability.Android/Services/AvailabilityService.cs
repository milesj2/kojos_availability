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
            if (!channelInitialized)
                CreateNotificationChannel();

            string message;
            string icoIdentifier;

            OnCallStatusModel onCallStatus = null;

            try
            {
                onCallStatus = await GartanHelper.GetOnCallStatus();
            }
            catch (Exception e)
            {
            }

            if (onCallStatus == null) return false;

            if (onCallStatus.OnTheRun && onCallStatus.OnCall)
            {
                message = "On call | On the run";
                icoIdentifier = "on_call_on_run";
            }
            else if (onCallStatus.OnTheRun && !onCallStatus.OnCall)
            {
                message = "Off call | On the run";
                icoIdentifier = "off_call_on_run";
            }
            else if (!onCallStatus.OnTheRun && onCallStatus.OnCall)
            {
                message = "On call | Off the run";
                icoIdentifier = "on_call_off_run";
            }
            else
            {
                message = "off call | off the run";
                icoIdentifier = "off_call_off_run";
            }

            CreateNotification("Status", message, icoIdentifier);

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
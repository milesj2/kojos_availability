using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KojosAvailability.Droid.Helpers
{
    public static class NotificationHelper
    {
        public static void StartAvailabilityNotification(Context context)
        {
            var javaClass = Java.Lang.Class.FromType(typeof(Services.AvailabilityService));
            var componentName = new ComponentName(context, javaClass);
            var jobBuilder = new JobInfo.Builder(1, componentName);
            jobBuilder.SetPeriodic((int)TimeSpan.FromMinutes(15).TotalMilliseconds);

            var jobInfo = jobBuilder.Build();

            var jobScheduler = (JobScheduler)context.GetSystemService("jobscheduler");
            var scheduleResult = jobScheduler.Schedule(jobInfo);

            if (JobScheduler.ResultSuccess == scheduleResult)
            {
                Console.WriteLine("Worked");
            }
            else
            {
                Console.WriteLine("Didn't");
            }
        }
    }
}
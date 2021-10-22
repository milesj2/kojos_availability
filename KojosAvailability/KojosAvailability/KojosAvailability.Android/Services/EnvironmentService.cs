using System;
using System.Threading.Tasks;
using Android.Content.Res;
using Android.OS;
using KojosAvailability.Droid.Services;
using KojosAvailability.Interfaces;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(EnvironmentService))]
namespace KojosAvailability.Droid.Services
{

    public class EnvironmentService : IEnvironment
    {
        public Task<Theme> GetOperatingSystemThemeAsync() => Task.FromResult(GetOperatingSystemTheme());

        public Theme GetOperatingSystemTheme()
        {
            //Ensure the device is running Android Froyo or higher because UIMode was added in Android Froyo, API 8.0
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Froyo)
            {
                var uiModelFlags = CrossCurrentActivity.Current.AppContext.Resources.Configuration.UiMode & UiMode.NightMask;

                switch (uiModelFlags)
                {
                    case UiMode.NightYes:
                        return Theme.Dark;

                    case UiMode.NightNo:
                        return Theme.Light;

                    default:
                        throw new NotSupportedException($"UiMode {uiModelFlags} not supported");
                }
            }
            else
            {
                return Theme.Light;
            }
        }
    }
}
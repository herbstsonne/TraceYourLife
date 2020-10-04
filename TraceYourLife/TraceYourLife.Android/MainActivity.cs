using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace TraceYourLife.Droid
{
    [Activity(Label = "TraceYourLife", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            CopyDatabaseFromAssetsToExternalAndroidStorage();
            LoadApplication(new App());
        }

        private void CopyDatabaseFromAssetsToExternalAndroidStorage()
        {
            try
            {
                //TODO entfernen
                File.Delete(Path.Combine(AppGlobal.DatabaseFilePathAndroid, AppGlobal.DatabaseFileName));
                if (!File.Exists(Path.Combine(AppGlobal.DatabaseFilePathAndroid, AppGlobal.DatabaseFileName)))
                {
                    using (var asset = Assets.Open(AppGlobal.DatabaseFileName))
                    using (var dest = File.Create(AppGlobal.DatabaseFilePathAndroid))
                    {
                        asset.CopyTo(dest);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}
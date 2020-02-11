using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Transformations;

namespace DLToolkitControlsSamples.Droid
{
    [Activity(Label = "DLToolkitControlsSamples.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		public static Activity Current { get; private set; }

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			CachedImageRenderer.Init(true);
            var ignore1 = typeof(CircleTransformation);

			LoadApplication(new App());

			Current = this;

			if (ContextCompat.CheckSelfPermission(MainActivity.Current, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
			{
				ActivityCompat.RequestPermissions(MainActivity.Current, new string[] { Manifest.Permission.ReadExternalStorage }, 1);
			}
		}
	}
}

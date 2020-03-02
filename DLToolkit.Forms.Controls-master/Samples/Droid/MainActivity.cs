using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Transformations;
using Plugin.Permissions;

namespace DLToolkitControlsSamples.Droid
{
    [Activity(Label = "Gallery Sample", Icon = "@drawable/icon", Theme = "@style/MyTheme", LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		public static Activity Current { get; private set; }

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			Rg.Plugins.Popup.Popup.Init(this, bundle);

			CachedImageRenderer.Init(true);
			var ignore1 = typeof(CircleTransformation);

			LoadApplication(new App());

			Current = this;

			Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}

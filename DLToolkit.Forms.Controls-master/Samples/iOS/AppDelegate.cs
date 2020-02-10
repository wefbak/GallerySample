
using FFImageLoading.Forms.Touch;
using FFImageLoading.Transformations;
using Foundation;
using UIKit;

namespace DLToolkitControlsSamples.iOS
{
    [Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
            CachedImageRenderer.Init();
            var ignore1 = typeof(CircleTransformation);
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}

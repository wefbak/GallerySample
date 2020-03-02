
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
            Rg.Plugins.Popup.Popup.Init();
            CachedImageRenderer.Init();
            var ignore1 = typeof(CircleTransformation);
			global::Xamarin.Forms.Forms.Init();
            
            LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

        public override UIWindow Window
        {
            get => base.Window;
            set
            {
                base.Window = value;

                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0) && value != null)
                {
                    Utils.SafeAreaInsets = new Xamarin.Forms.Thickness(value.SafeAreaInsets.Left, value.SafeAreaInsets.Top, value.SafeAreaInsets.Right, value.SafeAreaInsets.Bottom);
                }
            }
        }
    }
}

using System.Linq;
using DLToolkitControlsSamples;
using DLToolkitControlsSamples.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static DLToolkitControlsSamples.MainPageModel;

[assembly: ExportRenderer(typeof(CustomView), typeof(MyRenderer))]
namespace DLToolkitControlsSamples.iOS.Renderers
{
    public class MyRenderer : ViewRenderer<CustomView, UIView>
    {
        UITapGestureRecognizer _tapRecognizer;
        UILongPressGestureRecognizer _longTapRecognizer;


        protected override void OnElementChanged(ElementChangedEventArgs<CustomView> e)
        {
            base.OnElementChanged(e);

            _tapRecognizer = new UITapGestureRecognizer(HandleTapped);
            _longTapRecognizer = new UILongPressGestureRecognizer(HandleLongTapped);
            this.NativeView.AddGestureRecognizer(_tapRecognizer);
            this.NativeView.AddGestureRecognizer(_longTapRecognizer);
        }

        protected override void Dispose(bool disposing)
        {
            this.NativeView.RemoveGestureRecognizer(_tapRecognizer);
            this.NativeView.RemoveGestureRecognizer(_longTapRecognizer);

            base.Dispose(disposing);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
        }

        void HandleTapped()
        {
            var myRecognizer = (MyTapRecognizer)this.Element.GestureRecognizers.FirstOrDefault(x => x.GetType() == typeof(MyTapRecognizer));

            myRecognizer.Tap((ItemModel)this.Element.BindingContext, false);
        }

        void HandleLongTapped()
        {
            var myRecognizer = (MyTapRecognizer)this.Element.GestureRecognizers.FirstOrDefault(x => x.GetType() == typeof(MyTapRecognizer));
            myRecognizer.Tap((ItemModel)this.Element.BindingContext, true);
        }
    }
}

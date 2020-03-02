using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Views;
using DLToolkitControlsSamples;
using DLToolkitControlsSamples.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static DLToolkitControlsSamples.MainPageModel;

[assembly: ExportRenderer(typeof(CustomView), typeof(MyRendererDroid))]
namespace DLToolkitControlsSamples.Droid.Renderers
{
    public class MyRendererDroid : ViewRenderer<CustomView, Android.Views.View>
	{
		public MyRendererDroid(Context context) : base(context) { }

		bool _tapping = false;

		CancellationTokenSource longPressToken;

		public override bool DispatchTouchEvent(MotionEvent e)
		{
			if (e.Action == MotionEventActions.Down)
			{
				_tapping = true;

				longPressToken?.Cancel();
				longPressToken = new CancellationTokenSource();

				var w = WaitLongPress(longPressToken.Token);
			}
			else if (e.Action == MotionEventActions.Outside && _tapping)
			{
				longPressToken?.Cancel();
				_tapping = false;
			}
			else if (e.Action == MotionEventActions.Up && _tapping)
			{
				_tapping = false;
				longPressToken?.Cancel();

				var myRecognizer = (MyTapRecognizer)this.Element.GestureRecognizers.FirstOrDefault(x => x.GetType() == typeof(MyTapRecognizer));
				myRecognizer?.Tap((ItemModel)this.Element.BindingContext, false);
			}

			return base.DispatchTouchEvent(e);
		}


        async Task WaitLongPress(CancellationToken ct)
        {
			await Task.Delay(1000);

			if (ct.IsCancellationRequested)
				return;

			_tapping = false;
			longPressToken?.Cancel();

			var myRecognizer = (MyTapRecognizer)this.Element.GestureRecognizers.FirstOrDefault(x => x.GetType() == typeof(MyTapRecognizer));
			myRecognizer?.Tap((ItemModel)this.Element.BindingContext, true);
		}
    }
}


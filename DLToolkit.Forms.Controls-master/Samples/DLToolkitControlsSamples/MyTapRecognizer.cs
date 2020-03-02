using System;
using Xamarin.Forms;
using static DLToolkitControlsSamples.MainPageModel;

namespace DLToolkitControlsSamples
{
    public class MyTapRecognizer : Element, IGestureRecognizer
    {
        public event EventHandler<TappedEventArgs> Tapped;

        public void Tap(ItemModel item, bool longTap)
        {
            Tapped?.Invoke(this, new TappedEventArgs(item, longTap));
        }
    }

    public class TappedEventArgs : EventArgs
    {
        public ItemModel Item { get; private set; }
        public bool LongTap { get; private set; }

        public TappedEventArgs(ItemModel item, bool longTap)
        {
            Item = item;
            LongTap = longTap;
        }
    }
}
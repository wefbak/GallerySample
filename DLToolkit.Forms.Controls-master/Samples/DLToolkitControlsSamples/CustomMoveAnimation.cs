using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Animations.Base;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace DLToolkitControlsSamples.PopUps
{
    public class CustomMoveAnimation : FadeBackgroundAnimation
    {
        private double _defaultTranslationX;
        private double _defaultTranslationY;

        public MoveAnimationOptions PositionIn { get; set; }
        public MoveAnimationOptions PositionOut { get; set; }

        public CustomMoveAnimation() : this(MoveAnimationOptions.Bottom, MoveAnimationOptions.Bottom) { }

        public CustomMoveAnimation(MoveAnimationOptions positionIn, MoveAnimationOptions positionOut)
        {
            PositionIn = positionIn;
            PositionOut = positionOut;

            DurationIn = DurationOut = 300;
            EasingIn = Easing.SinOut;
            EasingOut = Easing.SinIn;
        }

        public override void Preparing(View content, PopupPage page)
        {
            base.Preparing(content, page);

            page.IsVisible = false;

            if (content == null) return;

            UpdateDefaultTranslations(content);
        }

        public override void Disposing(View content, PopupPage page)
        {
            base.Disposing(content, page);

            page.IsVisible = true;

            if (content == null) return;

            content.TranslationX = _defaultTranslationX;
            content.TranslationY = _defaultTranslationY;
        }

        public async override Task Appearing(View content, PopupPage page)
        {
            var taskList = new List<Task>();

            taskList.Add(base.Appearing(content, page));

            if (content != null)
            {
                var topOffset = GetTopOffset(content, page);
                var leftOffset = GetLeftOffset(content, page);

                switch (PositionIn)
                {
                    case MoveAnimationOptions.Top:
                        content.TranslationY = -topOffset;
                        break;
                    case MoveAnimationOptions.Bottom:
                        content.TranslationY = topOffset;
                        break;

                    case MoveAnimationOptions.Left:
                        content.TranslationX = -leftOffset;
                        break;
                    case MoveAnimationOptions.Right:
                        content.TranslationX = leftOffset;
                        break;
                }

                // Review if wait is still necessary to avoid flickering in PopUp Menu
                await Task.Delay(1);

                taskList.Add(content.TranslateTo(_defaultTranslationX, _defaultTranslationY, DurationIn, EasingIn));
            }

            page.IsVisible = true;

            await Task.WhenAll(taskList);
        }

        public async override Task Disappearing(View content, PopupPage page)
        {
            var taskList = new List<Task>();

            taskList.Add(base.Disappearing(content, page));

            if (content != null)
            {
                UpdateDefaultTranslations(content);

                var topOffset = GetTopOffset(content, page);
                var leftOffset = GetLeftOffset(content, page);

                switch (PositionOut)
                {
                    case MoveAnimationOptions.Top:
                        taskList.Add(content.TranslateTo(_defaultTranslationX, -topOffset, DurationOut, EasingOut));
                        break;
                    case MoveAnimationOptions.Bottom:
                        taskList.Add(content.TranslateTo(_defaultTranslationX, topOffset, DurationOut, EasingOut));
                        break;
                    case MoveAnimationOptions.Left:
                        taskList.Add(content.TranslateTo(-leftOffset, _defaultTranslationY, DurationOut, EasingOut));
                        break;
                    case MoveAnimationOptions.Right:
                        taskList.Add(content.TranslateTo(leftOffset, _defaultTranslationY, DurationOut, EasingOut));
                        break;
                }
            }

            await Task.WhenAll(taskList);
        }

        private void UpdateDefaultTranslations(View content)
        {
            _defaultTranslationX = content.TranslationX;
            _defaultTranslationY = content.TranslationY;
        }
    }
}

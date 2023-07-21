using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml;

namespace ReproAnimationCrash
{
    internal static class AnimationControlVisualStates
    {
        internal const string Animate = nameof(Animate);
        internal const string Normal = nameof(Normal);
    }

    [TemplateVisualState(GroupName = "Common", Name = AnimationControlVisualStates.Animate)]
    [TemplatePart(Name = "AnimationToNormalStoryBoard", Type = typeof(Storyboard))]
    [TemplatePart(Name = "AnimationToAnimateStoryboard", Type = typeof(Storyboard))]
    public sealed class AnimationControl : ContentControl
    {
        public static readonly DependencyProperty AnimateProperty =
            DependencyProperty.Register(nameof(Animate), typeof(bool), typeof(AnimationControl), new PropertyMetadata(false, OnAnimateChanged));

        public AnimationControl()
        {
            DefaultStyleKey = typeof(AnimationControl);
        }

        public bool Animate
        {
            get => (bool)GetValue(AnimateProperty);
            set => SetValue(AnimateProperty, value);
        }

        private static void OnAnimateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not bool animate || d is not Control control)
            {
                return;
            }

            VisualStateManager.GoToState(
                control,
                animate ? AnimationControlVisualStates.Animate : AnimationControlVisualStates.Normal,
                true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PCBuilder.ViewModel
{
    public abstract class FrameViewModel<F, W> : BaseViewModel
        where F : Page
        where W : Window
    {
        public F Owner { get; private set; }
        public W OwnerWindow { get; private set; }

        public FrameViewModel(F owner, W window)
        {
            Owner = owner;
            OwnerWindow = window;
        }

        protected void AnimateAwake(FrameworkElement block)
        {
            ThicknessAnimation marginAnim = new ThicknessAnimation();

            marginAnim.From = new Thickness(0,15,0,0);
            marginAnim.To = new Thickness(0);
            marginAnim.EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut };

            marginAnim.Duration = TimeSpan.FromSeconds(0.4);

            block.BeginAnimation(FrameworkElement.MarginProperty, marginAnim);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCBuilder.CustomUI
{
    /// <summary>
    /// Логика взаимодействия для TextBoxEx.xaml
    /// </summary>
    public partial class TextBoxEx : UserControl
    {
        public static readonly DependencyProperty PlaceholderProperty;

        public static readonly RoutedEvent ChangedEvent;

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public event RoutedEventHandler Changed
        {
            add => AddHandler(ChangedEvent, value);
            remove => RemoveHandler(ChangedEvent, value);
        }

        public string Text { get; private set; }

        public bool IsEmpty { get; private set; }

        private Thickness _thickness;
        private double _fontsize;

        static TextBoxEx()
        {
            PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string),
                typeof(TextBoxEx), new PropertyMetadata(string.Empty, OnPlaceholderChanged));

            ChangedEvent = EventManager.RegisterRoutedEvent(
                "Changed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextBoxEx)
                );
        }

        public TextBoxEx()
        {
            InitializeComponent();

            box.TextChanged += Box_TextChanged;

            IsEmpty = true;

            DataContext = this;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            _thickness = new Thickness(0, (box.ActualHeight / 2) - (placeholder.ActualHeight / 2), 12, 0);
            _fontsize = FontSize;

            placeholder.Margin = _thickness;
        }

        public void PlaceholderAnimationOut()
        {
            DoubleAnimation anim0 = new DoubleAnimation();

            anim0.From = _fontsize;
            anim0.To = 9;
            anim0.Duration = TimeSpan.FromMilliseconds(400);
            anim0.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut };

            ThicknessAnimation anim1 = new ThicknessAnimation();

            anim1.From = _thickness;
            anim1.To = new Thickness(0, 6, 12, 0);
            anim1.Duration = TimeSpan.FromMilliseconds(400);
            anim1.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut };

            placeholder.BeginAnimation(TextBlock.FontSizeProperty, anim0);
            placeholder.BeginAnimation(MarginProperty, anim1);
        }
        public void PlaceholderAnimationIn()
        {
            DoubleAnimation anim0 = new DoubleAnimation();

            anim0.From = 9;
            anim0.To = _fontsize;
            anim0.Duration = TimeSpan.FromMilliseconds(700);
            anim0.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut };

            ThicknessAnimation anim1 = new ThicknessAnimation();

            anim1.From = new Thickness(0, 6, 12, 0);
            anim1.To = _thickness;
            anim1.Duration = TimeSpan.FromMilliseconds(700);
            anim1.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut };

            placeholder.BeginAnimation(TextBlock.FontSizeProperty, anim0);
            placeholder.BeginAnimation(MarginProperty, anim1);
        }

        private void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isEmpty = box.Text == string.Empty;

            if (!isEmpty && IsEmpty)
                PlaceholderAnimationOut();
            else if (isEmpty && !IsEmpty)
                PlaceholderAnimationIn();

            IsEmpty = isEmpty;

            Text = box.Text;

            RaiseEvent(new RoutedEventArgs(ChangedEvent, this));
        }

        private static void OnPlaceholderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TextBoxEx instance = sender as TextBoxEx;

            if (instance != null)
            {
                instance.placeholder.Text = e.NewValue as string;
            }
        }
    }
}

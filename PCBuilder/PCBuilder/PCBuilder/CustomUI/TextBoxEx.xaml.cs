using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class TextBoxEx : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty PlaceholderProperty;
        public static readonly DependencyProperty OnlyNumbersProperty;

        public static readonly DependencyProperty ChangedCommandProperty;

        public static readonly RoutedEvent ChangedEvent;

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public bool OnlyNumbers
        {
            get { return (bool)GetValue(OnlyNumbersProperty); }
            set { SetValue(OnlyNumbersProperty, value); }
        }

        public ICommand ChangedCommand
        {
            get => GetValue(ChangedCommandProperty) as ICommand;
            set => SetValue(ChangedCommandProperty, value);
        }

        public event RoutedEventHandler Changed
        {
            add => AddHandler(ChangedEvent, value);
            remove => RemoveHandler(ChangedEvent, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => box.Text = value;
        }

        public bool IsEmpty { get; private set; }

        private Thickness _thickness;
        private double _fontsize;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)control).PropertyChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)control).PropertyChanged -= value;
            }
        }

        static TextBoxEx()
        {
            PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string),
                typeof(TextBoxEx), new PropertyMetadata(string.Empty, OnPlaceholderChanged));

            ChangedCommandProperty = DependencyProperty.Register(
                "ChangedCommand", typeof(ICommand), typeof(TextBoxEx)
                );

            OnlyNumbersProperty = DependencyProperty.Register(
                "OnlyNumbers", typeof(bool), typeof(TextBoxEx), new PropertyMetadata(false)
                );

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
            if (OnlyNumbers && !int.TryParse(box.Text, out _) && !string.IsNullOrEmpty(box.Text))
            {
                box.Text = _text;
                return;
            }

            _text = box.Text;
                
            bool isEmpty = box.Text == string.Empty;

            if (!isEmpty && IsEmpty)
                PlaceholderAnimationOut();
            else if (isEmpty && !IsEmpty)
                PlaceholderAnimationIn();

            IsEmpty = isEmpty;

            RaiseEvent(new RoutedEventArgs(ChangedEvent, this));

            if (ChangedCommand == null)
                return;

            if (ChangedCommand.CanExecute(this))
                ChangedCommand.Execute(this);
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

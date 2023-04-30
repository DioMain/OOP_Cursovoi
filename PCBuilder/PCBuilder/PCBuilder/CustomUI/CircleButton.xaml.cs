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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCBuilder.CustomUI
{
    public partial class CircleButton : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ImageSourceProperty;
        public static readonly DependencyProperty RadiusProperty;
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty SelectedColorProperty;
        public static readonly DependencyProperty BorderColorProperty;
        public static readonly DependencyProperty BorderSizeProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IdProperty;

        public static readonly DependencyProperty ClickCommandProperty;

        #region props

        public ImageSource ImageSource
        {
            get => GetValue(ImageSourceProperty) as ImageSource;
            set => SetValue(ImageSourceProperty, value);
        }

        public int Radius
        {
            get => (int)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }
        public int BorderSize
        {
            get => (int)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }
        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public Brush Color
        {
            get => GetValue(ColorProperty) as Brush;
            set => SetValue(ColorProperty, value);
        }
        public Brush SelectedColor
        {
            get => GetValue(SelectedColorProperty) as Brush;
            set => SetValue(SelectedColorProperty, value);
        }
        public Brush BorderColor
        {
            get => GetValue(BorderColorProperty) as Brush;
            set => SetValue(BorderColorProperty, value);
        }

        public ICommand ClickCommand
        {
            get => GetValue(ClickCommandProperty) as ICommand;
            set => SetValue(ClickCommandProperty, value);
        }

        #endregion

        static CircleButton()
        {
            ImageSourceProperty = DependencyProperty.Register(
                "ImageSource", typeof(ImageSource), typeof(CircleButton)
                );

            RadiusProperty = DependencyProperty.Register(
                "Radius", typeof(int), typeof(CircleButton)
                );

            BorderSizeProperty = DependencyProperty.Register(
                "BorderSize", typeof(int), typeof(CircleButton)
                );

            ColorProperty = DependencyProperty.Register(
                "Color", typeof(Brush), typeof(CircleButton)
                );

            IdProperty = DependencyProperty.Register(
                "Id", typeof(int), typeof(CircleButton), new PropertyMetadata(0)
                );

            SelectedColorProperty = DependencyProperty.Register(
                "SelectedColor", typeof(Brush), typeof(CircleButton)
                );

            BorderColorProperty = DependencyProperty.Register(
                "BorderColor", typeof(Brush), typeof(CircleButton)
                );

            ClickCommandProperty = DependencyProperty.Register(
                "ClickCommand", typeof(ICommand), typeof(CircleButton)
                );

            IsSelectedProperty = DependencyProperty.Register(
                "IsSelected", typeof(bool), typeof(CircleButton), new PropertyMetadata(false) { PropertyChangedCallback = OnSelectedChange }
                );
        }

        public CircleButton()
        {
            InitializeComponent();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            img.Width = Radius / 1.6;
            img.Height = Radius / 1.6;
        }

        private static void OnSelectedChange(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            CircleButton button = sender as CircleButton;

            if ((bool)args.NewValue)
            {
                button.selected.Visibility = Visibility.Visible;
                button.nonSelected.Visibility = Visibility.Collapsed;
            }
            else
            {
                button.selected.Visibility = Visibility.Collapsed;
                button.nonSelected.Visibility = Visibility.Visible;
            }
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ClickCommand == null)
                return;

            if (ClickCommand.CanExecute(this))
                ClickCommand.Execute(this);
        }

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
    }
}

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
        public static readonly DependencyProperty ImageSourceProp;
        public static readonly DependencyProperty RadiusProp;
        public static readonly DependencyProperty ColorProp;

        #region props

        public ImageSource ImageSource
        {
            get => GetValue(ImageSourceProp) as ImageSource;
            set => SetValue(ImageSourceProp, value);
        }

        public int Radius
        {
            get => (int)GetValue(RadiusProp);
            set => SetValue(RadiusProp, value);
        }

        public Brush Color
        {
            get => GetValue(ColorProp) as Brush;
            set => SetValue(ColorProp, value);
        }

        #endregion

        static CircleButton()
        {
            ImageSourceProp = DependencyProperty.Register(
                "ImageSource", typeof(ImageSource), typeof(CircleButton)
                );

            RadiusProp = DependencyProperty.Register(
                "Radius", typeof(int), typeof(CircleButton), new PropertyMetadata(16) { PropertyChangedCallback = OnRadiusChanged }
                );

            ColorProp = DependencyProperty.Register(
                "Color", typeof(Brush), typeof(CircleButton), new PropertyMetadata(Brushes.White)
                );
        }

        public CircleButton()
        {
            InitializeComponent();
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

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            ell.Height = Radius;
            ell.Width = Radius;
        }

        private static void OnRadiusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CircleButton button = sender as CircleButton;

            button.Radius = (int)e.NewValue;

            button.img.Width = button.Radius / 1.6;
            button.img.Height = button.Radius / 1.6;
        }

        //#region GETSET

        //public static int GetRadius(DependencyObject obj)
        //{
        //    return (int)obj.GetValue(RadiusProp);
        //}
        //public static void SetRadius(DependencyObject obj, int value)
        //{
        //    obj.SetValue(RadiusProp, value);
        //}

        //public static ImageSource GetImageSource(DependencyObject obj)
        //{
        //    return (ImageSource)obj.GetValue(ImageSourceProp);
        //}
        //public static void SetImageSource(DependencyObject obj, ImageSource value)
        //{
        //    obj.SetValue(ImageSourceProp, value);
        //}

        //public static Brush GetColor(DependencyObject obj)
        //{
        //    return (Brush)obj.GetValue(ColorProp);
        //}
        //public static void SetColor(DependencyObject obj, Brush value)
        //{
        //    obj.SetValue(ColorProp, value);
        //}

        //#endregion
    }
}

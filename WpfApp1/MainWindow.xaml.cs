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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    enum Precipitation
    {
        sunny,
        cloudy,
        rain,
        snow
    }
    class WeatherControl : DependencyObject
    {
        private string wind_direction;
        private int wind_speed;
        private Precipitation precipitation;
        public string WindDirection
        {
            get { return wind_direction; }
            set { wind_direction = value; }
        }
        public int WindSpeed
        {
            get { return wind_speed; }
            set { wind_speed = value; }
        }
        public WeatherControl(string winddir, int windsp, Precipitation precipitation) //конструктор класа WeatherControl
        {
            this.WindDirection = winddir;
            this.WindSpeed = windsp;
            this.precipitation = precipitation;
        }
        public static readonly DependencyProperty TempProperty; //регистрация переменной в хранилище свойств

        public int Temp // Свойство температура
        {
            get => (int)GetValue(TempProperty);
            set => SetValue(TempProperty, value);
        }


        private static object CoerceTemp(DependencyObject d, object value) //метод проверки корректности исходных значений-корректно / не коррекктно
        {
            int t = (int)value;
            if (t >= -50 && t <= 50) return true;
            else return null;
        }
        private static bool ValidateTemp(object value) //метод проверки корректности исходных значений
        {
            int t = (int)value;
            if (t >= -50 && t <= 50) return true;
            else return false;
        }
        static WeatherControl()
        {
            TempProperty = DependencyProperty.Register(
            nameof(Temp),
            typeof(int),
             typeof(WeatherControl),
            new FrameworkPropertyMetadata(
            0,
            FrameworkPropertyMetadataOptions.AffectsMeasure |
            FrameworkPropertyMetadataOptions.AffectsRender,
            null,
            new CoerceValueCallback(CoerceTemp)),
          new ValidateValueCallback(ValidateTemp));
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WeatherControl weatherControl = new WeatherControl("Северный", 10, Precipitation.snow);

        }
    }
}

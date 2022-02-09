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

namespace Lab6_6WpfApp4
{
    enum Precipitation
    {
        sunny,
        cloudy,
        rain,
        snow
    }

    internal class WeatherControl : DependencyObject
    {
        private Precipitation precipitation;// это поля
        private string wind_direction;
        private int wind_speed;
        
        public string WindDirection { get; set; } // обычные свойства

        public int WindSpeed { get; set; }

        public WeatherControl(string winddir, int windsp, Precipitation precipitation) // конструктор не статический
        {
            this.WindDirection = winddir;
            this.WindSpeed = windsp;
            this.precipitation = precipitation;
        }
        public static readonly DependencyProperty TempProperty; // свойства 
        public int Temp
        {
            get => (int)GetValue(TempProperty); // получить
            set => SetValue(TempProperty, value);  // установить
        }
        static WeatherControl() // статический конструктор, который вызывается один раз при первом обращении к классу
        {
            TempProperty = DependencyProperty.Register(// регистрация в конструкторе
                nameof(Temp),// имя свойства
                typeof(int), // тип свойства
                typeof(WeatherControl), // тип родителя
                new FrameworkPropertyMetadata( // методата
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |  // флаги
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null, // действия при изменении нет
                    new CoerceValueCallback(CoerceTemp)), // метод проверка
                new ValidateValueCallback(ValidateTemp));
        }
        private static bool ValidateTemp(object value)
        {
            int v = (int)value; // реализация проверка
            if (v >= -50 && v <= 50)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static object CoerceTemp(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50 && v <= 50)
            {
                return v;
            }
            else
            {
                return null;
            }

            
        }

    }
}

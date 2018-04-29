using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Resources;

namespace XMLWeather
{
    public partial class TodayScreen : UserControl
    {
        public TodayScreen()
        {
            InitializeComponent();
            DisplayForecast();
        }

        public void DisplayForecast()
        {
            int n = 3;
            foreach(Day d in Form1.days)
            {
                Control icon = this.Controls["icon" + n];
                Control time = this.Controls["timeLabel" + n];
                Control temp = this.Controls["tempLabel" + n];

                temp.Text = d.currentTemp + "°C";
                time.Text = d.currentTime;

                //Image p = ResourceManager.f;

                n = n + 3;
            }
        }
    }
}

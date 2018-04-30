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
                if (n < 18)
                {
                    Control time = this.Controls["timeLabel" + n];
                    Control temp = this.Controls["tempLabel" + n];

                    temp.Text = d.currentTemp + "°C";
                    time.Text = d.currentTime;

                    var icon = (PictureBox)this.Controls.Find("icon" + n, false)[0];
                    var img = (Image)Properties.Resources.ResourceManager.GetObject("_" + d.condition);
                    icon.Image = img;

                    n = n + 3;
                }

            }
        }
    }
}

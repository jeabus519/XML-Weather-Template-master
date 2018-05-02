using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMLWeather
{
    public partial class ForeScreen : UserControl
    {
        public ForeScreen()
        {
            InitializeComponent();
            DisplayForecast();

            //dateTime stuff
            timeLabel.Text = DateTime.Now.ToString("HH:mm");
        }

        public void DisplayForecast()
        {
            int n = 0;
            foreach (Day d in Form1.fDays)
            {
                if (n > 5)
                {
                    return;
                }
                if (n >= 1)
                {
                    Control day = this.Controls["day" + n];
                    Control hi = this.Controls["high" + n];
                    Control lo = this.Controls["low" + n];

                    day.Text = d.date;
                    hi.Text = d.tempHigh + "°C";
                    lo.Text = d.tempLow + "°C";

                    var icon = (PictureBox)this.Controls.Find("icon" + n, false)[0];
                    var img = (Image)Properties.Resources.ResourceManager.GetObject("_" + d.condition);
                    icon.Image = img;
                }

                n++;
            }
        }
    }
}

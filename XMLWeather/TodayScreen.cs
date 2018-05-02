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

            //dateTime stuff
            timeLabel.Text = DateTime.Now.ToString("HH:mm");
            dateLabel.Text = DateTime.Now.ToString("dddd MMMM dd, yyy");
            dateLabel.Location = new Point(225 - dateLabel.Width/2, 65);
            dateLine.Width = dateLabel.Width;
            dateLine.Location = new Point(dateLabel.Location.X, dateLabel.Location.Y + dateLabel.Height);
        }

        public void DisplayForecast()
        {
            int n = 3;
            foreach(Day d in Form1.days)
            {
                if (n <= 18)
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
                else
                {
                    locationLabel.Text = d.location;
                    currentTempLabel.Text = d.currentTemp + "°C";
                    highLabel.Text = d.tempHigh + "°C";
                    lowLabel.Text = d.tempLow + "°C";

                    var img = (Image)Properties.Resources.ResourceManager.GetObject("_" + d.condition);
                    iconCurrent.BackgroundImage = img;
                }

            }
        }

        private void TodayScreen_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();
            ForeScreen fs = new ForeScreen();
            f.Controls.Remove(this);
            f.Controls.Add(fs);
            fs.Focus();
        }
    }
}

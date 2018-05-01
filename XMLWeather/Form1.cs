using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace XMLWeather
{
    public partial class Form1 : Form
    {
        public static List<Day> days = new List<Day>();
        public static List<Day> fDays = new List<Day>();

        public Form1()
        {
            InitializeComponent();
            GetData();
            Extract3Hour();
            ExtractCurrent();
            ExtractForecast();

            // open weather screen for todays weather
            TodayScreen ts = new TodayScreen();
            this.Controls.Add(ts);
        }

        private static void GetData()
        {
            WebClient client = new WebClient();

            // one day forecast 
            client.DownloadFile("http://api.openweathermap.org/data/2.5/weather?q=Stratford,CA&mode=xml&units=metric&appid=3f2e224b815c0ed45524322e145149f0", "WeatherData.xml");
            // mulit day forecast
            client.DownloadFile("http://api.openweathermap.org/data/2.5/forecast/daily?q=Stratford,CA&mode=xml&units=metric&cnt=7&appid=3f2e224b815c0ed45524322e145149f0", "WeatherData7Day.xml");
            // 5 day 3 hour
            client.DownloadFile("http://api.openweathermap.org/data/2.5/forecast?id=6157977&mode=xml&units=metric&appid=3f2e224b815c0ed45524322e145149f0", "HourlyData.xml");
        }

        private void ExtractCurrent()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("WeatherData.xml");

            XmlNode city = doc.SelectSingleNode("current/city");
            XmlNode country = doc.SelectSingleNode("current/city/country");
            XmlNode temp = doc.SelectSingleNode("current/temperature");
            XmlNode conditions = doc.SelectSingleNode("current/weather");

            Day d = new Day();

            d.location = city.Attributes["name"].Value + ", " + country.InnerText;

            d.tempHigh = temp.Attributes["max"].Value;
            d.tempLow = temp.Attributes["min"].Value;

            double tempVal = Convert.ToDouble(temp.Attributes["value"].Value);
            tempVal = Math.Round(tempVal);
            d.currentTemp = Convert.ToString(tempVal);

            d.conditionName = conditions.Attributes["value"].Value;
            d.condition = conditions.Attributes["icon"].Value;
            string st = d.condition.Substring(0, 2);
            if (conditions.Attributes["number"].Value == "802" ||
                conditions.Attributes["number"].Value == "803" ||
                conditions.Attributes["number"].Value == "804") //weather codes with icons other than what openweathermap returns
            {
                d.condition = conditions.Attributes["number"].Value;
            }
            else if //icons that don't have day/night versions
                (st == "03" ||
                st == "09" ||
                st == "11" ||
                st == "13")
            {
                d.condition = st;
            }

            days.Add(d);
        }

        private void ExtractForecast()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("WeatherData7Day.xml");
            Day d = new Day();
            XmlNodeList timeList = doc.GetElementsByTagName("time");

            int x = 0;
            foreach(XmlNode n in timeList)
            {
                if (x >= 1 && x <= 5)
                {
                    XmlNode time = n;
                    XmlNode conditions = n.SelectSingleNode("symbol");
                    XmlNode temp = n.SelectSingleNode("temperature");

                    double tempVal = Convert.ToDouble(temp.Attributes["max"].Value);
                    tempVal = Math.Round(tempVal);
                    d.tempHigh = Convert.ToString(tempVal);

                    tempVal = Convert.ToDouble(temp.Attributes["min"].Value);
                    tempVal = Math.Round(tempVal);
                    d.tempLow = Convert.ToString(tempVal);

                    DateTime t = Convert.ToDateTime(time.Attributes["day"].Value);
                    d.date = t.ToString("dddd, MMMM dd");

                    d.condition = conditions.Attributes["var"].Value;
                    string st = d.condition.Substring(0, 2);
                    if (conditions.Attributes["number"].Value == "802" ||
                        conditions.Attributes["number"].Value == "803" ||
                        conditions.Attributes["number"].Value == "804") //weather codes with icons other than what openweathermap returns
                    {
                        d.condition = conditions.Attributes["number"].Value;
                    }
                    else if //icons that don't have day/night versions
                        (st == "03" ||
                        st == "09" ||
                        st == "11" ||
                        st == "13")
                    {
                        d.condition = st;
                    }
                }

                x++;
                if (x > 5)
                {
                    return;
                }
            }
        }

        private void Extract3Hour()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("HourlyData.xml");
            XmlNodeList timeList = doc.GetElementsByTagName("time");

            int x = 0;
            foreach(XmlNode n in timeList)
            {
                XmlNode time = n;
                XmlNode temp = n.SelectSingleNode("temperature");
                XmlNode conditions = n.SelectSingleNode("symbol");

                Day d = new Day();

                string s = n.Attributes["from"].Value;
                d.currentTime = s.Substring(s.IndexOf("T")+1, 5);

                //rounds temperature value to nearest whole number
                double tempVal = Convert.ToDouble(temp.Attributes["value"].Value);
                tempVal = Math.Round(tempVal);
                d.currentTemp = Convert.ToString(tempVal);

                d.condition = conditions.Attributes["var"].Value;
                string st = d.condition.Substring(0, 2);
                if (conditions.Attributes["number"].Value == "802" ||
                    conditions.Attributes["number"].Value == "803" ||
                    conditions.Attributes["number"].Value == "804") //weather codes with icons other than what openweathermap returns
                {
                    d.condition = conditions.Attributes["number"].Value;
                }
                else if //icons that don't have day/night versions
                    (st == "03"||
                    st == "09"||
                    st == "11"||
                    st == "13")
                {
                    d.condition = st;
                }


                days.Add(d);

                x++;
                if(x >= 6)
                {
                    return;
                }
            }
        }
    }
}

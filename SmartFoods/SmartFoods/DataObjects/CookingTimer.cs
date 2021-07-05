using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SmartFoods.Views;

namespace SmartFoods.DataObjects
{
    public class CookingTimer
    {
        public Alarm alarmVeiw { get; set; }
        public Button removeBtn { get; set; }
        public Label timeLabel { get; set; }
        int timeInSec;

        public void SetUp(int mins)
        {
            timeInSec = 60 * mins;
            TimeToString(timeInSec);
        }

        public bool DecrementTimer()
        {
            timeInSec--;
            if (timeInSec < 0)
            {
                alarmVeiw.OnTimerFinish();
                return false;
                // play annoying noise
            }
            else
            {
                TimeToString(timeInSec);
            }
            return true;
        }

        private void TimeToString(int secs)
        {
            int mins = (int)Math.Floor((double)secs / 60);
            secs -= mins * 60;
            int hours = (int)Math.Floor((double)mins / 60);
            mins -= hours * 60;
            string timeStr = "";
            if (hours < 10)
            {
                timeStr += "0";
            }
            timeStr += hours.ToString() + ":";
            if (mins < 10)
            {
                timeStr += "0";
            }
            timeStr += mins.ToString() + ":";
            if (secs < 10)
            {
                timeStr += "0";
            }
            timeStr += secs.ToString();
            timeLabel.Text = timeStr;
        }
    }
}
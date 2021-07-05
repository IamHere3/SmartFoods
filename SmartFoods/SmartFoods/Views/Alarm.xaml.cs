using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartFoods.DataObjects;
/// <summary>
/// This alarms to the application
/// </summary>
namespace SmartFoods.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alarm : ContentPage
    {
        bool language = SettingsManager.Language;
        private static int nextRowNum = 0;
        Button[] timerRemove = new Button[5];
        Label[] timer = new Label[5];
        List<CookingTimer> timersList;
        private static int maxAlarms = 5;
        public Alarm()
        {
            InitializeComponent();
            timersList = new List<CookingTimer>();
            SetLanguageForLabels();
            MessagingCenter.Subscribe<Settings>(this, "Hi", (sender) => {
                UpdateSettings();
            });
        }

        private void AddAlarmButton_Clicked(object sender, EventArgs e)
        {
            /*string newTimer = NewTimer.Text; // need to do validation
            int timerTimeSec;
            if (nextRowNum < 5 & int.TryParse(newTimer, out timerTimeSec))
            {
                int i = nextRowNum;
                Device.StartTimer(TimeSpan.FromSeconds(1), ()=>DecrementTimer(i));
                
                timer[nextRowNum] = new Label
                {
                    Text = newTimer,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };

                TimersGrid.Children.Add(timer[nextRowNum], 1, nextRowNum + 1);
                timerRemove[nextRowNum] = new Button
                {
                    Text = "X",
                };

                timerRemove[nextRowNum].Clicked += AlarmCancel_Clicked;
                TimersGrid.Children.Add(timerRemove[nextRowNum], 2, nextRowNum + 1);
                nextRowNum++;
            }*/
            AddAlarm();
            DisplayAllAlarms();
        }

        private void DisplayAllAlarms()
        {
            TimersGrid.Children.Clear(); // clear display
            int i = 0;
            foreach (CookingTimer ct in timersList)
            {
                DisplayAlarm(ct, i);
                i++;
            }
        }

        private void DisplayAlarm(CookingTimer ct, int i)
        {
            TimersGrid.Children.Add(ct.timeLabel, 1, i);
            TimersGrid.Children.Add(ct.removeBtn, 2, i);
        }

        /// <summary>
        /// Adds a new alarm to the list
        /// </summary>
        private void AddAlarm()
        {
            if (timersList.Count < maxAlarms)
            {
                CookingTimer cookingTimer = new CookingTimer();
                string newTimer = NewTimer.Text; // need to do validation
                int timerTimeMins;
                if (timersList.Count < maxAlarms & int.TryParse(newTimer, out timerTimeMins))
                {

                    cookingTimer.timeLabel = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center
                    };

                    cookingTimer.SetUp(timerTimeMins);
                    Device.StartTimer(TimeSpan.FromSeconds(1), () => cookingTimer.DecrementTimer());

                    cookingTimer.removeBtn = new Button
                    {
                        Text = "X",
                        // could have img here
                    };

                    cookingTimer.removeBtn.Clicked += AlarmCancel_Clicked2;
                    cookingTimer.alarmVeiw = this;
                    timersList.Add(cookingTimer);
                }

            }
        }

        /*private void AlarmCancel_Clicked(object sender, EventArgs e)
        {
            // super ugly code
            int i, rowNum = -1;
            Button btn = sender as Button;
            for (i = 0; i <= 4; i++)
            {
                if(timerRemove[i] == btn)
                {
                    rowNum = i;
                }
            }

            if(rowNum != -1)
            {
                TimersGrid.Children.Remove(btn);
                TimersGrid.Children.Remove(timer[rowNum]);
            }
        }*/

        private void AlarmCancel_Clicked2(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            CookingTimer temp = new CookingTimer();
            if (timersList.Count > 0)
            {
                foreach (CookingTimer ct in timersList)
                {
                    if (ct.removeBtn == btn) // if timer is the one which user asked to remove
                    {
                        // remove timer
                        temp = ct;
                        //  timersList.Remove(ct);
                    }
                }
                timersList.Remove(temp);
            }

            DisplayAllAlarms();
        }

        private bool DecrementTimer(int timerNum)
        {
            int t;
            string text = timer[timerNum].Text;
            if (int.TryParse(text, out t))
            {
                t--;
                if (t < 0)
                {
                    return false;
                    // play annoying noise
                }
                else
                {
                    timer[timerNum].Text = t.ToString();
                }
            }
            return true;
        }

        public void OnTimerFinish()
        {
            DisplayAlert("timer complete", " ", "back");
        }



        private void SetLanguageForLabels()
        {
            if (language == true)
            {
                alarmListLabel.Text = "Alarm List";
                addAlarmBtn.Text = "Add Alarm";
                alarmListLabel.HorizontalOptions = LayoutOptions.Center;
            }
            else
            {
                alarmListLabel.Text = "Lista allarmi";
                addAlarmBtn.Text = "Aggiungi allarme";
                alarmListLabel.HorizontalOptions = LayoutOptions.Center;
            }
        }

        public void UpdateSettings()
        {
            language = SettingsManager.Language;
            SetLanguageForLabels();
        }
    }
}

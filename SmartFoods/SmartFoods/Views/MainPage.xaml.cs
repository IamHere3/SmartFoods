using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartFoods.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        bool language = SettingsManager.Language;
        public MainPage()
        {
            InitializeComponent();
            languageSelection();
            MessagingCenter.Subscribe<Settings>(this, "Hi", (sender) => {
                languageSelection();
            });
        }

        public void languageSelection()
        {
            if (SettingsManager.Language)
            {
                Home.Title = "Home";
                Timer.Title = "Timer";
            }
            else
            {
                Home.Title = "Casa";
                Timer.Title = "Timer";
            }
        }
    }
}
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartFoods.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SmartFoods
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            SettingsManager.Difficulty = 5;
            SettingsManager.Language = false;
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

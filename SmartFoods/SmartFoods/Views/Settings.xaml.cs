using SmartFoods.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartFoods.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        int limitDifficultyTo;
        bool language = SettingsManager.Language;
        String FoodTheme;
        String Theme;

        //List<UserSettings> userSettings = new List<UserSettings>();
        public Settings()
        {
            InitializeComponent();
            languagePopulation();
        }

        private void languagePopulation()
        {
            if (language == true)
            {
                difficulty.Title = "Limit difficulty too";
                theme.Title = "Change Theme";
                theme.Items.Add("Grey");
                theme.Items.Add("White");
                theme.Items.Add("Blue");

                languagelabel.Text = "Language";

                SaveButton.Source = "save.png";

                EnglishButton.Source = "EnglishSelected.png";
                ItalianButton.Source = "ItalianUnselected.png";

            }
            else
            {
                difficulty.Title = "Limita anche la difficoltà";
                theme.Title = "Cambia tema";
                theme.Items.Add("Grigio");
                theme.Items.Add("Bianca");
                theme.Items.Add("Blu");

                languagelabel.Text = "linguaggio";

                SaveButton.Source = "saveITA.png";

                EnglishButton.Source = "EnglishUnselected.png";
                ItalianButton.Source = "ItalianSelected.png";
            }
        }

        private void languagePopulationUpdate()
        {
            language = SettingsManager.Language;

            if (language == true)
            {
                difficulty.Title = "Limit difficulty too";
                theme.Title = "Change Theme";
                theme.Items.Remove("Grigio");
                theme.Items.Remove("Bianca");
                theme.Items.Remove("Blu");
                theme.Items.Add("Grey");
                theme.Items.Add("White");
                theme.Items.Add("Blue");

                languagelabel.Text = "Language";

                SaveButton.Source = "save.png";

            }
            else
            {
                difficulty.Title = "Limita anche la difficoltà";

                theme.Title = "Cambia tema";
                theme.Items.Remove("Grey");
                theme.Items.Remove("White");
                theme.Items.Remove("Blue");
                theme.Items.Add("Grigio");
                theme.Items.Add("Bianca");
                theme.Items.Add("Blu");

                languagelabel.Text = "linguaggio";

                SaveButton.Source = "saveITA.png";
            }
        }

        private void Difficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            limitDifficultyTo = difficulty.SelectedIndex;
            limitDifficultyTo = limitDifficultyTo + 1;
        }

        private void Theme_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  Theme = theme.SelectedIndex;
        }

        private void Food_SelectedIndexChanged(object sender, EventArgs e)
        {
            // FoodTheme = foodtheme.SelectedItem;
        }

        private void Italian_Clicked(object sender, EventArgs e)
        {
            SettingsManager.Language = false;
            EnglishButton.Source = "EnglishUnselected.png";
            ItalianButton.Source = "ItalianSelected.png";
        }

        private void English_Clicked(object sender, EventArgs e)
        {
            SettingsManager.Language = true;
            EnglishButton.Source = "EnglishSelected.png";
            ItalianButton.Source = "ItalianUnselected.png";
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            if (limitDifficultyTo > 0)
            {
                SettingsManager.Difficulty = limitDifficultyTo;
            }

            languagePopulationUpdate();
            MessagingCenter.Send<Settings>(this, "Hi");
            Navigation.PopModalAsync();
        }

        protected override void OnDisappearing()
        {
            //ItemsPage.UpdateSettings();
        }
    }
}
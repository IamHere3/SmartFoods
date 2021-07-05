using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SmartFoods.Models;
using SmartFoods.Views;

namespace SmartFoods.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        //image states
        string kitchen;
        string kitchenSelected;
        string ingredients;
        string ingredientsSelected;
        string recipes;
        string recipesSelected;
        string favourites;
        string favouritesSelected;
        string settings;
        string settingsSelected;


        public static bool language;// = SettingsManager.Language;
        public ItemsPage()
        {
            InitializeComponent();
            language = SettingsManager.Language;
            SelectImage();
            MessagingCenter.Subscribe<Settings>(this, "Hi", (sender) => {
                UpdateSettings();
            });
        }

        // got the change page code from docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/navigation/hierarchical
        // as it alowed for a back button to be displayed with out having to implement one
        private void SelectImage()
        {
            
            if (language == true)
            {
                kitchen = "mykitchenbanner.png";
                kitchenSelected = "mykitchenbanner2.png";
                ingredients = "ingredientsbanner.png";
                ingredientsSelected = "ingredientsbanner2.png";
                recipes = "recipesbanner.png";
                recipesSelected = "recipesbanner2.png";
                favourites = "favouritesbanner.png";
                favouritesSelected = "favouritesbanner2.png";
                settings = "settingsbanner.png";
                settingsSelected = "settingsbanner2.png";
                BrowseItemsPage.Title = "Smarts Food";
            }
            else
            {
                kitchen = "Italianmykitchenbanner.png";
                kitchenSelected = "Italianmykitchenbanner2.png";
                ingredients = "Italianingredientsbanner.png";
                ingredientsSelected = "Italianingredientsbanner2.png";
                recipes = "Italianrecipesbanner.png";
                recipesSelected = "Italianrecipesbanner2.png";
                favourites = "Italianfavouritesbanner.png";
                favouritesSelected = "Italianfavouritesbanner2.png";
                settings = "Italiansettingsbanner.png";
                settingsSelected = "Italiansettingsbanner2.png";
            }

            myKitchen.Source = kitchen;
            myIngredients.Source = ingredients;
            myRecipes.Source = recipes;
            myFavourites.Source = favourites;
            mySettings.Source = settings;
        }

        async void MyKitchen(object sender, EventArgs e)
        {
            
            Image image = sender as Image;
            image.Source = kitchenSelected;
            await Navigation.PushAsync(new MyKitchen());
            image.Source = kitchen;
        }

        async void Ingredients(object sender, EventArgs e)
        {
            Image image = sender as Image;
            image.Source = ingredientsSelected;
            await Navigation.PushAsync(new Ingredients());
            image.Source = ingredients;
        }

        async void Recipes(object sender, EventArgs e)
        {
            Image image = sender as Image;
            image.Source = recipesSelected;
            await Navigation.PushAsync(new Recipes());
            image.Source = recipes;
        }

        async void Favourites(object sender, EventArgs e)
        {
            Image image = sender as Image;
            image.Source = favouritesSelected;
            await Navigation.PushAsync(new Favourites());
            image.Source = favourites;
        }

        async void Settings(object sender, EventArgs e)
        {
            Image image = sender as Image;
            image.Source = settingsSelected;
            var modalPage = new Settings();
            image.Source = settings;
            await Navigation.PushModalAsync(modalPage);
        }


        public void UpdateSettings()
        { 
            //Task.WaitAll();
            //image.Source = settings;
            //var poppedPage = await Navigation.PopModalAsync();
            language = SettingsManager.Language;
            SelectImage();
        }
    }
}
using SmartFoods.DataObjects;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartFoods.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionPage : ContentPage
    {
        Image save;
        bool defaultLanguage = true;
        List<UserSettings> userSettings = new List<UserSettings>();
        List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
        List<RecipeUtensil> recipeUtensils = new List<RecipeUtensil>();

        bool language = SettingsManager.Language;

        string favrioutImage = "";
        string SelectedfavrioutImage = "";

        public InstructionPage(Recipe recipe)
        {
            InitializeComponent();

            recipeIngredients = DBManager.SelectRecipeIngredients(recipe.Id);
            recipeUtensils = DBManager.SelectRecipeUtensils(recipe.Id);
            int recipeIngredientsCount = 4;
            int UtensilsCount = 4;


            Grid grid = new Grid
            {
                Margin = new Thickness(8, 0, 8, 0),

                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                              {
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = new GridLength(150)},
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = GridLength.Auto },
                                  new RowDefinition { Height = new GridLength(30) },
                                  new RowDefinition { Height = GridLength.Auto },
                              },
                ColumnDefinitions =
                              {
                                  new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
                                  new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                                  new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) }
                              }
            };

            string recipeName = "";
            string ingredients = "";
            string amount = "";
            string utensilsneeded = "";
            string howTomake = "";
            string recipeDescription = "";

            if (language == true)
            {
                recipeName = recipe.EngName;
                ingredients = "Ingredients";
                amount = "Amount";
                utensilsneeded = "Utensils needed";
                howTomake = "How To make";
                recipeDescription = recipe.Description;
                favrioutImage = "favorite.png";
                SelectedfavrioutImage = "favoriteSelected.png";
            }
            else
            {
                recipeName = recipe.ItlName;
                ingredients = "Ingredienti";
                amount = "Quantità";
                utensilsneeded = "Utensili necessari";
                howTomake = "come fare";
                favrioutImage = "italianfavorite.png";
                recipeDescription = recipe.ItlDescription;
                SelectedfavrioutImage = "italianfavoriteselected.png";
            }
            grid.Children.Add(new Label
            {
                Text = recipeName,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20
            }, 0, 3, 0, 1);

            grid.Children.Add(new Image
            {
                Source = recipe.ImgLoc                
            }, 0, 3, 1, 2);

            if (DBManager.IsFavourite(11, recipe.Id))
            {
                save = new Image
                {
                    Margin = new Thickness(0, 0, 120, 0),
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.End,
                    Source = "favoriteSelected.png"
                };
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    ToggleFavourite(recipe.Id);
                };
                save.GestureRecognizers.Add(tapGestureRecognizer);
                grid.Children.Add(save, 0, 3, 1, 3);
            }
            else
            {
                save = new Image
                {
                    Margin = new Thickness(0, 0, 120, 0),
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.End,
                    Source = favrioutImage
                };
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    ToggleFavourite(recipe.Id);
                };
                save.GestureRecognizers.Add(tapGestureRecognizer);
                grid.Children.Add(save, 0, 3, 1, 3);
            }

            grid.Children.Add(new Label
            {
                Text = ingredients,
                FontAttributes = FontAttributes.Bold
            }, 0, 3);

            grid.Children.Add(new Label
            {
                Text = amount,
                FontAttributes = FontAttributes.Bold
            }, 1, 3);

            grid.Children.Add(new Label
            {
                Text = utensilsneeded,
                FontAttributes = FontAttributes.Bold
            }, 2, 3);

            foreach (RecipeIngredient recipeIngredients in recipeIngredients)
            {
                string langrecipeIngredients;
                if (language == true)
                {
                    langrecipeIngredients = recipeIngredients.EngName;
                }
                else
                {
                    langrecipeIngredients = recipeIngredients.ItlName;
                }
                
                grid.Children.Add(new Label
                {
                    Text = langrecipeIngredients
                }, 0, recipeIngredientsCount);
                

                grid.Children.Add(new Label
                {
                    Text = System.Convert.ToString(recipeIngredients.Quantity) + "  " + recipeIngredients.Unit
                }, 1, recipeIngredientsCount);

                recipeIngredientsCount++;
            }

            foreach (RecipeUtensil recipeUtensils in recipeUtensils)
            {
                string langrecipeUtensils;
                if (language == true)
                {
                    langrecipeUtensils = recipeUtensils.EngName;
                }
                else
                {
                    langrecipeUtensils = recipeUtensils.ItlName;
                }

                grid.Children.Add(new Label
                {
                    Text = langrecipeUtensils

                }, 2, UtensilsCount);

                UtensilsCount++;
            }

            grid.Children.Add(new Label
            {
                Text = howTomake,
                BackgroundColor = Color.Tan,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                Margin = new Thickness(15, 10, 15, 0)
            }, 0, 3, 12, 13);

            grid.Children.Add(new Label
            {
                Text = recipeDescription,
                VerticalTextAlignment = TextAlignment.Start,
                Margin = new Thickness(15, 0, 15, 0)
            }, 0, 3, 13, 14);



            var scrollView = new ScrollView
            {
                Content = grid
            };

            this.Content = scrollView;

        }

        /* private void Button_Clicked(object sender, EventArgs e)
         {
             DisplayAlert("ha", Result, "Close");
         } */

        private void ToggleFavourite(int recipeId)
        {
            if(DBManager.IsFavourite(11, recipeId))
            {
                DBManager.RemoveFavouriteFromAccount(11, recipeId);
                save.Source = favrioutImage;
            }
            else
            {
                DBManager.AddFavouriteToAccount(11 ,recipeId);
                save.Source = SelectedfavrioutImage;
            }
            
        }
    }
}
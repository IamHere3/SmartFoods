using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartFoods.DataObjects;

namespace SmartFoods.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favourites : ContentPage
    {
        bool language = SettingsManager.Language;
        List<Recipe> recipes = new List<Recipe>();
        public Favourites()
        {
            // string Result;
            InitializeComponent();

            if (language == false)
            {
                Title = "Favourites";
            }
            else
            {
                Title = "Preferiti";
            }


            //DBManager.SimpleQueryAsync(recipes).Wait();
            recipes = DBManager.SelectFavouriteRecipes(11);


            int NumberOfRecipies = recipes.Count();
            Button[] RecipeSelected = new Button[NumberOfRecipies];

            Grid grid = new Grid
            {
                Margin = new Thickness(2, 10, 4, 0),
                //VerticalOptions = LayoutOptions.FillAndExpand,
                //foreach (Recipe recipe in recipes)
                RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(40)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(60)},
                                new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)}

                            },
                ColumnDefinitions =
                            {
                                new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                                new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                                new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) }
                            }
            };

            string TimeString(int mins)
            {
                string time = "";
                if (mins < 60)
                {
                    time = "00:" + mins.ToString() + ":00";
                }
                else
                {
                    int hours = (int)Math.Floor((double)mins / 60);
                    if (hours < 10)
                    {
                        time += "0";
                    }
                    time += hours.ToString() + ":";
                    mins -= (hours * 60);
                    if (mins < 10)
                    {
                        time += "0";
                    }
                    time += mins.ToString() + ":00";
                }
                return time;
            }

            int rowNum = 0;
            // Sets secound row
            int SecondRow = 1;
            // Sets the buttons boundry allowing it to cover each slection
            int ButtonSpan = 2;

            // vars for language
            string recipeName = "";
            string prepTime = "";
            string nextPage = "";

            foreach (Recipe recipe in recipes) // should be fairly simple to work out since the structures are intuitive...good luck.
            {
                // Changes time from minuets to hours
                int Preptime = recipe.PrepTime;
                string Time = TimeString(Preptime);

                int difficultyRating = recipe.Difficulty;
                string difficultyImage = "";


                switch (difficultyRating)
                {
                    case 1:
                        difficultyImage = "OneStarDifficulty.png";
                        break;
                    case 2:
                        difficultyImage = "TwoStarDifficulty.png";
                        break;
                    case 3:
                        difficultyImage = "ThreeStarDifficulty.png";
                        break;
                    case 4:
                        difficultyImage = "FourStarDifficulty.png";
                        break;
                    case 5:
                        difficultyImage = "FiveStarDifficulty.png";
                        break;
                };

                if (language == false)
                {
                    recipeName = recipe.EngName;
                    prepTime = "Prep Time";
                    nextPage = "next Page";
                }
                else
                {
                    recipeName = recipe.ItlName;
                    prepTime = "Tempo di preparazione";
                    nextPage = "pagina successiva";
                }

                grid.Children.Add(new Label
                {
                    Text = recipeName,
                    Margin = new Thickness(10, 10, 0, 0),
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 15
                }, 0, 2, rowNum, SecondRow);

                grid.Children.Add(new Image
                {
                    Source = difficultyImage
                }, 1, SecondRow);

                grid.Children.Add(new Image
                {
                    Source = recipe.ImgLoc,
                    Margin = new Thickness(10, 0, 0, 10)
                }, 0, SecondRow);

                grid.Children.Add(new Label
                {
                    Text = prepTime,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 15
                }, 2, rowNum);

                grid.Children.Add(new Label
                {
                    Text = Time,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 15
                }, 2, SecondRow);

                // creates a invisiable "button"
                recipe.IdBtn = new Button
                {
                    BackgroundColor = Color.Transparent,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                };

                recipe.IdBtn.Clicked += RecipeClicked;
                grid.Children.Add(recipe.IdBtn, 0, 3, rowNum, ButtonSpan);

                rowNum = rowNum + 2;
                SecondRow = SecondRow + 2;
                ButtonSpan = ButtonSpan + 2;
            }

            if (ButtonSpan > 20)
            {
                grid.Children.Add(new Button
                {
                    Text = nextPage,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 15
                }, 0, 3, 20, 21);
            }
            else
            {
                grid.Children.Add(new Button
                {
                    Text = nextPage,
                    FontAttributes = FontAttributes.Bold,
                    IsEnabled = false,
                    FontSize = 15
                }, 0, 3, 20, 21);
            }


            // allows for scrolling
            var scrollView = new ScrollView
            {
                Content = grid
            };

            this.Content = scrollView;

            /*Content = holder;
            var stack = new StackLayout();
            stack.Children.Add(grid);
            stack.Children.Add(test);*/

        }

        async void RecipeClicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            foreach (Recipe r in recipes)
            {
                if (r.IdBtn == btn) // if timer is the one which user asked to remove
                {
                    await Navigation.PushAsync(new InstructionPage(r));
                }
            }
        }
    }
}
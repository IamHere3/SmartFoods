using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFoods.DataObjects;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartFoods.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ingredients : ContentPage
    {
        List<Ingredient> ingredients;
        List<AccountIngredient> accountIngredients;

        public Ingredients()
        {
            InitializeComponent();
            SetupPicker();
            UpdateMyIngredients();
            SetLanguageForLabels();
        }

        private void SetupPicker()
        {
            ingredients = DBManager.SelectIngredients(11);
            IngredientsPicker.ItemsSource = ItemsForPicker(ingredients);
        }

        private List<string> ItemsForPicker(List<Ingredient> ingredients)
        {
            List<string> stringList = new List<string>();
            foreach (Ingredient i in ingredients)
            {
                if (SettingsManager.Language)
                {
                    stringList.Add(i.EngName);
                }
                else
                {
                    stringList.Add(i.ItlName);
                }
            }
            return stringList;
        }

        private Ingredient GetSelectedIngredient(string ingredientName)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Id = -1;
            foreach (Ingredient i in ingredients)
            {
                if (SettingsManager.Language)
                {
                    if (i.EngName == ingredientName)
                    {
                        ingredient = i;
                        break;
                    }
                }
                else
                {
                    if (i.ItlName == ingredientName)
                    {
                        ingredient = i;
                        break;
                    }
                }
            }
            return ingredient;
        }

        private void NewIngredientBtn_Clicked(object sender, EventArgs e)
        {
            string selectedIngAsStr = "null";
            if (IngredientsPicker.SelectedItem != null)
            {
                selectedIngAsStr = IngredientsPicker.SelectedItem.ToString();

                Ingredient ingredient = GetSelectedIngredient(selectedIngAsStr);
                decimal quantity = 0;
                string unit = "";

                unit = UnitEntry.Text;

                if (ingredient.Id > -1 & decimal.TryParse(QtyEntry.Text, out quantity))
                {
                    DBManager.AddIngredientToAccount(11, ingredient.Id, quantity, unit);
                }
                UpdateMyIngredients();
                SetupPicker();
            }
        }

        private void UpdateMyIngredients()
        {
            IngredientsGrid.Children.Clear();
            accountIngredients = DBManager.SelectAccountIngredients(11);

            int n = 0; //increment

            foreach (AccountIngredient i in accountIngredients)
            {
                if (SettingsManager.Language)
                {
                    i.Label = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        Text = i.EngName + " - " + i.Quantity.ToString() + " " + i.Unit
                    };
                }
                else
                {
                    i.Label = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        Text = i.ItlName + " - " + i.Quantity.ToString() + " " + i.Unit
                    };
                }

                i.RemoveBtn = new Button
                {
                    Text = "-",
                    // could have img here
                };

                i.RemoveBtn.Clicked += RemoveBtn_Clicked;

                IngredientsGrid.Children.Add(i.Label, 0, n);
                IngredientsGrid.Children.Add(i.RemoveBtn, 1, n);
                n++;
            }
        }

        private void RemoveBtn_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            foreach (AccountIngredient i in accountIngredients)
            {
                if (i.RemoveBtn == btn) // if timer is the one which user asked to remove
                {
                    // remove timer
                    DBManager.RemoveIngredientFromAccount(11, i.IngredientId);
                }
            }

            UpdateMyIngredients();
            SetupPicker();
        }

        private void SetLanguageForLabels()
        {
            if (SettingsManager.Language)
            {
                IngTitle.Title = "Ingredients";
                ingredientLabel.Text = "Ingredient";
                qtyLabel.Text = "Qty";
                unitLabel.Text = "Unit";
                myIngredientsLabel.Text = "My Ingredients";
            }
            else
            {
                IngTitle.Title = "Ingredienti";
                ingredientLabel.Text = "Ingredienti";
                qtyLabel.Text = "Qty"; // not sure if there's an abbreviation in itl
                unitLabel.Text = "Unità";
                myIngredientsLabel.Text = "I miei ingredienti";
            }
        }
    }
}
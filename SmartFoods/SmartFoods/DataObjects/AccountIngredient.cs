using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartFoods.DataObjects
{
    public class AccountIngredient
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "account_id")]
        public int AccountId { get; set; }

        [JsonProperty(PropertyName = "ingredient_id")]
        public int IngredientId { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        public string EngName { get; set; }

        public string ItlName { get; set; }

        public Label Label { get; set; }

        public Button RemoveBtn { get; set; }
    }
}

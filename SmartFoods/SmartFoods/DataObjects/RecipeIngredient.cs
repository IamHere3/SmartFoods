using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SmartFoods.DataObjects
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "recipe_id")]
        public int RecipeId { get; set; }

        [JsonProperty(PropertyName = "ingredient_id")]
        public int IngredientId { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        [JsonProperty(PropertyName = "eng_name")]
        public string EngName { get; set; }

        [JsonProperty(PropertyName = "itl_name")]
        public string ItlName { get; set; }
    }
}

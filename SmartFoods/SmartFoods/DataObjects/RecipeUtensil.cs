using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SmartFoods.DataObjects
{
    public class RecipeUtensil
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "recipe_id")]
        public int RecipeId { get; set; }

        [JsonProperty(PropertyName = "utensil_id")]
        public int UtensilId { get; set; }

        [JsonProperty(PropertyName = "eng_name")]
        public string EngName { get; set; }

        [JsonProperty(PropertyName = "itl_name")]
        public string ItlName { get; set; }

    }
}

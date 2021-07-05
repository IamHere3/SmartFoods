using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartFoods.DataObjects
{
    public class Recipe
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "eng_name")]
        public string EngName { get; set; }

        [JsonProperty(PropertyName = "itl_name")]
        public string ItlName { get; set; }

        [JsonProperty(PropertyName = "img_loc")]
        public string ImgLoc { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "diff_rating")]
        public int Difficulty { get; set; }

        [JsonProperty(PropertyName = "prep_time")]
        public int PrepTime { get; set; }

        [JsonProperty(PropertyName = "itl_description")]
        public string ItlDescription { get; set; }

        // might need to move 
        public Button IdBtn { get; set; }

        /* options for time variable:
         * - try and do a time variable in c# (probably be a massive pain)
         * - store as an int (mins) then convert in c# (my preference since I understand it)
         * - store as string (easiest)
         * - not have one (current solution)
         */
    }
}


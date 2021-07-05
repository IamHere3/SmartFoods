using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SmartFoods.DataObjects
{
    public class Utensil
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "eng_name")]
        public string EngName { get; set; }

        [JsonProperty(PropertyName = "itl_name")]
        public string ItlName { get; set; }

        [JsonProperty(PropertyName = "itl_description")]
        public string ItlDescription { get; set; }


    }
}

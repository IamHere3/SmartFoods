using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SmartFoods.DataObjects
{
    public class AccountUtensil
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "account_id")]
        public int AccountId { get; set; }

        [JsonProperty(PropertyName = "utensil_id")]
        public int UtensilId { get; set; }

    }
}

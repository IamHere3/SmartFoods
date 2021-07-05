using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SmartFoods.DataObjects
{
    public class Account
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}

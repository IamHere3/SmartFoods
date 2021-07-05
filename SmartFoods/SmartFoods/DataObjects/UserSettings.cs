using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartFoods.DataObjects
{
    class UserSettings
    {
        // adds to favourites
        public int FavouriteID{ get; set; }

        // language option
        public bool defaultLanguage { get; set; }

        public Button Language { get; set; }
}
}

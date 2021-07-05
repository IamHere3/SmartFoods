using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartFoods.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyKitchen : ContentPage
	{
        bool language = SettingsManager.Language;
        string potImgSelected, potImgUnselected, panImgSelected, panImgUnselected, ovenImgSelected, ovenImgUnselected, microwaveImgSelected, microwaveImgUnselected;
        string kettleImgSelected, kettleImgUnselected, hobImgSelected, hobImgUnselected, toasterImgSelected, toasterImgUnselected, roastingTinImgSelected, roastingTinImgUnselected;
        public MyKitchen ()
		{
			InitializeComponent ();
            ImagePicker();
            UserSettings();

        }

        private void ImagePicker()
        {
            if (language == true)
            {
                potImgSelected = "PotSelected.png";
                potImgUnselected = "PotUnselected.png";
                panImgSelected = "PanSelected.png";
                panImgUnselected = "PanUnselected.png";
                ovenImgSelected = "OvenSelected.png";
                ovenImgUnselected = "OvenUnselected.png";
                microwaveImgSelected = "MicrowaveSelected.png";
                microwaveImgUnselected = "MicrowaveUnselected.png";
                kettleImgSelected = "KettleSelected.png";
                kettleImgUnselected = "KettleUnselected.png";
                hobImgSelected = "HobSelected.png";
                hobImgUnselected = "HobUnselected.png";
                toasterImgSelected = "ToasterSelected.png";
                toasterImgUnselected = "ToasterUnselected.png";
                roastingTinImgUnselected = "RoastingTinUnselected.png";
                roastingTinImgSelected = "RoastingTinSelected.png";
                KitTitle.Title = "My Kitchen";
            }
            else
            {
                potImgSelected = "ItalianPotSelected.png";
                potImgUnselected = "ItalianPotUnselected.png";
                panImgSelected = "ItalianPanSelected.png";
                panImgUnselected = "ItalianPanUnselected.png";
                ovenImgSelected = "ItalianOvenSelected.png";
                ovenImgUnselected = "ItalianOvenUnselected.png";
                microwaveImgSelected = "ItalianMicrowaveSelected.png";
                microwaveImgUnselected = "ItalianMicrowaveUnselected.png";
                kettleImgSelected = "ItalianKettleSelected.png";
                kettleImgUnselected = "ItalianKettleUnselected.png";
                hobImgSelected = "ItalianHobSelected.png";
                hobImgUnselected = "ItalianHobUnselected.png";
                toasterImgSelected = "ItalianToasterUnselected.png";
                toasterImgUnselected = "ItalianToasterUnselected.png";
                roastingTinImgUnselected = "ItalianRoastingTinUnelected.png";
                roastingTinImgSelected = "ItalianRoastingTinSelected.png";
                KitTitle.Title = "La mia cucina";
            }
        }

        private void UserSettings()
        {
            if(DBManager.IsUtensilSelected(11, 61))
            {
                Potstoggle.Source = potImgSelected;
            }
            else
            {
                Potstoggle.Source = potImgUnselected;
            }

            if (DBManager.IsUtensilSelected(11, 62))
            {
                Panstoggle.Source = panImgSelected;
            }
            else
            {
                Panstoggle.Source = panImgUnselected;
            }

            if (DBManager.IsUtensilSelected(11, 63))
            {
                Ovenstoggle.Source = ovenImgSelected;
            }
            else
            {
                Ovenstoggle.Source = ovenImgUnselected;
            }

            if (DBManager.IsUtensilSelected(11, 64))
            {
                Microwavestoggle.Source = microwaveImgSelected;
            }
            else
            {
                Microwavestoggle.Source = microwaveImgUnselected;
            }

            if (DBManager.IsUtensilSelected(11, 65))
            {
                Kettlestoggle.Source = kettleImgSelected;
            }
            else
            {
                Kettlestoggle.Source = kettleImgUnselected;
            }

            if (DBManager.IsUtensilSelected(11, 66))
            {
                Hobstoggle.Source = hobImgSelected;
            }
            else
            {
                Hobstoggle.Source = hobImgUnselected;
            }

            if (DBManager.IsUtensilSelected(11, 67))
            {
                Toasterstoggle.Source = toasterImgSelected;
            }
            else
            {
                Toasterstoggle.Source = toasterImgUnselected;
            }

            if (DBManager.IsUtensilSelected(11, 68))
            {
                RoastingTinstoggle.Source = roastingTinImgSelected;
            }
            else
            {
                RoastingTinstoggle.Source = roastingTinImgUnselected;
            }
        }

        private void Pots_OnChanged(object sender, ToggledEventArgs e)
        {
            Image image = sender as Image;
            string source = image.Source as FileImageSource;  
            if (String.Equals(source, potImgSelected))
            {
                image.Source = potImgUnselected;
                DBManager.RemoveUtensilFromAccount(11, 61);
            }
            else
            {
                image.Source = potImgSelected;
                DBManager.AddUtensilToAccount(11, 61);
            }
        }

        private void Pans_OnChanged(object sender, ToggledEventArgs e)
        {
            Image image = sender as Image;
            string source = image.Source as FileImageSource;
            if (String.Equals(source, panImgSelected))
            {
                image.Source = panImgUnselected;
                DBManager.RemoveUtensilFromAccount(11, 62);
            }
            else
            {
                image.Source = panImgSelected;
                DBManager.AddUtensilToAccount(11, 62);
            }
        }

        private void MyOven_OnChanged(object sender, ToggledEventArgs e)
        {
            Image image = sender as Image;
            string source = image.Source as FileImageSource;
            if (String.Equals(source, ovenImgSelected))
            {
                image.Source = ovenImgUnselected;
                DBManager.RemoveUtensilFromAccount(11, 63);
            }
            else
            {
                image.Source = ovenImgSelected;
                DBManager.AddUtensilToAccount(11, 63);
            }
        }

        private void MyMicrowave_OnChanged(object sender, ToggledEventArgs e)
        {
            Image image = sender as Image;
            string source = image.Source as FileImageSource;
            if (String.Equals(source, microwaveImgSelected))
            {
                image.Source = microwaveImgUnselected;
                DBManager.RemoveUtensilFromAccount(11, 64);
            }
            else
            {
                image.Source = microwaveImgSelected;
                DBManager.AddUtensilToAccount(11, 64);
            }
        }

        private void MyKettle_OnChanged(object sender, ToggledEventArgs e)
        {
            Image image = sender as Image;
            string source = image.Source as FileImageSource;
            if (String.Equals(source, kettleImgSelected))
            {
                image.Source = kettleImgUnselected;
                DBManager.RemoveUtensilFromAccount(11, 65);
            }
            else
            {
                image.Source = kettleImgSelected;
                DBManager.AddUtensilToAccount(11, 65);
            }
        }

        private void RoastingTins_OnChanged(object sender, ToggledEventArgs e)
        {
            Image image = sender as Image;
            string source = image.Source as FileImageSource;
            if (String.Equals(source, roastingTinImgSelected))
            {
                image.Source = roastingTinImgUnselected;
                DBManager.RemoveUtensilFromAccount(11, 68);
            }
            else
            {
                image.Source = roastingTinImgSelected;
                DBManager.AddUtensilToAccount(11, 68);
            }
        }

        private void Hob_OnChanged(object sender, ToggledEventArgs e)
        {
            Image image = sender as Image;
            string source = image.Source as FileImageSource;
            if (String.Equals(source, hobImgSelected))
            {
                image.Source = hobImgUnselected;
                DBManager.RemoveUtensilFromAccount(11, 66);
            }
            else
            {
                image.Source = hobImgSelected;
                DBManager.AddUtensilToAccount(11, 66);
            }
        }

        private void Toaster_OnChanged(object sender, ToggledEventArgs e)
        {
            Image image = sender as Image;
            string source = image.Source as FileImageSource;
            if (String.Equals(source, toasterImgSelected))
            {
                image.Source = toasterImgUnselected;
                DBManager.RemoveUtensilFromAccount(11, 67);
            }
            else
            {
                image.Source = toasterImgSelected;
                DBManager.AddUtensilToAccount(11, 67);
            }
        }
    }
}
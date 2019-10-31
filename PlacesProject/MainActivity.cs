using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Google.Places;
using System.Collections.Generic;
using Android.Content;

namespace PlacesProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button displayButton;
        TextView addressText, nameText, coordiateText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            displayButton = (Button)FindViewById(Resource.Id.displayButton);
            addressText = (TextView)FindViewById(Resource.Id.addressText);
            nameText = (TextView)FindViewById(Resource.Id.nameText);
            coordiateText = (TextView)FindViewById(Resource.Id.cordinateText);

            displayButton.Click += DisplayButton_Click;

            if (!PlacesApi.IsInitialized)
            {
                PlacesApi.Initialize(this, "AIzaSyAnJxOKPKQOjE5-add your own api key");
            }
        }

        private void DisplayButton_Click(object sender, System.EventArgs e)
        {
            List<Place.Field> fields = new List<Place.Field>();

            fields.Add(Place.Field.Id);
            fields.Add(Place.Field.Name);
            fields.Add(Place.Field.LatLng);
            fields.Add(Place.Field.Address);

            Intent intent = new Autocomplete.IntentBuilder(AutocompleteActivityMode.Overlay, fields)
                .SetCountry("US")
                .Build(this);

            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            var place = Autocomplete.GetPlaceFromIntent(data);
            addressText.Text = place.Address;
            nameText.Text = place.Name;
            coordiateText.Text = "Latitude = " + place.LatLng.Latitude.ToString() + ", Longitude = " + place.LatLng.Longitude.ToString();
        }
    }
}
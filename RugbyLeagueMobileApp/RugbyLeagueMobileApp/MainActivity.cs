using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Collections.Generic;

namespace RugbyLeagueMobileApp
{
    [Activity(Label = "Launching App...", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private AppServices services = new AppServices();
        private string currentUser = "Axel";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            ActionBar.Title = "Welcome, " + currentUser + "!";

            
        }
    }
}


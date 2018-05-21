using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RugbyLeagueMobileApp
{
    [Activity(Label = "Loading...")]
    public class ViewPlayerData : Activity
    {
        private AppServices services = new AppServices();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewPlayers);
            ActionBar.Title = "View All Players";
        }
    }
}
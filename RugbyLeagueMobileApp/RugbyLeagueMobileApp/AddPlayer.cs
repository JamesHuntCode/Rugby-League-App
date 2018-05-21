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
    [Activity(Label = "AddPlayer")]
    public class AddPlayer : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddPlayer);
            ActionBar.Title = "Add A New Player";
        }
    }
}
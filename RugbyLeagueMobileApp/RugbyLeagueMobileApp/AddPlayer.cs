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
using Android.Views.InputMethods;

namespace RugbyLeagueMobileApp
{
    [Activity(Label = "Loading...")]
    public class AddPlayer : Activity
    {
        private AppServices services = new AppServices();

        // Widgets Used:
        Button addPlayer;
        Button exit;
        EditText firstName;
        EditText lastName;
        EditText nickname;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddPlayer);
            ActionBar.Title = "Add New Player";
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Set Widgets:
            addPlayer   = FindViewById<Button>(Resource.Id.btnSaveNewPlayer);
            exit        = FindViewById<Button>(Resource.Id.btnReturnHome);
            firstName   = FindViewById<EditText>(Resource.Id.etFirstName);
            lastName    = FindViewById<EditText>(Resource.Id.etSurname);
            nickname    = FindViewById<EditText>(Resource.Id.etNickname);

            // Handle Clicks:

            // Save Player Data
            addPlayer.Click += delegate
            {
                string fN = firstName.Text;
                string sN = lastName.Text;
                string nN = nickname.Text;

                if ((fN == String.Empty) || (sN == String.Empty))
                {
                    Toast.MakeText(this, "Invalid inputs.", ToastLength.Long).Show();
                }
                else
                {
                    // Add new player data:
                    Player newPlayer = new Player();
                    newPlayer.FirstName = fN;
                    newPlayer.LastName = sN;
                    newPlayer.Nickname = nN;

                    services.AddNewPlayer(newPlayer);

                    // Alert user:
                    Toast.MakeText(this, "succesfully added new player.", ToastLength.Long).Show();

                    // Clear inputs:
                    firstName.Text = "";
                    lastName.Text = "";
                    nickname.Text = "";

                    // Hide Keyboard:
                    InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                    inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
                }
            };

            // Return Home
            exit.Click += delegate
            {
                Intent newActivity = new Intent(this, typeof(ViewPlayerData));

                StartActivity(newActivity);
            };
        }

        /// <summary>
        /// User has selected to go home on the action bar.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
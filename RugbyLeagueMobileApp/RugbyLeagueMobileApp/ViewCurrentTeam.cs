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
    public class ViewCurrentTeam : Activity
    {
        private AppServices services = new AppServices();

        // Widgets Used
        Button overrideTeam;
        ListView teamPlayers;

        // Data Structures Used:
        List<Player> teamData = new List<Player>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewTeam);
            ActionBar.Title = "Your Team";

            // Set Widgets:
            overrideTeam = FindViewById<Button>(Resource.Id.btnOverrideCurrentTeam);
            teamPlayers = FindViewById<ListView>(Resource.Id.lvTeamPlayers);

            // Configure ListView:



            // Handle Clicks:

            // Create New Team Button:
            overrideTeam.Click += delegate
            {
                // Check if the user really wants to override their current team configuration:
                AlertDialog.Builder confirm = new AlertDialog.Builder(this);
                confirm.SetTitle("Confirm");

                string line1 = "Creating a new team will delete your current team.";
                string line2 = "Remove current team and make a new one?";

                string message = line1 + "\n\n" + line2;
                confirm.SetMessage(message);

                confirm.SetPositiveButton("Yes", delegate
                {
                    Intent newActivity = new Intent(this, typeof(CreateNewTeam));
                    StartActivity(newActivity);

                    confirm.Dispose();
                });

                confirm.SetNegativeButton("No", delegate
                {
                    Toast.MakeText(this, "Creation of new team cancelled.", ToastLength.Long).Show();

                    confirm.Dispose();
                });

                confirm.Show();
            };
        }
    }
}
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
        List<Player> teamData;
        List<string> formattedTeamData = new List<string>();
        ArrayAdapter<string> teamDataAdapter = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewTeam);
            ActionBar.Title = "Your Team";

            // Set Widgets:
            overrideTeam = FindViewById<Button>(Resource.Id.btnOverrideCurrentTeam);
            teamPlayers = FindViewById<ListView>(Resource.Id.lvTeamPlayers);

            // Configure ListView:

            // Push formatted team data to list view:
            teamData = services.GetTeamData();

            for (int i = 0; i < teamData.Count; i++)
            {
                Player currentInLoop = teamData[i];

                string line1 = "Name: " + currentInLoop.FirstName + " " + currentInLoop.LastName;
                string line2 = "Number: " + currentInLoop.PlayerNumber;
                string line3 = "Position: " + currentInLoop.PlayerPosition;

                string displayMe = line1 + "\n\n" + line2 + "\n\n" + line3;

                formattedTeamData.Add(displayMe);
            }

            teamDataAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, formattedTeamData);
            teamPlayers.Adapter = teamDataAdapter;

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
                    confirm.Dispose();
                });

                confirm.Show();
            };
        }
    }
}
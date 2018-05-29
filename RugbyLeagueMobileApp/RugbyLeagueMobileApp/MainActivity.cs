using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Collections.Generic;
using Android.Content;

namespace RugbyLeagueMobileApp
{
    [Activity(Label = "Team Designer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private AppServices services;
        private string currentUser;

        // Widgets Used:
        Button viewCurrentTeam;
        Button createNewTeam;
        Button viewPlayerData;

        protected override void OnRestart()
        {
            base.OnRestart();
            base.Recreate();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            services = new AppServices();
            currentUser = "Axel";

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            ActionBar.Title = "Team Designer - Home";
            ActionBar.Subtitle = "Welcome back, " + currentUser + ".";

            // Set Widgets:
            viewCurrentTeam     = FindViewById<Button>(Resource.Id.btnViewTeam);
            createNewTeam       = FindViewById<Button>(Resource.Id.btnCreateNewTeam);
            viewPlayerData      = FindViewById<Button>(Resource.Id.btnViewPlayerData);

            // Handle Clicks:

            // View Current Team:
            viewCurrentTeam.Click += delegate
            {
                if (services.UserHasTeam())
                {
                    Intent newActivity = new Intent(this, typeof(ViewCurrentTeam));

                    StartActivity(newActivity);
                }
                else
                {
                    AlertDialog.Builder inform = new AlertDialog.Builder(this);
                    inform.SetTitle("Alert");

                    string line1 = "You haven't created a team to view.";
                    string line2 = "Do you want to create one now?";

                    string message = line1 + "\n\n" + line2;
                    inform.SetMessage(message);

                    inform.SetPositiveButton("Yes", delegate
                    {
                        Intent newActivity = new Intent(this, typeof(CreateNewTeam));
                        StartActivity(newActivity);

                        inform.Dispose();
                    });

                    inform.SetNegativeButton("No", delegate
                    {
                        inform.Dispose();
                    });

                    inform.Show();
                }
            };

            // Create New Team
            createNewTeam.Click += delegate
            {
                // Confirm the user wishes to override old team by creating new one (only if they have a team already).
                if (services.UserHasTeam())
                {
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
                }
                else
                {
                    // If they don't have an active team, go to next activity *(IF THEY HAVE PLAYER DATA RECORDED)*.
                    if (services.GetAllPlayerData().Count > 0)
                    {
                        Intent newActivity = new Intent(this, typeof(CreateNewTeam));
                        StartActivity(newActivity);
                    }
                    else
                    {
                        // Ask user if they wish to start adding players*
                        AlertDialog.Builder confirm = new AlertDialog.Builder(this);
                        confirm.SetTitle("Confirm");

                        string line1 = "You can't create a team until you add some player data.";
                        string line2 = "Do you want to add player data now?";

                        string message = line1 + "\n\n" + line2;
                        confirm.SetMessage(message);

                        confirm.SetPositiveButton("Yes", delegate
                        {
                            Intent newActivity = new Intent(this, typeof(AddPlayer));
                            StartActivity(newActivity);

                            confirm.Dispose();
                        });

                        confirm.SetNegativeButton("No", delegate
                        {
                            Toast.MakeText(this, "Add players before creating a team.", ToastLength.Long).Show();

                            confirm.Dispose();
                        });

                        confirm.Show();
                    }
                }
            };

            // View All Player Data
            viewPlayerData.Click += delegate
            {
                if (services.UserHasAddedPlayers())
                {
                    Intent newActivity = new Intent(this, typeof(ViewPlayerData));
                    StartActivity(newActivity);
                }
                else
                {
                    Toast.MakeText(this, "You don't have any player data to view.", ToastLength.Long).Show();
                }
            };
        }
    }
}


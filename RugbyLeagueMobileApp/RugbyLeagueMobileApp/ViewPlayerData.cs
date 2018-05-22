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

        // Widgets Used:
        ListView players;
        Button addNewPlayer;

        // Adapters Used:
        ArrayAdapter<string> playeradapter = null;

        // Data Structures Used:
        List<Player> RawJSONdata;
        List<string> formattedJSONdata;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewPlayers);
            ActionBar.Title = "View All Players";

            // Set Widgets:
            addNewPlayer = FindViewById<Button>(Resource.Id.btnAddPlayer);

            // Fetch & Format Data
            RawJSONdata = services.GetAllPlayerData();
            formattedJSONdata = new List<string>();

            for (int i = 0; i < RawJSONdata.Count; i++)
            {
                string displayme = "";

                if (RawJSONdata[i].Nickname != String.Empty && RawJSONdata[i].Nickname != null)
                {
                    displayme = RawJSONdata[i].Nickname;
                }
                else
                {
                    displayme = RawJSONdata[i].FirstName + " " + RawJSONdata[i].LastName;
                }

                formattedJSONdata.Add(displayme);
            }

            // Configure ListView
            players = FindViewById<ListView>(Resource.Id.lvPlayers);
            playeradapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, formattedJSONdata);
            players.Adapter = playeradapter;

            // Handle Clicks:

            // Add New Player Button
            addNewPlayer.Click += delegate
            {
                Intent newActivity = new Intent(this, typeof(AddPlayer));
                StartActivity(newActivity);
            };

            // Players ListView
            players.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => PlayerClicked(sender, e);
        }

        /// <summary>
        /// User has clicked on a player in the ListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="event args"></param>
        protected void PlayerClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            string selectedItem = ((TextView)e.View).Text;
        }
    }
}
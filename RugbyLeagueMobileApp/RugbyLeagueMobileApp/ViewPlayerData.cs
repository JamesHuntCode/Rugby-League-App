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
        ArrayAdapter<Player> playeradapter = null;

        // Data Structures Used:
        List<Player> playerJSONdata;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewPlayers);
            ActionBar.Title = "View All Players";

            // Set Widgets:
            addNewPlayer = FindViewById<Button>(Resource.Id.btnAddPlayer);

            // Get Data
            playerJSONdata = services.GetAllPlayerData();

            // Configure ListView
            players = FindViewById<ListView>(Resource.Id.lvPlayers);
            playeradapter = new ArrayAdapter<Player>(this, Android.Resource.Layout.SimpleListItem1, playerJSONdata);
            players.Adapter = playeradapter;

            // Handle Clicks:

            // Add New Player Button
            addNewPlayer.Click += delegate
            {

            };

            // Players ListView
            players.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => PlayerClicked(sender, e);
        }

        /// <summary>
        /// User had clicked on a player in the ListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="event args"></param>
        protected void PlayerClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            string selectedItem = ((TextView)e.View).Text;
        }
    }
}
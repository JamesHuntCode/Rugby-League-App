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
    public class CreateNewTeam : Activity
    {
        private AppServices services = new AppServices();

        // Widgets Used:
        Button addPlayer;
        Button save;
        Spinner playerSelector;
        EditText numberSelector;
        ListView addedPlayers;

        // Adapters Used:
        ArrayAdapter<string> playerSelectorAdapter  = null;
        ArrayAdapter<string> addedPlayersAdapter    = null;

        // Data Structures Used:
        List<Player> RawJSONdata;
        List<string> formattedJSONdata;
        List<string> addedPlayerData;

        // Global Variables Used:
        string currentlySelectedPlayer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateTeam);
            ActionBar.Title = "Create New Team";

            // Set Widgets:
            addPlayer        = FindViewById<Button>(Resource.Id.btnAddPlayerToTeam);
            save             = FindViewById<Button>(Resource.Id.btnFinished);
            numberSelector   = FindViewById<EditText>(Resource.Id.etNumberInput);

            // Configure Spinner:
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

            playerSelector = FindViewById<Spinner>(Resource.Id.spinnerPlayerSelector);
            playerSelectorAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Android.Resource.Id.Text1);
            playerSelectorAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            playerSelector.Adapter = playerSelectorAdapter;

            // Populate & Refresh Spinner Data
            for (int i = 0; i < formattedJSONdata.Count; i++)
            {
                playerSelectorAdapter.Add(formattedJSONdata[i]);
            }

            playerSelectorAdapter.NotifyDataSetChanged();

            // Set currently selected spinner item.
            currentlySelectedPlayer = RawJSONdata[0].FirstName + " " + RawJSONdata[0].LastName;

            // Configure ListView:
            addedPlayerData = new List<string>();
            addedPlayers = FindViewById<ListView>(Resource.Id.lvNewTeam);
            addedPlayersAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, addedPlayerData);
            addedPlayers.Adapter = addedPlayersAdapter;

            // Handle Clicks:

            // Add Player To Team
            addPlayer.Click += delegate
            {

            };

            // Save Created Team
            save.Click += delegate
            {

            };

            // Added players ListView
            addedPlayers.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => AddedPlayerClicked(sender, e);

        }

        /// <summary>
        /// User had clicked on a player in the ListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="event args"></param>
        protected void AddedPlayerClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            string selectedItem = ((TextView)e.View).Text;
        }
    }
}
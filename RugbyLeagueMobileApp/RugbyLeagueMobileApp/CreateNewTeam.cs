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
    public class CreateNewTeam : Activity
    {
        private AppServices services = new AppServices();

        // Widgets Used:
        Button addPlayer;
        Button save;
        Spinner playerSelector;
        Spinner numberSelector;
        Spinner positionSelector;
        ListView addedPlayers;

        // Adapters Used:
        ArrayAdapter<string> playerSelectorAdapter  = null;
        ArrayAdapter<string> playerNumberAdapter = null;
        ArrayAdapter<string> playerPositionAdapter = null;
        ArrayAdapter<string> addedPlayersAdapter    = null;

        // Data Structures Used:
        List<Player> newTeamMembers = new List<Player>();
        List<Player> RawJSONdata;
        List<string> addedPlayerData;

        // Global Variables Used:
        string currentlySelectedPlayer;
        string currentlySelectedPlayerNumber;
        string currentlySelectedPlayerPosition;
        bool canAdd = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateTeam);
            ActionBar.Title = "Create New Team";

            // Set Widgets:
            addPlayer        = FindViewById<Button>(Resource.Id.btnAddPlayerToTeam);
            save             = FindViewById<Button>(Resource.Id.btnFinished);
            numberSelector   = FindViewById<Spinner>(Resource.Id.spinnerNumberSelector);
            positionSelector = FindViewById<Spinner>(Resource.Id.spinnerPositionSelector);

            // Configure Spinners:

            // Names:
            RawJSONdata = services.GetAllPlayerData();

            playerSelector = FindViewById<Spinner>(Resource.Id.spinnerPlayerSelector);
            playerSelectorAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Android.Resource.Id.Text1);
            playerSelectorAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            playerSelector.Adapter = playerSelectorAdapter;

            playerSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => PlayerSelected(sender, e);

            // Push name selections to adapter
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

                playerSelectorAdapter.Add(displayme);
            }

            // Numbers:
            numberSelector = FindViewById<Spinner>(Resource.Id.spinnerNumberSelector);
            playerNumberAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Android.Resource.Id.Text1);
            playerNumberAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            numberSelector.Adapter = playerNumberAdapter;

            // Push number selections to adapter
            for (int i = 0; i < 13; i++)
            {
                playerNumberAdapter.Add(Convert.ToString(i + 1));
            }

            numberSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => NumberSelected(sender, e);

            // Positions:
            positionSelector = FindViewById<Spinner>(Resource.Id.spinnerPositionSelector);
            playerPositionAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Android.Resource.Id.Text1);
            playerPositionAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            positionSelector.Adapter = playerPositionAdapter;

            // Push position selections to adapter
            playerPositionAdapter.Add("Wing");
            playerPositionAdapter.Add("Loose Forward");
            playerPositionAdapter.Add("Centre");
            playerPositionAdapter.Add("Fly-half");
            playerPositionAdapter.Add("Scrum-half");
            playerPositionAdapter.Add("Number Eight");
            playerPositionAdapter.Add("Flanker");
            playerPositionAdapter.Add("Hooker");
            playerPositionAdapter.Add("Prop");

            positionSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => PositionSelected(sender, e);

            // Configure ListView:
            addedPlayerData = new List<string>();
            addedPlayers = FindViewById<ListView>(Resource.Id.lvNewTeam);
            addedPlayersAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, addedPlayerData);
            addedPlayers.Adapter = addedPlayersAdapter;

            // Handle Clicks:

            // Add Player To Team
            addPlayer.Click += delegate
            {
                if (canAdd && (currentlySelectedPlayer != "No remaining players."))
                {
                    // Capture details:
                    string line1 = "Name: " + currentlySelectedPlayer;
                    string line2 = "Number: " + currentlySelectedPlayerNumber;
                    string line3 = "Position: " + currentlySelectedPlayerPosition;

                    string displayMe = line1 + "\n\n" + line2 + "\n\n" + line3;
                    addedPlayersAdapter.Add(displayMe);

                    playerSelectorAdapter.Remove(currentlySelectedPlayer);
                    playerNumberAdapter.Remove(currentlySelectedPlayerNumber);
                    playerSelectorAdapter.NotifyDataSetChanged();
                    playerPositionAdapter.NotifyDataSetChanged();

                    // Push new team member to array:
                    Player newTeamMember = new Player();
                    newTeamMember.FirstName = currentlySelectedPlayer.Split(' ')[0];
                    newTeamMember.LastName = currentlySelectedPlayer.Split(' ')[1];
                    newTeamMember.PlayerNumber = currentlySelectedPlayerNumber;
                    newTeamMember.PlayerPosition = currentlySelectedPlayerPosition;
                    newTeamMembers.Add(newTeamMember);

                    // Set new selected values for spinners:
                    if (playerSelectorAdapter.Count > 0)
                    {
                        currentlySelectedPlayer = playerSelectorAdapter.GetItem(0);
                        currentlySelectedPlayerNumber = playerNumberAdapter.GetItem(0);
                        currentlySelectedPlayerPosition = playerPositionAdapter.GetItem(0);
                        playerSelectorAdapter.NotifyDataSetChanged();
                        playerPositionAdapter.NotifyDataSetChanged();
                    }
                    else
                    {
                        playerSelectorAdapter.Add("No remaining players.");
                        playerSelectorAdapter.NotifyDataSetChanged();

                        canAdd = false;
                    }

                    // Scroll to most recently added element:
                    addedPlayers.SmoothScrollToPositionFromTop(addedPlayersAdapter.Count, 0);
                }
                else
                {
                    Toast.MakeText(this, "You have no players left to allocate.", ToastLength.Long).Show();
                }
            };

            // Save Created Team
            save.Click += delegate
            {
                // Save all team data:
                services.CreateNewTeam(newTeamMembers);

                // Navigate to view team activity:
                Intent newActivity = new Intent(this, typeof(ViewCurrentTeam));

                StartActivity(newActivity);
            };

            // Added players ListView
            addedPlayers.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => AddedPlayerClicked(sender, e);

        }

        /// <summary>
        /// User has selected an item in the drop down list of players.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PlayerSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            currentlySelectedPlayer = ((TextView)e.View).Text;
        }

        /// <summary>
        /// User has selected a number from the drop down list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NumberSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            currentlySelectedPlayerNumber = ((TextView)e.View).Text;
        }

        /// <summary>
        /// User has selected a position from the drop down list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PositionSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            currentlySelectedPlayerPosition = ((TextView)e.View).Text;
        }

        /// <summary>
        /// User has clicked on a player in the ListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="event args"></param>
        protected void AddedPlayerClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            string selectedItem = ((TextView)e.View).Text;

            // Generate a popup asking if the user wishes to remove that player from the team.
            AlertDialog.Builder inform = new AlertDialog.Builder(this);
            inform.SetTitle("Remove Player?");

            inform.SetMessage("Are you sure you want to remove " + selectedItem.Split(' ')[1].Trim() + " from the team?");

            inform.SetPositiveButton("Yes", delegate
            {
                // Remove selected player from team:
                addedPlayersAdapter.Remove(selectedItem);
                addedPlayersAdapter.NotifyDataSetChanged();

                // Add removed player data back into spinners
                string name = (selectedItem.Split(' ')[1].Trim() + " " + selectedItem.Split(' ')[2].Trim()).Split('\n')[0];
                string number = (selectedItem.Split(' ')[3].Trim()).Split('\n')[0];

                // See if there are any remaining players
                bool noPlayersRemaining = ArraySearch("No remaining players.", playerSelectorAdapter);

                if (noPlayersRemaining)
                {
                    playerSelectorAdapter.Clear();
                    currentlySelectedPlayer = name;
                    canAdd = true;
                }

                playerSelectorAdapter.Add(name);
                playerSelectorAdapter.NotifyDataSetChanged();

                playerNumberAdapter.Add(number);
                playerNumberAdapter.NotifyDataSetChanged();

                inform.Dispose();
            });

            inform.SetNegativeButton("No", delegate
            {
                inform.Dispose();
            });

            inform.Show();
        }

        /// <summary>
        /// Method to linear search through an array adapter for a specified string.
        /// </summary>
        /// <param name="searchFor"></param>
        /// <param name="searchThrough"></param>
        /// <returns></returns>
        protected bool ArraySearch(string searchFor, ArrayAdapter searchThrough)
        {
            for (int i = 0; i < searchThrough.Count; i++)
            {
                if (searchFor == searchThrough.GetItem(i).ToString())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
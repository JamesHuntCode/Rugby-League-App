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
using System.IO;
using Newtonsoft.Json;

namespace RugbyLeagueMobileApp
{
    /// <summary>
    /// Class containing generic methods for use across all application activities.
    /// </summary>
    public class AppServices
    {
        /// <summary>
        /// Path of JSON file which contains player data.
        /// </summary>
        private System.IO.Stream playerdata = Application.Context.Assets.Open("playerdata.json");

        /// <summary>
        /// Path of JSON file which contains team summary data.
        /// </summary>
        private System.IO.Stream teamdata = Application.Context.Assets.Open("team.json");

        /// <summary>
        /// Method to read all data from player data json file.
        /// </summary>
        /// <returns></returns>
        public List<Player> GetAllPlayerData()
        {
            return ReadFile(playerdata);
        }

        /// <summary>
        /// Method to read all team data from json file.
        /// </summary>
        /// <returns></returns>
        public List<Player> GetTeamData()
        {
            return ReadFile(teamdata);
        }

        /// <summary>
        /// Method to add a new player to JSON data file.
        /// </summary>
        /// <param name="newPlayer"></param>
        public void AddNewPlayer(Player newPlayer)
        {
            List<Player> players = GetAllPlayerData();
            players.Add(newPlayer);

            UpdateAllPlayerData(players);
        }

        /// <summary>
        /// Method to write new team data to JSON file.
        /// </summary>
        /// <param name="data"></param>
        public void CreateNewTeam(List<Player> data)
        {
            WriteFile(teamdata, data);
        }

        /// <summary>
        /// Method to determine if user has already created a team.
        /// </summary>
        /// <returns></returns>
        public bool UserHasTeam()
        {
            return ReadFile(teamdata).Count > 0;
        }

        /// <summary>
        /// Method to determine if user has added players to their records.
        /// </summary>
        /// <returns></returns>
        public bool UserHasAddedPlayers()
        {
            return ReadFile(playerdata).Count > 0;
        }

        // PRIVATE METHODS:

        /// <summary>
        /// Method to update data held on all players.
        /// </summary>
        /// <returns></returns>
        private void UpdateAllPlayerData(List<Player> newdata)
        {
            WriteFile(playerdata, newdata);
        }

        /// <summary>
        /// Method to read JSON data from a specified file path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<Player> ReadFile(System.IO.Stream path)
        {
            List<Player> data = new List<Player>();
            string jsondata;

            using (StreamReader SR = new StreamReader(path))
            {
                jsondata = SR.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Player>>(jsondata);
            }

            return data;
        }

        /// <summary>
        /// Method to write JSON data to a specified file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private void WriteFile(System.IO.Stream path, List<Player> data)
        {
            string updatedJSONdata = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (StreamWriter SW = new StreamWriter(path))
            {
                SW.Write(updatedJSONdata);
            }
        }
    }
}
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
        public List<string> GetAllPlayerData()
        {
            return new List<string>();
        }

        /// <summary>
        /// Method to update data held on all players.
        /// </summary>
        /// <returns></returns>
        public bool UpdateAllPlayerData()
        {
            return true;
        }

        /// <summary>
        /// Method to determine if user has already created a team.
        /// </summary>
        /// <returns></returns>
        public bool UserHasTeam()
        {
            return true;
        }

        // METHODS ONLY USED WITHIN THIS CLASS (COMPLETELY PRIVATE):

        /// <summary>
        /// Method to read JSON data from a specified file path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<Player> ReadFile(string path)
        {
            List<Player> data = new List<Player>();

            try
            {
                using (StreamReader SR = new StreamReader(path))
                {
                    string jsondata = SR.ReadToEnd();
                    data = JsonConvert.DeserializeObject<List<Player>>(jsondata);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return data;
        }

        /// <summary>
        /// Method to write JSON to a specified file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool WriteFile(string path, List<Player> data)
        {
            bool success = true;

            try
            {
                string updatedJSONdata = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(path, updatedJSONdata);
            }
            catch (Exception)
            {
                success = false;
                throw;
            }

            return success;
        }
    }
}
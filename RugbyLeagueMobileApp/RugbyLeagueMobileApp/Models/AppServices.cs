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
        /// Method to update data held on one specific player.
        /// </summary>
        /// <param name="playerToBeUpdated"></param>
        /// <returns></returns>
        public bool UpdateIndividualPlayerData(Player playerToBeUpdated)
        {
            return true;
        }
    }
}
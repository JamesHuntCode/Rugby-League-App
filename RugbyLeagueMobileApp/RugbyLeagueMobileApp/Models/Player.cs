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
    /// Class to contain required data fields for the players.
    /// </summary>
    public class Player
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }

        public string PlayerNumber { get; set; }
        public string PlayerPosition { get; set; }
    }
}
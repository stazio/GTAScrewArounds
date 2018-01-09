using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // These are for the KeyDown and KeyUp events
using GTA; // This is the root GTA namespace. Game, Player, World, etc...
using GTA.Native; // This is the GTA native namespace. For use when doing a Function.Call
using GTA.Math; // This includes information about positions. Vectors!
using GTA.NaturalMotion; 
using System.Reflection;
using System.Resources;
using System.Collections;

namespace Staz_GTA_Screws // Optional; Recommended
{
    public class Some_Script : Script // Change the name of this!
    {
        public string BUILD { get {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            } }

        // This function is called when the game loads in
        // Do important first time things here!

        private UI ui;
        public Some_Script()
        {
            GTA.UI.Notify("Loaded Staz'z Screw Thingies! Build " + BUILD);

            ui = new Staz_GTA_Screws.UI();
            Tick += ui.OnTick;
            KeyDown += ui.OnKeyDown;
            
        }
    }
}

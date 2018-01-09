using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace Staz_GTA_Screws
{
    public static class Actions
    {
        
        private static void ENTITIES_NOT_PLAYER(EntityDelegate e)
        {
            int handle = Game.Player.Character.Handle;
            foreach (Entity enitity in World.GetAllEntities())
            {
                if (enitity.Handle != handle)
                {
                    e(enitity);
                }
            }
        }

        private delegate void EntityDelegate(Entity e);

        internal static void KILL_ALL()
        {
            ENTITIES_NOT_PLAYER((e) => e.Health = -1);
        }

        internal static void REPEATING_KILL_ALL(bool toggle)
        {
            isRepeatadlyKilling = toggle;
        }

        internal static void EXPLODE_ALL()
        {
            ENTITIES_NOT_PLAYER((e) => World.AddExplosion(e.Position, ExplosionType.PetrolPump, 5, 0));
        }

        internal static void REPEATING_EXPLOSION(bool toggle)
        {
            isRepeatadlyExploding = toggle;
        }

        internal static void REMOVE_ENTITIES()
        {
            ENTITIES_NOT_PLAYER((e) => e.Delete());
        }

        internal static void SPAWN_CAR(bool toggle)
        {
            isSpawningCar = toggle;
        }

        internal static void PLAYER_INVINCIBLE(bool invincibility)
        {
            Game.Player.IsInvincible = invincibility;
        }

        internal static void TARGET_INVINCIBLE()
        {
            Game.Player.GetTargetedEntity().IsInvincible = true;
        }

        private static bool isRepeatadlyExploding = false, isRepeatadlyKilling = false, isSpawningCar = false;
        internal static void Tick(object sender, EventArgs e)
        {
            if (isRepeatadlyExploding)
                EXPLODE_ALL();
            if (isRepeatadlyKilling)
                KILL_ALL();
            if (isSpawningCar && Game.Player.Character.IsShooting)
            {
                Vehicle v = World.CreateVehicle(GTA.Native.VehicleHash.Alpha, 
                    (Game.Player.Character.ForwardVector*15) + Game.Player.Character.Position, 
                    Game.Player.Character.Heading);
                
                //GTA.UI.ShowSubtitle(v.Exists().ToString());
                if (!v.Exists())
                {
                    foreach (Vehicle veh in World.GetAllVehicles())
                    {
                        if (veh.IsVisible && veh.IsOnScreen)
                            continue;
                        veh.Delete();
                    }
                }
                else
                {
                    //Vector3 dims = v.Model.GetDimensions();
                    //Vector3 pos = Utils.MoveForward(v.Position, v.Model.GetDimensions().Z, v.Heading);
                    //v.Position = pos;
                    v.Speed = 200;
                }
            }
        }
    }
}

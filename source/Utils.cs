using GTA.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staz_GTA_Screws

{
    public static class Utils
    {
        public static Vector3 MoveForward(Vector3 pos, float transformation, float heading)
        {
            pos.X = (float)(pos.X + (Math.Sin(heading) * transformation));
            pos.Y = (float)(pos.Y + (Math.Cos(heading) * transformation));
            return pos;
        }
    }
}

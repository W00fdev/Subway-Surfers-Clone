using System;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Logic.Utils
{
    public static class Utils
    {
        public static Vector3 Multiply(this Vector3 origin, Vector3 scale)
            => new Vector3(origin.x * scale.x, origin.y * scale.y, origin.z * scale.z);
    }
}

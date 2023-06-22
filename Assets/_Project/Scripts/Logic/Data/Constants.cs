using System;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Logic.Data
{
    public static class Constants
    {
        public static Vector3 GroundColliderSize = new Vector3(8f, 0.5f, 20f);
        public static Vector3 GroundColliderOffsetPerSize = new Vector3(0f, -0.5f, 0.5f);
    }
}

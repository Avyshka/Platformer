using System;
using UnityEngine;

namespace Platformer.Configs
{
    [Serializable]
    public struct AIConfig
    {
        public float speed;
        public float minSqrDistanceToTarget;
        public Transform[] waypoints;
    }
}
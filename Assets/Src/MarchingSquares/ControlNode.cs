﻿using UnityEngine;

namespace Platformer.MarchingSquares
{
    public class ControlNode : Node
    {
        public bool Active;

        public ControlNode(Vector3 position, bool active) : base(position)
        {
            Active = active;
        }
    }
}
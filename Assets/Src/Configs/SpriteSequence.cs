using System;
using System.Collections.Generic;
using Platformer.Enums;
using UnityEngine;

namespace Platformer.Configs
{
    [Serializable]
    public sealed class SpriteSequence
    {
        public AnimState Track;
        public bool Loop;
        public float Speed;
        public List<Sprite> Sprites = new List<Sprite>();
    }
}
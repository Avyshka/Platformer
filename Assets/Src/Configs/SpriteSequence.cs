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
        public List<Sprite> Sprites = new List<Sprite>();
    }
}
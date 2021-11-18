using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Configs
{
    [CreateAssetMenu(fileName = "SpriteAnimCfg", menuName = "Configs / Animation Cfg", order = 1)]
    public class SpriteAnimatorConfig : ScriptableObject
    {
        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}
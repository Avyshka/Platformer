using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Views
{
    public class CannonView : MonoBehaviour
    {
        public Transform muzzleTransform;
        public Transform emitterTransform;
        public List<LevelObjectView> bulletViews;
    }
}
using UnityEngine;

namespace Platformer.Utils
{
    public static class VectorUtils
    {
        public static Vector3 Change(this Vector3 obj, object x = null, object y = null, object z = null)
        {
            return new Vector3(
                x == null ? obj.x : (float) x,
                y == null ? obj.y : (float) y,
                z == null ? obj.x : (float) z
            );
        }

        public static Vector2 Change(this Vector2 obj, object x = null, object y = null, object z = null)
        {
            return new Vector2(
                x == null ? obj.x : (float) x,
                y == null ? obj.y : (float) y
            );
        }
    }
}
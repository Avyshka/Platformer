using System;
using UnityEngine;

namespace Platformer.Views
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform Transform;
        public SpriteRenderer Renderer;
        public Collider2D Collider;
        public Rigidbody2D Rigidbody;

        public Action<LevelObjectView> OnLevelObjectContact;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var levelObject = collider.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }
    }
}
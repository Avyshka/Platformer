using System;
using UnityEngine;

namespace Platformer.Views
{
    public class LevelObjectTrigger : MonoBehaviour
    {
        public event EventHandler<GameObject> TriggerEnter;
        public event EventHandler<GameObject> TriggerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Invader invader))
            {
                TriggerEnter?.Invoke(this, invader.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Invader invader))
            {
                TriggerExit?.Invoke(this, invader.gameObject);
            }
        }
    }
}
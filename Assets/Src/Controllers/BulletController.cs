using Platformer.Views;
using UnityEngine;

namespace Src.Controllers
{
    public class BulletController
    {
        private Vector3 _velocity;
        private LevelObjectView _view;

        public BulletController(LevelObjectView view)
        {
            _view = view;
            SetActive(false);
        }

        public void SetActive(bool value)
        {
            _view.gameObject.SetActive(value);
        }
        
        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            var angle = Vector3.Angle(Vector3.left, _velocity);
            var axis = Vector3.Cross(Vector3.left, _velocity);
            _view.Transform.rotation = Quaternion.AngleAxis(angle, axis);
        }
        
        public void Throw(Vector3 spawnPosition, Vector3 velocity)
        {
            SetActive(true);
            SetVelocity(velocity);
            _view.Transform.position = spawnPosition;
            _view.Rigidbody.velocity = Vector2.zero;
            _view.Rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}
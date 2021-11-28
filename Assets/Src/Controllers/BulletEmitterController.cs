using System.Collections.Generic;
using Platformer.Views;
using UnityEngine;

namespace Platformer.Controllers
{
    public class BulletEmitterController
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _transform;

        private int _currentIndex;
        private float _timeTillNextBullet;
        private float _delay = 1f;
        private float _startSpeed = 10f;

        public BulletEmitterController(List<LevelObjectView> bulletViews, Transform transform)
        {
            _transform = transform;
            foreach (var bulletView in bulletViews)
            {
                _bullets.Add(new BulletController(bulletView));
            }
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;
                _bullets[_currentIndex].Throw(_transform.position, -_transform.up * _startSpeed);
                _currentIndex++;
                if (_currentIndex >= _bullets.Count)
                {
                    _currentIndex = 0;
                }
            }
        }
    }
}
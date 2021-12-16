using System;
using System.Collections.Generic;
using Platformer.Views;
using UnityEngine;

namespace Platformer.Managers
{
    public class LevelCompleteManager : IDisposable
    {
        private readonly Vector3 _startPosition;
        private readonly LevelObjectView _characterView;
        private readonly List<LevelObjectView> _deathZones;
        private readonly List<LevelObjectView> _winZones;

        public LevelCompleteManager(LevelObjectView characterView, List<LevelObjectView> deathZones,
            List<LevelObjectView> winZones)
        {
            _characterView = characterView;
            _deathZones = deathZones;
            _winZones = winZones;

            _startPosition = _characterView.Transform.position;
            _characterView.OnLevelObjectContact += OnLevelObjectContact;
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_deathZones.Contains(contactView))
            {
                _characterView.Transform.position = _startPosition;
            }
        }

        public void Dispose()
        {
            _characterView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}
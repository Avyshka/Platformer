using System;
using System.Collections.Generic;
using Platformer.Controllers;
using Platformer.Enums;
using Platformer.Views;
using UnityEngine;

namespace Platformer.Managers
{
    public class CoinsManager : IDisposable
    {
        private readonly LevelObjectView _characterView;
        private readonly SpriteAnimatorController _spriteAnimator;
        private readonly List<LevelObjectView> _coinViews;
        private readonly List<LevelObjectView> _chestViews;

        public CoinsManager(
            LevelObjectView characterView,
            SpriteAnimatorController spriteAnimator,
            List<LevelObjectView> coinViews,
            List<LevelObjectView> chestViews
        )
        {
            _characterView = characterView;
            _spriteAnimator = spriteAnimator;
            _coinViews = coinViews;
            _chestViews = chestViews;

            _characterView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (var coinView in _coinViews)
            {
                _spriteAnimator.StartAnimation(coinView.Renderer, AnimState.Coin);
            }

            foreach (var chestView in _chestViews)
            {
                _spriteAnimator.StartAnimation(chestView.Renderer, AnimState.Chest);
            }
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_coinViews.Contains(contactView) || _chestViews.Contains(contactView))
            {
                _spriteAnimator.StopAnimation(contactView.Renderer);
                GameObject.Destroy(contactView.gameObject);
            }
        }

        public void Dispose()
        {
            _characterView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}
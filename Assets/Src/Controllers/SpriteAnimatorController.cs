using System;
using System.Collections.Generic;
using Platformer.Configs;
using Platformer.Enums;
using Platformer.Models;
using UnityEngine;

namespace Platformer.Controllers
{
    public class SpriteAnimatorController : IDisposable
    {
        private readonly SpriteAnimatorConfig _config;

        private readonly Dictionary<SpriteRenderer, SequenceAnimation> _activeAnimation =
            new Dictionary<SpriteRenderer, SequenceAnimation>();

        public SpriteAnimatorController(SpriteAnimatorConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track)
        {
            if (_activeAnimation.TryGetValue(spriteRenderer, out var animation))
            {
                ChangeAnimation(animation, track);
            }
            else
            {
                AddAnimation(spriteRenderer, track);
            }
        }

        private void ChangeAnimation(SequenceAnimation animation, AnimState track)
        {
            animation.Sleep = false;
            
            if (animation.Track != track)
            {
                var config = _config.Sequences.Find(sequence => sequence.Track == track);
                animation.Track = track;
                animation.Sprites = config.Sprites;
                animation.Counter = 0;
            }
        }

        private void AddAnimation(SpriteRenderer spriteRenderer, AnimState track)
        {
            var config = _config.Sequences.Find(sequence => sequence.Track == track);
            _activeAnimation.Add(
                spriteRenderer,
                new SequenceAnimation()
                {
                    Loop = config.Loop,
                    Speed = config.Speed,
                    Track = config.Track,
                    Sprites = config.Sprites
                });
        }

        public void StopAnimation(SpriteRenderer spriteRenderer)
        {
            if (_activeAnimation.ContainsKey(spriteRenderer))
            {
                _activeAnimation.Remove(spriteRenderer);
            }
        }

        public void Update()
        {
            foreach (var animation in _activeAnimation)
            {
                animation.Value.Update();
                if (!animation.Value.IsComplete)
                {
                    animation.Key.sprite = animation.Value.CurrentSprite;
                }
            }
        }

        public void Dispose()
        {
            _activeAnimation.Clear();
        }
    }
}
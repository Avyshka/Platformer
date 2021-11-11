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

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
        {
            if (_activeAnimation.TryGetValue(spriteRenderer, out var animation))
            {
                ChangeAnimation(animation, track, loop, speed);
            }
            else
            {
                AddAnimation(spriteRenderer, track, loop, speed);
            }
        }

        private void ChangeAnimation(SequenceAnimation animation, AnimState track, bool loop, float speed)
        {
            animation.Sleep = false;
            animation.Loop = loop;
            animation.Speed = speed;
            animation.Counter = 0;
            if (animation.Track != track)
            {
                animation.Track = track;
                animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
            }
        }

        private void AddAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
        {
            _activeAnimation.Add(
                spriteRenderer,
                new SequenceAnimation()
                {
                    Loop = loop,
                    Speed = speed,
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites
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
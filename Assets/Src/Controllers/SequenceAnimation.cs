using System.Collections.Generic;
using Platformer.Enums;
using UnityEngine;

namespace Platformer.Models
{
    public sealed class SequenceAnimation
    {
        public AnimState Track;
        public List<Sprite> Sprites;
        public bool Loop;
        public float Speed;
        public float Counter;
        public bool Sleep;

        public bool IsComplete => Counter > Sprites.Count;
        public Sprite CurrentSprite => Sprites[Frame];
        private int Frame => (int) Counter;

        public void Update()
        {
            if (Sleep) return;

            Counter += Time.deltaTime * Speed;

            if (Loop)
            {
                RewindWithCheck();
            }
            else if (IsComplete)
            {
                Stop();
            }
        }

        private void RewindWithCheck()
        {
            while (IsComplete)
            {
                Counter -= Sprites.Count;
            }
        }

        private void Stop()
        {
            Counter = Sprites.Count;
            Sleep = true;
        }
    }
}
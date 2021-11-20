using Platformer.Controllers;
using Platformer.Enums;
using Platformer.Models;
using Platformer.Utils;
using Platformer.Views;
using UnityEngine;

namespace Src.Controllers
{
    public class PlayerController
    {
        private readonly Vector3 _leftScale = new Vector3(-1, 1, 1);
        private readonly Vector3 _rightScale = new Vector3(1, 1, 1);

        private readonly LevelObjectView _view;
        private readonly SpriteAnimatorController _spriteAnimator;
        private readonly ContactPooler _contactPooler;
        
        private float _xAxisInput;

        private bool _isJump;
        private bool _isMoving;

        private float _movingSpeed = 140f;
        private float _movingThreshold = 0.1f;
        private float _jumpSpeed = 6;
        private float _jumpThreshold = 1f;

        private float _xVelocity;
        private float _yVelocity;

        private int MovingDirection => _xAxisInput < 0 ? -1 : 1;
        private Vector3 ScaleDirection => _xAxisInput < 0 ? _leftScale : _rightScale;
        private bool IsGrounded => _contactPooler.IsGrounded;
        private bool IsNeedJump => _isJump && Mathf.Abs(_view.Rigidbody.velocity.y) <= _jumpThreshold;
        
        public PlayerController(LevelObjectView player, SpriteAnimatorController spriteAnimator)
        {
            _view = player;
            _spriteAnimator = spriteAnimator;
            ChangeAnimation(AnimState.Idle);
            _contactPooler = new ContactPooler(_view.Collider);
        }
        
        public void Update()
        {
            _spriteAnimator.Update();
            _contactPooler.Update();

            UpdateControls();

            if (_isMoving)
            {
                MoveTowards();
            }

            if (IsGrounded)
            {
                ChangeAnimation( _isMoving ? AnimState.Run : AnimState.Idle);
                
                if (IsNeedJump)
                {
                    Jump();
                }
            }
            else
            {
                FallDown();
            }
        }
        
        private void ChangeAnimation(AnimState track)
        {
            _spriteAnimator.StartAnimation(_view.Renderer, track);
        }
        
        private void UpdateControls()
        {
            _xAxisInput = Input.GetAxis(InputModel.Horizontal);
            _isJump = Input.GetAxis(InputModel.Vertical) > 0;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;
        }
        
        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _movingSpeed * MovingDirection;
            _view.Rigidbody.velocity = _view.Rigidbody.velocity.Change(x: _xVelocity);
            _view.Transform.localScale = ScaleDirection;
        }

        private void Jump()
        {
            _view.Rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }

        private void FallDown()
        {
            if (Mathf.Abs(_view.Rigidbody.velocity.y) <= _jumpThreshold)
            {
                ChangeAnimation(AnimState.Jump);
            }
        }
    }
}
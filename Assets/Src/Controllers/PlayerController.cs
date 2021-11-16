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
        
        private float _xAxisInput;

        private bool _isJump;
        private bool _isMoving;

        private float _movingSpeed = 3f;
        private float _movingThreshold = 0.1f;
        private float _jumpSpeed = 5f;
        private float _jumpThreshold = 1f;

        private float _gravity = -9.8f;
        private float _groundLevel = -3.0f;
        private float _yVelocity;

        private int MovingDirection => _xAxisInput < 0 ? -1 : 1;
        private Vector3 ScaleDirection => _xAxisInput < 0 ? _leftScale : _rightScale;
        private bool IsGrounded => _view.Transform.position.y <= _groundLevel && _yVelocity <= 0;
        private bool IsNeedJump => _isJump && _yVelocity <= 0;
        
        public PlayerController(LevelObjectView player, SpriteAnimatorController spriteAnimator)
        {
            _view = player;
            _spriteAnimator = spriteAnimator;
            ChangeAnimation(AnimState.Idle);
        }
        
        public void Update()
        {
            _spriteAnimator.Update();

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
                else if (_yVelocity < 0)
                {
                    StayOnGround();
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
            _view.Transform.position += Vector3.right * (Time.deltaTime * _movingSpeed * MovingDirection);
            _view.Transform.localScale = ScaleDirection;
        }

        private void Jump()
        {
            _yVelocity = _jumpSpeed;
        }

        private void StayOnGround()
        {
            _yVelocity = float.Epsilon;
            _view.Transform.position = _view.Transform.position.Change(y: _groundLevel);
        }
        
        private void FallDown()
        {
            if (Mathf.Abs(_yVelocity) > _jumpThreshold)
            {
                ChangeAnimation(AnimState.Jump);
            }
            _yVelocity += _gravity * Time.deltaTime;
            _view.Transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
        }
    }
}
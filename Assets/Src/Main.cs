using Platformer.Configs;
using Platformer.Controllers;
using Platformer.Enums;
using Platformer.Views;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private int animationSpeed = 10;
        [SerializeField] private LevelObjectView playerView;
        
        private SpriteAnimatorConfig _playerConfig;
        private SpriteAnimatorController _playerAnimator;

        private void Start()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
                _playerAnimator.StartAnimation(
                    playerView.Renderer,
                    AnimState.Run,
                    true,
                    animationSpeed
                );
            }
        }

        private void Update()
        {
            _playerAnimator.Update();
        }
    }
}
using Platformer.Configs;
using Platformer.Controllers;
using Platformer.Views;
using Src.Controllers;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView playerView;

        private SpriteAnimatorConfig _playerConfig;
        private SpriteAnimatorController _spriteAnimator;

        private PlayerController _playerController;

        private void Start()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            if (_playerConfig)
            {
                _spriteAnimator = new SpriteAnimatorController(_playerConfig);
                _playerController = new PlayerController(playerView, _spriteAnimator);
            }
        }

        private void Update()
        {
            _playerController.Update();
        }
    }
}
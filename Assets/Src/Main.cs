using System.Collections.Generic;
using System.Linq;
using Platformer.Configs;
using Platformer.Controllers;
using Platformer.Managers;
using Platformer.Views;
using Src.Controllers;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView playerView;
        [SerializeField] private Transform background;
        [SerializeField] private List<LevelObjectView> coinViews;
        [SerializeField] private List<LevelObjectView> chestViews;
        [SerializeField] private List<LevelObjectView> deathZones;
        [SerializeField] private List<LevelObjectView> winZones;

        private SpriteAnimatorConfig _playerConfig;
        private SpriteAnimatorController _spriteAnimator;

        private PlayerController _playerController;
        private CameraController _cameraController;

        private SpriteAnimatorConfig _coinsConfig;
        private SpriteAnimatorController _coinsAnimator;
        
        private void Start()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            if (_playerConfig)
            {
                _spriteAnimator = new SpriteAnimatorController(_playerConfig);
                _playerController = new PlayerController(playerView, _spriteAnimator);
                _cameraController = new CameraController(playerView.Transform, Camera.main.transform, background);
                new LevelCompleteManager(playerView, deathZones, winZones);
            }
            
            _coinsConfig = Resources.Load<SpriteAnimatorConfig>("CoinAnimCfg");
            if (_coinsConfig)
            {
                _coinsAnimator = new SpriteAnimatorController(_coinsConfig);
                var coinsManager = new CoinsManager(playerView, _coinsAnimator, coinViews, chestViews);
            }
        }

        private void Update()
        {
            _playerController.Update();
            _cameraController.Update();
            _coinsAnimator.Update();
        }
    }
}
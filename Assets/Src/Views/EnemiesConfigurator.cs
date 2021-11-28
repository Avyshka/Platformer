using Platformer.Configs;
using Platformer.Models;
using Src.Controllers;
using UnityEngine;

namespace Platformer.Views
{
    public class EnemiesConfigurator : MonoBehaviour
    {
        [SerializeField] private AIConfig _simplePatrolAIConfig;
        [SerializeField] private LevelObjectView _simplePatrolAIView;

        private SimplePatrolAIController _simplePatrolAIController;

        private void Start()
        {
            _simplePatrolAIController = new SimplePatrolAIController(
                _simplePatrolAIView,
                new SimplePatrolAIModel(_simplePatrolAIConfig)
            );
        }

        private void FixedUpdate()
        {
            _simplePatrolAIController?.FixedUpdate();
        }
    }
}
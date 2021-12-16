using Platformer.Configs;
using Platformer.Controllers;
using Platformer.Models;
using UnityEngine;

namespace Platformer.Views
{
    public class SimpleAIConfigurator : MonoBehaviour
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
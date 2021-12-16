using System.Collections.Generic;
using Pathfinding;
using Platformer.Controllers;
using Platformer.Interfaces;
using Platformer.Managers;
using Platformer.Models;
using UnityEngine;

namespace Platformer.Views
{
    public class ProtectorAIConfigurator : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _protectorAIView;
        [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
        [SerializeField] private AIPatrolPath _protectorAIPatrolPath;
        [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
        [SerializeField] private Transform[] _protectorWaypoints;

        private ProtectorAIController _protectorAIController;
        private ProtectedZone _protectedZone;

        private void Start()
        {
            _protectorAIController = new ProtectorAIController(
                _protectorAIView,
                new PatrolAIModel(_protectorWaypoints),
                _protectorAIDestinationSetter,
                _protectorAIPatrolPath
            );
            _protectorAIController.Init();
            _protectedZone = new ProtectedZone(_protectedZoneTrigger, new List<IProtector> {_protectorAIController});
            _protectedZone.Init();
        }

        private void OnDestroy()
        {
            _protectorAIController.Deinit();
            _protectedZone.Deinit();
        }
    }
}
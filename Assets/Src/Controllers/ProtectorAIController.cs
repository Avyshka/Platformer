using System;
using Pathfinding;
using Platformer.Interfaces;
using Platformer.Models;
using Platformer.Views;
using UnityEngine;

namespace Platformer.Controllers
{
    public class ProtectorAIController : IProtector
    {
        private readonly LevelObjectView _view;
        private readonly PatrolAIModel _model;
        private readonly AIDestinationSetter _destinationSetter;
        private readonly AIPatrolPath _patrolPath;

        private bool _isPatrolling;

        public ProtectorAIController(LevelObjectView view, PatrolAIModel model, AIDestinationSetter destinationSetter,
            AIPatrolPath patrolPath)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = view != null ? model : throw new ArgumentNullException(nameof(model));
            _destinationSetter = destinationSetter != null
                ? destinationSetter
                : throw new ArgumentNullException(nameof(destinationSetter));
            _patrolPath = patrolPath != null ? patrolPath : throw new ArgumentNullException(nameof(patrolPath));
        }

        public void Init()
        {
            _destinationSetter.target = _model.GetNextTarget();
            _isPatrolling = true;
            _patrolPath.TargetReached += OnTargetReached;
        }

        public void Deinit()
        {
            _patrolPath.TargetReached -= OnTargetReached;
        }

        private void OnTargetReached(object sender, EventArgs eventArgs)
        {
            _destinationSetter.target = _isPatrolling
                ? _model.GetNextTarget()
                : _model.GetClosestTarget(_view.Transform.position);
        }

        public void StartProtection(GameObject invader)
        {
            _isPatrolling = false;
            _destinationSetter.target = invader.transform;
        }

        public void FinishProtection(GameObject invader)
        {
            _isPatrolling = true;
            _destinationSetter.target = _model.GetClosestTarget(_view.Transform.position);
        }
    }
}
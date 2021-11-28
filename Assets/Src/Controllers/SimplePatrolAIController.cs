using System;
using Platformer.Models;
using Platformer.Views;
using UnityEngine;

namespace Platformer.Controllers
{
    public class SimplePatrolAIController
    {
        private readonly LevelObjectView _view;
        private readonly SimplePatrolAIModel _model;

        public SimplePatrolAIController(LevelObjectView view, SimplePatrolAIModel model)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.Transform.position) * Time.fixedDeltaTime;
            _view.Rigidbody.velocity = newVelocity;
        }
    }
}
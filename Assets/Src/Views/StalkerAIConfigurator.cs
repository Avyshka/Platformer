using Pathfinding;
using Platformer.Configs;
using Platformer.Controllers;
using Platformer.Models;
using UnityEngine;

namespace Platformer.Views
{
    public class StalkerAIConfigurator : MonoBehaviour
    {
        [SerializeField] private AIConfig _stalkerAIConfig;
        [SerializeField] private LevelObjectView _stalkerAIView;
        [SerializeField] private Seeker _stalkerAISeeker;
        [SerializeField] private Transform _stalkerAITarget;

        private StalkerAIController _stalkerAIController;

        private void Start()
        {
            _stalkerAIController = new StalkerAIController(
                _stalkerAIView,
                new StalkerAIModel(_stalkerAIConfig),
                _stalkerAISeeker,
                _stalkerAITarget
            );
            InvokeRepeating(nameof(RecalculateAIPath), 0.0f, 1.0f);
        }

        private void FixedUpdate()
        {
            _stalkerAIController?.FixedUpdate();
        }

        private void RecalculateAIPath()
        {
            _stalkerAIController.RecalculatePath();
        }
    }
}
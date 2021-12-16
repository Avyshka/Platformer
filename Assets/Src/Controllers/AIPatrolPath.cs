using System;
using Pathfinding;

namespace Platformer.Controllers
{
    public class AIPatrolPath : AIPath
    {
        public event EventHandler TargetReached;

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            DispatchTargetReached();
        }

        protected virtual void DispatchTargetReached()
        {
            TargetReached?.Invoke(this, EventArgs.Empty);
        }
    }
}
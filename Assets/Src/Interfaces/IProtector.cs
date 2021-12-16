using UnityEngine;

namespace Platformer.Interfaces
{
    public interface IProtector
    {
        public void StartProtection(GameObject invader);
        public void FinishProtection(GameObject invader);
    }
}
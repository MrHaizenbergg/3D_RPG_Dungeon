using UnityEngine;

namespace RpgLogic.Npc.Occupations
{
    public interface IOccupation
    {
        string Name { get; }

        void Trigger(GameObject other);
    }
}
using RpgLogic.Npc;
using UnityEngine;

namespace RpgLogic.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Npc Event", menuName = "Game Events/Npc Event")]

    public class NpcEvent : BaseGameEvent<NPC> { }
}

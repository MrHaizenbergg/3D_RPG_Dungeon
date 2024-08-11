using UnityEngine;
using RpgLogic.Items;

namespace RpgLogic.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Item Event", menuName = "Game Events/Item Event")]

    public class ItemEvent : BaseGameEvent<Items.Item> { }
}

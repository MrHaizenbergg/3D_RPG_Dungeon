using System;
using UnityEngine.Events;

namespace RpgLogic.Events
{
   [Serializable] public class UnityHotbarItemEvent : UnityEvent<Items.Item> { }
}

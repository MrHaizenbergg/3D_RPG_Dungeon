using RpgLogic.Npc.Vendors;
using System;
using UnityEngine.Events;

namespace RpgLogic.Events
{
    [Serializable] public class UnityVendorDataEvent : UnityEvent<VendorData> { }
}
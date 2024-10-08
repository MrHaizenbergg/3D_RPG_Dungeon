using RpgLogic.Npc.Vendors;
using UnityEngine;

namespace RpgLogic.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Vendor Data Event", menuName = "Game Events/Vendor Data Event")]

    public class VendorDataEvent : BaseGameEvent<VendorData> { }
}

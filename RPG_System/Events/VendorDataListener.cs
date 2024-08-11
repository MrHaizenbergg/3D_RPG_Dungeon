using RpgLogic.Events.CustomEvents;
using RpgLogic.Events.Listeners;
using RpgLogic.Npc.Vendors;

namespace RpgLogic.Events
{
    public class VendorDataListener : BaseGameEventListener<VendorData, VendorDataEvent, UnityVendorDataEvent> { }
}

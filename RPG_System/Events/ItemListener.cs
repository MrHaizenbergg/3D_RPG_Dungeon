using RpgLogic.Items;
using RpgLogic.Events.CustomEvents;

namespace RpgLogic.Events.Listeners
{
    public class ItemListener : BaseGameEventListener<Items.Item, ItemEvent, UnityHotbarItemEvent> { }
}
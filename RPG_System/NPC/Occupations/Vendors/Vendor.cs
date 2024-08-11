using RpgLogic.Events.CustomEvents;
using RpgLogic.Items;
using RpgLogic.Npc.Occupations;
using System.Collections;
using UnityEngine;

namespace RpgLogic.Npc.Vendors
{
    public class Vendor : MonoBehaviour, IOccupation
    {
        [SerializeField] private VendorDataEvent onStartVendorScenario=null;

        public string Name => "Let's trade";

        private IItemContainer itemContainer = null;

        private void Start() => itemContainer = GetComponent<IItemContainer>();

        public void Trigger(GameObject other)
        {
            var otherItemContainer = other.GetComponent<IItemContainer>();

            if (otherItemContainer == null) return;

            VendorData vendorData= new VendorData(otherItemContainer,itemContainer);

            onStartVendorScenario.Raise(vendorData);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgLogic.Items.Hotbars
{
    public class Hotbar : MonoBehaviour
    {
       [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[10];

        public void Add(Item itemToAdd)
        {
            foreach (HotbarSlot hotbarSlot in hotbarSlots)
            {
                if (hotbarSlot.AddItem(itemToAdd)) { return; }             
            }
        }
    }
}
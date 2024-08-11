using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgLogic.Items.Magic
{
    public class Spell : ScriptableObject, IHotbarItem
    {
        [Header("Basic Info")]
        [SerializeField] private new string name = "New Spell Name";
        [SerializeField] private Sprite icon = null;

        [SerializeField] private Element element = null;


        public string Name => name;
        public Sprite Icon => icon;
        public Element Element => element;

        public void Use()
        {
            Debug.Log("Casting " + name);
        }
    }
}
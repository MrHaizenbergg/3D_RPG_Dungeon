using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgLogic.Items.Magic
{
    [CreateAssetMenu(fileName = "New Element", menuName = "Magic/Element")]
    public class Element : ScriptableObject
    {
        [SerializeField] private new string name = "New Element Name";
        [SerializeField] private Color textColour;

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
    }
}


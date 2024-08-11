using UnityEngine;
using System.Text;
using System;
using System.Collections.Generic;

namespace RpgLogic.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemRPG : Item, IHotbarItem
    {
        [SerializeField] private protected bool isDefaultItem = false;
        public bool IsDefaultItem {  get { return isDefaultItem; } }

        [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();

        [SerializeField] private Rarity rarity;

        public Rarity Rarity { get { return rarity; } }
        public List<ModifierData> Modifiers { get { return modifiersData; } }

        [Header("ItemData")]
        [SerializeField][Min(0)] private int sellPrice = 1;
        [SerializeField][Min(1)] private int maxStack = 1;

        public override string ColouredName
        {
            get
            {
                string hexColour = ColorUtility.ToHtmlStringRGB(rarity.TextColour);
                return $"<color=#{hexColour}>{name}</color>";
            }
        }

        public override string GetInfoDisplayText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(Rarity.Name).AppendLine();
            stringBuilder.Append("<color=green>Use: ").Append(name).Append("</color>").AppendLine();
            stringBuilder.Append("Max Stack: ").Append(maxStack).AppendLine();
            stringBuilder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

            return stringBuilder.ToString();
        }

        public int SellPrice { get { return sellPrice; } }
        public int MaxStack { get { return maxStack; } }

        public override void Use()
        {
            Debug.Log("Item " + name + " Use");

            if (modifiersData.Count > 0)
            {
                foreach (ModifierData data in modifiersData)
                {
                    data.statMofifier.AffectCharacter(PlayerManager.instance.Player, data.value);
                    Debug.Log("Item " + name + " Use" + data.statMofifier.name);
                }
            }
        }
    }
}

[Serializable]
public class ModifierData
{
    public CharacterStatModifier statMofifier;
    public float value;
}
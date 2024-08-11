using System.Text;
using UnityEngine;

namespace RpgLogic.Items
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
    public class Equipment : ItemRPG
    {
        [SerializeField] private EquipmentSlot equipSlot;
        public EquipmentSlot EquipSlot { get { return equipSlot; } }

        [SerializeField] private GameObject itemObject;
        public GameObject ItemObject { get { return itemObject; } }

        [SerializeField] private bool twoHand;
        [SerializeField] private bool twoHandLong;

        public bool TwoHand { get { return twoHand; } }
        public bool TwoHandLong {  get { return twoHandLong; } }

        [SerializeField] private SkinnedMeshRenderer mesh;
        public SkinnedMeshRenderer Mesh { get { return mesh; } }

        [SerializeField] private int armorModifier;
        [SerializeField] private int damageModifier;

        public int ArmorModifier { get { return armorModifier; } }
        public int DamageModifier {  get { return damageModifier; } }

        public override void Use()
        {
            base.Use();

            EquipmentManager.Instance.Equip(this);
        }
        public void Unequip()
        {
            EquipmentManager.Instance.Unequip((int)equipSlot);

            if ((int)equipSlot != 3)
                EquipmentManager.Instance.EquipDefaultItem((int)equipSlot);
        }

        public override string GetInfoDisplayText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(Rarity.Name).AppendLine();
            stringBuilder.Append("<color=green>Use: ").Append(name).Append("</color>").AppendLine();

            if (damageModifier > 0)
                stringBuilder.Append("Damage: ").Append(damageModifier).AppendLine();
            if (armorModifier > 0)
                stringBuilder.Append("Armor: ").Append(armorModifier).AppendLine();

            stringBuilder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

            return stringBuilder.ToString();
        }
    }

    public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
    public enum EquipmentMeshRegion { Head, Legs, Arms, Torso } //Correspond body blendshapes
}
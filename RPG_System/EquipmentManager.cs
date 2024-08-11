using RpgLogic.Items;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Debug.LogWarning("EquipManager more one");
    }
    #endregion

    [SerializeField] private Equipment[] defaultItems;
    [SerializeField] private SkinnedMeshRenderer[] currentMeshes;
    [SerializeField] private EquipInventory equipInventory;

    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    private GameObject currentWeapon = null;
    private GameObject currentShield = null;

    public bool shieldEquip { get; private set; } = false;
    public bool weaponTwoHand { get; private set; } = false;
    public bool weaponTwoHandLong {  get; private set; } = false;

    public Equipment[] currentEquipment { get; private set; }

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    [SerializeField] private InventoryRPG inventory = null;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipSlot;

        Equipment oldItem = Unequip(slotIndex);

        ItemSlot itemSlot = new ItemSlot();
        itemSlot.item = newItem;

        equipInventory.EquipItem(itemSlot.item as Equipment);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        Debug.Log("SlotIndex: " + slotIndex);
        if (slotIndex == 3) //WeaponSlot
        {
            currentEquipment[3] = newItem;
            currentWeapon = Instantiate(newItem.ItemObject);

            if (newItem.TwoHand)
            {
                weaponTwoHand = true;
                weaponTwoHandLong = false;
                currentWeapon.transform.rotation = Quaternion.Euler(21, 39, -95);
                //currentWeapon.transform.localScale = new Vector3(100, 100, 100);
                Debug.Log("Twohand");
                Debug.Log("WeaponHand " + weaponTwoHand);
            }
            if (newItem.TwoHandLong)
            {
                weaponTwoHandLong = true;
                weaponTwoHand= false;
                currentWeapon.transform.rotation = Quaternion.Euler(21, 39, -95);
                Debug.Log("TwohandLong");
                Debug.Log("WeaponHand " + weaponTwoHand);
            }
            if(!weaponTwoHandLong && !weaponTwoHand) 
            {
                currentWeapon.transform.rotation = Quaternion.Euler(-180, -90, 90);
                Debug.Log("TwohandFalse");
                Debug.Log("WeaponHand " + weaponTwoHand);
            }

            currentWeapon.transform.SetParent(rightHand.transform, false);
            currentWeapon.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            currentWeapon.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
            currentWeapon.GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();

            if (newItem.TwoHand || newItem.TwoHandLong)
            {
                Unequip(4);
                Debug.Log("ShieldUnequip");
            }

            Debug.Log("SpawnSword");
            return;
        }
        if (slotIndex == 4) //ShieldSlot
        {
            currentEquipment[4] = newItem;
            currentShield = Instantiate(newItem.ItemObject);
            currentShield.transform.SetParent(leftHand.transform, false);
            currentShield.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            currentShield.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
            currentShield.GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();
            shieldEquip = true;

            if (weaponTwoHand || weaponTwoHandLong)
            {
                Unequip(4);
            }
            Debug.Log("SpawnShield");
            return;
        }

        currentEquipment[slotIndex] = newItem;

        currentMeshes[slotIndex].sharedMesh = newItem.Mesh.sharedMesh;
    }
    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null && !currentMeshes[3])
            {
                currentMeshes[slotIndex].sharedMesh = null;
            }

            Equipment oldItem = currentEquipment[slotIndex];

            ItemSlot itemSlot = new ItemSlot();
            itemSlot.item = oldItem;

            equipInventory.UnequipItem(itemSlot.item as Equipment);

            Debug.Log("CurrentEquipment: " + currentEquipment[slotIndex]);
            if (slotIndex == 3)
            {
                weaponTwoHand = false;
                weaponTwoHandLong = false;
                Destroy(currentWeapon);
                shieldEquip = false;
                Destroy(currentShield);

                if (currentEquipment[3] == null)
                {
                    currentEquipment[3] = oldItem;
                    currentWeapon = Instantiate(oldItem.ItemObject);

                    Debug.Log("CurrentEquipment2: " + currentEquipment[slotIndex]);

                    if (oldItem.TwoHand)
                    {
                        weaponTwoHand = true;
                        weaponTwoHandLong = false;
                        currentWeapon.transform.rotation = Quaternion.Euler(-214, -133, 87);
                    }
                    if (oldItem.TwoHandLong)
                    {
                        weaponTwoHandLong= true;
                        weaponTwoHand = false;
                        currentWeapon.transform.rotation = Quaternion.Euler(-214, -133, 87);
                    }
                    if(!weaponTwoHand && !weaponTwoHandLong)
                    {
                        currentWeapon.transform.rotation = Quaternion.Euler(-180, -90, 90);
                    }

                    currentWeapon.transform.SetParent(rightHand.transform, false);
                    Debug.Log("CurrentSword: " + weaponTwoHand);
                    Debug.Log("CurrentSpear: " + weaponTwoHandLong);
                }
            }
            else if (slotIndex == 4)
            {
                shieldEquip = false;
                Destroy(currentShield);

                if (currentEquipment[4] == null)
                {
                    currentEquipment[4] = oldItem;
                    currentShield = Instantiate(oldItem.ItemObject);
                    //weaponSlotPlayer.twoHandSword = currentWeapon;
                    //go.transform.localScale = new Vector3(100,100, 100);
                    currentShield.transform.rotation = Quaternion.Euler(-180, -90, 90);
                    currentShield.transform.SetParent(rightHand.transform, false);
                    shieldEquip = true;
                    Debug.Log("SpawnSwordOld");
                }
            }
            else
            {
                currentMeshes[slotIndex].sharedMesh = oldItem.Mesh.sharedMesh;
            }

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }

        return null;
    }
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    public void EquipDefaultItem(int slot)
    {
        Equip(defaultItems[slot]);
    }

    private void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }
}

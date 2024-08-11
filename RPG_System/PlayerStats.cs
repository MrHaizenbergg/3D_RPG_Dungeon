using UnityEngine.UI;
using UnityEngine;
using RpgLogic.Items;

public class PlayerStats : CharacterStats
{
    public PlayerLevel playerLevel {  get; set; }

    [SerializeField] private Text damageStats;
    [SerializeField] private Text armorStats;

    public event System.Action<float, float> OnStaminaChanged;

    private void Start()
    {
        playerLevel=GetComponent<PlayerLevel>();
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
        UpdateStatsUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            TakeDamage(15);
        }

        if (currentStamina < maxStamina)
        {
            if (currentStamina > 20)
                blockStamina = false;

            currentStamina += 0.08f;

            if (OnStaminaChanged != null)
            {
                OnStaminaChanged(maxStamina, currentStamina);
            }
        }
    }

    private void UpdateStatsUI()
    {
        damageStats.text = Damage.GetValue().ToString();
        armorStats.text = Armor.GetValue().ToString();
        Debug.Log("UpdateStatsUI");
    }

    public void MinusStamina(int stamina)
    {
        stamina += Armor.GetValue();
        stamina = Mathf.Clamp(stamina, 0, int.MaxValue);

        currentStamina -= stamina;
        Debug.Log(transform.name + " minus " + stamina + " stamina.");

        if (OnStaminaChanged != null)
        {
            OnStaminaChanged(maxStamina, currentStamina);
        }

        if (currentStamina <= 0)
        {
            currentStamina = 0;

            blockStamina = true;
            Debug.Log("Вы устали");
        }
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (oldItem != null)
        {
            Armor.RemoveModifier(oldItem.ArmorModifier);
            Damage.RemoveModifier(oldItem.DamageModifier);
        }

        if (newItem != null)
        {
            Armor.AddModifier(newItem.ArmorModifier);
            Damage.AddModifier(newItem.DamageModifier);
        }

        UpdateStatsUI();
    }

    public override void Die()
    {
        base.Die();
        //Kill the Player
        PlayerManager.instance.KillPlayer();
    }
}

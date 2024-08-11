using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private protected int maxHealth = 100;
    [SerializeField] private protected int maxStamina = 100;

    public int currentHealth { get; private set; }
    public float currentStamina {  get; set; }

    public bool blockHealth = false;
    public bool blockStamina = false;

    [SerializeField] private Stat damage;
    [SerializeField] private Stat armor;

    public Stat Damage { get { return damage; } }
    public Stat Armor { get { return armor; } }

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action OnGetHit;
    public event System.Action OnHealPotion;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    public void TakeDamage(int damage)
    {
        //if (blockHealth) //ShieldDamagRezist
        //damage -= 2;

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        //Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnGetHit != null)
        {
            OnGetHit.Invoke();
        }

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HealFromPotion(int amount)
    {
        if (amount <= 0) { return; }

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        if (OnHealPotion != null)
        {
            OnHealPotion.Invoke();
        }

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public virtual void Die()
    {
        //Debug.Log(transform.name + " died.");
    }
}

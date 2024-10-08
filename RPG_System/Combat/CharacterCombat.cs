using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    [SerializeField] private protected float attackSpeed = 1f;
    [SerializeField] private protected float attackCooldown = 0f;
    private float blockCooldown = 0f;

    const float combatCooldown = 5;
    private protected float lastAttackTime;

    private protected bool inCombat;
    public bool InCombat {  get { return inCombat; } }

    private bool inBlock;
    public bool InBlock {  get { return inBlock; } }

    public event System.Action OnAttack;
    public event System.Action OnBlock;

    private protected CharacterStats myStats;
    CharacterStats opponentStats;

    protected virtual void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
        attackCooldown -= Time.deltaTime;
        blockCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }

        if (blockCooldown >= 0f)
        {
            inBlock = true;
            myStats.blockHealth = true;
        }
        else
        {
            inBlock = false;
            myStats.blockHealth = false;
        }
    }

    public virtual void AttackEnemy(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f && myStats.blockStamina == false)
        {
            opponentStats = targetStats;

            if (OnAttack != null)
                OnAttack();

            //myStats.MinusStamina(10);
            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public virtual void Attack()
    {
        if (attackCooldown <= 0f && myStats.blockStamina == false)
        {
            if (OnAttack != null)
                OnAttack();

            //myStats.MinusStamina(10);
            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public virtual void AttackHit_AnimationEvent()
    {
        if (opponentStats != null)
        {
            opponentStats.TakeDamage(myStats.Damage.GetValue());

            if (opponentStats.currentHealth <= 0)
            {
                inCombat = false;
            }
        }
    }

    public virtual void ShieldBlock()
    {
        Debug.Log("Block");

        if (OnBlock != null)
            OnBlock();

        blockCooldown = 0.1f;
    }
}

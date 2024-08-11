using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerCombat : CharacterCombat
{
    private EquipmentManager EquipmentManager;
    private PlayerStats playerStats;

    public event System.Action OnAttackPlayer;

    protected override void Start()
    {
        base.Start();
        EquipmentManager = EquipmentManager.Instance;
        playerStats = GetComponent<PlayerStats>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetButton("ShieldBlock") && EquipmentManager.shieldEquip)
        {
            ShieldBlock();
        }
    }

    public override void Attack()
    {
        if (attackCooldown <= 0f && myStats.blockStamina == false)
        {
            if (OnAttackPlayer != null)
                OnAttackPlayer();

            playerStats.MinusStamina(10);
            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }

        AttackHit_AnimationEvent();
    }

    public override void AttackHit_AnimationEvent()
    {
        base.AttackHit_AnimationEvent();
    }

    public override void ShieldBlock()
    {
        base.ShieldBlock();
    }
}

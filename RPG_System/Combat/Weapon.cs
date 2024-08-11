using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private CharAnimEventReciever animEventReciever;
    [SerializeField] private PlayerStats myStats;
    [SerializeField] private Collider boxCollider;

    private void OnEnable()
    {
        if (animEventReciever == null)
            animEventReciever = PlayerManager.instance.Player.GetComponentInChildren<CharAnimEventReciever>();
        if (myStats == null)
            myStats = PlayerManager.instance.Player.GetComponent<PlayerStats>();

        animEventReciever.CanAttackEvent += EnableWeaponCollider;
        animEventReciever.NotCanAttackEvent += DisableWeaponCollider;
    }

    private void OnDisable()
    {
        animEventReciever.CanAttackEvent -= EnableWeaponCollider;
        animEventReciever.NotCanAttackEvent -= DisableWeaponCollider;
    }

    private void EnableWeaponCollider(object sender,EventArgs e)
    {
        boxCollider.enabled = true;
    }

    private void DisableWeaponCollider(object sender, EventArgs e)
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<EnemyStats>())
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();

            if (enemy != null)
            {
                enemy.TakeDamage(myStats.Damage.GetValue());
            }
        }
    }
}

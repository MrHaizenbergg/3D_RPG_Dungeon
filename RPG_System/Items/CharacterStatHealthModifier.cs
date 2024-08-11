using UnityEngine;

[CreateAssetMenu(fileName ="New StatForChange",menuName ="ChangeStatModifiers")]
public class CharacterStatHealthModifier : CharacterStatModifier
{
    public override void AffectCharacter(GameObject character, float value)
    {
        PlayerStats stats = character.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.HealFromPotion((int)value);
            Debug.Log("Use HealModifier");
        }
    }
}

using System;
using UnityEngine;

public class CharacterItemForAnimations : MonoBehaviour
{
    [SerializeField] private CharAnimEventReciever animReciever;

    [Header("ItemsForAnimations")]
    [SerializeField] private GameObject healthPotion;
    [SerializeField] private GameObject weaponHolderR;

    private void Start()
    {
        animReciever.ShowHealthPotionEvent += HealthPotionOn;
        animReciever.HideHealthPotionEvent += HealthPotionOff;
    }
    private void OnDisable()
    {
        animReciever.ShowHealthPotionEvent-= HealthPotionOn;
        animReciever.HideHealthPotionEvent -= HealthPotionOff;
    }

    public void HealthPotionOn(object sender,EventArgs e) 
    {
        healthPotion.SetActive(true); 
        weaponHolderR.SetActive(false);
    }

    public void HealthPotionOff(object sender, EventArgs e) 
    {
        healthPotion.SetActive(false); 
        weaponHolderR.SetActive(true);
    }
}

using UnityEngine;

namespace RpgLogic.Items
{
    public abstract class Item: ScriptableObject
    {
        [Header("Basic Info")]
        [SerializeField] private new string name = "New Item Name";
        [SerializeField] private string description = "New Item Description";
        [SerializeField] private Sprite icon = null;
        [SerializeField] private AudioClip actionSFX = null;

        public string Name => name;
        public string Desctiption => description;
        public abstract string ColouredName { get; }
        public Sprite Icon => icon;
        public AudioClip ActionSFX => actionSFX;

        public abstract void Use();

        public abstract string GetInfoDisplayText();
    }
}
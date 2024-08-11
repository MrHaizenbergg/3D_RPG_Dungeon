using TMPro;
using UnityEngine;

namespace RpgLogic.Npc
{
    public class NPC_UI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI npcNameText = null;
        [SerializeField] private TextMeshProUGUI npcGreetingText = null;
        [SerializeField] private GameObject occupationButtonPrefab = null;
        [SerializeField] private Transform occupationButtonHolder = null;

        public void SetNPC(NPC npc)
        {
            npcNameText.text = npc.Name;
            npcGreetingText.text = npc.GreetingText;

            foreach (Transform child in occupationButtonHolder)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < npc.Occupations.Length; i++)
            {
                GameObject buttonInstance = Instantiate(occupationButtonPrefab, occupationButtonHolder);
                buttonInstance.GetComponent<OccupationButton>().Initialize(npc.Occupations[i],npc.OtherInteractor);
            }
        }
    }
}


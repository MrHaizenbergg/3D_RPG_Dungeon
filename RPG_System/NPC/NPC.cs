using RpgLogic.Events.CustomEvents;
using RpgLogic.Npc.Occupations;
using UnityEngine;

namespace RpgLogic.Npc
{
    public class NPC : Interactable
    {
        [SerializeField] private NpcEvent onStartInteraction=null;

        [SerializeField] private new string name = "New NPC Name";
        [SerializeField] private string greetingText = "Hello adventurer!";

        private IOccupation[] occupations=new IOccupation[0];
        //DialogSystem

        public string Name=>name;
        public string GreetingText=>greetingText;
        public IOccupation[] Occupations => occupations;

        public GameObject OtherInteractor {  get; private set; }=null;

        private void Start() => occupations = GetComponents<IOccupation>();

        public override void Interact()
        {
            OtherInteractor = player.gameObject;

            base.Interact();
            GameControll.instance.ShowCursor();
            onStartInteraction.Raise(this);
        }
    }
}



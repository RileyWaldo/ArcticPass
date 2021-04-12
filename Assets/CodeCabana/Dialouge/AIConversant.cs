using UnityEngine;

namespace CodeCabana.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue = null;
        //[SerializeField] GameObject[] otherNPC;
        [SerializeField] string[] NPCNames = null;

        public string GetNPCName(DialogueSpeaker speaker)
        {
            string nameToReturn = "NamelessOne";
            if (speaker == DialogueSpeaker.NPC1)
            {
                if (NPCNames[0] != null)
                    nameToReturn = NPCNames[0];
            }
            else if (speaker == DialogueSpeaker.NPC2)
            {
                if (NPCNames[1] != null)
                    nameToReturn = NPCNames[1];
            }
            else if(speaker == DialogueSpeaker.NPC3)
            {
                if (NPCNames[2] != null)
                    nameToReturn = NPCNames[2];
            }
            return nameToReturn;
        }

        public void SetNewDialogue(Dialogue newDialogue)
        {
            dialogue = newDialogue;
        }
    }
}

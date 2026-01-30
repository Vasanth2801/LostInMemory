using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isPlayerClose;

    void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DIalogueManager>().StartDialogue(dialogue);
    }
}
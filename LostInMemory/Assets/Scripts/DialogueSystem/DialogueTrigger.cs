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

    private void Update()
    {
        if(isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            BlackSmith();
        }
        else
        {
            return;
        }
    }

    public void BlackSmith()
    {
        FindObjectOfType<DIalogueManager>().BlackSmith(dialogue);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
}
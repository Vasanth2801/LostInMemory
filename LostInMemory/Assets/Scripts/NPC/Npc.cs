using UnityEngine;
using TMPro;

public class Npc : MonoBehaviour
{
    public bool isPlayerClose;
    public string npcName;
    public TextMeshProUGUI nameText;
    public GameObject shopPanel;

    void Start()
    {
        nameText.text = npcName;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isPlayerClose)
        {
            shopPanel.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
}

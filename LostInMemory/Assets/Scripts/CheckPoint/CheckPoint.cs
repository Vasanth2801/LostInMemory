using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject SavePointPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SavePointPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SavePointPanel.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        SavePointPanel.SetActive(false);
    }

    public void SaveGame()
    {
        Debug.Log("Game Saved!");
        SavePointPanel.SetActive(false);
    }
}
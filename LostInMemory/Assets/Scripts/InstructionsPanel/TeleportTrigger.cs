using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] GameObject teleportPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            teleportPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CloseTeleportPanel()
    {
        teleportPanel.SetActive(false);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}

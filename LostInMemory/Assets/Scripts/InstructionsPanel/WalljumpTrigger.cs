using UnityEngine;

public class WalljumpTrigger : MonoBehaviour
{
    [SerializeField] GameObject walljumpPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            walljumpPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CloseWalljumpPanel()
    {
        walljumpPanel.SetActive(false);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
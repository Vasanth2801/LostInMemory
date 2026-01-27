using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public GameObject jumpPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jumpPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CloseJumpPanel()
    {
        jumpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}

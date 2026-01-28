using UnityEngine;

public class RunandSlideTrigger : MonoBehaviour
{
    [SerializeField] GameObject runAndSlidePanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            runAndSlidePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ClosePanel()
    {
        runAndSlidePanel.SetActive(false);
        Time.timeScale = 1f; 
        Destroy(gameObject);
    }
}

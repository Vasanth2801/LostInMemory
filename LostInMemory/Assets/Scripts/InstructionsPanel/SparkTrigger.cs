using UnityEngine;

public class SparkTrigger : MonoBehaviour
{
    [SerializeField] GameObject sparkPanel;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sparkPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CloseSparkPanel()
    {
        sparkPanel.SetActive(false);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}

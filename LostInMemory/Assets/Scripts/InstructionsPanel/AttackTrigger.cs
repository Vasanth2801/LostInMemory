using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] GameObject attackPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            attackPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CloseAttackPanel()
    {
        attackPanel.SetActive(false);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}

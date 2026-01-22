using UnityEngine;
using TMPro;

public class Loot : MonoBehaviour
{
    private Player player;
    [SerializeField] private CollectableSO collectableSO;
    public Animator animator;
    public TextMeshProUGUI itemNameText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

       if(player == null)
        {
            return;
        }

        CollectItem();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = null;
        }
    }

    void CollectItem()
    {
        itemNameText.text = "Found" + collectableSO.itemName;
        animator.Play("ScrollAnimation");
        collectableSO.Collect(player);
        Destroy(gameObject,0.5f);
    }
}
using UnityEngine;
using TMPro;
using System.Collections;

public class Loot : MonoBehaviour
{
    private Player player;
    [SerializeField] private CollectableSO collectableSO;
    public Animator animator;
    public TextMeshProUGUI itemNameText;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] bool canBeCollected;
    [SerializeField] private float collectDelay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

       if(player == null || !canBeCollected)
        {
            return;
        }

        CollectItem();
    }

    public void Initialize(CollectableSO collectableSO)
    {
        this.collectableSO = collectableSO;
        sr.sprite = collectableSO.itemSprite;

        StartCoroutine(EnableCollection());
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

    IEnumerator EnableCollection()
    {
        yield return new WaitForSeconds(collectDelay);
        canBeCollected = true;
    }
}
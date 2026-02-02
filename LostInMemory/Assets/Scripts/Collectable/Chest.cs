using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Chest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<CollectableSO> lootTable = new List<CollectableSO>();
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float launchForce = 30f;
    private bool isOpen = false;
    PlayerInput playerInput;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerInput>(out PlayerInput input))
        {
            playerInput = input;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out PlayerInput input))
        {
            playerInput = null;
        }
    }

    private void Update()
    {
        if (isOpen || playerInput == null)
        {
            return;
        }

        if(playerInput.actions["Interact"].WasPressedThisFrame())
        {
            StartCoroutine(OpenChest());
        }
    }

    IEnumerator OpenChest()
    {
        isOpen = true;
        animator.Play("ChestAnimation");
        yield return new WaitForSeconds(spawnDelay);

        foreach (CollectableSO loot in lootTable)
        {
            Loot newLoot = Instantiate(lootPrefab, transform.position, Quaternion.identity).GetComponent<Loot>();
            newLoot.Initialize(loot);

            Rigidbody2D rb = newLoot.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
            rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(spawnDelay);
    }
}

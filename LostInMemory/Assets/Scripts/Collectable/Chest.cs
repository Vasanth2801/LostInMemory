using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CollectableSO collectable;
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    private bool isOpen = false;
    PlayerInput playerInput;

    private void OnTriggerEnter2D(Collider2D collision)
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

       Loot newLoot = Instantiate(lootPrefab, transform.position, Quaternion.identity).GetComponent<Loot>();
    }
}

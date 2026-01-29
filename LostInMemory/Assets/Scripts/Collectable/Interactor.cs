using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public bool isRange;
    public UnityEvent onInteract;


    private void Update()
    {
        if(isRange && Input.GetKeyDown(KeyCode.E))
        {
            onInteract.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isRange = false;
        }
    }
}

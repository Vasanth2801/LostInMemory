using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [SerializeField] private float duration;

    public void OpenChest()
    {
        Debug.Log("Chest opened!");
        Destroy(gameObject, duration);
    }
}
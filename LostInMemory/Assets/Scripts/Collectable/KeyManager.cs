using TMPro;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyText;
    public int keyCount = 0;

    private void Start()
    {
        keyText.text = "Keys: " + keyCount;
    }

    void Update()
    {
        keyText.text = "Keys: " + keyCount;
    }

    public void AddKey()
    {
        keyCount++;
        Debug.Log("Key collected! Total keys: " + keyCount);
    }
}

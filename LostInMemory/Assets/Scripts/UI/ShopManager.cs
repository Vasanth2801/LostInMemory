using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void UpgradeHeal()
    {
        Debug.Log("Upgrade Heal");
    }

    public void UpgradeSpark()
    {
        Debug.Log("Upgrade Spark");
    }

    public void UpgradeTeleport()
    {
        Debug.Log("Upgrade Teleport");
    }
}
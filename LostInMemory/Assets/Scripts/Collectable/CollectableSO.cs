using UnityEngine;

public abstract class CollectableSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;

    public abstract void Collect(Player player);
}

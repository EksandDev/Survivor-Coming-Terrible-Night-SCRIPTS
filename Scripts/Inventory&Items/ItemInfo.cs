using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private ItemCreator _creator;

    public string GetItemName()
    {
        return _creator.ItemName;
    }

    public GameObject GetItemPrefab()
    {
        return _creator.ItemPrefab;
    }

    public GameObject GetItemIcon()
    {
        return _creator.ItemIcon;
    }

    public float GetLiveDamage()
    {
        return _creator.LiveDamage;
    }

    public float GetObjectDamage()
    {
        return _creator.ObjectDamage;
    }

    public GameObject[] GetItemsForCraft()
    {
        return _creator.ItemsForCraft;
    }

    public int GetItemID()
    {
        return _creator.ItemID;
    }
}

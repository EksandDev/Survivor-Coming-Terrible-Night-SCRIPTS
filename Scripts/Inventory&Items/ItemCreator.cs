using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
public class ItemCreator : ScriptableObject
{
    public string ItemName;
    public GameObject ItemPrefab;
    public GameObject ItemIcon;
    public float LiveDamage;
    public float ObjectDamage;
    public GameObject[] ItemsForCraft;
    public int ItemID;
}

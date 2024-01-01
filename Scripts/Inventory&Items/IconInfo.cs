using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconInfo : MonoBehaviour
{
    [SerializeField] private IconCreator _creator;

    public GameObject GetIconPrefab()
    {
        return _creator.IconPrefab;
    }

    public int GetIconID()
    {
        return _creator.IconID;
    }
}

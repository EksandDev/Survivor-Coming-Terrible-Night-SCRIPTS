using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMineable
{
    public float Health { get; set; }
    public GameObject[] Resources {  get; set; }
    public Vector3 SpawnResourcesPosition { get; }

    public void Destruction(float damage);
    public void DropItem();
}

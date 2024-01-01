using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, IEatable
{
    private float _satiety = 10;

    public float Satiety 
    {
        get { return _satiety; }
    }
}

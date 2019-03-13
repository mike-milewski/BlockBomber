using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour 
{
    public static Destructable Instance;

    [SerializeField]
    private int Points;

    void Awake()
    {
        Instance = this;
    }

    public int GetPoints()
    {
        return Points;
    }
}

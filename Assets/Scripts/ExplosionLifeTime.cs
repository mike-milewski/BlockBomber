using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLifeTime : MonoBehaviour 
{
    public static ExplosionLifeTime Instance;

    [SerializeField]
    private float LifeTime;

    public float GetLifeTime
    {
        get
        {
            return LifeTime;
        }
        set
        {
            LifeTime = value;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}

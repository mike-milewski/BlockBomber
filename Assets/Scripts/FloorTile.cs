using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField]
    private int TileHealth;

    private int GetHealth
    {
        get
        {
            return TileHealth;
        }
        set
        {
            TileHealth = value;
        }
    }

    public void DoDamage()
    {
        TileHealth--;
        MoveDownABit();
        if(TileHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void MoveDownABit()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
    }
}

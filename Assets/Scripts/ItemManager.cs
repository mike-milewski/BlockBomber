using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance = null;

    [SerializeField]
    private Transform Float_Parent;

    [SerializeField]
    private List<Items> items = new List<Items>();

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public float DropRate(float Rate)
    {
        if(Random.value * 100 <= Rate)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (Random.value * 100 < items[i].GetPercentSpawn)
                {
                    Instantiate(items[i], new Vector3(ExplosionParticles.Instance.GetHit.transform.position.x, items[i].transform.position.y, 
                                                      ExplosionParticles.Instance.GetHit.transform.position.z), Quaternion.identity);
                }

                //items[i].transform.parent = Float_Parent.transform;
            }
        }

        return Rate;
    }
}

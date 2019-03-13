using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticles : MonoBehaviour 
{
    public static ExplosionParticles Instance;

    public GameObject[] Particles;

    private RaycastHit Hit;

    [SerializeField]
    private float RayDistance;

    [SerializeField]
    private LayerMask _layerMask;

    void Awake()
    {
        Instance = this;

        ExpParticles();
    }

    public float GetRay
    {
        get
        {
            return RayDistance;
        }
        set
        {
            RayDistance = value;
        }
    }

    public RaycastHit GetHit
    {
        get
        {
            return Hit;
        }
        set
        {
            Hit = value;
        }
    }

    void ExpParticles()
    {
        for (int i = 0; i < Particles.Length; i++)
        {
            Ray CastingRay = new Ray(Particles[i].transform.position, Particles[i].transform.forward);

            if (Physics.Raycast(CastingRay, out Hit, RayDistance, _layerMask))
            {
                CheckHitColliders(i);
            }
        }
    }

    void CheckHitColliders(int i)
    {
        if (Hit.collider.GetComponent<Blocker>())
        {
            if(!Character.Instance.GetExploPass)
            Particles[i].gameObject.SetActive(false);
        }
        if (Hit.collider.GetComponent<Destructable>())
        {
            if(GameManager.Instance.GetObjective == Objectives.ScoreAmt)
            {
                ItemManager.Instance.DropRate(40f);
            }

            SoundManager.Instance.BlockExplosionSE();

            Destroy(Hit.collider.gameObject);
        }
        if (Hit.collider.GetComponent<Player>())
        {
            if(!Character.Instance.GetIndestructibleArmor)
            Hit.collider.gameObject.GetComponent<Player>().Die();
        }
        if(Hit.collider.GetComponent<Powerups>())
        {
            Destroy(Hit.collider.gameObject);
        }
    }
}

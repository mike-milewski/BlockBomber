using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour 
{
    public static ExplosionRadius Instance;

    public ParticleSystem[] _particles;

    public BoxCollider[] Colliders;

    void Awake()
    {
        Instance = this;

        GetParticles();
    }

    public void GetParticles()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            ParticleSystem.MainModule mm = _particles[i].main;

            mm.startLifetime = 1.0f + Character.Instance.GetExplosionRadius;

            ExplosionParticles.Instance.GetRay += Character.Instance.GetExplosionRadius;
        }
        for (int j = 0; j < Colliders.Length; j++)
        {
            Colliders[j].size += new Vector3(0, 0, 1.0f + Character.Instance.GetExplosionRadius);
        }
    }
}

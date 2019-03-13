using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour 
{
    public static Bomb Instance;

    [SerializeField]
    private float TimeTillExplode, RayDistance;

    private float TimeToAnimate;

    [SerializeField]
    private GameObject WickSparks;

    [SerializeField]
    private Animator Anim;

    private RaycastHit Hit;

    private bool GloveMove;

    void Awake()
    {
        Instance = this;

        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        TimeToAnimate = 1.0f;
    }

    public bool GetMove
    {
        get
        {
            return GloveMove;
        }
        set
        {
            GloveMove = value;
        }
    }

    void CheckRotation()
    {
        if(this.transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            Ray CastingRayU = new Ray(this.transform.position, Vector3.forward);
            if (Physics.Raycast(CastingRayU, out Hit, RayDistance))
            {
                if (Hit.collider.GetComponent<Destructable>() || Hit.collider.GetComponent<Blocker>()
                    || Hit.collider.GetComponent<Bomb>())
                {
                    GetMove = false;
                }
            }
        }
        if (this.transform.rotation == Quaternion.Euler(0, 180, 0) || this.transform.rotation == Quaternion.Euler(0, -180, 0))
        {
            Ray CastingRayD = new Ray(this.transform.position, -Vector3.forward);
            if (Physics.Raycast(CastingRayD, out Hit, RayDistance))
            {
                if (Hit.collider.GetComponent<Destructable>() || Hit.collider.GetComponent<Blocker>()
                    || Hit.collider.GetComponent<Bomb>())
                {
                    GetMove = false;
                }
            }
        }
        if (this.transform.rotation == Quaternion.Euler(0, 90, 0))
        {
            Ray CastingRayR = new Ray(this.transform.position, Vector3.right);
            if (Physics.Raycast(CastingRayR, out Hit, RayDistance))
            {
                if (Hit.collider.GetComponent<Destructable>() || Hit.collider.GetComponent<Blocker>()
                    || Hit.collider.GetComponent<Bomb>())
                {
                    GetMove = false;
                }
            }
        }
        if (this.transform.rotation == Quaternion.Euler(0, -90, 0))
        {
            Ray CastingRayL = new Ray(this.transform.position, -Vector3.right);
            if (Physics.Raycast(CastingRayL, out Hit, RayDistance))
            {
                if (Hit.collider.GetComponent<Destructable>() || Hit.collider.GetComponent<Blocker>()
                    || Hit.collider.GetComponent<Bomb>())
                {
                    GetMove = false;
                }
            }
        }
    }

    void CheckForHoles()
    {
        Ray CastingRay = new Ray(this.transform.position, Vector3.down);
        if(Physics.Raycast(CastingRay, out Hit, 0.3f))
        {
            if(Hit.collider.GetComponent<TileColliderTrigger>())
            {
                if (Character.Instance.GetBombCount < Character.Instance.GetMaxBombCount)
                {
                    Character.Instance.GetBombCount = Character.Instance.GetMaxBombCount;
                }

                InformationTracker.Instance.UpdateBombs();

                Destroy(this.gameObject);
            }
        }
        else
        {
            this.transform.Translate(Vector3.down * 5.5f * Time.deltaTime);
        }
    }

    void Update()
    {
        //CheckForHoles();

        CheckRotation();

        TimeToAnimate -= Time.deltaTime;
        if(TimeToAnimate <= 0)
        {
            Anim.SetBool("SetExplosion", true);

            WickSparks.SetActive(true);

            TimeTillExplode -= Time.deltaTime;
            if(TimeTillExplode <= 0)
            {
                Explode();
            }
        }

        if(GloveMove)
        {
            MoveBomb();
        }
    }

    void MoveBomb()
    {
        this.transform.Translate(Vector3.forward * 7f * Time.deltaTime);
    }

    void Explode()
    {
        Destroy(Instantiate(GameManager.Instance.GetExplosionPrefab(), new Vector3(transform.position.x,
                                                                          GameManager.Instance.GetExplosionPrefab().transform.position.y, 
                                                                          transform.position.z), Quaternion.identity), ExplosionLifeTime.Instance.GetLifeTime);

        if(Character.Instance.GetBombCount < Character.Instance.GetMaxBombCount)
        {
            Character.Instance.GetBombCount = Character.Instance.GetMaxBombCount;
        }

        InformationTracker.Instance.UpdateBombs();

        GetMove = false;

        Destroy(gameObject);
    }
}

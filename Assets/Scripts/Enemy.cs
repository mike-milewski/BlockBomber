using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacterInterface
{
    private RaycastHit Hit;

    private Vector3 TargetPos;

    [SerializeField]
    private float Speed, RayDistance;

    [SerializeField]
    private bool CanDropBomb;

    [SerializeField]
    private bool BlockerInWay, Moving;

    [SerializeField]
    private LayerMask _LayerMask;

    [SerializeField]
    private float[] Rotations;

    [SerializeField]
    private float CoolDown;

    public Direction Dire;

    private bool CanMove
    {
        get
        {
            return !BlockerInWay && !Moving;
        }
    }

    void Awake()
    {
        Moving = false;
        BlockerInWay = false;
        CanDropBomb = true;

        CoolDown = 1.0f;

        CheckRotationOrientation();
    }

    void Update()
    {
        CoolDown -= Time.deltaTime;

        /*
        Ray CastingRayForward = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(CastingRayForward, out Hit, 1, _LayerMask))
        {
            DropBomb();
        }
         */

        Move();
    }

    public void CheckRotationOrientation()
    {
        if (this.transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            Dire = Direction.Up;
        }
        else if (this.transform.rotation == Quaternion.Euler(0, 180, 0))
        {
            Dire = Direction.Down;
        }
        else if (this.transform.rotation == Quaternion.Euler(0, 90, 0))
        {
            Dire = Direction.Right;
        }
        else if (this.transform.rotation == Quaternion.Euler(0, -90, 0))
        {
            Dire = Direction.Left;
        }
    }

    void ChangeRotation()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, Rotations.Length), 0);
    }

    public void CheckForHoles()
    {
        Ray CastingRayDown = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(CastingRayDown, out Hit, 2.5f))
        {
            if (Hit.collider != null)
            {

            }
        }
        else
        {
            //Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Move()
    {
        switch (Dire)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Ray CastingRayU = new Ray(transform.position, Vector3.forward);
                Debug.DrawRay(transform.position, Vector3.forward);
                if (Physics.Raycast(CastingRayU, out Hit, RayDistance))
                {
                    //Debug.Log("Blocker!");
                    BlockerInWay = Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                       || Hit.collider.GetComponent<Player>();

                    Moving = false;
                }
                else
                {
                    BlockerInWay = false;

                    if (Moving)
                    {
                        if (Vector3.Distance(this.transform.position, TargetPos) <= 0.01f)
                        {
                            Moving = false;

                            transform.position = TargetPos;

                            CheckForHoles();
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime);
                            CheckForHoles();
                        }
                    }
                }
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                Ray CastingRayD = new Ray(transform.position, -Vector3.forward);
                Debug.DrawRay(transform.position, -Vector3.forward);
                if (Physics.Raycast(CastingRayD, out Hit, RayDistance))
                {
                    //Debug.Log("Blocker!");
                    BlockerInWay = Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                       || Hit.collider.GetComponent<Player>();

                    Moving = false;
                }
                else
                {
                    BlockerInWay = false;

                    if (Moving)
                    {
                        if (Vector3.Distance(this.transform.position, TargetPos) <= 0.01f)
                        {
                            Moving = false;

                            transform.position = TargetPos;

                            CheckForHoles();
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime);
                            CheckForHoles();
                        }
                    }
                }
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                Ray CastingRayR = new Ray(transform.position, Vector3.right);
                Debug.DrawRay(transform.position, Vector3.right);
                if (Physics.Raycast(CastingRayR, out Hit, RayDistance))
                {
                    Debug.Log("Blocker!");
                    BlockerInWay = Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                       || Hit.collider.GetComponent<Player>();

                    Moving = false;
                }
                else
                {
                    BlockerInWay = false;

                    if (Moving)
                    {
                        if (Vector3.Distance(this.transform.position, TargetPos) <= 0.01f)
                        {
                            Moving = false;

                            transform.position = TargetPos;

                            CheckForHoles();
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime);
                            CheckForHoles();
                        }
                    }
                }
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                Ray CastingRayL = new Ray(transform.position, Vector3.left);
                Debug.DrawRay(transform.position, -Vector3.right);
                if (Physics.Raycast(CastingRayL, out Hit, RayDistance))
                {
                    Debug.Log("Blocker!");
                    BlockerInWay = Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                        || Hit.collider.GetComponent<Player>();

                    Moving = false;
                }
                else
                {
                    BlockerInWay = false;

                    if (Moving)
                    {
                        if (Vector3.Distance(this.transform.position, TargetPos) <= 0.01f)
                        {
                            Moving = false;

                            transform.position = TargetPos;

                            CheckForHoles();
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime);
                            CheckForHoles();
                        }
                    }
                }
                break;
        }

        if (CanMove)
        {
            SetTargetPosition();
        }
        else
        {
            CheckForBlocker();
        }

        /*
        if (Moving)
        {
            if(Vector3.Distance(this.transform.position, TargetPos) <= 0.01f)
            {
                Moving = false;

                transform.position = TargetPos;

                CheckForHoles();
            }   
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime);
                CheckForHoles();
            }
        }
         */
    }

    private void ChooseRandomDirection()
    {
        Dire = (Direction)Random.Range(0, 4);
    }

    void CheckForBlocker()
    {
        if(CoolDown <= 0)
        {
            ChangeRotation();

            CheckRotationOrientation();

            CoolDown = 1.0f;
        }
    }

    void SetTargetPosition()
    {
        if (CoolDown <= 0)
        {
            ChooseRandomDirection();

            Moving = true;
            CoolDown = 1.0f;

            switch (Dire)
            {
                case Direction.Up:
                    TargetPos = transform.position + Vector3.forward * 2;
                    break;
                case Direction.Down:
                    TargetPos = transform.position + -Vector3.forward * 2;
                    break;
                case Direction.Left:
                    TargetPos = transform.position + -Vector3.right * 2;
                    break;
                case Direction.Right:
                    TargetPos = transform.position + Vector3.right * 2;
                    break;
            }
        } 
    }

    public void DropBomb()
    {
        Vector3 PlayerPos = this.transform.position;
        Vector3 PlayerDirection = this.transform.forward;

        Quaternion PlayerRotation = this.transform.rotation;

        Vector3 SpawnPos = new Vector3(PlayerPos.x, GameManager.Instance.GetBombPrefab().transform.position.y, PlayerPos.z) + PlayerDirection * 2;

        if (this.CanDropBomb)
        {
            switch (Dire)
            {
                case Direction.Up:
                    DropBombUp(ref PlayerRotation, ref SpawnPos);
                    break;
                case Direction.Down:
                    DropBombDown(ref PlayerRotation, ref SpawnPos);
                    break;
                case Direction.Right:
                    DropDombRight(ref PlayerRotation, ref SpawnPos);
                    break;
                case Direction.Left:
                    DropBombLeft(ref PlayerRotation, ref SpawnPos);
                    break;
            }
        }
    }

    private void DropBombLeft(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayL = new Ray(this.transform.position, Vector3.left);
        if (Physics.Raycast(CastingRayL, out Hit, RayDistance))
        {
            if (Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                || Hit.collider.GetComponent<Player>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), new Vector3(this.transform.position.x,
                                                                         GameManager.Instance.GetBombPrefab().transform.position.y,
                                                                         this.transform.position.z), Quaternion.identity);

                this.CanDropBomb = false;
            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            this.CanDropBomb = false;
        }
    }

    private void DropDombRight(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayR = new Ray(this.transform.position, Vector3.right);
        if (Physics.Raycast(CastingRayR, out Hit, RayDistance))
        {
            if (Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                || Hit.collider.GetComponent<Player>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), new Vector3(this.transform.position.x,
                                                                         GameManager.Instance.GetBombPrefab().transform.position.y,
                                                                         this.transform.position.z), Quaternion.identity);

                this.CanDropBomb = false;
            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            this.CanDropBomb = false;
        }
    }

    private void DropBombDown(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayD = new Ray(this.transform.position, -Vector3.forward);
        if (Physics.Raycast(CastingRayD, out Hit, RayDistance))
        {
            if (Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                || Hit.collider.GetComponent<Player>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), new Vector3(this.transform.position.x,
                                                                         GameManager.Instance.GetBombPrefab().transform.position.y,
                                                                         this.transform.position.z), Quaternion.identity);

                this.CanDropBomb = false;
            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            this.CanDropBomb = false;
        }
    }

    private void DropBombUp(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayU = new Ray(this.transform.position, Vector3.forward);
        if (Physics.Raycast(CastingRayU, out Hit, RayDistance))
        {
            if (Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                || Hit.collider.GetComponent<Player>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), new Vector3(this.transform.position.x,
                                                                         GameManager.Instance.GetBombPrefab().transform.position.y,
                                                                         this.transform.position.z), Quaternion.identity);

                this.CanDropBomb = false;
            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            this.CanDropBomb = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction { Up, Right, Down, Left }

public class Character : MonoBehaviour, ICharacterInterface
{
    public static Character Instance;

    private Vector3 Pos;

    private RaycastHit hit;

    public Direction Dire;

    [SerializeField]
    private float Speed, MaxSpeed, RayDistance;

    [SerializeField]
    private int Lives, MaxLives, BombCount, MaxBombCount, ExploRadius, MaxRadius;

    private int CoolDown;

    private bool CanMove, Moving, BlockerInWay;

    [SerializeField]
    private bool CarryingBomb, IsIndestructible, ExploPass, BlockPass, BombPass, BoxGlove, CanDropBmb;

    public int GetLives
    {
        get
        {
            return Lives;
        }
        set
        {
            Lives = value;
        }
    }

    public int GetExplosionRadius
    {
        get
        {
            return ExploRadius;
        }
        set
        {
            ExploRadius = value;
        }
    }

    public int GetMaxRadius
    {
        get
        {
            return MaxRadius;
        }
        set
        {
            MaxRadius = value;
        }
    }

    public int GetMaxLives
    {
        get
        {
            return MaxLives;
        }
        set
        {
            MaxLives = value;
        }
    }

    public int GetBombCount
    {
        get
        {
            return BombCount;
        }
        set
        {
            BombCount = value;
        }
    }

    public int GetMaxBombCount
    {
        get
        {
            return MaxBombCount;
        }
        set
        {
            MaxBombCount = value;
        }
    }

    private float RayDis
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

    public float GetSpeed
    {
        get
        {
            return Speed;
        }
        set
        {
            Speed = value;
        }
    }

    public float GetMaxSpeed
    {
        get
        {
            return MaxSpeed;
        }
        set
        {
            MaxSpeed = value;
        }
    }

    public int GetCoolDown
    {
        get
        {
            return CoolDown;
        }
        set
        {
            CoolDown = value;
        }
    }

    private bool IsBlockerInWay
    {
        get
        {
            return BlockerInWay;
        }
        set
        {
            BlockerInWay = value;
        }
    }

    public bool GetCarryingBomb
    {
        get
        {
            return CarryingBomb;
        }
        set
        {
            CarryingBomb = value;
        }
    }

    public bool GetExploPass
    {
        get
        {
            return ExploPass;
        }
        set
        {
            ExploPass = value;
        }
    }

    public bool GetBlockPass
    {
        get
        {
            return BlockPass;
        }
        set
        {
            BlockPass = value;
        }
    }

    public bool GetBombPass
    {
        get
        {
            return BombPass;
        }
        set
        {
            BombPass = value;
        }
    }

    public bool GetBoxingGlove
    {
        get
        {
            return BoxGlove;
        }
        set
        {
            BoxGlove = value;
        }
    }

    public bool GetIndestructibleArmor
    {
        get
        {
            return IsIndestructible;
        }
        set
        {
            IsIndestructible = value;
        }
    }

    public bool GetCanMove
    {
        get
        {
            return CanMove;
        }
        set
        {
            CanMove = value;
        }
    }

    public bool GetMoving
    {
        get
        {
            return Moving;
        }
        set
        {
            Moving = value;
        }
    }
    public bool GetCanDropBmb
    {
        get
        {
            return CanDropBmb;
        }
        set
        {
            CanDropBmb = value;
        }
    }

    void Awake()
    {
        Instance = this;

        CanMove = true;
        Moving = false;
        BlockerInWay = false;

        CheckRotationOrientation();
    }

    public void CheckRotationOrientation()
    {
        if(this.transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            Dire = Direction.Up;
        }
        if (this.transform.rotation == Quaternion.Euler(0, 180, 0) || this.transform.rotation == Quaternion.Euler(0, -180, 0))
        {
            Dire = Direction.Down;
        }
        if (this.transform.rotation == Quaternion.Euler(0, 90, 0))
        {
            Dire = Direction.Right;
        }
        if (this.transform.rotation == Quaternion.Euler(0, -90, 0))
        {
            Dire = Direction.Left;
        }
    }

    public void CheckForHoles()
    {
        if(Dire == Direction.Up)
        {
            Ray CastingRayDown = new Ray(transform.position, new Vector3(0, -1, 1));
            if (Physics.Raycast(CastingRayDown, out hit, 3.5f))
            {
                if (hit.collider != null)
                {
                }
            }
            else
            {
                BlockerInWay = true;
            }
        }
        if (Dire == Direction.Down)
        {
            Ray CastingRayDown = new Ray(transform.position, new Vector3(0, -1, -1));
            if (Physics.Raycast(CastingRayDown, out hit, 3.5f))
            {
                if (hit.collider != null)
                {
                }
            }
            else
            {
                BlockerInWay = true;
            }
        }
        if (Dire == Direction.Right)
        {
            Ray CastingRayDown = new Ray(transform.position, new Vector3(1, -1, 0));
            if (Physics.Raycast(CastingRayDown, out hit, 3.5f))
            {
                if (hit.collider != null)
                {
                }
            }
            else
            {
                BlockerInWay = true;
            }
        }
        if (Dire == Direction.Left)
        {
            Ray CastingRayDown = new Ray(transform.position, new Vector3(-1, -1, 0));
            if (Physics.Raycast(CastingRayDown, out hit, 3.5f))
            {
                if (hit.collider != null)
                {
                }
            }
            else
            {
                BlockerInWay = true;
            }
        }
    }

    public void Die()
    {
        SoundManager.Instance.DieSE();

        GetIndestructibleArmor = false;
        GetExploPass = false;
        GetBombPass = false;
        GetBlockPass = false;
        GetBoxingGlove = false;
        GetCanDropBmb = true;

        Lives--;

        MeshRenderer[] Renderer = this.GetComponentsInChildren<MeshRenderer>();

        foreach (var r in Renderer)
        {
            r.enabled = false;
        }

        Collider Coll = GetComponent<Collider>();

        Coll.enabled = false;

        Character.Instance.GetCanMove = true;
        Character.Instance.GetMoving = false;

        InformationTracker.Instance.UpdateLives();

        if (Lives > 0)
        {
            GameManager.Instance.StartCoroutine("RespawnPlayer");
        }
        if (Lives <= 0)
        {
            GameManager.Instance.GameOver();
        }

        CarryingBomb = false;

        Player.Instance.enabled = false;
    }

    public void Move()
    {
        CoolDown--;

        switch(Dire)
        {
            case Direction.Up:
                Ray CastingRayU = new Ray(this.transform.position, Vector3.forward);
                CastingRayU = RayCastUp(CastingRayU);
                CheckForHoles();
                break;
            case Direction.Down:
                Ray CastingRayD = new Ray(this.transform.position, -Vector3.forward);
                CastingRayD = RayCastDown(CastingRayD);
                CheckForHoles();
                break;
            case Direction.Right:
                Ray CastingRayR = new Ray(this.transform.position, Vector3.right);
                CastingRayR = RayCastRight(CastingRayR);
                CheckForHoles();
                break;
            case Direction.Left:
                Ray CastingRayL = new Ray(this.transform.position, Vector3.left);
                CastingRayL = RayCastLeft(CastingRayL);
                CheckForHoles();
                break;
        }
    }

    public Ray RayCastLeft(Ray CastingRayL)
    {
        if (Physics.Raycast(CastingRayL, out hit, RayDistance))
        {
            if (hit.collider.GetComponent<Blocker>() || hit.collider.GetComponent<Enemy>())
            {
                BlockerInWay = true;
            }
            if (hit.collider.GetComponent<Bomb>())
            {
                if(!GetBombPass)
                {
                    BlockerInWay = true;
                }
                else if(GetBombPass)
                {
                    BlockerInWay = false;
                }

                if(BoxGlove)
                {
                    Bomb.Instance.transform.rotation = this.transform.rotation;

                    Bomb.Instance.GetMove = true;
                }
            }
            if(hit.collider.GetComponent<Destructable>())
            {
                if(!GetBlockPass)
                {
                    BlockerInWay = true;
                }
                else if(GetBlockPass)
                {
                    BlockerInWay = false;
                }
            }
        }
        else
        {
            BlockerInWay = false;
        }
        if (CanMove)
        {
            Pos = this.transform.position;
            MovePlayer();
        }
        if (Moving)
        {
            if (this.transform.position == Pos)
            {
                Moving = false;
                CanMove = true;

                MovePlayer();
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, Pos, Time.deltaTime * Speed);
        }
        return CastingRayL;
    }

    public Ray RayCastRight(Ray CastingRayR)
    {
        if (Physics.Raycast(CastingRayR, out hit, RayDistance))
        {
            if (hit.collider.GetComponent<Blocker>() || hit.collider.GetComponent<Enemy>())
            {
                BlockerInWay = true;
            }
            if (hit.collider.GetComponent<Bomb>())
            {
                if (!GetBombPass)
                {
                    BlockerInWay = true;
                }
                else if (GetBombPass)
                {
                    BlockerInWay = false;
                }

                if (BoxGlove)
                {
                    Bomb.Instance.transform.rotation = this.transform.rotation;

                    Bomb.Instance.GetMove = true;
                }
            }
            if (hit.collider.GetComponent<Destructable>())
            {
                if (!GetBlockPass)
                {
                    BlockerInWay = true;
                }
                else if (GetBlockPass)
                {
                    BlockerInWay = false;
                }
            }
        }
        else
        {
            BlockerInWay = false;
        }
        if (CanMove)
        {
            Pos = this.transform.position;
            MovePlayer();
        }
        if (Moving)
        {
            if (this.transform.position == Pos)
            {
                Moving = false;
                CanMove = true;

                MovePlayer();
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, Pos, Time.deltaTime * Speed); ;
        }
        return CastingRayR;
    }

    public Ray RayCastDown(Ray CastingRayD)
    {
        if (Physics.Raycast(CastingRayD, out hit, RayDistance))
        {
            if (hit.collider.GetComponent<Blocker>() || hit.collider.GetComponent<Enemy>())
            {
                BlockerInWay = true;
            }
            if (hit.collider.GetComponent<Bomb>())
            {
                if (!GetBombPass)
                {
                    BlockerInWay = true;
                }
                else if (GetBombPass)
                {
                    BlockerInWay = false;
                }

                if (BoxGlove)
                {
                    Bomb.Instance.transform.rotation = this.transform.rotation;

                    Bomb.Instance.GetMove = true;
                }
            }
            if (hit.collider.GetComponent<Destructable>())
            {
                if (!GetBlockPass)
                {
                    BlockerInWay = true;
                }
                else if (GetBlockPass)
                {
                    BlockerInWay = false;
                }
            }
        }
        else
        {
            BlockerInWay = false;
        }
        if (CanMove)
        {
            Pos = this.transform.position;
            MovePlayer();
        }
        if (Moving)
        {
            if (this.transform.position == Pos)
            {
                Moving = false;
                CanMove = true;

                MovePlayer();
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, Pos, Time.deltaTime * Speed); ;
        }
        return CastingRayD;
    }

    public Ray RayCastUp(Ray CastingRayU)
    {
        if (Physics.Raycast(CastingRayU, out hit, RayDistance))
        {
            if (hit.collider.GetComponent<Blocker>() || hit.collider.GetComponent<Enemy>())
            {
                BlockerInWay = true;
            }
            if(hit.collider.GetComponent<Bomb>())
            {
                if (!GetBombPass)
                {
                    BlockerInWay = true;
                }
                else if (GetBombPass)
                {
                    BlockerInWay = false;
                }

                if (BoxGlove)
                {
                    Bomb.Instance.transform.rotation = this.transform.rotation;

                    Bomb.Instance.GetMove = true;
                }
            }
            if (hit.collider.GetComponent<Destructable>())
            {
                if (!GetBlockPass)
                {
                    BlockerInWay = true;
                }
                else if (GetBlockPass)
                {
                    BlockerInWay = false;
                }
            }
        }
        else
        {
            BlockerInWay = false;
        }
        if (CanMove)
        {
            Pos = transform.position;
            MovePlayer();
        }
        if (Moving)
        {
            if (transform.position == Pos)
            {
                Moving = false;
                CanMove = true;

                MovePlayer();
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, Pos, Time.deltaTime * Speed); ;
        }
        return CastingRayU;
    }

    void MovePlayer()
    {
        #region KeyboardInput
        if (CoolDown <= 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                CheckForHoles();
                if (Dire != Direction.Up)
                {
                    CoolDown = 5;
                    Dire = Direction.Up;
                }
                else if (!BlockerInWay)
                {
                    CanMove = false;
                    Moving = true;
                    Pos += Vector3.forward * 2;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                CheckForHoles();
                if (Dire != Direction.Down)
                {
                    CoolDown = 5;
                    Dire = Direction.Down;
                }
                else if (!BlockerInWay)
                {
                    CanMove = false;
                    Moving = true;
                    Pos -= Vector3.forward * 2;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                CheckForHoles();
                if (Dire != Direction.Right)
                {
                    CoolDown = 5;
                    Dire = Direction.Right;
                }
                else if (!BlockerInWay)
                {
                    CanMove = false;
                    Moving = true;
                    Pos += Vector3.right * 2;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                CheckForHoles();
                if (Dire != Direction.Left)
                {
                    CoolDown = 5;
                    Dire = Direction.Left;
                }
                else if (!BlockerInWay)
                {
                    CanMove = false;
                    Moving = true;
                    Pos -= Vector3.right * 2;
                }
            }
        }
        #endregion

        #region GvrInput
        if(CoolDown <= 0)
        {
            if(GvrControllerInput.ClickButton)
            {
                Vector2 pos = GvrControllerInput.TouchPos;

                // UP
                if (pos.y < .5f && pos.x < .75f && pos.x > .25f) // UP
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    CheckForHoles();
                    if (Dire != Direction.Up)
                    {
                        CoolDown = 5;
                        Dire = Direction.Up;
                    }
                    else if (!BlockerInWay)
                    {
                        CanMove = false;
                        Moving = true;
                        Pos += Vector3.forward * 2;
                    }
                }
                else if (pos.y > .5f && pos.x < .75f && pos.x > .25f) // DOWN
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    CheckForHoles();
                    if (Dire != Direction.Down)
                    {
                        CoolDown = 5;
                        Dire = Direction.Down;
                    }
                    else if (!BlockerInWay)
                    {
                        CanMove = false;
                        Moving = true;
                        Pos -= Vector3.forward * 2;
                    }
                }
                else if (pos.x < .5f && pos.y < .75f && pos.y > .25f) // LEFT
                {
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    CheckForHoles();
                    if (Dire != Direction.Left)
                    {
                        CoolDown = 5;
                        Dire = Direction.Left;
                    }
                    else if (!BlockerInWay)
                    {
                        CanMove = false;
                        Moving = true;
                        Pos -= Vector3.right * 2;
                    }
                }
                else if (pos.x > .5f && pos.y < .75f && pos.y > .25f) // RIGHT
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    CheckForHoles();
                    if (Dire != Direction.Right)
                    {
                        CoolDown = 5;
                        Dire = Direction.Right;
                    }
                    else if (!BlockerInWay)
                    {
                        CanMove = false;
                        Moving = true;
                        Pos += Vector3.right * 2;
                    }
                }
            }
        }
        #endregion
    }

    public void PickUpBomb()
    {
        #region Input
        if (Dire == Direction.Up)
        {
            Ray CastingRayDown = new Ray(transform.position, Vector3.forward);
            if (Physics.Raycast(CastingRayDown, out hit, RayDis))
            {
                if (hit.collider.GetComponent<Bomb>())
                {
                    CarryingBomb = true;

                    hit.transform.SetParent(this.transform, true);

                    hit.collider.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
                }
            }
        }
        if (Dire == Direction.Down)
        {
            Ray CastingRayDown = new Ray(transform.position, -Vector3.forward);
            if (Physics.Raycast(CastingRayDown, out hit, RayDis))
            {
                if (hit.collider.GetComponent<Bomb>())
                {
                    CarryingBomb = true;

                    hit.transform.SetParent(this.transform, true);

                    hit.collider.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
                }
            }
        }
        if (Dire == Direction.Right)
        {
            Ray CastingRayDown = new Ray(transform.position, Vector3.right);
            if (Physics.Raycast(CastingRayDown, out hit, RayDis))
            {
                if (hit.collider.GetComponent<Bomb>())
                {
                    CarryingBomb = true;

                    hit.transform.SetParent(this.transform, true);

                    hit.collider.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
                }
            }
        }
        if (Dire == Direction.Left)
        {
            Ray CastingRayDown = new Ray(transform.position, -Vector3.right);
            if (Physics.Raycast(CastingRayDown, out hit, RayDis))
            {
                if (hit.collider.GetComponent<Bomb>())
                {
                    CarryingBomb = true;

                    hit.transform.SetParent(this.transform, true);

                    hit.collider.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
                }
            }
        }
        #endregion
    }

    public void ReleaseBomb()
    {
        Ray CastingRayUp = new Ray(transform.position, Vector3.up);
        if (Physics.Raycast(CastingRayUp, out hit, RayDis))
        {
            if (hit.collider.GetComponent<Bomb>())
            {
                CarryingBomb = false;

                hit.transform.parent = null;

                Vector3 PlayerPos = this.transform.position;
                Vector3 PlayerDirection = this.transform.forward;

                //hit.transform.GetComponent<Rigidbody>().AddForce(PlayerDirection * 6.0f);

                hit.transform.position = new Vector3(PlayerPos.x, hit.transform.position.y, PlayerPos.z) + PlayerDirection * 6;
            }
        }
    }

    public void DropBomb()
    {
        if(!CarryingBomb)
        {
            if (this.BombCount > 0)
            {
                Vector3 PlayerPos = this.transform.position;
                //Vector3 PlayerDirection = this.transform.forward;

                Quaternion PlayerRotation = this.transform.rotation;

                Vector3 SpawnPos = new Vector3(PlayerPos.x, GameManager.Instance.GetBombPrefab().transform.position.y, PlayerPos.z);

                switch (Dire)
                {
                    case Direction.Up:
                        DropBombUp(ref PlayerRotation, ref SpawnPos);
                        break;
                    case Direction.Down:
                        DropBombDown(ref PlayerRotation, ref SpawnPos);
                        break;
                    case Direction.Right:
                        DropBombRight(ref PlayerRotation, ref SpawnPos);
                        break;
                    case Direction.Left:
                        DropBombLeft(ref PlayerRotation, ref SpawnPos);
                        break;
                }

                InformationTracker.Instance.UpdateBombs();
            }
        } 
    }

    private void DropBombLeft(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayL = new Ray(this.transform.position, Vector3.left);
        if (Physics.Raycast(CastingRayL, out hit, RayDis))
        {
            if (hit.collider.GetComponent<Blocker>() || hit.collider.GetComponent<Destructable>()
                || hit.collider.GetComponent<Enemy>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

                this.BombCount--;

                InformationTracker.Instance.UpdateBombs();
            }
            if (hit.collider.GetComponent<Bomb>())
            {

            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            this.BombCount--;

            InformationTracker.Instance.UpdateBombs();
        }
    }

    private void DropBombRight(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayR = new Ray(this.transform.position, Vector3.right);
        if (Physics.Raycast(CastingRayR, out hit, RayDis))
        {
            if (hit.collider.GetComponent<Blocker>() || hit.collider.GetComponent<Destructable>()
                || hit.collider.GetComponent<Enemy>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

                this.BombCount--;

                InformationTracker.Instance.UpdateBombs();
            }
            if (hit.collider.GetComponent<Bomb>())
            {

            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            this.BombCount--;

            InformationTracker.Instance.UpdateBombs();
        }
    }

    private void DropBombDown(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayD = new Ray(this.transform.position, -Vector3.forward);
        if (Physics.Raycast(CastingRayD, out hit, RayDis))
        {
            if (hit.collider.GetComponent<Blocker>() || hit.collider.GetComponent<Destructable>()
                || hit.collider.GetComponent<Enemy>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

                this.BombCount--;

                InformationTracker.Instance.UpdateBombs();
            }
            if (hit.collider.GetComponent<Bomb>())
            {

            }
        }
        else
        {

            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            this.BombCount--;

            InformationTracker.Instance.UpdateBombs();
        }
    }

    private void DropBombUp(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayU = new Ray(this.transform.position, Vector3.forward);
        if (Physics.Raycast(CastingRayU, out hit, RayDis))
        {
            if (hit.collider.GetComponent<Blocker>() || hit.collider.GetComponent<Destructable>()
                || hit.collider.GetComponent<Enemy>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

                this.BombCount--;

                InformationTracker.Instance.UpdateBombs();
            }
            if(hit.collider.GetComponent<Bomb>())
            {

            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            this.BombCount--;

            InformationTracker.Instance.UpdateBombs();
        }
    }
}
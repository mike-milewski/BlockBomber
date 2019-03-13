using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Directional { Up, Down, Left, Right };

public class VrControllerMovement : MonoBehaviour
{
    public static VrControllerMovement Instance;

    public GameObject Player;

    private RaycastHit Hit;

    private Directional Direct;

    public int Dindex;

    [SerializeField]
    private bool CanDropBomb;

    private float RayDis;

    public bool GetCanDropBomb
    {
        get
        {
            return CanDropBomb;
        }
        set
        {
            CanDropBomb = value;
        }
    }

    void Awake()
    {
        Instance = this;

        RayDis = 2f;
    }

    public void CheckRotationOrientation()
    {
        if (this.transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            Direct = Directional.Up;
        }
        if (this.transform.rotation == Quaternion.Euler(0, 180, 0) || this.transform.rotation == Quaternion.Euler(0, -180, 0))
        {
            Direct = Directional.Down;
        }
        if (this.transform.rotation == Quaternion.Euler(0, 90, 0))
        {
            Direct = Directional.Right;
        }
        if (this.transform.rotation == Quaternion.Euler(0, -90, 0))
        {
            Direct = Directional.Left;
        }
    }

    void Update()
    {
        Vector3 Forward = Player.transform.TransformDirection(Vector3.forward) * 2.0f;

        Debug.DrawRay(Player.transform.position, Forward);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.DropBomb();
        }

        Bomb();
        Rotate();

        switch(Direct)
        {
            case Directional.Up:
                CastingUp();
                break;
            case Directional.Down:
                CastingDown();
                break;
            case Directional.Left:
                CastingLeft();
                break;
            case Directional.Right:
                CastingRight();
                break;
        }

        if (Input.GetKey(KeyCode.W))
        {
            //Player.transform.Rotate(0, 10 * Time.deltaTime, 0);

            Player.transform.rotation = Quaternion.Euler(0, 0, 0);

            Direct = Directional.Up;

            Debug.Log(Direct);
        }
        if(Input.GetKey(KeyCode.D))
        {
            //Player.transform.Rotate(0, 10 * Time.deltaTime, 0);

            Player.transform.rotation = Quaternion.Euler(0, 90, 0);

            Direct = Directional.Right;

            Debug.Log(Direct);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Player.transform.Rotate(0, 10 * Time.deltaTime, 0);

            Player.transform.rotation = Quaternion.Euler(0, -180, 0);

            Direct = Directional.Down;

            Debug.Log(Direct);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Player.transform.Rotate(0, 10 * Time.deltaTime, 0);

            Player.transform.rotation = Quaternion.Euler(0, -90, 0);

            Direct = Directional.Left;

            Debug.Log(Direct);
        }
    }

    void Bomb()
    {
        //If CENTER button is pressed.
        if(GvrControllerInput.ClickButtonDown)
        {
            if (GvrControllerInput.TouchPos.x > 0.3f && GvrControllerInput.TouchPos.x < 0.7f)
            {
                if (GvrControllerInput.TouchPos.x > 0.3f && GvrControllerInput.TouchPos.x < 0.3f)
                {
                    DropBomb();
                }
            }
        }
    }

    void Rotate()
    {
        //If BOTTOM button is pressed.
        if (GvrControllerInput.ClickButtonDown)
        {
            if (GvrControllerInput.TouchPos.x > 0.3f && GvrControllerInput.TouchPos.x < 0.7f)
            {
                if (GvrControllerInput.TouchPos.y > 0.7f)
                {
                    Player.transform.rotation *= Quaternion.Euler(0, 90, 0);
                }
            }
        }
    }

    private void CastingRight()
    {
        Ray CastingRayR = new Ray(Player.transform.position, Vector3.right);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Physics.Raycast(CastingRayR, out Hit, 2))
            {
                if (Hit.collider.GetComponent<Blocker>())
                {

                }
            }
            else
            {
                Player.transform.position += Player.transform.forward * 2.0f;
            }
        }

        if (GvrControllerInput.ClickButtonDown)
        {
            if (Physics.Raycast(CastingRayR, out Hit, 2))
            {
                if (Hit.collider.GetComponent<Blocker>())
                {

                }
            }
            else
            {
                if (GvrControllerInput.TouchPos.x > 0.3f && GvrControllerInput.TouchPos.x < 0.7f)
                {
                    if (GvrControllerInput.TouchPos.y < 0.3f)
                    {
                        Player.transform.position += Player.transform.forward * 2.0f;
                    }
                }
                /*
                if (GvrControllerInput.TouchPos.y > 0.3f && GvrControllerInput.TouchPos.y < 0.7f)
                {
                    if (GvrControllerInput.TouchPos.x < 0.3f)
                    {
                        Player.transform.position += Player.transform.forward * 2.0f;
                    }
                    if (GvrControllerInput.TouchPos.x > 0.7f)
                    {
                        Player.transform.position -= Player.transform.forward * 2.0f;
                    }
                }
                 */
            }
        }
    }

    private void CastingLeft()
    {
        Ray CastingRayL = new Ray(Player.transform.position, Vector3.left);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Physics.Raycast(CastingRayL, out Hit, 2))
            {
                if (Hit.collider.GetComponent<Blocker>())
                {

                }
            }
            else
            {
                Player.transform.position += Player.transform.forward * 2.0f;
            }
        }

        if (GvrControllerInput.ClickButtonDown)
        {
            if (Physics.Raycast(CastingRayL, out Hit, 2))
            {
                if (Hit.collider.GetComponent<Blocker>())
                {

                }
            }
            else
            {
                if (GvrControllerInput.TouchPos.x > 0.3f && GvrControllerInput.TouchPos.x < 0.7f)
                {
                    if (GvrControllerInput.TouchPos.y < 0.3f)
                    {
                        Player.transform.position += Player.transform.forward * 2.0f;
                    }
                }
                /*
                if (GvrControllerInput.TouchPos.y > 0.3f && GvrControllerInput.TouchPos.y < 0.7f)
                {
                    if (GvrControllerInput.TouchPos.x < 0.3f)
                    {
                        Player.transform.position += Player.transform.forward * 2.0f;
                    }
                    if (GvrControllerInput.TouchPos.x > 0.7f)
                    {
                        Player.transform.position -= Player.transform.forward * 2.0f;
                    }
                }
                 */
            }
        }
    }

    private void CastingDown()
    {
        Ray CastingRayD = new Ray(Player.transform.position, -Vector3.forward);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Physics.Raycast(CastingRayD, out Hit, 2))
            {
                if (Hit.collider.GetComponent<Blocker>())
                {

                }
            }
            else
            {
                Player.transform.position += Player.transform.forward * 2.0f;
            }
        }

        if (GvrControllerInput.ClickButtonDown)
        {
            if (Physics.Raycast(CastingRayD, out Hit, 2))
            {
                if (Hit.collider.GetComponent<Blocker>())
                {

                }
            }
            else
            {
                if (GvrControllerInput.TouchPos.x > 0.3f && GvrControllerInput.TouchPos.x < 0.7f)
                {
                    if (GvrControllerInput.TouchPos.y < 0.3f)
                    {
                        Player.transform.position += Player.transform.forward * 2.0f;
                    }
                }
                /*
                if (GvrControllerInput.TouchPos.y > 0.3f && GvrControllerInput.TouchPos.y < 0.7f)
                {
                    if (GvrControllerInput.TouchPos.x < 0.3f)
                    {
                        Player.transform.position += Player.transform.forward * 2.0f;
                    }
                    if (GvrControllerInput.TouchPos.x > 0.7f)
                    {
                        Player.transform.position -= Player.transform.forward * 2.0f;
                    }
                }
                 */
            }
        }
    }

    private void CastingUp()
    {
        Ray CastingRayU = new Ray(Player.transform.position, Vector3.forward);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Physics.Raycast(CastingRayU, out Hit, 2))
            {
                if (Hit.collider.GetComponent<Blocker>())
                {

                }
            }
            else
            {
                Player.transform.position += Player.transform.forward * 2.0f;
            }
        }

        if (GvrControllerInput.ClickButtonDown)
        {
            if (Physics.Raycast(CastingRayU, out Hit, 2))
            {
                if (Hit.collider.GetComponent<Blocker>())
                {

                }
            }
            else
            {
                if (GvrControllerInput.TouchPos.x > 0.3f && GvrControllerInput.TouchPos.x < 0.7f)
                {
                    if (GvrControllerInput.TouchPos.y < 0.3f)
                    {
                        Player.transform.position += Player.transform.forward * 2.0f;
                    }
                }
                /*
                if (GvrControllerInput.TouchPos.y > 0.3f && GvrControllerInput.TouchPos.y < 0.7f)
                {
                    if (GvrControllerInput.TouchPos.x < 0.3f)
                    {
                        Player.transform.position += Player.transform.forward * 2.0f;
                    }
                    if (GvrControllerInput.TouchPos.x > 0.7f)
                    {
                        Player.transform.position -= Player.transform.forward * 2.0f;
                    }
                }
                 */
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
            switch (Direct)
            {
                case Directional.Up:
                    DropBombUp(ref PlayerRotation, ref SpawnPos);
                    break;
                case Directional.Down:
                    DropBombDown(ref PlayerRotation, ref SpawnPos);
                    break;
                case Directional.Right:
                    DropBombRight(ref PlayerRotation, ref SpawnPos);
                    break;
                case Directional.Left:
                    DropBombLeft(ref PlayerRotation, ref SpawnPos);
                    break;
            }
        }
    }

    private void DropBombLeft(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayL = new Ray(this.transform.position, Vector3.left);
        if (Physics.Raycast(CastingRayL, out Hit, RayDis))
        {
            if (Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                || Hit.collider.GetComponent<Enemy>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), new Vector3(this.transform.position.x,
                                                                         GameManager.Instance.GetBombPrefab().transform.position.y,
                                                                         this.transform.position.z), Quaternion.identity);

                //this.CanDropBomb = false;
            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            //this.CanDropBomb = false;
        }
    }

    private void DropBombRight(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayR = new Ray(this.transform.position, Vector3.right);
        if (Physics.Raycast(CastingRayR, out Hit, RayDis))
        {
            if (Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                || Hit.collider.GetComponent<Enemy>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), new Vector3(this.transform.position.x,
                                                                         GameManager.Instance.GetBombPrefab().transform.position.y,
                                                                         this.transform.position.z), Quaternion.identity);

                //this.CanDropBomb = false;
            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            //this.CanDropBomb = false;
        }
    }

    private void DropBombDown(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayD = new Ray(this.transform.position, -Vector3.forward);
        if (Physics.Raycast(CastingRayD, out Hit, RayDis))
        {
            if (Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                || Hit.collider.GetComponent<Enemy>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), new Vector3(this.transform.position.x,
                                                                         GameManager.Instance.GetBombPrefab().transform.position.y,
                                                                         this.transform.position.z), Quaternion.identity);

                //this.CanDropBomb = false;
            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            //this.CanDropBomb = false;
        }
    }

    private void DropBombUp(ref Quaternion PlayerRotation, ref Vector3 SpawnPos)
    {
        Ray CastingRayU = new Ray(this.transform.position, Vector3.forward);
        if (Physics.Raycast(CastingRayU, out Hit, RayDis))
        {
            if (Hit.collider.GetComponent<Blocker>() || Hit.collider.GetComponent<Destructable>()
                || Hit.collider.GetComponent<Enemy>())
            {
                Instantiate(GameManager.Instance.GetBombPrefab(), new Vector3(this.transform.position.x,
                                                                         GameManager.Instance.GetBombPrefab().transform.position.y,
                                                                         this.transform.position.z), Quaternion.identity);

                //this.CanDropBomb = false;
            }
        }
        else
        {
            Instantiate(GameManager.Instance.GetBombPrefab(), SpawnPos, PlayerRotation);

            //this.CanDropBomb = false;
        }
    }
}

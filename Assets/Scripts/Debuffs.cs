using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Debuff { SpeedDown, ExplosionDown, CantBomb, ReduceBomb, RandomDebuff};

public class Debuffs : MonoBehaviour 
{
    public static Debuffs Instance;

    [SerializeField]
    private Debuff debuff;

    void Awake()
    {
        Instance = this;
    }

    void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.GetComponent<Player>())
        {
            SufferDebuff();

            Destroy(gameObject);
        }
    }

    void SufferDebuff()
    {
        switch(debuff)
        {
            case Debuff.CantBomb:
                break;
            case Debuff.ExplosionDown:
                ExplosionDown();
                break;
            case Debuff.SpeedDown:
                SpeedDown();
                break;
            case Debuff.ReduceBomb:
                BombDown();
                break;
            case Debuff.RandomDebuff:
                RandomDebuff();
                break;
        }
    }

    void CantPlaceBomb()
    {
        Character.Instance.GetCanDropBmb = false;
        PowerUpManager.Instance.StartCoroutine("CanDropBombCount");
    }

    void SpeedDown()
    {
        if(Character.Instance.GetSpeed > 1)
        {
            Character.Instance.GetSpeed--;

            InformationTracker.Instance.UpdateSpeed();
        }
    }

    void ExplosionDown()
    {
        if(Character.Instance.GetExplosionRadius > 1)
        {
            Character.Instance.GetExplosionRadius--;

            InformationTracker.Instance.UpdateRadius();
        }
    }

    void BombDown()
    {
        if (Character.Instance.GetMaxBombCount > 1)
        {
            Character.Instance.GetMaxBombCount--;

            Character.Instance.GetBombCount = Character.Instance.GetMaxBombCount;

            InformationTracker.Instance.UpdateBombs();
        }
    }

    void RandomDebuff()
    {
        debuff = (Debuff)Random.Range(0, 3);
        SufferDebuff();
        Debug.Log(debuff);
    }
}

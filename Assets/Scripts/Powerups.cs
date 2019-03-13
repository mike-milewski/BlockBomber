using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BuffsAndDebuffs { ExtraBomb, ExplosionExpand, Bomberman, Accelerator, BombPass,
                       FireExtinguisher, Heart, MysteryItem, Time, SBomb, BlockPass,
                       MaxExplosion, IndestructibleArmor, Glove};

public class Powerups : MonoBehaviour 
{
    public static Powerups Instance;

    [SerializeField]
    private BuffsAndDebuffs _powers;

    [SerializeField]
    private float PercentToSpawn;

    void Awake()
    {
        Instance = this;
    }

    void OnTriggerEnter(Collider Coll)
    {
        if(Coll.gameObject.GetComponent<Player>())
        {
            GetPower();

            SoundManager.Instance.ReceivePowerupSE();

            Destroy(gameObject);
        }
    }

    public float GetPercent
    {
        get
        {
            return PercentToSpawn;
        }
        set
        {
            PercentToSpawn = value;
        }
    }

    void GetPower()
    {
        switch(_powers)
        {
            case BuffsAndDebuffs.ExtraBomb:
                Extrabomb();
                break;
            case BuffsAndDebuffs.ExplosionExpand:
                ExplosionExpansion();
                break;
            case BuffsAndDebuffs.MaxExplosion:
                MaxExplo();
                break;
            case BuffsAndDebuffs.Bomberman:
                BomberMan();
                break;
            case BuffsAndDebuffs.FireExtinguisher:
                Extinguisher();
                break;
            case BuffsAndDebuffs.Heart:
                heart();
                break;
            case BuffsAndDebuffs.MysteryItem:
                Mystery();
                break;
            case BuffsAndDebuffs.Time:
                Timer();
                break;
            case BuffsAndDebuffs.IndestructibleArmor:
                IndestructArmor();
                break;
            case BuffsAndDebuffs.Accelerator:
                SpeedUp();
                break;
            case BuffsAndDebuffs.SBomb:
                SuperBomb();
                break;
            case BuffsAndDebuffs.BlockPass:
                BlockPasser();
                break;
            case BuffsAndDebuffs.BombPass:
                BombPasser();
                break;
            case BuffsAndDebuffs.Glove:
                BoxingGlove();
                break;
        }
    }

    void Extrabomb()
    {
        Character.Instance.GetMaxBombCount++;

        Character.Instance.GetBombCount = Character.Instance.GetMaxBombCount;

        InformationTracker.Instance.UpdateBombs();
    }

    void ExplosionExpansion()
    {
        Character.Instance.GetExplosionRadius++;

        InformationTracker.Instance.UpdateRadius();

        if (Character.Instance.GetExplosionRadius >= Character.Instance.GetMaxRadius)
        {
            Character.Instance.GetExplosionRadius = Character.Instance.GetMaxRadius;

            InformationTracker.Instance.UpdateRadius();
        }
    }

    void MaxExplo()
    {
        Character.Instance.GetExplosionRadius = Character.Instance.GetMaxRadius;

        InformationTracker.Instance.UpdateRadius();
    }

    void IndestructArmor()
    {
        Character.Instance.GetIndestructibleArmor = true;
        PowerUpManager.Instance.StartCoroutine("IndestructibleArmorCount");
    }

    void heart()
    {
        if(Character.Instance.GetLives < Character.Instance.GetMaxLives)
        {
            Character.Instance.GetLives += 2;

            InformationTracker.Instance.UpdateLives();

            if (Character.Instance.GetLives > Character.Instance.GetMaxLives)
            {
                Character.Instance.GetLives = Character.Instance.GetMaxLives;

                InformationTracker.Instance.UpdateLives();
            }
        }        
    }

    void BomberMan()
    {
        if (Character.Instance.GetLives < Character.Instance.GetMaxLives)
        {
            Character.Instance.GetLives++;

            InformationTracker.Instance.UpdateLives();

            if (Character.Instance.GetLives > Character.Instance.GetMaxLives)
            {
                Character.Instance.GetLives = Character.Instance.GetMaxLives;

                InformationTracker.Instance.UpdateLives();
            }
        }        
    }

    void Extinguisher()
    {
        Character.Instance.GetLives = Character.Instance.GetMaxLives;

        InformationTracker.Instance.UpdateLives();

        if(GameManager.Instance.GetObjective == Objectives.ScoreAmt)
        {
            ScoreTracker.Instance.GetScore += 500;

            ScoreTracker.Instance.ScoreText.text = "Score: " + ScoreTracker.Instance.GetScore;
        }
    }

    void Mystery()
    {
        ScoreTracker.Instance.GetScore += 1000; // (GameManager.Instance.NumEnemies) * 1000;
    }

    void Timer()
    {
        StageTimer.Instance.GetStageTime = StageTimer.Instance.GetMaxStageTime;

        StageTimer.Instance.GetTimeText().text = StageTimer.Instance.GetStageTime.ToString();
    }

    void SpeedUp()
    {
        if (Character.Instance.GetSpeed != Character.Instance.GetMaxSpeed)
        {
            Character.Instance.GetSpeed++;

            InformationTracker.Instance.UpdateSpeed();
        }
    }

    void BombPasser()
    {
        Character.Instance.GetBombPass = true;
    }

    void BlockPasser()
    {
        Character.Instance.GetBlockPass = true;
    }

    void SuperBomb()
    {
        Character.Instance.GetExploPass = true;
    }

    void BoxingGlove()
    {
        Character.Instance.GetBoxingGlove = true;
    }
}

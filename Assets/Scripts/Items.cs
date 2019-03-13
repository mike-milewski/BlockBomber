using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour 
{
    public static Items Instance;

    [SerializeField]
    private float PercentSpawn;

    [SerializeField]
    private int ScoreAmount;

    public float GetPercentSpawn
    {
        get
        {
            return PercentSpawn;
        }
        set
        {
            PercentSpawn = value;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void OnTriggerEnter(Collider Coll)
    {
        if(Coll.gameObject.GetComponent<Player>())
        {
            SoundManager.Instance.PointsSE();

            ScoreTracker.Instance.GetScore += ScoreAmount;

            ScoreTracker.Instance.ScoreText.text = "Score: " + ScoreTracker.Instance.GetScore;

            if(GameManager.Instance.GetObjective == Objectives.ScoreAmt)
            {
                GameManager.Instance.CheckScoreInStage();
            }

            Destroy(gameObject);
        }
    }
}

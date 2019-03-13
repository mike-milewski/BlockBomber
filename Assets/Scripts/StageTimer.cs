using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour 
{
    public static StageTimer Instance;

    [SerializeField]
    private Text TimeText;

    [SerializeField]
    private float StageTime, MaxStageTime;

    [SerializeField]
    private bool StartTimer;

    public Text GetTimeText()
    {
        return TimeText;
    }

    public float GetStageTime
    {
        get
        {
            return StageTime;
        }
        set
        {
            StageTime = value;
        }
    }

    public float GetMaxStageTime
    {
        get
        {
            return MaxStageTime;
        }
        set
        {
            MaxStageTime = value;
        }
    }

    public bool GetStartTimer
    {
        get
        {
            return StartTimer;
        }
        set
        {
            StartTimer = value;
        }
    }

    void Awake()
    {
        Instance = this;

        TimeText.enabled = false;
    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if(GetStartTimer == true)
        {
            StageTime -= Time.deltaTime;
        }

        float minutes = Mathf.Floor(StageTime / 60);
        float seconds = Mathf.Floor(StageTime % 60);

        TimeText.text = " " + minutes.ToString("0") + ":" + seconds.ToString("00");

        if(StageTime <= 0)
        {
            StageTime = 0.0f;

            TimesUp(1);
        }
    }

    void TimesUp(int SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}

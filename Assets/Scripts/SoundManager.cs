using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour 
{
    public static SoundManager Instance = null;

    private AudioSource Source;

    [SerializeField]
    private AudioClip[] Clips;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Source = GetComponent<AudioSource>();
    }

    public void DieSE()
    {
        Source.clip = Clips[0];

        Source.Play();
    }

    public void GameOverSE()
    {
        Source.clip = Clips[1];

        Source.Play();
    }

    public void LevelWinSE()
    {
        Source.clip = Clips[2];

        Source.Play();
    }

    public void BlockExplosionSE()
    {
        Source.clip = Clips[3];

        Source.Play();
    }

    public void PointsSE()
    {
        Source.clip = Clips[4];

        Source.Play();
    }

    public void NavigationSE()
    {
        Source.clip = Clips[5];

        Source.Play();
    }

    public void SelectSE()
    {
        Source.clip = Clips[6];

        Source.Play();
    }

    public void ReceivePowerupSE()
    {
        Source.clip = Clips[7];

        Source.Play();
    }
}

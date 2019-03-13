using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationTracker : MonoBehaviour 
{
    public static InformationTracker Instance;

    [SerializeField]
    private Text[] InfoText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InfoText[0].text = "Bombs: " + Character.Instance.GetBombCount;
        InfoText[1].text = "Lives: " + Character.Instance.GetLives;
        InfoText[2].text = "Radius: " + Character.Instance.GetExplosionRadius;
        InfoText[3].text = "Speed: " + Character.Instance.GetSpeed;
    }

    public void UpdateBombs()
    {
        InfoText[0].text = "Bombs: " + Character.Instance.GetBombCount;
    }

    public void UpdateLives()
    {
        InfoText[1].text = "Lives: " + Character.Instance.GetLives;
    }

    public void UpdateRadius()
    {
        InfoText[2].text = "Radius: " + Character.Instance.GetExplosionRadius;
    }

    public void UpdateSpeed()
    {
        InfoText[3].text = "Speed: " + Character.Instance.GetSpeed;
    }

    public void ObjectiveText(string ObjText)
    {
        InfoText[4].text = ObjText;
    }
}

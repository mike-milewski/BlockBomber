using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour 
{
    void Start()
    {
        ResetVariables();
    }

    void ResetVariables()
    {
        GameManager.Instance.StageObjective();
    }
}

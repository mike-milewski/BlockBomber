using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManagers : MonoBehaviour 
{
    void Awake()
    {
        if(GameManager.Instance != null)
        {
            GameObject GM = GameObject.FindObjectOfType<GameManager>().gameObject;

            Destroy(GM);
        }
    }
}

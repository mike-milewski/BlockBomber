using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    void OnTriggerEnter(Collider Coll)
    {
        if(GameManager.Instance.GetObjective == Objectives.Keys)
        {
            if (Coll.gameObject.GetComponent<Player>())
            {
                GameManager.Instance.NumKeys--;

                GameManager.Instance.CheckKeysInStage();

                InformationTracker.Instance.ObjectiveText("-Keys- \n" + GameManager.Instance.NumKeys);

                Destroy(gameObject);
            }
        }   
    }
}

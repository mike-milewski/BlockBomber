using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour 
{
    [SerializeField]
    private float RunningTime;

    void Update()
    {
        RunningTime += Time.deltaTime;

        float y = Mathf.Sin(RunningTime);

        transform.position = new Vector2(transform.position.x, y);
    }
}

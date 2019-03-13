using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    void Update()
    {
        if (Time.timeScale == 1)
        {
            this.Move();

            //this.BombDrop();

            //this.GvrBomb();

            this.GrabAndReleaseBome();

            this.GVRgrabAndReleaseBome();
        }
        
    }

    //Keyboard.
    void GrabAndReleaseBome()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!this.GetCarryingBomb)
            {
                this.DropBomb();

                this.PickUpBomb();
            }
            else
            {
                this.ReleaseBomb();
            }
        }
    }

    //VR.
    void GVRgrabAndReleaseBome()
    {
        if (GvrControllerInput.AppButtonDown)
        {
            if (!this.GetCarryingBomb)
            {
                this.DropBomb();

                this.PickUpBomb();
            }
            else
            {
                this.ReleaseBomb();
            }
        }
    }

    void BombDrop()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !this.GetCarryingBomb && GetCanDropBmb)
        {
            this.DropBomb();
        }
    }

    //If in VR mode.
    void GvrBomb()
    {
        if(GvrControllerInput.AppButtonDown)
        {
            DropBomb();
        }
    }
}

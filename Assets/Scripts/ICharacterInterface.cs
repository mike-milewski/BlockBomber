using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterInterface
{
    void Move();

    void DropBomb();

    void CheckRotationOrientation();

    void CheckForHoles();

    void Die();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdjustDrums : MonoBehaviour
{
    public float movementAmount = 0.05f;

    public void MoveUp()
    {
        transform.Translate(0, movementAmount, 0);
    }

    public void MoveDown()
    {
        transform.Translate(0, -movementAmount, 0);
    }

    public void MoveLeft()
    {
        transform.Translate(-movementAmount, 0, 0);
    }

    public void MoveRight()
    {
        transform.Translate(movementAmount, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public Vector2 GetMovimentVectorNormalized()
    {
        Vector2 InputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            InputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            InputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            InputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            InputVector.x = +1;
        }
        InputVector = InputVector.normalized;

        return InputVector;
    }
}

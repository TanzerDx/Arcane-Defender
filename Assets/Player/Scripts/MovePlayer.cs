using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    void Update()
    {
        MovePlayerLogic(moveSpeed);
    }

    private void MovePlayerLogic(float moveSpeed)
    {
        float xValue = (Input.GetAxis("Horizontal")) * moveSpeed  * Time.deltaTime;
        float yValue = (Input.GetAxis("Vertical")) * moveSpeed  * Time.deltaTime;

        transform.Translate(xValue, yValue , 0, Space.World);
    }

}

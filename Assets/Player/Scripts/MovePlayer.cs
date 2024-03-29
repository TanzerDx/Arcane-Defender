using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private Animator PlayerAnim;

    [SerializeField] private SpriteRenderer sprite;

    void Update()
    {
        MovePlayerLogic(moveSpeed);
    }

    private void MovePlayerLogic(float moveSpeed)
    {
        float xValue = (Input.GetAxis("Horizontal")) * moveSpeed  * Time.deltaTime;
        float yValue = (Input.GetAxis("Vertical")) * moveSpeed  * Time.deltaTime;
        
        PlayerAnim.SetBool("IsMoving", xValue != 0 || yValue != 0);

        if (xValue > 0)
        {
            sprite.flipX = true;
        }
        else if (xValue < 0)
        {
            sprite.flipX = false;
        }

        transform.Translate(xValue, yValue , 0, Space.World);
    }

}

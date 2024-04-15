using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private Animator PlayerAnim;

    [SerializeField] private SpriteRenderer sprite;

    public AudioClip walkClip;
    AudioSource playerSource;

    bool isMoving;

    private void Awake() {
        playerSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        MovePlayerLogic(moveSpeed);
    }

    private void MovePlayerLogic(float moveSpeed)
    {
        float xValue = (Input.GetAxis("Horizontal")) * moveSpeed  * Time.deltaTime;
        float yValue = (Input.GetAxis("Vertical")) * moveSpeed  * Time.deltaTime;
        
        isMoving = xValue != 0 || yValue != 0;
        PlayerAnim.SetBool("IsMoving", isMoving);

        if (xValue > 0)
        {
            sprite.flipX = true;
        }
        else if (xValue < 0)
        {
            sprite.flipX = false;
        }

        transform.Translate(xValue, yValue , 0, Space.World);

        if(isMoving && !playerSource.isPlaying)
        {
            playerSource.clip = walkClip;
            playerSource.pitch = Random.Range(1, 1.5f);
            playerSource.Play();
        }
        else if (!isMoving && playerSource.clip == walkClip)
        {
            playerSource.Stop();
        }
    }

}

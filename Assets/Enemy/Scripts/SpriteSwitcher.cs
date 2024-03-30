using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    
    private Vector2 lastpos;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = transform.position;
        lastpos = new Vector2(pos.x, pos.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currPos = transform.position;
        if (lastpos.x > currPos.x)
        {
            sprite.flipX = false;
        }
        else if (lastpos.x < currPos.x)
        {
            sprite.flipX = true;
        }

        lastpos = currPos;
    }
}

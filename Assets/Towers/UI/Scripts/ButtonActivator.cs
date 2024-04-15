using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{

    [SerializeField] private Button[] interactables;
    private float timer = 0f;

    // Start is called before the first frame update

    private void EnableAll()
    {
        foreach (Button click in interactables)
        {
            click.interactable = true;
        }
    }

    private void OnEnable()
    {
        timer = 0;
    }

    private void OnDisable()
    {
        foreach (Button click in interactables)
        {
            click.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0.3f)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.3f)
        {
            EnableAll();
        }
    }
}

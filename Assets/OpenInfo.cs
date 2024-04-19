using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenInfo : MonoBehaviour
{

    [SerializeField] private GameObject[] collection;

    [SerializeField] private GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(int number)
    {
        panel.SetActive(true);
        for (int i = 0; i < 8; i++)
        {
            if (i != number)
            {
                collection[i].SetActive(false);
            }
        }
    }
}

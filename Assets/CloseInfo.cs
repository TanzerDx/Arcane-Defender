using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInfo : MonoBehaviour
{
    
    [SerializeField] private GameObject[] collection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        gameObject.SetActive(false);
        foreach (GameObject go in collection)
        {
            go.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int wave = 1;

    private Queue<GameObject[]> waves = new Queue<GameObject[]>();
    [SerializeField] private Button StartWave;

    [SerializeField] private GameObject[] wave1;
    [SerializeField] private GameObject[] wave2;
    [SerializeField] private GameObject[] wave3;
    [SerializeField] private GameObject[] wave4;
    [SerializeField] private GameObject[] wave5;

    [SerializeField] private ObjectPool reference;
    

    // Start is called before the first frame update
    void Start()
    {
        waves.Enqueue(wave1);
        waves.Enqueue(wave2);
        waves.Enqueue(wave3);
        waves.Enqueue(wave4);
        waves.Enqueue(wave5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //This is where we will create the waves in the inspector.
    //This script will manage the UI stuff

    public void LaunchWave()
    {
        Debug.Log("It's clicked");
        StartWave.interactable = false;
        GameObject[] currentWave = waves.Dequeue();
        reference.LaunchNewWave(currentWave);
    }
}

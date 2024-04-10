using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int wave = 0;

    private Queue<GameObject[]> waves = new Queue<GameObject[]>();
    [SerializeField] private Button StartWave;
    [SerializeField] private Text ButtonText;

    [SerializeField] private GameObject[] wave1;
    [SerializeField] private GameObject[] wave2;
    [SerializeField] private GameObject[] wave3;
    [SerializeField] private GameObject[] wave4;
    [SerializeField] private GameObject[] wave5;

    [SerializeField] private ObjectPool reference;

    private float timerBeforNextWave = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        reference.OnWaveSpawned += SetTimer;
        
        waves.Enqueue(wave1);
        waves.Enqueue(wave2);
        waves.Enqueue(wave3);
        waves.Enqueue(wave4);
        waves.Enqueue(wave5);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerBeforNextWave - Time.deltaTime <= 0 && timerBeforNextWave > 0)
        {
            timerBeforNextWave = 0;
            LaunchWave();
        }
        else if(timerBeforNextWave > 0)
        {
            timerBeforNextWave -= Time.deltaTime;
            ButtonText.text = "Time remaining : " + (int)timerBeforNextWave;
        }
    }
    
    //This is where we will create the waves in the inspector.
    //This script will manage the UI stuff

    public void LaunchWave()
    {
        if (waves.Count > 0)
        {
            wave += 1;
            // Debug.Log("It's clicked");
            StartWave.interactable = false;
            GameObject[] currentWave = waves.Dequeue();
            ButtonText.text = "Wave " + wave;
            reference.LaunchNewWave(currentWave);
        }
        else
        {
            StartWave.interactable = false;
            ButtonText.text = "No waves left!";
        }
        
        timerBeforNextWave = 0;
    }

    private void SetTimer(object sender, ObjectPool.TimerEventArgs time)
    {
        timerBeforNextWave = time.timer;
    }
}

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
    [SerializeField] private GameObject[] wave6;
    [SerializeField] private GameObject[] wave7;
    [SerializeField] private GameObject[] wave8;
    [SerializeField] private GameObject[] wave9;
    [SerializeField] private GameObject[] wave10;
    [SerializeField] private GameObject[] wave11;
    [SerializeField] private GameObject[] wave12;
    [SerializeField] private GameObject[] wave13;
    [SerializeField] private GameObject[] wave14;
    [SerializeField] private GameObject[] wave15;
    [SerializeField] private GameObject[] wave16;
    [SerializeField] private GameObject[] wave17;
    [SerializeField] private GameObject[] wave18;
    [SerializeField] private GameObject[] wave19;
    [SerializeField] private GameObject[] wave20;

    [SerializeField] private ObjectPool reference;

    [SerializeField] private int crystalBonus = 1;
    [SerializeField] private int resourceBonus = 2;

    private float timerBeforNextWave = 0;

    private Bank bank;
    

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        
        reference.OnWaveSpawned += SetTimer;
        
        waves.Enqueue(wave1);
        waves.Enqueue(wave2);
        waves.Enqueue(wave3);
        waves.Enqueue(wave4);
        waves.Enqueue(wave5);
        waves.Enqueue(wave6);
        waves.Enqueue(wave7);
        waves.Enqueue(wave8);
        waves.Enqueue(wave9);
        waves.Enqueue(wave10);
        waves.Enqueue(wave11);
        waves.Enqueue(wave12);
        waves.Enqueue(wave13);
        waves.Enqueue(wave14);
        waves.Enqueue(wave15);
        waves.Enqueue(wave16);
        waves.Enqueue(wave17);
        waves.Enqueue(wave18);
        waves.Enqueue(wave19);
        waves.Enqueue(wave20);
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
            bank.Deposit(crystalBonus*(int)timerBeforNextWave, resourceBonus*(int)timerBeforNextWave);
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

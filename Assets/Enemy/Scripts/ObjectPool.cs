using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    public event EventHandler<TimerEventArgs> OnWaveSpawned;

    public class TimerEventArgs : EventArgs
    {
        public int timer;
    }

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnTimer = 3f;
    [SerializeField] int poolSize = 1;
    
    [SerializeField] private Button StartWave;
    [SerializeField] private Text ButtonText;


    //private int enemyOnScreen = 0;

    //private bool isWaveGoing = false;

    private GameObject[] currentWave;

    private GameObject[] pool;

    void Awake()
    {
        currentWave = new GameObject[] { };
        pool = new GameObject[] { };
        
    }

    /*private void Update()
    {
        if (isWaveGoing && enemyOnScreen == 0)
        {
            //Debug.Log("Wave done!");
            //EndOfWave();
            
        }
    }*/

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(currentWave[i], transform);
            pool[i].SetActive(false);
            //pool[i].GetComponent<Enemy>().OnEnemyKilled += RemainingEnemies;
        }
        
        //Debug.Log("Populated !");
    }

    
    
    //Create a method that allows us to start an new wave
    //Make it public so I can call it from somewhere else
    //Also link the button to a script so we can deactivate it

    public void LaunchNewWave(GameObject[] wave)
    {
        //Failsafe in case some enemies are not properly deleted (which should not happen)
        //May cause some issues if we try to delete something already deleted
        
        // foreach (GameObject enemy in pool)
        // {
        //     Destroy(enemy);
        // }

        //isWaveGoing = true;
        poolSize = wave.Length;
        //enemyOnScreen = wave.Length;
        currentWave = wave;
        //Debug.Log("Pool size = " + poolSize);
        PopulatePool();
        StartCoroutine(InstantiateEnemies());
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (pool[i] && pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator InstantiateEnemies()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool[i].SetActive(true);
            yield return new WaitForSeconds(spawnTimer);
        }

        if (OnWaveSpawned != null)
        {
            OnWaveSpawned(this, new TimerEventArgs { timer = poolSize*4 });
        }

        StartWave.interactable = true;
        //Debug.Log("Hey!");
    }

    /*private void EndOfWave()
    {
        isWaveGoing = false;
        StartWave.interactable = true;

        foreach (GameObject enemy in pool)
        {
            // Apparently, destroying an object doesn't force me to unsubscribe before
            //enemy.GetComponent<Enemy>().OnEnemyKilled -= RemainingEnemies;
            Destroy(enemy);
        }

        ButtonText.text = "Next wave";
    }*/

    /*private void RemainingEnemies(object sender, EventArgs e)
    {
        enemyOnScreen -= 1;
        Debug.Log("Enemies remaining = " + enemyOnScreen);
    }*/
}

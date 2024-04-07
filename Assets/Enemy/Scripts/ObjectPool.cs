using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnTimer = 3f;
    [SerializeField] int poolSize = 1;
    [SerializeField] private Button StartWave;

    private GameObject[] currentWave;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
        currentWave = new GameObject[] { };
    }

    void Start()
    {
        StartCoroutine(InstantiateEnemies());
    }

    void PopulatePool()
    {

        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    //Create a method that allows us to start an new wave
    //Make it public so I can link it to a button
    //Also link the button to a script so we can deactivate it

    public void LaunchNewWave(GameObject[] wave)
    {
        poolSize = wave.Length;
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator InstantiateEnemies()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }

        StartWave.interactable = true;
    }

}

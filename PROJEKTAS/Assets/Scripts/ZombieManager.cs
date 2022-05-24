using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    [SerializeField]
    int maxEnemyCount = 10;

    public GameObject[] spawners;
    public GameObject zombie;
    public int enemyCount;
    public int xPos;
    public int zPos;

    // Start is called before the first frame update
    void Awake()
    {
        spawners = new GameObject[maxEnemyCount];
        for(int i = 0; i < maxEnemyCount; i++)
        {
            spawners[i] = this.transform.GetChild(i).gameObject;
        }
    }

    public void SpawnEnemy()
    {
        int spawnPointID = Random.Range(0, spawners.Length);
        Vector3 spawnPos = new Vector3(spawners[spawnPointID].transform.position.x, 5f, spawners[spawnPointID].transform.position.z);
        Instantiate(zombie, spawnPos, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            SpawnEnemy();
        }
        /*
        while (enemyCount < maxEnemyCount)
        {
            xPos = Random.Range(550, 653);
            zPos = Random.Range(910, 1000);
            Instantiate(zombie, new Vector3(xPos, 145.5f, zPos), Quaternion.identity);
            enemyCount += 1;
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField]
    int spawnerCount = 10;
    public int maxEnemyCount = 50;
    public float lastSpawnTime1 = 0;
    public float lastSpawnTimeAll = 0;
    [SerializeField]
    float cooldown1 = 5;
    [SerializeField]
    float cooldownAll = 10;

    public GameObject[] spawners;
    public GameObject zombie;
    public int enemyCount;
    public int xPos;
    public int zPos;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 1;
        
        spawners = new GameObject[spawnerCount];
        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = this.transform.GetChild(i).gameObject;
        }
        SpawnEnemies();
        //coroutine = SpawnEnemies();
        //StartCoroutine(coroutine);

        //InvokeRepeating("SpawnEnemies", 0f, 60f);
    }

    private void SpawnEnemies()
    {
        if (enemyCount < maxEnemyCount && Time.time - lastSpawnTimeAll >= cooldownAll)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                Vector3 spawnPos = new Vector3(spawners[i].transform.position.x, 5f, spawners[i].transform.position.z);
                spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos);
                Instantiate(zombie, spawnPos, Quaternion.identity);

            }
            enemyCount += spawners.Length;
            lastSpawnTimeAll = Time.time;

        }
    }
    public void SpawnEnemy()
    {
        if (enemyCount < maxEnemyCount && Time.time - lastSpawnTime1 >= cooldown1)
        {
            int spawnPointID = Random.Range(0, spawners.Length);
            Vector3 spawnPos = new Vector3(spawners[spawnPointID].transform.position.x, 5f, spawners[spawnPointID].transform.position.z);
            spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos);

            Instantiate(zombie, spawnPos, Quaternion.identity);
            enemyCount++;
            lastSpawnTime1 = Time.time;
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
        SpawnEnemies();
        if (Input.GetKeyDown(KeyCode.T))
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

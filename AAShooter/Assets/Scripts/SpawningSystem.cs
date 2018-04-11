using UnityEngine;
using System.Collections;

public class SpawningSystem : MonoBehaviour
{
    public GameObject[] enemySpawners;    
    private GameObject enemySpawnPoint;
    public GameObject enemy;
    private int enemySpawnChoice;
    private float enemyTimer = 0.0f;
    private bool canSpawnEnemy = true;

    public GameObject[] planeSpawners;
    private GameObject planeSpawnPoint;
    public GameObject plane;
    private int planeSpawnChoice;
    private float planeTimer = 0.0f;

    protected GameObject[] bloodSplats;

    // Update is called once per frame
    void Update ()
    {
	    if(canSpawnEnemy)
        {
            enemyTimer += Time.deltaTime;

            if (enemyTimer >= 2)
            {
                canSpawnEnemy = false;
                enemyTimer = 0.0f;
                ChoosingEnemyPoint();
            }            
        }
        
        planeTimer += Time.deltaTime;

        if(planeTimer >= 6)
        {
            planeTimer = 0.0f;
            ChoosingPlaneSpawn();
        }
	}

    void ChoosingEnemyPoint()
    {        
        enemySpawnChoice = Random.Range(0, enemySpawners.Length);
        enemySpawnPoint = enemySpawners[enemySpawnChoice];
        
        if (enemySpawnPoint.GetComponent<WaypointBehaviour>().isAvailable == false)        
        {
            canSpawnEnemy = true;
        }
        
        else if (enemySpawnPoint.GetComponent<WaypointBehaviour>().isAvailable == true)        
        {        
            enemySpawnPoint.GetComponent<WaypointBehaviour>().isAvailable = false;  
            SpawnEnemy();            
        }        
    }

    void SpawnEnemy()
    {
        GameObject test = Instantiate(enemy, enemySpawnPoint.transform.position, enemySpawnPoint.transform.rotation) as GameObject;
        test.GetComponent<EnemyBehaviour>().setPoints(enemySpawnPoint.transform.GetChild(0), enemySpawnPoint.transform.GetChild(1), enemySpawnPoint.transform);
        test.GetComponent<EnemyBehaviour>().SpawnPoint(enemySpawnChoice);
        
        canSpawnEnemy = true;        
    }

    public void FreeSpawn(int freedPoint)
    {
        GameObject free = enemySpawners[freedPoint];
        free.GetComponent<WaypointBehaviour>().isAvailable = true;
    }

    void ChoosingPlaneSpawn()
    {
        planeSpawnChoice = Random.Range(0, planeSpawners.Length);
        planeSpawnPoint = planeSpawners[planeSpawnChoice];
        GameObject obj = Instantiate(plane, planeSpawnPoint.transform.position, planeSpawnPoint.transform.rotation) as GameObject;
    }
}
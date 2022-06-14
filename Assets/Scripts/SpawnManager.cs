using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    public GameObject enemyPrefab;
    private int nSpawns = 4;
    private Vector3[] coordSpawns;
    public List<TurtleShellController> enemies;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        coordSpawns = new Vector3[nSpawns];

        coordSpawns[0] = new Vector3(55.37f, 2.67f, 41.82f);
        coordSpawns[1] = new Vector3(20.30f, 1.49f, 50.57f);
        coordSpawns[2] = new Vector3(14.49f, 1.56f, 29.74f);
        coordSpawns[3] = new Vector3(43.03f, 2.81f, 23.38f);
    }

    // Update is called once per frame
    public void StartSpawn()
    {
        InvokeRepeating("SpawnEnemy", 0f, 45f);
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < nSpawns; i++)
        {
            Instantiate(enemyPrefab, coordSpawns[i], Quaternion.identity);
        }
    }
}

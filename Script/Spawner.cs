using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.gameManagerInstance.gameTime / 10f); //<why? Mathf.FloorToInt 

        if (timer > (level == 0 ? 0.5f : 0.2f))
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.gameManagerInstance.pool.Get(level);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;//Range(1)<GetComponentsInChildren
    }
}

public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [Tooltip("Time in seconds between block creation.")] [SerializeField] float spawnInterval = 3f;
    [Tooltip("Drag prefab here.")][SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject spawnLocation;
    int playerLives;

    // Start is called before the first frame update
    void Start()
    {
        playerLives = GetComponent<Player>().playerLives;
        StartCoroutine("SpawnBlocks", 3);
    }

    IEnumerator SpawnBlocks()
    {
        if(playerLives > 0)
        {
            Instantiate(blockPrefab, spawnLocation.transform);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerLives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        throw new NotImplementedException();
    }
}

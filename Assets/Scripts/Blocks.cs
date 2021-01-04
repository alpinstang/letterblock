using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [Tooltip("Time in seconds between block creation.")] [SerializeField] float spawnInterval = 1f;
    [SerializeField] float blockSpeed = 10f;
    [Tooltip("Drag prefab here.")][SerializeField] GameObject blockPrefab;
    [Tooltip("Drag player here.")] [SerializeField] GameObject player;
    [SerializeField] GameObject spawnLocation;
    int playerLives;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            playerLives = player.GetComponent<Player>().playerLives;
        } catch {
            Debug.LogWarning("Player lives not set from player script!");
            playerLives = 3;
        }
        InvokeRepeating("CreateBlock", spawnInterval, spawnInterval + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLives <= 0)
        {
            EndGame();
        }
    }

    private void CreateBlock()
    {
        print("creating block...");
        Instantiate(blockPrefab, transform);
        var rb = blockPrefab.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(rb.velocity.x, blockSpeed, rb.velocity.z);
    }

    private void EndGame()
    {
        // end game
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] internal int playerLives = 3;
    [Tooltip("Horizontal Speed (m/s)")][SerializeField] float xSpeed = 5f;
    [Tooltip("How far the player can go at/off the edge of the screen.")] [SerializeField] float xRange = 5f;
    [Tooltip("Drag UI Text object here.")][SerializeField] Text scoreText;
    [Tooltip("Drag UI Text object here.")] [SerializeField] Text livesText;
    [SerializeField] int score = 0;
    [Tooltip("How many points for a Match 3.")][SerializeField] int matchPointValue = 5;
    [SerializeField] protected List<GameObject> blocks;
    int position = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        MonitorControls();
        UpdateUI();
    }

    private void MonitorControls()
    {
        float xThrow = Input.GetAxis("Horizontal");
        print("xThrow = " + xThrow);
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float xPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        print("xPos = " + xPos);
        transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString();
        livesText.text = playerLives.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bomb")
        {
            playerLives--;
        } else
        {
            ProcessBlocks(collision);
        }

    }

    private void ProcessBlocks(Collision collision)
    {
        // connect block to paddle so it follows movement
        var joint = gameObject.AddComponent<HingeJoint>();
        joint.connectedBody = collision.rigidbody;

        // Keep track of blocks in stack
        blocks.Add(collision.gameObject);
        print("added: " + collision.gameObject.tag + " @ " + position);
        if (blocks.Count >= 3)
        {
            CheckForPattern();
        }
    }

    private void CheckForPattern()
    {
        int index = blocks.Count;
        print(index + " items in list.");
        GameObject thisBlock = blocks[index];
        GameObject prevBlock = blocks[index - 1];
        if(prevBlock.CompareTag(thisBlock.tag))
        {
            print("Same Color!");
            GameObject thirdBlock = blocks[index - 2];
            if(thirdBlock.CompareTag(prevBlock.tag))
            {
                print("Match 3!");
                RemoveMatch(index);
                AddScore(matchPointValue);
            }
        }
    }

    private void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    private void RemoveMatch(int index)
    {
        blocks.RemoveRange(index, 2);
        Destroy(blocks[index]);
        Destroy(blocks[index - 1]);
        Destroy(blocks[index - 2]);
    }
}

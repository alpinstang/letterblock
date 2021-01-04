using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] internal int playerLives = 3;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] Text scoreText;
    [SerializeField] int score = 0;
    [SerializeField] int matchValue = 5;
    [SerializeField] List<GameObject> blocks;
    int position = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Left"))
        {
            print("left");
        }
        else if(Input.GetButtonDown("Right"))
        {
            print("right");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Keep track of blocks in stack
        blocks.Add(collision.gameObject);
        print("added: " + collision.gameObject.tag + " @ " + position);
        CheckPattern();
    }

    private void CheckPattern()
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
                AddScore(matchValue);
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

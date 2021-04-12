using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    private int health;
    private int armor;
    private int score;
    public Text healthText;
    public Text armorText;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        armor = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + health;
        armorText.text = "" + armor;
        scoreText.text = "" + score;
    }

    public void AddToScore(int cost) 
    {
        score += cost;
    }
    public int GetScore()
    {
        return score;
    }
}

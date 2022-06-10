using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int health = 100;
    private int cherryCount = 20;
    private int enemyCount;

    private int maxHealth = 100;
    private int maxCherryCount = 20;
    private int maxEnemyCount;

    public Text scoreText;
    public Text healthText;
    public Text cherryCountText;
    public Text enemyCountText;


    GameObject cherryman;
    private void Start()
    {
        scoreText.text = score.ToString();
        healthText.text = health.ToString();    
        cherryCountText.text = cherryCount.ToString();
        cherryman = GameObject.FindGameObjectWithTag("Player");
        enemyCount = GameObject.Find("SpawnManager").GetComponent<ObjectPool>().number;
        maxEnemyCount = enemyCount;
        enemyCountText.text = enemyCount.ToString();
        Debug.Log(cherryman.tag);
    }
    

    public void ScoreUpdate(int scoreValue) // This Method is for Score Updatation
    {

        score = score + scoreValue;
        scoreText.text = score.ToString();
        int enemyDie = 1;
        enemyCount = Mathf.Clamp(enemyCount - enemyDie, 0, maxEnemyCount);
        enemyCountText.text = enemyCount.ToString();
        if(GameObject.Find("SpawnManager").GetComponent<ObjectPool>().number * 10 == score)
        {
            SceneManager.LoadScene(4);
        }
    }

    public void healthUpdate(int healthValue) //This method is for health Updatation
    {
        health = Mathf.Clamp(health + healthValue, 0, maxHealth);
        healthText.text = health.ToString();
        if (health <= 0) // If Cherryman health is 0 in that time Game over scene is activated
        {
            Debug.Log("Now My health is Zero");
            cherryman.GetComponent<AnimationScript>().Die();
            SceneManager.LoadScene(3);
        }
    }

    public void CherryUpdate(int cherryValue) //This method is for collecting cherry and Throwing cherry Updatation 
    {
        cherryCount = Mathf.Clamp(cherryCount + cherryValue, 0, maxCherryCount);
        cherryCountText.text = cherryCount.ToString();
    }
}

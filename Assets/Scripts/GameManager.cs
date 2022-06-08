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

    private int maxHealth = 100;
    private int maxCherryCount = 20;

    public Text scoreText;
    public Text healthText;
    public Text cherryCountText;


    GameObject cherryman;
    private void Awake()
    {
        scoreText.text = score.ToString();
        healthText.text = health.ToString();    
        cherryCountText.text = cherryCount.ToString();
        cherryman = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(cherryman.tag);
    }

    public void ScoreUpdate(int scoreValue)
    {

        score = score + scoreValue;
        scoreText.text = score.ToString();
        if(GameObject.Find("SpawnManager").GetComponent<ObjectPool>().number * 10 == score)
        {
            SceneManager.LoadScene(4);
        }
    }

    public void healthUpdate(int healthValue)
    {
        health = Mathf.Clamp(health + healthValue, 0, maxHealth);
        healthText.text = health.ToString();
        if (health <= 0)
        {
            Debug.Log("Now My health is Zero");
            cherryman.GetComponent<AnimationScript>().Die();
            SceneManager.LoadScene(3);
        }
    }

    public void CherryUpdate(int cherryValue)
    {
        cherryCount = Mathf.Clamp(cherryCount + cherryValue, 0, maxCherryCount);
        cherryCountText.text = cherryCount.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int health = 100;
    private int cherryCount = 20;

    public void ScoreUpdate(int scoreValue)
    {
        score = score + scoreValue;
    }

    public void healthUpdate(int healthValue)
    {
        health = health + healthValue;
    }

    public void CherryUpdate(int cherryValue)
    {
        cherryCount = cherryCount + cherryValue;
    }
}

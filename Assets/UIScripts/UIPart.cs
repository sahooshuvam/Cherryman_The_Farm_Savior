using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject instructionPanel1;
    public GameObject instructionPanel2;
    public GameObject instructionPanel3;
    public GameObject aboutTheGame;
    public GameObject menuPanel;
    public GameObject settingPanel;
    public GameObject loadImage;
    //public Button start;
    void Start()
    {
      
    }

    public void Instruction1()
    {
        instructionPanel1.SetActive(true);
        instructionPanel2.SetActive(false);
    }
    public void Instruction2()
    {
        instructionPanel1.SetActive(false);
        instructionPanel2.SetActive(true);
        instructionPanel3.SetActive(false);
    }
    public void Instruction3()
    {
        instructionPanel2.SetActive(false);
        instructionPanel3.SetActive(true);
    }
   public void TapToPlay()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    public void Load()
    {
        loadImage.SetActive(true);
        Destroy(loadImage, 6f);
        SceneManager.LoadScene(1);
    }
    public void AboutTheGame()
    {
        aboutTheGame.SetActive(true);
    }
    public void BackToMenu()
    {
        aboutTheGame.SetActive(false);
        settingPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void Settings()
    {
        settingPanel.SetActive(true);
    }
    void Update()
    {
        
    }
}

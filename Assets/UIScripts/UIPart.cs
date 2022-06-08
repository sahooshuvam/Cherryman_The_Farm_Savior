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
    public GameObject loadImage;
    public GameObject quitGame;
    //public Button start;
    void Start()
    {
      // start.onClick.AddListener(Instruction1);
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
    public void QuitPanel()
    {
        quitGame.SetActive(true);
    }
    void Update()
    {
        
    }
}

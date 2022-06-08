using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    /* public GameObject loadingScreen;
     public Slider slider;
     public Text percentage;
     public void Loadlevel1(int sceneIndex)
     {
         // AsyncOperation operation=SceneManager.LoadSceneAsync(sceneIndex);
         StartCoroutine(Asychronosuly(sceneIndex));
     }
     IEnumerator Asychronosuly(int sceneIndex)
     {
         AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
         loadingScreen.SetActive(true);
         while(!operation.isDone)
         {
             float progress = Mathf.Clamp01(operation.progress / 0.9f);
             //Debug.Log(operation.progress);
             slider.value = progress;
             percentage.text = progress * 100f + "%";
            yield return null;
         }
     }*/
    public GameObject loadImage;
    public void Loading()
    {
        Instantiate(loadImage);
        loadImage.SetActive(false);
        SceneManager.LoadScene(1);
    }
    
}

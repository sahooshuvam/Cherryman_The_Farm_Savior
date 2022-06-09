using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
    bool muted = false;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1f); // Setting default volume to high
            Load();
        }
        else
            Save();
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;    // Assigning slider value to volume

        Save();
    }
    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");   //Setting musicvolume value to slider
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);  // Setting the slider value to musicvolume
    }
    public void Mute()
    {
       
            if (muted == false)
            {
                muted = true;
                AudioListener.pause = true;
            }  
    }
    public void UnMute()
    {
        if (muted == true)
        {
            muted = false;
            AudioListener.pause = false;
        }
    }

    
}

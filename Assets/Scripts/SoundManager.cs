using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public List<AudioClip> audioClips;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayerWalkOne()
    {
        audioSource.PlayOneShot(audioClips[0]); //cherryman walk one sound is player
    }
    public void PlayerWalkTwo()
    {
        audioSource.PlayOneShot(audioClips[1]);//cherryman walk two sound is player
    }
    public void Jump()
    {
        audioSource.PlayOneShot(audioClips[2]);//cherryman jump sound is player
    }
    public void InsectEat()
    {
        audioSource.PlayOneShot(audioClips[3]); //Spider Eating Sound is playing
    }
    public void InsectDie()
    {
        audioSource.PlayOneShot(audioClips[4]); //Spider Die sound is playing
    }
  
    public void ThrowCherry()
    {
        audioSource.PlayOneShot(audioClips[5]); //cherryman throw sound is player
    }
    public void InsectAttack()
    {
        audioSource.PlayOneShot(audioClips[6]); //Spider Attack sound is playing
    }
    public void InsectWalk()
    {
        audioSource.PlayOneShot(audioClips[7]); //Spider Walk sound is playing
    }
}

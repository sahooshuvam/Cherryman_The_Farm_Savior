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
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void PlayerWalkTwo()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }
    public void Jump()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    public void InsectEat()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }
    public void InsectDie()
    {
        audioSource.PlayOneShot(audioClips[4]);
    }
  
    public void ThrowCherry()
    {
        audioSource.PlayOneShot(audioClips[5]);
    }
    public void InsectAttack()
    {
        audioSource.PlayOneShot(audioClips[6]);
    }
    public void InsectWalk()
    {
        audioSource.PlayOneShot(audioClips[7]);
    }
}
